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
    
    public partial class InvoiceHeaderDetailsView
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> IndimanInvoiceDate { get; set; }
        public string IndimanInvoiceNumber { get; set; }
        public System.DateTime FactoryInvoiceDate { get; set; }
        public string FactoryInvoiceNumber { get; set; }
        public string Week { get; set; }
        public string Month { get; set; }
        public System.DateTime ShipmentDate { get; set; }
        public string CompanyName { get; set; }
        public string PortName { get; set; }
        public string ShipmentModeName { get; set; }
        public Nullable<int> BillTo { get; set; }
        public string AWBNumber { get; set; }
        public string StatusName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPostalCode { get; set; }
        public string CompanyContact { get; set; }
        public string BillToCompanyName { get; set; }
        public string BillToAddress { get; set; }
        public string BillToCompanyState { get; set; }
        public string BillToCountry { get; set; }
    }
}
