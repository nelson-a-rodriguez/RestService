using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEBSUMA
{
    public class RegistroDireccion
    {
        public int excode {get;set;}
        public string exdetail { get; set; }

        public Estado estado { get; set; }

        public class Estado
        {
            public string id { get; set; }
            public string descripcion { get; set; }
            public List<Ciudad> ciudades { get; set; }
            
            public class Ciudad
            {
                public string id { get; set; }
                public string descripcion { get; set; }
                public List<Municipio> municipios { get; set; }

                public class Municipio
                {
                    public string id { get; set; }
                    public string descripcion { get; set; }
                    public List<Parroquia> parroquias { get; set; }

                    public class Parroquia
                    {
                        public string id { get; set; }
                        public string descripcion { get; set; }
                        public List<Urbanizacion> urbanizaciones { get; set; }

                        public class Urbanizacion
                        {
                            public string id { get; set; }
                            public string descripcion { get; set; }
                        }
                    }
                }
            }
        }
    }
}
