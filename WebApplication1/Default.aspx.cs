
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
//using HttpUtils;
//using System.Web.UI.MobileControls;
using RestSharp;
using System.Web.Script.Services;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestService;
using System.Data.SqlClient;

namespace SampleRestWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        //Entorno entornojson = new Entorno();

        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonStringjerarquia = "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\"}";

            string jsonStringinventario = "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\"}";

            string jsonStringinsupdcliente = "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\","
                        + "\"DIRECT\" : \"\","
                        + "\"IDOCTYP\" : \"\","
                        + "\"MANDT\" : \"\","
                        + "\"MESTYP\" : \"\","
                        + "\"RCVPOR\" : \"\","
                        + "\"RCVPRN\" : \"\","
                        + "\"RCVPRT\" : \"\","
                        + "\"SNDPOR\" : \"\","
                        + "\"SNDPRN\" : \"\","
                        + "\"SNDPRT\" : \"\","
                        + "\"Documento\" : \"V-2896298\","
                        + "\"Nombre\" : \"QAaaaaaa\","
                        + "\"Apellido\" : \"QAaaaaaaa\","
                        + "\"Direccion\" : \"QAAAAAAAAAAA\","
                         + "\"Ciudad\" : \"QAAAAAAAAAAA\","
                        + "\"Pais\" : \"VE\","
                        + "\"Estado\" : \"VAR\","
                        + "\"CodigoPostal\" : \"1070\","
                        + "\"PuntoDeReferencia\" : \"QAaaaaaaaaaaaaa\","
                        + "\"Telefono1\" : \"22222222\","
                        + "\"Telefono2\" : \"22222222\","
                        + "\"Email\" : \"222@2.com\","
                        + "\"OrganizacionDeVentas\" : \"0100\","
                        + "\"CanalDeDistribucion\" : \"02\","
                        + "\"Division\" : \"01\"}";

            string jsonStringinsupddeldireccion = "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\","
                        + "\"DIRECT\" : \"\","
                        + "\"IDOCTYP\" : \"\","
                        + "\"MANDT\" : \"\","
                        + "\"MESTYP\" : \"\","
                        + "\"RCVPOR\" : \"\","
                        + "\"RCVPRN\" : \"\","
                        + "\"RCVPRT\" : \"\","
                        + "\"SNDPOR\" : \"\","
                        + "\"SNDPRN\" : \"\","
                        + "\"SNDPRT\" : \"\","
                        + "\"Documento\" : \"V-2896298\","
                        + "\"Nombre\" : \"QAaaaaaa\","
                        + "\"Apellido\" : \"QAaaaaaaa\","
                        + "\"Direccion\" : \"QAAAAAAAAAAA\","
                         + "\"Ciudad\" : \"QAAAAAAAAAAA\","
                        + "\"Pais\" : \"VE\","
                        + "\"Estado\" : \"VAR\","
                        + "\"CodigoPostal\" : \"1070\","
                        + "\"PuntoDeReferencia\" : \"QAaaaaaaaaaaaaa\","
                        + "\"Telefono1\" : \"22222222\","
                        + "\"Telefono2\" : \"22222222\","
                        + "\"Email\" : \"222@2.com\","
                        + "\"OrganizacionDeVentas\" : \"0100\","
                        + "\"CanalDeDistribucion\" : \"02\","
                        + "\"Division\" : \"01\"}";

            string jsonStringinsoferta= "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\","
                        + "\"DIRECT\" : \"\","
                        + "\"IDOCTYP\" : \"\","
                        + "\"MANDT\" : \"\","
                        + "\"MESTYP\" : \"\","
                        + "\"RCVPOR\" : \"\","
                        + "\"RCVPRN\" : \"\","
                        + "\"RCVPRT\" : \"\","
                        + "\"SNDPOR\" : \"\","
                        + "\"SNDPRN\" : \"\","
                        + "\"SNDPRT\" : \"\","
                        + "\"Documento\" : \"V-2896298\","
                        + "\"Nombre\" : \"QAaaaaaa\","
                        + "\"Apellido\" : \"QAaaaaaaa\","
                        + "\"Direccion\" : \"QAAAAAAAAAAA\","
                         + "\"Ciudad\" : \"QAAAAAAAAAAA\","
                        + "\"Pais\" : \"VE\","
                        + "\"Estado\" : \"VAR\","
                        + "\"CodigoPostal\" : \"1070\","
                        + "\"PuntoDeReferencia\" : \"QAaaaaaaaaaaaaa\","
                        + "\"Telefono1\" : \"22222222\","
                        + "\"Telefono2\" : \"22222222\","
                        + "\"Email\" : \"222@2.com\","
                        + "\"OrganizacionDeVentas\" : \"0100\","
                        + "\"CanalDeDistribucion\" : \"02\","
                        + "\"Division\" : \"01\"}";

            string jsonStringfactura = "{"
                        + "\"Name\" : \"DV1\","
                        + "\"User\" : \"webplz\","
                        + "\"Password\" : \"webplz.2015\","
                        + "\"Client\" : \"300\","
                        + "\"Language\" : \"S\","
                        + "\"AppServerHost\" : \"172.20.3.200\","
                        + "\"SystemNumber\" : \"00\","
                        + "\"PoolSize\" : \"10\","
                        + "\"ConnectionIdleTimeout\" : \"10\","
                        + "\"DIRECT\" : \"\","
                        + "\"IDOCTYP\" : \"\","
                        + "\"MANDT\" : \"\","
                        + "\"MESTYP\" : \"\","
                        + "\"RCVPOR\" : \"\","
                        + "\"RCVPRN\" : \"\","
                        + "\"RCVPRT\" : \"\","
                        + "\"SNDPOR\" : \"\","
                        + "\"SNDPRN\" : \"\","
                        + "\"SNDPRT\" : \"\","
                        + "\"Documento\" : \"V-2896298\","
                        + "\"Nombre\" : \"QAaaaaaa\","
                        + "\"Apellido\" : \"QAaaaaaaa\","
                        + "\"Direccion\" : \"QAAAAAAAAAAA\","
                         + "\"Ciudad\" : \"QAAAAAAAAAAA\","
                        + "\"Pais\" : \"VE\","
                        + "\"Estado\" : \"VAR\","
                        + "\"CodigoPostal\" : \"1070\","
                        + "\"PuntoDeReferencia\" : \"QAaaaaaaaaaaaaa\","
                        + "\"Telefono1\" : \"22222222\","
                        + "\"Telefono2\" : \"22222222\","
                        + "\"Email\" : \"222@2.com\","
                        + "\"OrganizacionDeVentas\" : \"0100\","
                        + "\"CanalDeDistribucion\" : \"02\","
                        + "\"Division\" : \"01\"}";
            
            //const string url = "http://172.20.1.36/nuevaweb/RestServiceImpl.svc/";
            //const string url = "http://localhost:1307/RestServiceImpl.svc/";
            const string url = "http://172.20.1.36/RestServiceImpl.svc/";            
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                var client = new RestClient();
                // This, of course, needs to be altered to match the
                // IP Address/Port of the server to which you are connecting
                client.BaseUrl = url;
                var request = new RestRequest();
                request.AddHeader("Content-Length", int.MaxValue.ToString());
                request.Method = Method.POST;
                request.RequestFormat = DataFormat.Json;

                //request.AddBody(jsonStringjerarquiaweb);
                //request.Resource = "/jerarquiaweb";

                //request.AddBody(jsonStringinventario);
                //request.Resource = "/inventario/1017/13W0100101";
                
                //request.AddBody(jsonStringinsupdcliente);
                //request.Resource = "/insupdcliente";
                
                //request.AddBody(jsonStringinsupddeldireccion);
                //request.Resource = "/insupddeldireccion";
                
                //request.AddBody(jsonStringinsoferta);
                //request.Resource = "/insoferta";
                
                //request.AddBody(jsonStringfactura);
                //request.Resource = "/factura/23";
                
                // The server's Rest method will probably return something 
                var response = client.Execute(request) as RestResponse;
                if (response != null && ((response.StatusCode == HttpStatusCode.OK) && (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
                {
                    var obj = response.Content;
                    Response.Write(obj);
                }
                else if (response != null)
                {
                    Response.Write(string.Format
                    ("Status code is {0} ({1}); response status is {2}", response.StatusCode, response.StatusDescription, response.ResponseStatus));
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
