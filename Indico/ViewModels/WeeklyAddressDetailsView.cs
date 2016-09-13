using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.ViewModels
{
    class WeeklyAddressDetailsView
    {
        public int Invoice { get; set; }
        public int OrderDetail { get; set; }
        public string OrderType { get; set; }
        public string VisualLayout { get; set; }
        public string Pattern { get; set; }
        public string Fabric { get; set; }
        public int Order { get; set; }
        public System.DateTime ETD { get; set; }
        public int? Qty { get; set; }
        public string PurchaseOrder { get; set; }
        public string Distributor { get; set; }
        public string Client { get; set; }      
        public string Status { get; set; }
        public string SizeDesc { get; set; }
        public decimal? TotalPrice { get; set; }
        public string ShipmentMode { get; set; }
        public string ShipTo { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string ContactDetails { get; set; }
        public string HSCode { get; set; }
        public string ItemSubGroup { get; set; }
        public string Gender { get; set; }
        public string ItemName { get; set; }
    }
}
