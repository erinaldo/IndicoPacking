namespace IndicoPacking.DAL.Objects.Views
{
    public class GetShipmentKeysViewBo
    {
        #region Properties

		
		public int ID { get; set; }
		public string ShipTo { get; set; }
		public int Shipment { get; set; }
		public DateTime ETD { get; set; }
		public string Port { get; set; }
		public string ShipmentMode { get; set; }
		public int? AvailableQuantity { get; set; }

        #endregion
    }
}