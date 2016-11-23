using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBSUMA
{
    public class RegistroCrearAfiliacionSuma
    {
        //CAMPOR MINIMOS PARA CREAR SOLICITUD DE AFILIACON
        //ENTIDAD Affiliate
        public string docnumber { get; set; }           // Documento de Identificación del Afiliado
        public int clientid { get; set; }               // id de Cliente del Afiliado en WEBPLAZAS
        public int storeid { get; set; }                // id de Sucursal de Afiliación
        public int channelid { get; set; }              // id de Canal de Afiliación
        public int typeid { get; set; }                 // id de Tipo de Afiliación
        public int sumastatusid { get; set; }           // id de Estatus de Afiliación en Sumastatus
        public string typedelivery { get; set; }        // Tipo de Envío de WEBPLAZAS
        public string twitter_account { get; set; }     // cuenta de Twitter
        public string facebook_account { get; set; }    // cuenta de Facebook
        public string instagram_account { get; set; }   // cuenta de Instagram        
        //ENTIDAD CLIENTE
        public string nationality { get; set; }         // Nacionalidad
        public string name { get; set; }                // Primer Nombre
        public string name2 { get; set; }               // Segundo Nombre 
        public string lastname1 { get; set; }           // Primer Apellido
        public string lastname2 { get; set; }           // Segundo Apellido 
        public string birthdate { get; set; }           // Fecha de Nacimiento
        public string gender { get; set; }              // Sexo
        public string maritalstatus { get; set; }       // Estado Civil
        public string occupation { get; set; }          // Ocupación
        public string phone1 { get; set; }              // Teléfono Habitación
        public string phone2 { get; set; }              // Teléfono Oficina
        public string phone3 { get; set; }              // Teléfono Celular
        public string email { get; set; }               // Email
        public string cod_estado { get; set; }          // Dirección Codigo Estado
        public string cod_ciudad { get; set; }          // Dirección Codigo Ciudad
        public string cod_municipio { get; set; }       // Dirección Codigo Municipio
        public string cod_parroquia { get; set; }       // Dirección Codigo Parroquia
        public string cod_urbanizacion { get; set; }    // Dirección Codigo Urbanización    
        //ENTIDAD CustomerInterest
        public List<int> Intereses { get; set; }        // Lista de Intereses del Afiliado
        public int usuarioAfiliacion { get; set; }
    }
}
