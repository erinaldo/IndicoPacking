namespace IndicoPacking.DAL.Objects.Views
{
    public class GetCartonLabelInfoBo
    {
        #region Properties

		
		public int ID { get; set; }
		public string PurchaseOrder { get; set; }
		public string VisualLayout { get; set; }
		public string SizeQuantities { get; set; }
		public int? Total { get; set; }
		public string Client { get; set; }
		public string Distributor { get; set; }
		public string FactoryInvoiceNumber { get; set; }

        #endregion
    }
}