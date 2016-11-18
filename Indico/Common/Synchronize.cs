using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using IndicoPacking.CustomModels;
using IndicoPacking.DAL.Base.Implementation;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.Tools;

namespace IndicoPacking.Common
{
    public class Synchronize
    {
        /// <summary>
        /// this method gets result set from GetOrderDetaildForGivenWeekView using given weekEndDate,and that week's starting data and save result data in  OrderDetailsTempTable 
        /// </summary>
        /// <param name="weekNum">week number</param>
        /// <param name="weekEndDate">ending date of the week</param>
        /// <author>rusith</author>
        public void Sync(int weekNum, DateTime weekEndDate)
        {
            var weekStartDate = weekEndDate.AddDays(-6);
            using(var con = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoPacking"].ConnectionString))
            using (var indicoConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString))
            {
                try
                {
                    var orderDetailsFormIndico = indicoConnection.Query<OrderDetailsFromIndicoModel>(string.Format("SELECT * FROM [dbo].[GetOrderDetaildForGivenWeekView] WHERE OrderDetailShipmentDate BETWEEN '{0}' AND '{1}'", weekStartDate, weekEndDate),commandTimeout:1000).ToList();
                    IndicoPackingLog.GetObject().Log("Got From Indico");
                    if (orderDetailsFormIndico.Count <= 0)
                        return;
                    
                    var id = 1;
                    var query = new StringBuilder();
                    foreach (var model in orderDetailsFormIndico)
                    {
                        var detail = model.Map();
                        for (var i = 1; i <= detail.Quantity; i++)
                        {
                            var newDetail = model.Map();
                            newDetail.ID = id;
                            newDetail.SequenceNumber = i;
                            newDetail.WeekEndDate = weekEndDate;
                            newDetail.WeekNumber = weekNum;

                            var q = FormatSqlQuery("INSERT INTO [dbo].[OrderDetailsFromIndico]" +
                                "([OrderID],[OrderDetailID],[OrderShipmentDate],[OrderDetailShipmentDate],[OrderType],[Distributor],[Client],[PurchaseOrder],[NamePrefix],[Pattern],[Fabric],[Material],[Gender],[AgeGroup],[SleeveShape],[SleeveLength],[ItemSubGroup]" +
                                ",[SizeName],[Quantity],[SequenceNumber],[Status],[PrintedCount],[PatternImagePath],[VLImagePath],[Number],[OtherCharges],[Notes],[PatternInvoiceNotes],[ProductNotes],[JKFOBCostSheetPrice],[IndimanCIFCostSheetPrice],[HSCode],[ItemName]," +
                                "[PurchaseOrderNo],[DistributorClientAddressID],[DistributorClientAddressName],[DestinationPort],[ShipmentMode],[PaymentMethod],[WeekNumber],[WeekEndDate],[JobName])" +
                                "VALUES ({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',{18},{19},'{20}'" +
                                ",{21},'{22}','{23}','{24}',{25},'{26}','{27}','{28}',{29},{30},'{31}','{32}','{33}',{34},'{35}','{36}','{37}','{38}',{39},'{40}','{41}');" +
                                Environment.NewLine, newDetail.OrderID.ToString(), newDetail.OrderDetailID.ToString(), newDetail.OrderShipmentDate.GetValueOrDefault().ToShortDateString(), newDetail.OrderDetailShipmentDate.GetValueOrDefault().ToShortDateString(),
                                newDetail.OrderType, newDetail.Distributor, newDetail.Client, newDetail.PurchaseOrder, newDetail.NamePrefix, newDetail.Pattern, newDetail.Fabric, newDetail.Material, newDetail.Gender, newDetail.AgeGroup,
                                newDetail.SleeveShape, newDetail.SleeveLength, newDetail.ItemSubGroup, newDetail.SizeName, newDetail.Quantity.GetValueOrDefault().ToString(), newDetail.SequenceNumber.ToString(), newDetail.Status,
                                newDetail.PrintedCount.ToString(), newDetail.PatternImagePath, newDetail.VLImagePath, newDetail.Number, newDetail.OtherCharges.ToString(), newDetail.Notes, newDetail.PatternInvoiceNotes, newDetail.ProductNotes,
                                newDetail.JKFOBCostSheetPrice.ToString(), newDetail.IndimanCIFCostSheetPrice.ToString(), newDetail.HSCode, newDetail.ItemName, newDetail.PurchaseOrderNo, newDetail.DistributorClientAddressID.ToString(),
                                newDetail.DistributorClientAddressName, newDetail.DestinationPort, newDetail.ShipmentMode, newDetail.PaymentMethod, newDetail.WeekNumber.ToString(), newDetail.WeekEndDate.ToString(), "");

                            query.Append(q);

                            //indicoPackingContext.OrderDetailsFromIndicoes.Add(newDetail);
                            id++;
                        }
                    }
                    try
                    {
                        con.Execute(query.ToString(), commandTimeout:1000);
                        IndicoPackingLog.GetObject().Log("Execute query");
                        con.Execute("EXEC [dbo].[SPC_SynchronizeOrderDetails]");

                        IndicoPackingLog.GetObject().Log("Synchronize order details");
                    }
                    finally
                    {
                        con.Execute("DELETE [dbo].[OrderDetailsFromIndico]");
                    }
                }
                catch (Exception e)
                {
                    IndicoPackingLog.GetObject().Log(e,"Unable to sync --");
                    indicoConnection.Close();

                    throw new Exception("cannot retrieve data from the database", e);
                }
            }
        }

        public static void SynchronizePorts()
        {
            var indicoConnection = ConnectionManager.IndicoConnection;
            var ports = indicoConnection.Query("SELECT * FROM [dbo].[DestinationPort]").ToList();
            indicoConnection.Close();
            if(ports.Count<1)
                return;
            using (var unit = new UnitOfWork())
            {
                foreach (var p in ports)
                {
                    var relatedPorts = unit.PortRepository.Where(new {IndicoPortId = p.ID});
                    if (relatedPorts.Count < 1)
                    {
                        var newPort = new PortBo {Description = p.Description, IndicoPortId = p.ID, Name = p.Name};
                        unit.PortRepository.Add(newPort);
                    }
                    else
                    {
                        var rp = relatedPorts.First();
                        if (rp.Name != p.Name)
                            rp.Name = p.Name;
                        if (rp.Description != p.Description)
                            rp.Description = p.Description;
                        if (relatedPorts.Count <= 0)
                            continue;
                        foreach (var tr in relatedPorts.Skip(1))
                            unit.PortRepository.Delete(tr);
                    }
                }
                unit.Complete();
            }
        }

        public static void SynchronizeDistributorClientAddresses()
        {
            using (var indicoConnection = ConnectionManager.IndicoConnection)
            {
                SynchronizePorts();

                var distributorClientAddressesFromIndico = indicoConnection.Query<DistributorClientAddressFromIndico>(@"
                                                                                        SELECT dca.ID AS IndicoDistributorClientAddressId
		                                                                                    ,dca.[Address]
		                                                                                    ,dca.[Suburb]
		                                                                                    ,dca.[PostCode]
		                                                                                    ,dca.[Country]
		                                                                                    ,dca.[ContactName]
		                                                                                    ,dca.[ContactPhone]
		                                                                                    ,dca.[CompanyName]
		                                                                                    ,dca.[State]
		                                                                                    ,dca.[Port]
		                                                                                    ,dca.[EmailAddress]
		                                                                                    ,dca.[AddressType]
		                                                                                    ,dca.[IsAdelaideWarehouse]
		                                                                                    ,d.Name AS DistributorName
	                                                                                    FROM [dbo].[DistributorClientAddress] dca
		                                                                                    INNER JOIN [dbo].[Company] d
			                                                                                    ON dca.Distributor = d.ID
	                                                                                    WHERE dca.[Address] != 'tba'").ToList();
                if (distributorClientAddressesFromIndico.Count <= 0)
                    return;
                using (var unit = new UnitOfWork())
                {
                    var localAddresses = unit.DistributorClientAddressRepository.Get().ToList();
                    foreach (var indicoAddress in distributorClientAddressesFromIndico)
                    {
                        var id = indicoAddress.IndicoDistributorClientAddressId;
                        var addresses = localAddresses.Where(la => la.IndicoDistributorClientAddressId == id).ToList();
                        if (addresses.Count < 1)
                        {
                            var port = 0;
                            if (indicoAddress.Port != null && indicoAddress.Port > 0)
                            {
                                var p = unit.PortRepository.Where(new {IndicoPortId = indicoAddress.Port}).First();
                                port = p.ID;
                            }
                           
                            var newAddress = new DistributorClientAddressBo
                            {
                                Address = indicoAddress.Address,
                                AddressType = indicoAddress.AddressType,
                                CompanyName = indicoAddress.CompanyName,
                                ContactName = indicoAddress.ContactName,
                                ContactPhone = indicoAddress.ContactPhone,
                                Country = indicoAddress.Country,
                                EmailAddress = indicoAddress.EmailAddress,
                                IndicoDistributorClientAddressId = indicoAddress.IndicoDistributorClientAddressId,
                                IsAdelaideWarehouse = indicoAddress.IsAdelaideWarehouse,
                                PostCode = indicoAddress.PostCode,
                                State = indicoAddress.State,
                                DistributorName = indicoAddress.DistributorName,
                                Suburb = indicoAddress.Suburb,
                                Port = port==0?null : port as int?
                            };
                            unit.DistributorClientAddressRepository.Add(newAddress);
                            continue;
                        }

                        var localAddress = localAddresses.First();

                        if (indicoAddress.Address != localAddress.Address)
                            localAddress.Address = indicoAddress.Address;

                        if (indicoAddress.IsAdelaideWarehouse != localAddress.IsAdelaideWarehouse)
                            localAddress.IsAdelaideWarehouse = indicoAddress.IsAdelaideWarehouse;

                        if (indicoAddress.AddressType != localAddress.AddressType)
                            localAddress.AddressType = indicoAddress.AddressType;

                        if (indicoAddress.CompanyName != localAddress.CompanyName)
                            localAddress.CompanyName = indicoAddress.CompanyName;

                        if (indicoAddress.ContactName != localAddress.ContactName)
                            localAddress.ContactName = indicoAddress.ContactName;

                        if (indicoAddress.ContactPhone != localAddress.ContactPhone)
                            localAddress.ContactPhone = indicoAddress.ContactPhone;

                        if (indicoAddress.Country != localAddress.Country)
                            localAddress.Country = indicoAddress.Country;

                        if (indicoAddress.DistributorName != localAddress.DistributorName)
                            localAddress.DistributorName = indicoAddress.DistributorName;

                        if (indicoAddress.EmailAddress != localAddress.EmailAddress)
                            localAddress.EmailAddress = indicoAddress.EmailAddress;

                        int? por = 0;
                        if (localAddress.ObjPort != null)
                            por = localAddress.ObjPort.IndicoPortId;
                        if (por != null && por > 0 && indicoAddress.Port != por && indicoAddress.Port != null && indicoAddress.Port > 0)
                        {
                            var p = unit.PortRepository.Where(new { IndicoPortId = indicoAddress.Port }).FirstOrDefault();
                            if (p != null)
                                localAddress.Port = p.ID;
                            else localAddress.Port = null;
                        }


                        if (indicoAddress.PostCode != localAddress.PostCode)
                            localAddress.PostCode = indicoAddress.PostCode;
                        if (indicoAddress.State != localAddress.State)
                            localAddress.State = indicoAddress.State;
                        if (indicoAddress.Suburb != localAddress.Suburb)
                            localAddress.Suburb = indicoAddress.Suburb;

                        if (localAddresses.Count > 1)
                        {
                            var td = localAddresses.Skip(1);
                            foreach (var t in td)
                                unit.DistributorClientAddressRepository.Delete(t);
                        }
                    }
                    unit.Complete();
                }
            }
        }

        private static string FormatSqlQuery(string text, params string[] parameters)
        {
            var p = new List<object>();
            if (parameters.Length <= 0)
                return string.Format(text, p.ToArray());
            p.AddRange(parameters.Select(para => !string.IsNullOrWhiteSpace(para) ? para.Replace("'", "''") : para));
            return string.Format(text, p.ToArray());
        }
    }

}
