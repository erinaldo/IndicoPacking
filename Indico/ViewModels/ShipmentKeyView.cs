using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.ViewModels
{
    public class ShipmentKeyView
    {
        public int ID { get; set; }
        public string ShipTo { get; set; }
        public int Shipment { get; set; }
        public DateTime ETD { get; set; }
        public string Port { get; set; }
        public string ShipmentMode { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? Qty { get; set; }
    }
}
