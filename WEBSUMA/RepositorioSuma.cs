using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBSUMA
{
    public class RepositorioSuma
    {
        public List<RegistroInteres> ConsultarInteres()
        {
            using (SumaLealtadEntities db = new SumaLealtadEntities())
            {
                List<RegistroInteres> intereses = new List<RegistroInteres>();                
                try
                {
                    var query = (from i in db.Interests
                                 select new RegistroInteres()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                     estatus = i.active == true ? "Activo":"Inactivo"
                                 }).OrderBy(x=>x.descripcion).ToList();
                    if (query != null)
                    {
                        intereses = query;
                    }
                    else
                    {
                        RegistroInteres fila = new RegistroInteres()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        intereses.Add(fila);
                    }
                    return intereses;
                }
                catch (Exception ex)
                {
                    intereses = new List<RegistroInteres>();        
                    RegistroInteres fila = new RegistroInteres()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    intereses.Add(fila);
                    return intereses;
                }
            }
        }

        public List<RegistroDireccion> ConsultarDireccion()
        {
            using (SumaLealtadEntities db = new SumaLealtadEntities())
            {
                List<RegistroDireccion> direcciones = new List<RegistroDireccion>();
                try
                {                    
                    var query = db.ESTADOS.OrderBy(x=>x.DESCRIPC_ESTADO);
                    foreach (var e in query)
                    {
                        RegistroDireccion direccion = new RegistroDireccion();
                        RegistroDireccion.Estado estado = new RegistroDireccion.Estado()
                        {
                            id = e.COD_ESTADO,
                            descripcion = e.DESCRIPC_ESTADO,
                        };
                        estado.ciudades = new List<RegistroDireccion.Estado.Ciudad>();
                        var query2 = (from es in db.ESTADOS
                                      where es.COD_ESTADO == e.COD_ESTADO
                                      from cs in es.CIUDADs
                                      select cs);
                        foreach (var c in query2)
                        {
                            RegistroDireccion.Estado.Ciudad ciudad = new RegistroDireccion.Estado.Ciudad()
                            {
                                id = c.COD_CIUDAD,
                                descripcion = c.DESCRIPC_CIUDAD
                            };
                            ciudad.municipios = new List<RegistroDireccion.Estado.Ciudad.Municipio>();
                            var query3 = (from cs in db.CIUDADES
                                          where cs.COD_CIUDAD == c.COD_CIUDAD
                                          from ms in cs.MUNICIPIOs
                                          select ms);
                            foreach (var m in query3)
                            {
                                RegistroDireccion.Estado.Ciudad.Municipio municipio = new RegistroDireccion.Estado.Ciudad.Municipio()
                                {
                                    id = m.COD_MUNICIPIO,
                                    descripcion = m.DESCRIPC_MUNICIPIO
                                };
                                municipio.parroquias = new List<RegistroDireccion.Estado.Ciudad.Municipio.Parroquia>();
                                var query4 = (from ms in db.MUNICIPIOS
                                              where ms.COD_MUNICIPIO == m.COD_MUNICIPIO
                                              from ps in ms.PARROQUIAs
                                              select ps);
                                foreach (var p in query4)
                                {
                                    RegistroDireccion.Estado.Ciudad.Municipio.Parroquia parroquia = new RegistroDireccion.Estado.Ciudad.Municipio.Parroquia()
                                    {
                                        id = p.COD_PARROQUIA,
                                        descripcion = p.DESCRIPC_PARROQUIA
                                    };
                                    parroquia.urbanizaciones = new List<RegistroDireccion.Estado.Ciudad.Municipio.Parroquia.Urbanizacion>();
                                    var query5 = (from ps in db.PARROQUIAS
                                                  where ps.COD_PARROQUIA == p.COD_PARROQUIA
                                                  from us in ps.URBANIZACIONs
                                                  select us);
                                    foreach (var u in query5)
                                    {
                                        RegistroDireccion.Estado.Ciudad.Municipio.Parroquia.Urbanizacion urbanizacion = new RegistroDireccion.Estado.Ciudad.Municipio.Parroquia.Urbanizacion()
                                        {
                                            id = u.COD_URBANIZACION,
                                            descripcion = u.DESCRIPC_URBANIZACION
                                        };
                                        parroquia.urbanizaciones.Add(urbanizacion);
                                        urbanizacion = null;
                                    }
                                    parroquia.urbanizaciones = parroquia.urbanizaciones.OrderBy(x => x.descripcion).ToList();
                                    municipio.parroquias.Add(parroquia);
                                    parroquia = null;
                                }
                                municipio.parroquias = municipio.parroquias.OrderBy(x => x.descripcion).ToList();
                                ciudad.municipios.Add(municipio);
                                municipio = null;
                            }
                            ciudad.municipios = ciudad.municipios.OrderBy(x => x.descripcion).ToList();
                            estado.ciudades.Add(ciudad);
                            ciudad = null;
                        }
                        estado.ciudades = estado.ciudades.OrderBy(x => x.descripcion).ToList();
                        direccion.estado = estado;
                        estado = null;                        
                        direcciones.Add(direccion);
                        direccion = null;
                    }                    
                    if (query != null)
                    {
                        return direcciones;
                    }
                    else
                    {
                        RegistroDireccion fila = new RegistroDireccion()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        direcciones.Add(fila);
                    }
                    return direcciones;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    direcciones = new List<RegistroDireccion>();
                    RegistroDireccion fila = new RegistroDireccion()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    direcciones.Add(fila);
                    return direcciones;
                }
            }
        }

    }
}