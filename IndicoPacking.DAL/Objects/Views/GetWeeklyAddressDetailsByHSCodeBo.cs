namespace IndicoPacking.DAL.Objects.Views
{
    public class GetWeeklyAddressDetailsByHSCodeBo
    {
        #region Properties

		
		public int ID { get; set; }
		public string HSCode { get; set; }
		public string Material { get; set; }
		public string ItemName { get; set; }
		public string Gender { get; set; }
		public string ItemSubGroup { get; set; }
		public int? Qty { get; set; }
		public decimal? TotalPrice { get; set; }

        #endregion
    }
}