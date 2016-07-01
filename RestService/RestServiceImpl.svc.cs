using RFCWEBSAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Newtonsoft.Json.Linq;


namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestServiceImpl" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestServiceImpl.svc or RestServiceImpl.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RestServiceImpl : IRestServiceImpl
    {
        public List<RegistroInventario> Inventario(string sCentro, string sJerarquiaWeb, string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosConexion datosconexion = serializer.Deserialize<DatosConexion>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datosconexion.Name,
                        User = datosconexion.User,
                        Password = datosconexion.Password,
                        Client = datosconexion.Client,
                        Language = datosconexion.Language,
                        AppServerHost = datosconexion.AppServerHost,
                        SystemNumber = datosconexion.SystemNumber,
                        PoolSize = datosconexion.PoolSize,
                        ConnectionIdleTimeout = datosconexion.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    List<RegistroInventario> resultado = rep.ConsultarInventario(sCentro, sJerarquiaWeb);
                    return resultado;
                }
                else
                {
                    return new List<RegistroInventario>()
                    {
                        new RegistroInventario()
                        {
                            excode = -2,
                            exdetail = "Archivo entrada vacío"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new List<RegistroInventario>()
                {
                    new RegistroInventario()
                    {
                        excode=ex.HResult, 
                        exdetail=ex.Message
                    }
                };
            }
        }


        public List<RegistroJerarquiaWeb> JerarquiaWeb(string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosCliente datoscliente = serializer.Deserialize<DatosCliente>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datoscliente.Name,
                        User = datoscliente.User,
                        Password = datoscliente.Password,
                        Client = datoscliente.Client,
                        Language = datoscliente.Language,
                        AppServerHost = datoscliente.AppServerHost,
                        SystemNumber = datoscliente.SystemNumber,
                        PoolSize = datoscliente.PoolSize,
                        ConnectionIdleTimeout = datoscliente.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    List<RegistroJerarquiaWeb> resultado = rep.ConsultarJerarquiaWeb();
                    return resultado;
                }
                else
                {
                    return new List<RegistroJerarquiaWeb>()
                    {
                        new RegistroJerarquiaWeb()
                        {
                            excode = -2,
                        exdetail = "Archivo entrada vacío"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new List<RegistroJerarquiaWeb>()
                {
                    new RegistroJerarquiaWeb()
                    {
                        excode=ex.HResult, 
                        exdetail=ex.Message
                    }
                };
            }
        }

        public RespuestaInsUpdCliente InsUpdCliente(string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosCliente datoscliente = serializer.Deserialize<DatosCliente>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datoscliente.Name,
                        User = datoscliente.User,
                        Password = datoscliente.Password,
                        Client = datoscliente.Client,
                        Language = datoscliente.Language,
                        AppServerHost = datoscliente.AppServerHost,
                        SystemNumber = datoscliente.SystemNumber,
                        PoolSize = datoscliente.PoolSize,
                        ConnectionIdleTimeout = datoscliente.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    string documentoformateado = FormatearDocumento(datoscliente.Documento);
                    if (documentoformateado == "")
                    {
                        return new RespuestaInsUpdCliente()
                        {
                            excode = -1,
                            exdetail = "Formato incorrecto Documento"
                        };
                    }
                    RegistroCliente cliente = rep.ConsultarClienteSap(documentoformateado);
                    if (cliente.CUSTOMERNO == "")
                    {
                        //crear
                        RegistroCrearCliente registrocrearcliente = new RegistroCrearCliente()
                        {
                            DIRECT = datoscliente.DIRECT,
                            IDOCTYP = datoscliente.IDOCTYP,
                            MANDT = datoscliente.MANDT,
                            MESTYP = datoscliente.MESTYP,
                            RCVPOR = datoscliente.RCVPOR,
                            RCVPRN = datoscliente.RCVPRN,
                            RCVPRT = datoscliente.RCVPRT,
                            SNDPOR = datoscliente.SNDPOR,
                            SNDPRN = datoscliente.SNDPRN,
                            SNDPRT = datoscliente.SNDPRT,
                            //ZCLIENTE
                            ZCLIENTE_STCD1 = documentoformateado,
                            ZCLIENTE_FIRSTNAME = datoscliente.Nombre,
                            ZCLIENTE_LASTNAME = datoscliente.Apellido,
                            ZCLIENTE_STREET = datoscliente.Direccion,
                            ZCLIENTE_PO_BOX = datoscliente.CodigoPostal,
                            ZCLIENTE_REGION = datoscliente.Estado,
                            ZCLIENTE_CITY = datoscliente.Ciudad,
                            ZCLIENTE_COUNTRY = datoscliente.Pais,
                            ZCLIENTE_REMARK = datoscliente.PuntoDeReferencia,
                            ZCLIENTE_TEL_NUMBER = datoscliente.Telefono1,
                            ZCLIENTE_MOB_NUMBER = datoscliente.Telefono2,
                            ZCLIENTE_CORREO = datoscliente.Email,
                            //ZAREA_VTAS
                            ZAREA_VTAS_SALESORG = datoscliente.OrganizacionDeVentas,
                            ZAREA_VTAS_DISTR_CHAN = datoscliente.CanalDeDistribucion,
                            ZAREA_VTAS_DIVISION = datoscliente.Division,
                            ZAREA_VTAS_STCD1 = documentoformateado
                        };
                        RespuestaCrearCliente r = rep.CrearClienteSap(registrocrearcliente);
                        return new RespuestaInsUpdCliente()
                        {
                            excode = r.excode,
                            exdetail = r.exdetail,
                            idClienteSap = r.idSapCliente
                        };
                    }
                    else
                    {
                        //actualizar
                        RegistroActualizarCliente registroactualizarcliente = new RegistroActualizarCliente()
                        {
                            PI_CUSTOMERNO = cliente.CUSTOMERNO,
                            PI_SALESORG = datoscliente.OrganizacionDeVentas,
                            PI_DISTR_CHAN = datoscliente.CanalDeDistribucion,
                            PI_DIVISION = datoscliente.Division,
                            PI_STCD1 = documentoformateado,
                            //ZCLIENTE
                            ZCLIENTE_NAME_FIRST = datoscliente.Nombre,
                            ZCLIENTE_NAME_LAST = datoscliente.Apellido,
                            ZCLIENTE_STREET = datoscliente.Direccion,
                            ZCLIENTE_POST_CODE1 = datoscliente.CodigoPostal,
                            ZCLIENTE_REGION = datoscliente.Estado,
                            ZCLIENTE_CITY1 = datoscliente.Ciudad,
                            ZCLIENTE_COUNTRY = datoscliente.Pais,
                            ZCLIENTE_REMARK = datoscliente.PuntoDeReferencia,
                            ZCLIENTE_TEL_NUMBER = datoscliente.Telefono1,
                            ZCLIENTE_MOB_NUMBER = datoscliente.Telefono2,
                            ZCLIENTE_CORREO = datoscliente.Email
                        };
                        RespuestaActualizarCliente r = rep.ActualizarClienteSap(registroactualizarcliente);
                        return new RespuestaInsUpdCliente()
                        {
                            excode = r.excode,
                            exdetail = r.exdetail,
                            idClienteSap = r.idSapClientePrincipal
                        };
                    }
                }
                else
                {
                    return new RespuestaInsUpdCliente()
                    {
                        excode = -2,
                        exdetail = "Archivo entrada vacío"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaInsUpdCliente()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message
                };
            }
        }

        public RespuestaInsUpdDelDireccion InsUpdDelDireccion(string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosDireccion datosdireccion = serializer.Deserialize<DatosDireccion>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datosdireccion.Name,
                        User = datosdireccion.User,
                        Password = datosdireccion.Password,
                        Client = datosdireccion.Client,
                        Language = datosdireccion.Language,
                        AppServerHost = datosdireccion.AppServerHost,
                        SystemNumber = datosdireccion.SystemNumber,
                        PoolSize = datosdireccion.PoolSize,
                        ConnectionIdleTimeout = datosdireccion.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    string documentoformateado1 = FormatearDocumento(datosdireccion.DocumentoPrincipal);
                    if (documentoformateado1 == "")
                    {
                        return new RespuestaInsUpdDelDireccion()
                        {
                            excode = -1,
                            exdetail = "Formato incorrecto Documento"
                        };
                    }
                    string documentoformateado2 = FormatearDocumento(datosdireccion.DocumentoAlterno);
                    if (documentoformateado2 == "")
                    {
                        return new RespuestaInsUpdDelDireccion()
                        {
                            excode = -1,
                            exdetail = "Formato incorrecto Documento"
                        };
                    }
                    //insertar, actualizar o eliminar direccion alterna
                    RegistroActualizarCliente registroactualizarcliente = new RegistroActualizarCliente()
                    {
                        PI_CUSTOMERNO = datosdireccion.idClienteSapPrincipal,
                        PI_SALESORG = datosdireccion.OrganizacionDeVentas,
                        PI_DISTR_CHAN = datosdireccion.CanalDeDistribucion,
                        PI_DIVISION = datosdireccion.Division,
                        PI_STCD1 = documentoformateado1,
                        //Z_DIRECC
                        Z_DIRECC_KUNNR = datosdireccion.idClienteSapAlterno,
                        Z_DIRECC_STCD1 = documentoformateado2,
                        Z_DIRECC_NAME_FIRST = datosdireccion.Nombre,
                        Z_DIRECC_NAME_LAST = datosdireccion.Apellido,
                        Z_DIRECC_STREET = datosdireccion.Dirección,
                        Z_DIRECC_POST_CODE1 = datosdireccion.CodigoPostal,
                        Z_DIRECC_REGION = datosdireccion.Estado,
                        Z_DIRECC_CITY1 = datosdireccion.Ciudad,
                        Z_DIRECC_COUNTRY = datosdireccion.Pais,
                        Z_DIRECC_REMARK = datosdireccion.PuntoDeReferencia,
                        Z_DIRECC_TEL_NUMBER = datosdireccion.Telefono1,
                        Z_DIRECC_MOB_NUMBER = datosdireccion.Telefono2,
                        Z_DIRECC_CORREO = datosdireccion.Email,
                        Z_DIRECC_ACCION = datosdireccion.Accion
                    };
                    RespuestaActualizarCliente r = rep.ActualizarClienteSap(registroactualizarcliente);
                    return new RespuestaInsUpdDelDireccion()
                    {
                        excode = r.excode,
                        exdetail = r.exdetail,
                        idClienteSapPrincipal = r.idSapClientePrincipal,
                        idClienteSapAlterno = r.idSapClienteAlterno
                    };
                }
                else
                {
                    return new RespuestaInsUpdDelDireccion()
                    {
                        excode = -2,
                        exdetail = "Archivo entrada vacío"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaInsUpdDelDireccion()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message
                };
            }
        }

        public RespuestaInsOferta InsOferta(string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosOferta datosoferta = serializer.Deserialize<DatosOferta>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datosoferta.Name,
                        User = datosoferta.User,
                        Password = datosoferta.Password,
                        Client = datosoferta.Client,
                        Language = datosoferta.Language,
                        AppServerHost = datosoferta.AppServerHost,
                        SystemNumber = datosoferta.SystemNumber,
                        PoolSize = datosoferta.PoolSize,
                        ConnectionIdleTimeout = datosoferta.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    string documentoformateado = FormatearDocumento(datosoferta.SOLICITANTE);
                    if (documentoformateado == "")
                    {
                        return new RespuestaInsOferta()
                        {
                            excode = -1,
                            exdetail = "Formato incorrecto Documento"
                        };
                    }
                    string documentoformateado2 = FormatearDocumento(datosoferta.DESTINATARIO);
                    if (documentoformateado2 == "")
                    {
                        return new RespuestaInsOferta()
                        {
                            excode = -1,
                            exdetail = "Formato incorrecto Documento 2"
                        };
                    }

                    RegistroCrearOferta registrocrearoferta = new RegistroCrearOferta()
                    {
                        DIRECT = datosoferta.DIRECT,
                        IDOCTYP = datosoferta.IDOCTYP,
                        MANDT = datosoferta.MANDT,
                        MESTYP = datosoferta.MESTYP,
                        RCVPOR = datosoferta.RCVPOR,
                        RCVPRN = datosoferta.RCVPRN,
                        RCVPRT = datosoferta.RCVPRT,
                        SNDPOR = datosoferta.SNDPOR,
                        SNDPRN = datosoferta.SNDPRN,
                        SNDPRT = datosoferta.SNDPRT,
                        //ZCLIENTE
                        ZSD_OFERTA1_AUART = datosoferta.CLASE_DE_OFERTA,
                        ZSD_OFERTA1_VKORG = datosoferta.ORGANIZACION,
                        ZSD_OFERTA1_VTWEG = datosoferta.CANAL,
                        ZSD_OFERTA1_SPART = datosoferta.SECTOR,
                        ZSD_OFERTA1_KUNNR = datosoferta.SOLICITANTE,
                        ZSD_OFERTA1_KUNNR2 = datosoferta.DESTINATARIO,
                        ZSD_OFERTA1_BSTKD = datosoferta.N_INTERNET,
                        ZSD_OFERTA1_BSTDK = datosoferta.FECHA_PEDIDO,
                        ZSD_OFERTA1_ANGDT = datosoferta.VALIDO_DESDE,
                        ZSD_OFERTA1_BNDDT = datosoferta.VALIDO_HASTA,
                        ZSD_OFERTA1_BNDDT2 = datosoferta.FECHA_ENTREGA,
                        ZSD_OFERTA1_TDLINE = datosoferta.TEXTO,
                        ZSD_OFERTA1_XFELD = datosoferta.RETIRAR_POR_SUCURSAL,
                        ZSD_OFERTA1_WERKS_D = datosoferta.SUCURSAL,
                    };
                    foreach (PosicionOferta p in datosoferta.POSICIONES_OFERTA)
                    {
                        RegistroPosicionCrearOferta p2 = new RegistroPosicionCrearOferta()
                        {
                            ZSD_OFERTA2_POSNR_VA = p.POSICION,
                            ZSD_OFERTA2_MATNR = p.MATERIAL,
                            ZSD_OFERTA2_KWMENG = p.CANTIDAD,
                            ZSD_OFERTA2_VRKME = p.UNIDAD_MEDIDA_DE_VENTA,
                            ZSD_OFERTA2_WERKS_EXT = p.CENTRO
                        };
                        registrocrearoferta.POSICIONES_OFERTA.Add(p2);
                    }
                    RespuestaCrearOferta r = rep.CrearOfertaSap(registrocrearoferta);
                    return new RespuestaInsOferta()
                    {
                        excode = r.excode,
                        exdetail = r.exdetail,
                        idOfertaSap = r.idSapOferta
                    };
                }
                else
                {
                    return new RespuestaInsOferta()
                    {
                        excode = -2,
                        exdetail = "Archivo entrada vacío"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RespuestaInsOferta()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message
                };
            }
        }

        public RegistroFactura Factura(string sPedido,string jsonString)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    DatosConexion datosconexion = serializer.Deserialize<DatosConexion>(jsonString);
                    RepositorioSAP.SapParametros parametros = new RepositorioSAP.SapParametros()
                    {
                        Name = datosconexion.Name,
                        User = datosconexion.User,
                        Password = datosconexion.Password,
                        Client = datosconexion.Client,
                        Language = datosconexion.Language,
                        AppServerHost = datosconexion.AppServerHost,
                        SystemNumber = datosconexion.SystemNumber,
                        PoolSize = datosconexion.PoolSize,
                        ConnectionIdleTimeout = datosconexion.ConnectionIdleTimeout
                    };
                    RepositorioSAP rep = new RepositorioSAP(parametros);
                    RegistroFactura resultado = rep.ConsultarFactura(sPedido);
                    return resultado;
                }
                else
                {
                    return new RegistroFactura()
                    {
                        excode = -2,
                        exdetail = "Archivo entrada vacío"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegistroFactura()
                {
                    excode = ex.HResult,
                    exdetail = ex.Message
                };
            }
        }


        private string FormatearDocumento(string Documento)
        {
            //V-14566318  => V-14566318-0
            //V-2896298   => V-02896298-0
            //J-123456789 => J-12345678-9
            if (Documento.Length < 11)
            {
                return Documento.Substring(0, 2) + Documento.Substring(2).PadLeft(8, '0') + "-0";
            }
            if (Documento.Length == 11)
            {
                return Documento.Substring(0, 10) + "-" + Documento.Substring(10);
            }
            else
            {
                return "";
            }
        }
    }
}
