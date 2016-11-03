namespace IndicoPacking.DAL.Objects.Views
{
    public class GetInvoiceOrderDetailItemsWithQuatityBreakdownBo
    {
        #region Properties

		
		public string PurchaseOrder { get; set; }
		public int ID { get; set; }
		public int IndicoOrderID { get; set; }
		public int IndicoOrderDetailID { get; set; }
		public int OrderNumber { get; set; }
		public string OrderType { get; set; }
		public string VisualLayout { get; set; }
		public string Pattern { get; set; }
		public string Fabric { get; set; }
		public string Gender { get; set; }
		public string AgeGroup { get; set; }
		public string SleeveShape { get; set; }
		public string SleeveLength { get; set; }
		public string SizeDesc { get; set; }
		public int? SizeQty { get; set; }
		public int? SizeSrno { get; set; }
		public string Distributor { get; set; }
		public string Client { get; set; }
		public decimal? FactoryPrice { get; set; }
		public decimal? IndimanPrice { get; set; }
		public int ShipmentDeatil { get; set; }
		public int? Invoice { get; set; }
		public decimal? OtherCharges { get; set; }
		public decimal? JKFOBCostSheetPrice { get; set; }
		public decimal? IndimanCIFCostSheetPrice { get; set; }
		public string Notes { get; set; }

        #endregion
    }
}