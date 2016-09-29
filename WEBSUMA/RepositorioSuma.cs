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
            using (Entities db = new Entities())
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
            using (Entities db = new Entities())
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

        public List<RegistroTipoDeAfiliacion> ConsultarTipoAfiliacion()
        {
            using (Entities db = new Entities())
            {
                List<RegistroTipoDeAfiliacion> tiposdeafiliacion = new List<RegistroTipoDeAfiliacion>();
                try
                {
                    var query = (from i in db.Types
                                 select new RegistroTipoDeAfiliacion()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        tiposdeafiliacion = query;
                    }
                    else
                    {
                        RegistroTipoDeAfiliacion fila = new RegistroTipoDeAfiliacion()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        tiposdeafiliacion.Add(fila);
                    }
                    return tiposdeafiliacion;
                }
                catch (Exception ex)
                {
                    tiposdeafiliacion = new List<RegistroTipoDeAfiliacion>();
                    RegistroTipoDeAfiliacion fila = new RegistroTipoDeAfiliacion()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    tiposdeafiliacion.Add(fila);
                    return tiposdeafiliacion;
                }
            }
        }

        public List<RegistroEstadoCivil> ConsultarEstadoCivil()
        {
            using (Entities db = new Entities())
            {
                List<RegistroEstadoCivil> estadosciviles = new List<RegistroEstadoCivil>();
                try
                {
                    var query = (from i in db.Civil_Statuses
                                 select new RegistroEstadoCivil()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,

                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        estadosciviles = query;
                    }
                    else
                    {
                        RegistroEstadoCivil fila = new RegistroEstadoCivil()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        estadosciviles.Add(fila);
                    }
                    return estadosciviles;
                }
                catch (Exception ex)
                {
                    estadosciviles = new List<RegistroEstadoCivil>();
                    RegistroEstadoCivil fila = new RegistroEstadoCivil()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    estadosciviles.Add(fila);
                    return estadosciviles;
                }
            }
        }

        public List<RegistroSexo> ConsultarSexo()
        {
            using (Entities db = new Entities())
            {
                List<RegistroSexo> sexos = new List<RegistroSexo>();
                try
                {
                    var query = (from i in db.Sexes
                                 select new RegistroSexo()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        sexos = query;
                    }
                    else
                    {
                        RegistroSexo fila = new RegistroSexo()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        sexos.Add(fila);
                    }
                    return sexos;
                }
                catch (Exception ex)
                {
                    sexos = new List<RegistroSexo>();
                    RegistroSexo fila = new RegistroSexo()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    sexos.Add(fila);
                    return sexos;
                }
            }
        }

        public List<RegistroNacionalidad> ConsultarNacionalidad()
        {
            using (Entities db = new Entities())
            {
                List<RegistroNacionalidad> nacionalidades = new List<RegistroNacionalidad>();
                try
                {
                    var query = (from i in db.Nacionalities
                                 select new RegistroNacionalidad()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        nacionalidades = query;
                    }
                    else
                    {
                        RegistroNacionalidad fila = new RegistroNacionalidad()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        nacionalidades.Add(fila);
                    }
                    return nacionalidades;
                }
                catch (Exception ex)
                {
                    nacionalidades = new List<RegistroNacionalidad>();
                    RegistroNacionalidad fila = new RegistroNacionalidad()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    nacionalidades.Add(fila);
                    return nacionalidades;
                }
            }
        }

        public List<RegistroCanal> ConsultarCanal()
        {
            using (Entities db = new Entities())
            {
                List<RegistroCanal> canales = new List<RegistroCanal>();
                try
                {
                    var query = (from i in db.Channels
                                 select new RegistroCanal()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        canales = query;
                    }
                    else
                    {
                        RegistroCanal fila = new RegistroCanal()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        canales.Add(fila);
                    }
                    return canales;
                }
                catch (Exception ex)
                {
                    canales = new List<RegistroCanal>();
                    RegistroCanal fila = new RegistroCanal()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    canales.Add(fila);
                    return canales;
                }
            }
        }

        public List<RegistroSucursal> ConsultarSucursal()
        {
            using (Entities db = new Entities())
            {
                List<RegistroSucursal> sucursales = new List<RegistroSucursal>();
                try
                {
                    var query = (from i in db.Stores
                                 select new RegistroSucursal()
                                 {
                                     excode = 0,
                                     exdetail = "",
                                     id = i.id,
                                     descripcion = i.name,
                                 }).OrderBy(x => x.descripcion).ToList();
                    if (query != null)
                    {
                        sucursales = query;
                    }
                    else
                    {
                        RegistroSucursal fila = new RegistroSucursal()
                        {
                            excode = 0,
                            exdetail = ""
                        };
                        sucursales.Add(fila);
                    }
                    return sucursales;
                }
                catch (Exception ex)
                {
                    sucursales = new List<RegistroSucursal>();
                    RegistroSucursal fila = new RegistroSucursal()
                    {
                        excode = ex.HResult,
                        exdetail = ex.Message
                    };
                    sucursales.Add(fila);
                    return sucursales;
                }
            }
        }
        
    }
}