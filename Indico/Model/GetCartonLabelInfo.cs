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
    
    public partial class GetCartonLabelInfo
    {
        public int ID { get; set; }
        public string PurchaseOrder { get; set; }
        public string VisualLayout { get; set; }
        public string SizeQuantities { get; set; }
        public string Client { get; set; }
        public string Distributor { get; set; }
        public string FactoryInvoiceNumber { get; set; }
        public Nullable<int> Total { get; set; }
    }
}
