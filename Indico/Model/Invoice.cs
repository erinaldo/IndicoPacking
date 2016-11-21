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
    
    public partial class Invoice
    {
        public Invoice()
        {
            this.OrderDeatilItems = new HashSet<OrderDeatilItem>();
        }
    
        public int ID { get; set; }
        public int ShipmentDetail { get; set; }
        public string FactoryInvoiceNumber { get; set; }
        public System.DateTime FactoryInvoiceDate { get; set; }
        public string AWBNumber { get; set; }
        public string IndimanInvoiceNumber { get; set; }
        public Nullable<System.DateTime> IndimanInvoiceDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public int LastModifiedBy { get; set; }
        public int Status { get; set; }
        public System.DateTime ShipmentDate { get; set; }
        public int ShipTo { get; set; }
        public Nullable<int> BillTo { get; set; }
        public int ShipmentMode { get; set; }
        public int Port { get; set; }
        public int Bank { get; set; }
        public Nullable<int> CourierCharges { get; set; }
    
        public virtual Bank Bank1 { get; set; }
        public virtual DistributorClientAddress DistributorClientAddress { get; set; }
        public virtual DistributorClientAddress DistributorClientAddress1 { get; set; }
        public virtual User User { get; set; }
        public virtual Port Port1 { get; set; }
        public virtual ShipmentDetail ShipmentDetail1 { get; set; }
        public virtual ShipmentMode ShipmentMode1 { get; set; }
        public virtual InvoiceStatu InvoiceStatu { get; set; }
        public virtual ICollection<OrderDeatilItem> OrderDeatilItems { get; set; }
    }
}
