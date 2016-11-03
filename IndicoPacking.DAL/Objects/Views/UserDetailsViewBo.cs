namespace IndicoPacking.DAL.Objects.Views
{
    public class UserDetailsViewBo
    {
        #region Properties

		
		public int ID { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Role { get; set; }
		public string EmailAddress { get; set; }
		public string MobileTelephoneNumber { get; set; }
		public string OfficeTelephoneNumber { get; set; }
		public bool GenderMale { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? DateLastLogin { get; set; }

        #endregion
    }
}