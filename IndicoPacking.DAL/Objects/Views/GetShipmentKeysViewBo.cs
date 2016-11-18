/*This code is a generated one , Change the source code of the generator if you want some change in this code
You can find the source code of the code generator from here -> https://github.com/rusith/MyCodeGenerator*/

using System;

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