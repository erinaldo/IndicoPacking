/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;

namespace IndicoPacking.DAL.Objects.Views
{
    public class GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimanBo
    {
        #region Properties

		
		public string PurchaseOrder { get; set; }
		public int IndicoOrderID { get; set; }
		public int IndicoOrderDetailID { get; set; }
		public string OrderType { get; set; }
		public string VisualLayout { get; set; }
		public string Pattern { get; set; }
		public string Fabric { get; set; }
		public string Gender { get; set; }
		public string AgeGroup { get; set; }
		public string SleeveShape { get; set; }
		public string SleeveLength { get; set; }
		public int? Qty { get; set; }
		public string Distributor { get; set; }
		public string Client { get; set; }
		public int ShipmentDeatil { get; set; }
		public int? Invoice { get; set; }
		public decimal? FactoryPrice { get; set; }
		public decimal JKFOBCostSheetPrice { get; set; }
		public decimal? IndimanPrice { get; set; }
		public decimal IndimanCIFCostSheetPrice { get; set; }
		public decimal? OtherCharges { get; set; }
		public string Notes { get; set; }
		public string ProductNotes { get; set; }
		public string PatternNotes { get; set; }

        #endregion
    }
}