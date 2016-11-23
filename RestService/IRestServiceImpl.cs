using RFCWEBSAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using WEBSUMA;
using System.IO;
using System.Web;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IRestServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "jerarquiaweb")]
        List<RegistroJerarquiaWeb> JerarquiaWeb(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "inventario/{sCentro}/{sJerarquiaWeb}")]
        List<RegistroInventario> Inventario(string sCentro, string sJerarquiaWeb, string jsonString);
        
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "insupdcliente")]
        RespuestaInsUpdCliente InsUpdCliente(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "insupddeldireccion")]
        RespuestaInsUpdDelDireccion InsUpdDelDireccion(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "insoferta")]
        RespuestaInsOferta InsOferta(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "factura/{sPedido}")]
        RegistroFactura Factura(string sPedido, string jsonString);

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "direcciones")]
        List<RegistroDireccion> Direcciones();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "intereses")]
        List<RegistroInteres> Intereses();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "tiposdeafiliacion")]
        List<RegistroTipoDeAfiliacion> TiposDeAfiliacion();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "estadosciviles")]
        List<RegistroEstadoCivil> EstadosCiviles();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "sexos")]
        List<RegistroSexo> Sexos();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "nacionalidades")]
        List<RegistroNacionalidad> Nacionalidades();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "canales")]
        List<RegistroCanal> Canales();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "sucursales")]
        List<RegistroSucursal> Sucursales();

        [OperationContract]
        [WebInvoke(Method = "GET",
            //RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "findafiliacionsuma/{sDocNumber}")]
        RespuestaFindAfiliacionSuma FindAfiliacionSuma(string sDocNumber);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "insafiliacionsuma")]
        RespuestaInsAfiliacionSuma InsAfiliacionSuma(string jsonString);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "insimagenafiliacionsuma/{sIdAfiliacion}")]
        RespuestaInsImagenAfiliacionSuma InsImagenAfiliacionSuma(string sIdAfiliacion, string jsonString);       
    }

    [DataContract]
    public class DatosConexion
    {
        //PARAMETROS DE CONFIGURACIÓN - CONEXION A SAP
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string User { set; get; }
        [DataMember]
        public string Password { set; get; }
        [DataMember]
        public string Client { set; get; }
        [DataMember]
        public string Language { set; get; }
        [DataMember]
        public string AppServerHost { set; get; }
        [DataMember]
        public string SystemNumber { set; get; }
        [DataMember]
        public string PoolSize { set; get; }
        [DataMember]
        public string ConnectionIdleTimeout { set; get; }
    }

    [DataContract]
    public class DatosCliente
    {
        //PARAMETROS DE CONFIGURACIÓN - CONEXION A SAP
        [DataMember]
        public string Name { set; get; }//="DV1" 
        [DataMember]
        public string User { set; get; }//="webplz" 
        [DataMember]
        public string Password { set; get; }//="webplz.2015" 
        [DataMember]
        public string Client { set; get; }//="300" 
        [DataMember]
        public string Language { set; get; }//="S" 
        [DataMember]
        public string AppServerHost { set; get; }//="172.20.3.200" 
        [DataMember]
        public string SystemNumber { set; get; }//="00"
        [DataMember]
        public string PoolSize { set; get; }//="10" 
        [DataMember]
        public string ConnectionIdleTimeout { set; get; }//="10"
        //PARAMETROS CONFIGURACIÓN - ESTRUCTURA DE CONTROL PARA INTERCAMBIO DE IDOCS
        [DataMember]
        public string DIRECT { set; get; }
        [DataMember]
        public string IDOCTYP { set; get; }
        [DataMember]
        public string MANDT { set; get; }
        [DataMember]
        public string MESTYP { set; get; }
        [DataMember]
        public string RCVPOR { set; get; }
        [DataMember]
        public string RCVPRN { set; get; }
        [DataMember]
        public string RCVPRT { set; get; }
        [DataMember]
        public string SNDPOR { set; get; }
        [DataMember]
        public string SNDPRN { set; get; }
        [DataMember]
        public string SNDPRT { set; get; }
        //DATOS DEL CLIENTE - ZCLIENTE
        [DataMember]
        public string Documento { set; get; }
        [DataMember]
        public string Nombre { set; get; }
        [DataMember]
        public string Apellido { set; get; }
        [DataMember]
        public string Direccion { set; get; }
        [DataMember]
        public string Ciudad { set; get; }
        [DataMember]
        public string Pais { set; get; }
        [DataMember]
        public string Estado { set; get; }
        [DataMember]
        public string CodigoPostal { set; get; }
        [DataMember]
        public string PuntoDeReferencia { set; get; }
        [DataMember]
        public string Telefono1 { set; get; }
        [DataMember]
        public string Telefono2 { set; get; }
        [DataMember]
        public string Email { set; get; }
        //DATOS DEL CLIENTE - ZAREA_VTAS
        [DataMember]
        public string OrganizacionDeVentas { set; get; }
        [DataMember]
        public string CanalDeDistribucion { set; get; }
        [DataMember]
        public string Division { set; get; }
    }

    [DataContract]
    public class DatosDireccion
    {
        //PARAMETROS DE CONFIGURACIÓN - CONEXION A SAP
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string User { set; get; }
        [DataMember]
        public string Password { set; get; }
        [DataMember]
        public string Client { set; get; }
        [DataMember]
        public string Language { set; get; }
        [DataMember]
        public string AppServerHost { set; get; }
        [DataMember]
        public string SystemNumber { set; get; }
        [DataMember]
        public string PoolSize { set; get; }
        [DataMember]
        public string ConnectionIdleTimeout { set; get; }
        //DATOS DE DIRECCION - Z_DIRECC
        [DataMember]
        public string idClienteSapPrincipal { set; get; }
        [DataMember]
        public string DocumentoPrincipal { set; get; }
        [DataMember]
        public string idClienteSapAlterno { set; get; }
        [DataMember]
        public string DocumentoAlterno { set; get; }
        [DataMember]
        public string Nombre { set; get; }
        [DataMember]
        public string Apellido { set; get; }
        [DataMember]
        public string Dirección { set; get; }
        [DataMember]
        public string Ciudad { set; get; }
        [DataMember]
        public string Pais { set; get; }
        [DataMember]
        public string Estado { set; get; }
        [DataMember]
        public string CodigoPostal { set; get; }
        [DataMember]
        public string PuntoDeReferencia { set; get; }
        [DataMember]
        public string Telefono1 { set; get; }
        [DataMember]
        public string Telefono2 { set; get; }
        [DataMember]
        public string Email { set; get; }
        [DataMember]
        public string OrganizacionDeVentas { set; get; }
        [DataMember]
        public string CanalDeDistribucion { set; get; }
        [DataMember]
        public string Division { set; get; }
        [DataMember]
        public string Accion { set; get; }
    }

    [DataContract]
    public class DatosOferta
    {
        //PARAMETROS DE CONFIGURACIÓN - CONEXION A SAP
        [DataMember]
        public string Name { set; get; }//="DV1" 
        [DataMember]
        public string User { set; get; }//="webplz" 
        [DataMember]
        public string Password { set; get; }//="webplz.2015" 
        [DataMember]
        public string Client { set; get; }//="300" 
        [DataMember]
        public string Language { set; get; }//="S" 
        [DataMember]
        public string AppServerHost { set; get; }//="172.20.3.200" 
        [DataMember]
        public string SystemNumber { set; get; }//="00"
        [DataMember]
        public string PoolSize { set; get; }//="10" 
        [DataMember]
        public string ConnectionIdleTimeout { set; get; }//="10"
        //PARAMETROS CONFIGURACIÓN - ESTRUCTURA DE CONTROL PARA INTERCAMBIO DE IDOCS
        [DataMember]
        public string DIRECT { set; get; }
        [DataMember]
        public string IDOCTYP { set; get; }
        [DataMember]
        public string MANDT { set; get; }
        [DataMember]
        public string MESTYP { set; get; }
        [DataMember]
        public string RCVPOR { set; get; }
        [DataMember]
        public string RCVPRN { set; get; }
        [DataMember]
        public string RCVPRT { set; get; }
        [DataMember]
        public string SNDPOR { set; get; }
        [DataMember]
        public string SNDPRN { set; get; }
        [DataMember]
        public string SNDPRT { set; get; }
        //DATOS DE OFERTA - ZSD_OFERTA1
        [DataMember]
        public string CLASE_DE_OFERTA { set; get; }
        [DataMember]
        public string ORGANIZACION { set; get; }
        [DataMember]
        public string CANAL { set; get; }
        [DataMember]
        public string SECTOR { set; get; }
        [DataMember]
        public string SOLICITANTE { set; get; }
        [DataMember]
        public string DESTINATARIO { set; get; }
        [DataMember]
        public string N_INTERNET { set; get; }
        [DataMember]
        public string FECHA_PEDIDO { set; get; }
        [DataMember]
        public string VALIDO_DESDE { set; get; }
        [DataMember]
        public string VALIDO_HASTA { set; get; }
        [DataMember]
        public string FECHA_ENTREGA { set; get; }
        [DataMember]
        public string TEXTO { set; get; }
        [DataMember]
        public string RETIRAR_POR_SUCURSAL { set; get; }
        [DataMember]
        public string SUCURSAL { set; get; }
        //POSICIONES DE LA OFERTA - ZSD_OFERTA2
        [DataMember]
        public List<PosicionOferta> POSICIONES_OFERTA { set; get; }        
    }

    [DataContract]
    public class DatosAfiliacionSuma
    {
        //CAMPOR MINIMOS PARA CREAR SOLICITUD DE AFILIACON
        //ENTIDAD Affiliate
        [DataMember]
        public string docnumber { get; set; }           // Documento de Identificación del Afiliado
        [DataMember]
        public int clientid { get; set; }               // id de Cliente del Afiliado en WEBPLAZAS
        [DataMember]
        public int storeid { get; set; }                // id de Sucursal de Afiliación
        [DataMember]
        public int channelid { get; set; }              // id de Canal de Afiliación
        [DataMember]
        public int typeid { get; set; }                 // id de Tipo de Afiliación
        [DataMember]
        public int sumastatusid { get; set; }           // id de Estatus de Afiliación en Sumastatus
        [DataMember]
        public string typedelivery { get; set; }        // Tipo de Envío de WEBPLAZAS
        [DataMember]
        public string twitter_account { get; set; }     // cuenta de Twitter
        [DataMember]
        public string facebook_account { get; set; }    // cuenta de Facebook
        [DataMember]
        public string instagram_account { get; set; }   // cuenta de Instagram        
        //ENTIDAD CLIENTE
        [DataMember]
        public string nationality { get; set; }         // Nacionalidad
        [DataMember]
        public string name { get; set; }                // Primer Nombre
        [DataMember]
        public string name2 { get; set; }               // Segundo Nombre 
        [DataMember]
        public string lastname1 { get; set; }           // Primer Apellido
        [DataMember]
        public string lastname2 { get; set; }           // Segundo Apellido 
        [DataMember]
        public string birthdate { get; set; }           // Fecha de Nacimiento
        [DataMember]
        public string gender { get; set; }              // Sexo
        [DataMember]
        public string maritalstatus { get; set; }       // Estado Civil
        [DataMember]
        public string occupation { get; set; }          // Ocupación
        [DataMember]
        public string phone1 { get; set; }              // Teléfono Habitación
        [DataMember]
        public string phone2 { get; set; }              // Teléfono Oficina
        [DataMember]
        public string phone3 { get; set; }              // Teléfono Celular
        [DataMember]
        public string email { get; set; }               // Email
        [DataMember]
        public string cod_estado { get; set; }          // Dirección Codigo Estado
        [DataMember]
        public string cod_ciudad { get; set; }          // Dirección Codigo Ciudad
        [DataMember]
        public string cod_municipio { get; set; }       // Dirección Codigo Municipio
        [DataMember]
        public string cod_parroquia { get; set; }       // Dirección Codigo Parroquia
        [DataMember]
        public string cod_urbanizacion { get; set; }    // Dirección Codigo Urbanización    
        //ENTIDAD CustomerInterest
        [DataMember]
        public List<int> Intereses { get; set; }        // Lista de Intereses del Afiliado
        [DataMember]
        public int usuarioAfiliacion { get; set; }
    }

    [DataContract]
    public class DatosArchivo
    {
        [DataMember]
        public string base64EncodedData { get; set; }
    }

}
