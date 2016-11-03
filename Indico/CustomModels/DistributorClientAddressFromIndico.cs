
namespace IndicoPacking.CustomModels
{
    internal class DistributorClientAddressFromIndico
    {
        public int IndicoDistributorClientAddressId { get; set; }
        public string Address { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public int Country { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string CompanyName { get; set; }
        public string State { get; set; }
        public int? Port { get; set; }
        public string EmailAddress { get; set; }
        public int? AddressType { get; set; }
        public bool IsAdelaideWarehouse { get; set; }
        public string DistributorName { get; set; }
        public int PortId { get; set; }
    }
}
