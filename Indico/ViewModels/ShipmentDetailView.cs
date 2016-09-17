using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.ViewModels
{
    class ShipmentDetailView
    {
        public int ID { get; set; }
        public int Shipment { get; set; }
        public int IndicoDistributorClientAddress { get; set; }
        public string ShipTo { get; set; }
        public string Port { get; set; }
        public string ShipmentMode { get; set; }
        public string PriceTerm { get; set; }
        public System.DateTime ETD { get; set; }
        public int Qty { get; set; }
        public int QuantityFilled { get; set; }
        public int QuantityYetToBeFilled { get; set; }
    }
}
