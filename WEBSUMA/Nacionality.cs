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
    
    public partial class Nacionality
    {
        public Nacionality()
        {
            this.CLIENTEs = new HashSet<CLIENTE>();
            this.CLIENTEs1 = new HashSet<CLIENTE>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string abbrev { get; set; }
    
        public virtual ICollection<CLIENTE> CLIENTEs { get; set; }
        public virtual ICollection<CLIENTE> CLIENTEs1 { get; set; }
    }
}
