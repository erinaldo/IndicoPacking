using System;
using IndicoPacking.Model;

namespace IndicoPacking.CustomModels
{
    public class OrderDetailsFromIndicoModel
    {
        #region Properties

        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public DateTime? OrderShipmentDate { get; set; }
        public DateTime OrderDetailShipmentDate { get; set; }
        public string OrderType { get; set; }
        public string Distributor { get; set; }
        public string JobName { get; set; }
        public string Client { get; set; }
        public string PurchaseOrder { get; set; }
        public string NamePrefix { get; set; }
        public string Pattern { get; set; }
        public string Fabric { get; set; }
        public string Material { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public string SleeveShape { get; set; }
        public string SleeveLength { get; set; }
        public string ItemSubGroup { get; set; }
        public string SizeName { get; set; }
        public int Quentity { get; set; }
        public string Status { get; set; }
        public int PrintedCount { get; set; }
        public string PatternImagePath { get; set; }
        public string VLImagePath { get; set; }
        public string Number { get; set; }
        public decimal OtherCharges { get; set; }
        public string Notes { get; set; }
        public string PatternInvoiceNotes { get; set; }
        public string ProductNotes { get; set; }
        public decimal? JKFOBCostSheetPrice { get; set; }
        public decimal? IndimanCIFCostSheetPrice { get; set; }
        public string HSCode { get; set; }
        public string ItemName { get; set; }
        public string PurchaseOrderNo { get; set; }
        public int DistributorClientAddressID { get; set; }
        public string DistributorClientAddressName { get; set; }
        public string DestinationPort { get; set; }
        public string ShipmentMode { get; set; }
        public string PaymentMethod { get; set; }


        #endregion

        public OrderDetailsFromIndico Map()
        {
            return new OrderDetailsFromIndico
            {
                OrderID = OrderId,
                OrderDetailID = OrderDetailId,
                OrderShipmentDate = OrderShipmentDate,
                OrderDetailShipmentDate = OrderDetailShipmentDate,
                OrderType = OrderType,
                Distributor = Distributor,
                Client = Client,
                PurchaseOrder = PurchaseOrder,
                NamePrefix = NamePrefix,
                Pattern = Pattern,
                Fabric = Fabric,
                Material = Material,
                Gender = Gender,
                AgeGroup = AgeGroup,
                SleeveShape = SleeveShape,
                SleeveLength = SleeveLength,
                ItemSubGroup = ItemSubGroup,
                SizeName = SizeName,
                Quantity = Quentity,
                Status = Status,
                PrintedCount = PrintedCount,
                PatternImagePath = PatternImagePath,
                VLImagePath = VLImagePath,
                Number = Number,
                OtherCharges = OtherCharges,
                Notes = Notes,
                PatternInvoiceNotes = PatternInvoiceNotes,
                JKFOBCostSheetPrice = JKFOBCostSheetPrice,
                IndimanCIFCostSheetPrice = IndimanCIFCostSheetPrice,
                HSCode = HSCode,
                ItemName = ItemName,
                PurchaseOrderNo = PurchaseOrderNo,
                DistributorClientAddressID = DistributorClientAddressID,
                DistributorClientAddressName = DistributorClientAddressName,
                DestinationPort = DestinationPort,
                ShipmentMode = ShipmentMode,
                PaymentMethod = PaymentMethod
            };
        }
    }
}
