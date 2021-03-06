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
    
    public partial class OrderDeatilItem
    {
        public int ID { get; set; }
        public int ShipmentDeatil { get; set; }
        public int IndicoOrderID { get; set; }
        public int IndicoOrderDetailID { get; set; }
        public Nullable<int> ShipmentDetailCarton { get; set; }
        public string OrderType { get; set; }
        public string Distributor { get; set; }
        public string Client { get; set; }
        public string PurchaseOrder { get; set; }
        public string VisualLayout { get; set; }
        public int OrderNumber { get; set; }
        public string Pattern { get; set; }
        public string ItemSubGroup { get; set; }
        public string SizeDesc { get; set; }
        public Nullable<int> SizeQty { get; set; }
        public Nullable<int> SizeSrno { get; set; }
        public string Status { get; set; }
        public bool IsPolybagScanned { get; set; }
        public Nullable<int> PrintedCount { get; set; }
        public string PatternImage { get; set; }
        public string VLImage { get; set; }
        public string PatternNumber { get; set; }
        public Nullable<System.DateTime> DateScanned { get; set; }
        public Nullable<int> Invoice { get; set; }
        public Nullable<decimal> FactoryPrice { get; set; }
        public Nullable<decimal> IndimanPrice { get; set; }
        public string Fabric { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public string SleeveShape { get; set; }
        public string SleeveLength { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public string Notes { get; set; }
        public string PatternInvoiceNotes { get; set; }
        public string ProductNotes { get; set; }
        public Nullable<decimal> JKFOBCostSheetPrice { get; set; }
        public Nullable<decimal> IndimanCIFCostSheetPrice { get; set; }
        public string HSCode { get; set; }
        public string ItemName { get; set; }
        public string Material { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string JobName { get; set; }
    
        public virtual ShipmentDetail ShipmentDetail { get; set; }
        public virtual ShipmentDetailCarton ShipmentDetailCarton1 { get; set; }
        public virtual Invoice Invoice1 { get; set; }
    }
}
