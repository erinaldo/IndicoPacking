//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndicoPacking.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carton
    {
        public Carton()
        {
            this.ShipmentDetailCartons = new HashSet<ShipmentDetailCarton>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<ShipmentDetailCarton> ShipmentDetailCartons { get; set; }
    }
}
