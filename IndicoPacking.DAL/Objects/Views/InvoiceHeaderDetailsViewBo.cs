/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;

namespace IndicoPacking.DAL.Objects.Views
{
    public class InvoiceHeaderDetailsViewBo
    {
        #region Properties

		
		public int ID { get; set; }
		public DateTime? IndimanInvoiceDate { get; set; }
		public string IndimanInvoiceNumber { get; set; }
		public DateTime? FactoryInvoiceDate { get; set; }
		public string FactoryInvoiceNumber { get; set; }
		public string Week { get; set; }
		public string Month { get; set; }
		public DateTime? ShipmentDate { get; set; }
		public string CompanyName { get; set; }
		public string PortName { get; set; }
		public string ShipmentModeName { get; set; }
		public int? BillTo { get; set; }
		public string AWBNumber { get; set; }
		public string StatusName { get; set; }
		public string CompanyAddress { get; set; }
		public string CompanyPostalCode { get; set; }
		public string CompanyContact { get; set; }
		public string BillToCompanyName { get; set; }
		public string BillToAddress { get; set; }
		public string BillToCompanyState { get; set; }
		public string BillToCountry { get; set; }

        #endregion
    }
}