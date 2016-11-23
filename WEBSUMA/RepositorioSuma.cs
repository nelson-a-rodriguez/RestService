using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                                     estatus = i.active == true ? "Activo" : "Inactivo"
                                 }).OrderBy(x => x.descripcion).ToList();
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
                    var query = db.ESTADOS.OrderBy(x => x.DESCRIPC_ESTADO);
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

        private string VerificarNumeroDeDocumentoCrear(string numerodedocumento)
        {
            using (Entities db = new Entities())
            {
                var query = (from a in db.Affiliates
                             where a.docnumber.Substring(2) == numerodedocumento
                             select a.docnumber
                             ).ToList();
                if (query.Count > 0)
                {
                    return query.First();
                }
                var query2 = (from c in db.CLIENTES
                              where c.NRO_DOCUMENTO == numerodedocumento
                              select c.TIPO_DOCUMENTO + "-" + c.NRO_DOCUMENTO
                            ).ToList();
                if (query2.Count > 0)
                {
                    return query2.First();
                }
                return null;
            }
        }

        public RespuestaFindAfiliacionSuma FindAfiliacionSuma(string docnumber)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    int idAfiliado = (from a in db.Affiliates
                                      where a.docnumber.Equals(docnumber)
                                      select a.id
                                   ).SingleOrDefault();
                    if (idAfiliado == 0)
                    {
                        string validacion = VerificarNumeroDeDocumentoCrear(docnumber.Substring(2));
                        if (validacion != null)
                        {
                            return new RespuestaFindAfiliacionSuma()
                            {
                                excode = -1,
                                exdetail = "El número de documento indicado (" + docnumber + ") ya está registrado como otro tipo de identificación (" + validacion + "), no se puede afiliar.",
                                idAfiliacion = 0
                            };
                        }
                        else
                        {
                            //NO AFILIADO, DOCUMENTO VERIFICADO
                            return new RespuestaFindAfiliacionSuma()
                            {
                                excode = 0,
                                exdetail = "",
                                idAfiliacion = 0
                            };
                        }
                    }
                    else
                    {
                        return new RespuestaFindAfiliacionSuma()
                        {
                            excode = 0,
                            exdetail = "",
                            idAfiliacion = idAfiliado
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new RespuestaFindAfiliacionSuma()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message,
                    idAfiliacion = 0
                };
            }
        }

        #region Funciones_Valores_Consecutivos
        private int AfilliatesID()
        {
            using (Entities db = new Entities())
            {
                if (db.Affiliates.Count() == 0)
                    return 1;
                return (db.Affiliates.Max(a => a.id) + 1);
            }
        }

        private int AfilliateAudID()
        {
            using (Entities db = new Entities())
            {
                if (db.AffiliateAuds.Count() == 0)
                    return 1;
                return (db.AffiliateAuds.Max(a => a.id) + 1);
            }
        }
        #endregion

        public RespuestaInsAfiliacionSuma InsAfiliacionSuma(RegistroCrearAfiliacionSuma registrocrearafiliacionsuma)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    var Affiliate = new Affiliate()
                    {
                        id = AfilliatesID(),
                        docnumber = registrocrearafiliacionsuma.docnumber,
                        clientid = registrocrearafiliacionsuma.clientid,
                        channelid = registrocrearafiliacionsuma.channelid,
                        typeid = registrocrearafiliacionsuma.typeid,
                        affiliatedate = DateTime.Now,
                        typedelivery = registrocrearafiliacionsuma.typedelivery,
                        //storeiddelivery = afiliado.storeiddelivery,
                        estimateddatedelivery = new DateTime(),
                        creationdate = DateTime.Now,
                        creationuserid = registrocrearafiliacionsuma.usuarioAfiliacion,
                        modifieddate = DateTime.Now,
                        modifieduserid = registrocrearafiliacionsuma.usuarioAfiliacion,
                        sumastatusid = registrocrearafiliacionsuma.sumastatusid,  
                        reasonsid = null,
                        twitter_account = string.IsNullOrEmpty(registrocrearafiliacionsuma.twitter_account) ? null : registrocrearafiliacionsuma.twitter_account,
                        facebook_account = string.IsNullOrEmpty(registrocrearafiliacionsuma.facebook_account) ? null : registrocrearafiliacionsuma.facebook_account,
                        instagram_account = string.IsNullOrEmpty(registrocrearafiliacionsuma.instagram_account) ? null : registrocrearafiliacionsuma.instagram_account,
                    };
                    db.Affiliates.Add(Affiliate);
                    //ENTIDAD CLIENTE
                    CLIENTE cliente = db.CLIENTES.FirstOrDefault(c => c.TIPO_DOCUMENTO + "-" + c.NRO_DOCUMENTO == registrocrearafiliacionsuma.docnumber);
                    if (cliente == null)
                    {
                        var CLIENTE = new CLIENTE()
                        {
                            TIPO_DOCUMENTO = registrocrearafiliacionsuma.docnumber.Substring(0, 1),
                            NRO_DOCUMENTO = registrocrearafiliacionsuma.docnumber.Substring(2),
                            E_MAIL = registrocrearafiliacionsuma.email == null ? "" : registrocrearafiliacionsuma.email,
                            NOMBRE_CLIENTE1 = registrocrearafiliacionsuma.name,
                            NOMBRE_CLIENTE2 = registrocrearafiliacionsuma.name2 == null ? "" : registrocrearafiliacionsuma.name2,
                            APELLIDO_CLIENTE1 = registrocrearafiliacionsuma.lastname1 == null ? "" : registrocrearafiliacionsuma.lastname1,
                            APELLIDO_CLIENTE2 = registrocrearafiliacionsuma.lastname2 == null ? "" : registrocrearafiliacionsuma.lastname2,
                            FECHA_NACIMIENTO = registrocrearafiliacionsuma.birthdate == null ? new DateTime?() : DateTime.ParseExact(registrocrearafiliacionsuma.birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture),

                            //NACIONALIDAD = afiliado.nationality == null ? "" : afiliado.nationality,
                            //SEXO = afiliado.gender == null ? "" : afiliado.gender,
                            //EDO_CIVIL = afiliado.maritalstatus == null ? "" : afiliado.maritalstatus,
                            //COD_SUCURSAL = afiliado.storeid,

                            //nuevos campos con claves a tablas nuevas
                            NACIONALITY_ID = registrocrearafiliacionsuma.nationality == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.nationality),
                            SEX_ID = registrocrearafiliacionsuma.gender == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.gender),
                            CIVIL_STATUS_ID = registrocrearafiliacionsuma.maritalstatus == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.maritalstatus),

                            //OCUPACION = afiliado.occupation == null ? "" : afiliado.occupation.Substring(0, 30),
                            TELEFONO_HAB = registrocrearafiliacionsuma.phone1,
                            TELEFONO_OFIC = registrocrearafiliacionsuma.phone2 == null ? "" : registrocrearafiliacionsuma.phone2,
                            TELEFONO_CEL = registrocrearafiliacionsuma.phone3 == null ? "" : registrocrearafiliacionsuma.phone3,
                            COD_ESTADO = registrocrearafiliacionsuma.cod_estado,
                            COD_CIUDAD = registrocrearafiliacionsuma.cod_ciudad,
                            COD_MUNICIPIO = registrocrearafiliacionsuma.cod_municipio,
                            COD_PARROQUIA = registrocrearafiliacionsuma.cod_parroquia,
                            COD_URBANIZACION = registrocrearafiliacionsuma.cod_urbanizacion,
                            FECHA_CREACION = DateTime.Now
                        };
                        //nuevos campos con claves a tablas nuevas
                        var query = db.Stores.OrderBy(x => x.store_code);
                        CLIENTE.STORE_ID = registrocrearafiliacionsuma.storeid; //(from q in query.AsEnumerable()
                                            //where q.store_code == afiliado.storeid.ToString()
                                            //select q.id).FirstOrDefault();
                        if (registrocrearafiliacionsuma.occupation == null)
                        {
                            CLIENTE.OCUPACION = registrocrearafiliacionsuma.occupation;
                        }
                        else if (registrocrearafiliacionsuma.occupation.Length > 30)
                        {
                            CLIENTE.OCUPACION = registrocrearafiliacionsuma.occupation.Substring(0, 30);
                        }
                        else
                        {
                            CLIENTE.OCUPACION = registrocrearafiliacionsuma.occupation;
                        }
                        db.CLIENTES.Add(CLIENTE);
                    }
                    else
                    {
                        cliente.E_MAIL = registrocrearafiliacionsuma.email == null ? "" : registrocrearafiliacionsuma.email;
                        cliente.NOMBRE_CLIENTE1 = registrocrearafiliacionsuma.name;
                        cliente.NOMBRE_CLIENTE2 = registrocrearafiliacionsuma.name2 == null ? "" : registrocrearafiliacionsuma.name2;
                        cliente.APELLIDO_CLIENTE1 = registrocrearafiliacionsuma.lastname1 == null ? "" : registrocrearafiliacionsuma.lastname1;
                        cliente.APELLIDO_CLIENTE2 = registrocrearafiliacionsuma.lastname2 == null ? "" : registrocrearafiliacionsuma.lastname2;
                        cliente.FECHA_NACIMIENTO = registrocrearafiliacionsuma.birthdate == null ? new DateTime?() : DateTime.ParseExact(registrocrearafiliacionsuma.birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //cliente.NACIONALIDAD = afiliado.nationality == null ? "" : afiliado.nationality;
                        //cliente.SEXO = afiliado.gender == null ? "" : afiliado.gender;
                        //cliente.EDO_CIVIL = afiliado.maritalstatus == null ? "" : afiliado.maritalstatus;
                        //cliente.COD_SUCURSAL = afiliado.storeid;

                        //nuevos campos con claves a tablas nuevas
                        cliente.NACIONALITY_ID = registrocrearafiliacionsuma.nationality == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.nationality);
                        cliente.SEX_ID = registrocrearafiliacionsuma.gender == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.gender);
                        cliente.CIVIL_STATUS_ID = registrocrearafiliacionsuma.maritalstatus == null ? 0 : Convert.ToInt32(registrocrearafiliacionsuma.maritalstatus);

                        var query = db.Stores.OrderBy(x => x.store_code);
                        cliente.STORE_ID = registrocrearafiliacionsuma.storeid; //(from q in query.AsEnumerable()
                                            //where q.store_code == afiliado.storeid.ToString()
                                            //select q.id).FirstOrDefault();

                        //cliente.OCUPACION = afiliado.occupation == null ? "" : afiliado.occupation;
                        cliente.TELEFONO_HAB = registrocrearafiliacionsuma.phone1;
                        cliente.TELEFONO_OFIC = registrocrearafiliacionsuma.phone2 == null ? "" : registrocrearafiliacionsuma.phone2;
                        cliente.TELEFONO_CEL = registrocrearafiliacionsuma.phone3 == null ? "" : registrocrearafiliacionsuma.phone3;
                        cliente.COD_ESTADO = registrocrearafiliacionsuma.cod_estado;
                        cliente.COD_CIUDAD = registrocrearafiliacionsuma.cod_ciudad;
                        cliente.COD_MUNICIPIO = registrocrearafiliacionsuma.cod_municipio;
                        cliente.COD_PARROQUIA = registrocrearafiliacionsuma.cod_parroquia;
                        cliente.COD_URBANIZACION = registrocrearafiliacionsuma.cod_urbanizacion;
                        if (registrocrearafiliacionsuma.occupation == null)
                        {
                            cliente.OCUPACION = registrocrearafiliacionsuma.occupation;
                        }
                        else if (registrocrearafiliacionsuma.occupation.Length > 30)
                        {
                            cliente.OCUPACION = registrocrearafiliacionsuma.occupation.Substring(0, 30);
                        }
                        else
                        {
                            cliente.OCUPACION = registrocrearafiliacionsuma.occupation;
                        }
                    }
                    //ENTIDAD CustomerInterest
                    foreach (var interes in registrocrearafiliacionsuma.Intereses)
                    {
                        CustomerInterest customerInterest = new CustomerInterest()
                        {
                            customerid = Affiliate.id,
                            interestid = interes,
                            comments = ""
                        };
                        db.CustomerInterests.Add(customerInterest);
                    }
                    ////ENTIDAD Photos_Affiliate
                    //if (file != null)
                    //{
                    //    try
                    //    {
                    //        int length = file.ContentLength;
                    //        byte[] buffer = new byte[length];
                    //        file.InputStream.Read(buffer, 0, length);
                    //        var Photos_Affiliate = new Photos_Affiliate()
                    //        {
                    //            photo = buffer,
                    //            photo_type = file.ContentType,
                    //            Affiliate_id = Affiliate.id
                    //        };
                    //        db.Photos_Affiliates.Add(Photos_Affiliate);
                    //    }
                    //    catch
                    //    {
                    //        return false;
                    //    }
                    //}
                    //PARA QUE LA IMAGEN DEL DOCUMENTO SEA OPCIONAL
                    //else
                    //{
                    //    return false;
                    //}
                    //ENTIDAD AffiliateAud
                    var affiliateauditoria = new AffiliateAud()
                    {
                        id = AfilliateAudID(),
                        affiliateid = Affiliate.id,
                        modifieduserid = Affiliate.modifieduserid,
                        modifieddate = System.DateTime.Now,
                        statusid = Affiliate.sumastatusid.Value,
                        reasonsid = 1,
                        comments = null
                    };
                    db.AffiliateAuds.Add(affiliateauditoria);
                    //YA NO SE ENVIARÁ INFORMACIÓN A LA WEB
                    //if (SaveWebPlazas(afiliado))
                    //{
                    db.SaveChanges();
                    return new RespuestaInsAfiliacionSuma()
                    {
                        excode = 0,
                        exdetail = "",
                        idAfiliacion = db.Affiliates.First(x => x.docnumber == Affiliate.docnumber).id
                    };
                }
            }        
            catch (Exception ex)
            {
                return new RespuestaInsAfiliacionSuma()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message,
                    idAfiliacion = 0
                };
            }
        }

        public RespuestaInsImagenAfiliacionSuma InsImagenAfiliacionSuma(int idAfiliacion, byte[] Imagen, string type)
        {
            try
            {
                using (Entities db = new Entities())
                {
                    var query = (from a in db.Affiliates
                                 where a.id == idAfiliacion
                                 select a.id
                                 ).FirstOrDefault();
                    if (query == null)
                    {
                        return new RespuestaInsImagenAfiliacionSuma()
                        {
                            excode = -1,
                            exdetail = "Afiliación no encontrada",
                            idAfiliacion = idAfiliacion
                        };
                    }
                    Photos_Affiliate photos_affiliate = db.Photos_Affiliates.FirstOrDefault(x => x.Affiliate_id == idAfiliacion);
                    if (photos_affiliate == null)
                    {
                        photos_affiliate = new Photos_Affiliate()
                        {
                            photo = Imagen,
                            photo_type = type,
                            Affiliate_id = idAfiliacion
                        };
                        db.Photos_Affiliates.Add(photos_affiliate);
                        db.SaveChanges();
                        return new RespuestaInsImagenAfiliacionSuma()
                        {
                            excode = 0,
                            exdetail = "",
                            idAfiliacion = idAfiliacion
                        };
                    }
                    else
                    {
                        return new RespuestaInsImagenAfiliacionSuma()
                        {
                            excode = -1,
                            exdetail = "Afiliación ya tiene imagen registrada",
                            idAfiliacion = idAfiliacion
                        };
                    }                    
                }
            }
            catch (Exception ex)
            {
                return new RespuestaInsImagenAfiliacionSuma()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message,
                    idAfiliacion = 0
                };
            }
        }

    }
}
