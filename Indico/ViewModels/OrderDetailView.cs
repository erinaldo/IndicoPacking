using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndicoPacking.ViewModels
{
    class OrderDetailView : IConvertible
    {
        public OrderDetailView()
        {

        }

        public OrderDetailView(int ID, string PurchaseOrder, int IndicoOrderID, int IndicoOrderDetailID, string OrderType, string VisualLayout, string Distributor, string Client, string Pattern, string Fabric, string Gender, string AgeGroup, string SleeveShape, string SleeveLength, string SizeDesc, int SizeQty, int SizeSrno, decimal FactoryPrice, decimal JKFOBCostSheetPrice, decimal IndimanPrice, decimal OtherCharges, string Notes, decimal IndimanCIFCostSheetPrice)
        {
            this.ID = ID;
            this.PurchaseOrder = PurchaseOrder;
            this.IndicoOrderID = IndicoOrderID;
            this.IndicoOrderDetailID = IndicoOrderDetailID;
            this.OrderType = OrderType;
            this.VisualLayout = VisualLayout;
            this.Distributor = Distributor;
            this.Client = Client;
            this.Pattern = Pattern;
            this.Fabric = Fabric;
            this.AgeGroup = AgeGroup;
            this.SleeveShape = SleeveShape;
            this.SleeveLength = SleeveLength;
            this.SizeDesc = SizeDesc;
            this.SizeQty = SizeQty;
            this.SizeSrno = SizeSrno;
            this.FactoryPrice = FactoryPrice;
            this.JKFOBCostSheetPrice = JKFOBCostSheetPrice;
            this.IndimanPrice = IndimanPrice;
            this.OtherCharges = OtherCharges;
            this.Notes = Notes;
            this.IndimanCIFCostSheetPrice = IndimanCIFCostSheetPrice;
        }

        public int ID { get; set; }
        public string PurchaseOrder { get; set; }
        public int IndicoOrderID { get; set; }
        public int IndicoOrderDetailID { get; set; }
        public string OrderType { get; set; }
        public string VisualLayout { get; set; }
        public string Distributor { get; set; }
        public string Client { get; set; }
        public string Pattern { get; set; }
        public string Fabric { get; set; }
        public string Gender { get; set; }
        public string AgeGroup { get; set; }
        public string SleeveShape { get; set; }
        public string SleeveLength { get; set; }
        public string SizeDesc { get; set; }
        public int? SizeQty { get; set; }
        public int? SizeSrno { get; set; }
        public decimal? FactoryPrice { get; set; }
        public decimal? JKFOBCostSheetPrice { get; set; }
        public decimal? IndimanPrice { get; set; }
        public decimal? OtherCharges { get; set; }
        public string Notes { get; set; }
        public decimal? IndimanCIFCostSheetPrice { get; set; }

        public TypeCode GetTypeCode()
        {
            return new TypeCode();;
           // throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return false;
           // throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            return 0;
           // throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            return 'a';
           // throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return DateTime.Now;
           // throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return 0;
           // throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            return 0.0;
           // throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return 0;
           // throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Empty;
           // throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return null;
            //throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return 0;
            //throw new NotImplementedException();
        }
    }
}
