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
    
    public partial class SPC_GetDetailForPackingList_Result
    {
        public int ShipmentDeatil { get; set; }
        public int IndicoOrderID { get; set; }
        public int IndicoOrderDetailID { get; set; }
        public int ShipmentDetailID { get; set; }
        public string Heading { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string InvoiceNumber { get; set; }
        public int CartonNumber { get; set; }
        public string Client { get; set; }
        public string PurchaseOrder { get; set; }
        public string VisualLayout { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string SizeString { get; set; }
        public string Pattern { get; set; }
        public string Distributor { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string OrderType { get; set; }
    }
}
