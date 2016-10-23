using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using IndicoPacking.CustomModels;
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
                var qu = "";
                try
                {
                    var indicoPackingContext = new IndicoPackingEntities();
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
                        qu = query.ToString();
                        con.Execute(qu,commandTimeout:1000);
                        IndicoPackingLog.GetObject().Log("Execute query");

                        con.Execute("EXEC [dbo].[SPC_SynchronizeOrderDetails]");
                        //indicoPackingContext.SaveChanges();
                        //indicoPackingContext.SynchronizeOrderDetails();

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
