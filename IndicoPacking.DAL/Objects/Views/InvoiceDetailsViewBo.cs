namespace IndicoPacking.DAL.Objects.Views
{
    public class InvoiceDetailsViewBo
    {
        #region Properties

		
		public int ID { get; set; }
		public DateTime? IndimanInvoiceDate { get; set; }
		public string IndimanInvoiceNumber { get; set; }
		public DateTime FactoryInvoiceDate { get; set; }
		public string FactoryInvoiceNumber { get; set; }
		public string Week { get; set; }
		public string Month { get; set; }
		public DateTime ShipmentDate { get; set; }
		public string CompanyName { get; set; }
		public string PortName { get; set; }
		public string ShipmentModeName { get; set; }
		public int? BillTo { get; set; }
		public string AWBNumber { get; set; }
		public DateTime ModifiedDate { get; set; }
		public int LastModifiedBy { get; set; }
		public string StatusName { get; set; }

        #endregion
    }
}