namespace IndicoPacking.DAL.Objects.Views
{
    public class GetWeeklyAddressDetailsByDistributorBo
    {
        #region Properties

		
		public int ID { get; set; }
		public int IndicoOrderDetailID { get; set; }
		public string Distributor { get; set; }
		public string Client { get; set; }
		public string OrderType { get; set; }
		public int? Qty { get; set; }
		public string GenderAgeGroup { get; set; }
		public string Material { get; set; }
		public decimal? FactoryPrice { get; set; }
		public decimal? OtherCharges { get; set; }
		public decimal? TotalPrice { get; set; }
		public decimal? Amount { get; set; }
		public string PurchaseOrder { get; set; }
		public string PurchaseOrderNo { get; set; }
		public string Fabric { get; set; }
		public string VisualLayout { get; set; }
		public string ItemName { get; set; }
		public string SizeQuantities { get; set; }

        #endregion
    }
}