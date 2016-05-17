
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
        Entorno entornojson = new Entorno();

        protected void Page_Load(object sender, EventArgs e)
        {
            string jsonString = "{"
                        + "\"Nombre\" : \"QA\","
                        + "\"Valor\" : \"QA1\"}";

            const string url = "http://172.20.1.36/nuevaweb/RestServiceImpl.svc/";

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
                request.AddBody(jsonString);
                request.Resource = "/postjerarquiaweb";
                // The server's Rest method will probably return something 
                var response = client.Execute(request) as RestResponse;
                if (response != null && ((response.StatusCode == HttpStatusCode.OK) &&
                 (response.ResponseStatus == ResponseStatus.Completed))) // It's probably not necessary to test both
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
