//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEBSUMA
{
    using System;
    using System.Collections.Generic;
    
    public partial class CIUDAD
    {
        public CIUDAD()
        {
            this.MUNICIPIOs = new HashSet<MUNICIPIO>();
            this.ESTADOes = new HashSet<ESTADO>();
        }
    
        public string COD_CIUDAD { get; set; }
        public string DESCRIPC_CIUDAD { get; set; }
    
        public virtual ICollection<MUNICIPIO> MUNICIPIOs { get; set; }
        public virtual ICollection<ESTADO> ESTADOes { get; set; }
    }
}
