using RFCWEBSAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace RestService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRestServiceImpl" in both code and config file together.
    [ServiceContract]
    public interface IRestServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getinventario/{sCentro}/{sJerarquiaWeb}")]
        List<RegistroInventario> Inventario(string sCentro, string sJerarquiaWeb);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "getjerarquiaweb")]
        List<RegistroJerarquiaWeb> JerarquiaWeb();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "postjerarquiaweb")]
        List<RegistroJerarquiaWeb> PostJerarquiaWeb(string jsonString);

    }
     
    [DataContract]
    public class Entorno
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Valor { get; set; }
    }

}
