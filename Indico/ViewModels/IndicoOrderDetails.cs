using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IndicoPacking.ViewModels
{
  class IndicoOrderDetails
  {
    public int OrderId { get; set; }
    public int OrderDetailId { get; set; }
    public DateTime OrderShipmentDate { get; set; }
    public DateTime OrderDetailShipmentDate { get; set; }
    public string OrderType { get; set; }
    public string Distributor { get; set; } 
    public string Client { get; set; }
    public string PurchaseOrder { get; set; }
    public string NamePrefix { get; set; }
    public string Pattern { get; set; }
    public string Fabric { get; set; }
    public string Material { get; set; }
    public string Gender { get; set; }
    public string AgeGroup { get; set; }
    public string SleeveShape { get; set; }
    public string SleeveLength { get; set; }
    public string ItemSubGroup { get; set; }
    public string SizeName { get; set; }
    public int Quentity { get; set; }
    public string Status { get; set; }
    public int PrintedCount { get; set; }
    public string PatternImagePath { get; set; }
    public string VLImagePath { get; set; }
    public string Number { get; set; }
    public double OtherCharges { get; set; }
    public string Notes { get; set; }
    public string PatternInvoiceNotes { get; set; }
    public double JKFOBCostSheetPrice { get; set; }
    public double IndimanCIFCostSheetPrice { get; set; }
    public string HSCode { get; set; }
    public string ItemName { get; set; }
    public string PurchaseOrderNo { get; set; }
  }
}
