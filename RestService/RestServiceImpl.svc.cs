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
        public List<RegistroInventario> Inventario(string sCentro, string sJerarquiaWeb)
        {
            string EntornoSAP = System.Configuration.ConfigurationManager.AppSettings["EntornoSAP"];
            RepositorioSAP rep = new RepositorioSAP(EntornoSAP);
            List<RegistroInventario> resultado = rep.ConsultarInventario(sCentro, sJerarquiaWeb);
            return resultado;
        }

        public List<RegistroJerarquiaWeb> JerarquiaWeb()
        {
            string EntornoSAP = System.Configuration.ConfigurationManager.AppSettings["EntornoSAP"];
            RepositorioSAP rep = new RepositorioSAP(EntornoSAP);
            List<RegistroJerarquiaWeb> resultado = rep.ConsultarJerarquiaWeb();
            return resultado;
        }

        public List<RegistroJerarquiaWeb> PostJerarquiaWeb(string jsonString)
        {
            string EntornoSAP;
            //EL ENTONRNO SAP VIENE EN UN JSON RECIBIDO COMO STRING
            if (!string.IsNullOrEmpty(jsonString))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var ent = serializer.Deserialize<Entorno>(jsonString);
                EntornoSAP = ent.Valor;
                RepositorioSAP rep = new RepositorioSAP(EntornoSAP);
                List<RegistroJerarquiaWeb> resultado = rep.ConsultarJerarquiaWeb();
                return resultado;
            }
            else
            {
                return null;
            }
        }
        
    }
}
