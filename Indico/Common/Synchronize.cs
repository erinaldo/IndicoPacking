using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using IndicoPacking.CustomModels;

namespace IndicoPacking.Common
{
    public class Synchronize
    {
        private string FormatSqlQuery(string text, params string[] parameters)
        {
            var p = new List<object>();
            if (parameters.Length > 0)
            {
                foreach (var para in parameters)
                {
                    if (!string.IsNullOrWhiteSpace(para))
                    {
                        p.Add(para.Replace("'", "''"));
                    }
                    else
                    {
                        p.Add(para);
                    }
                }
            }

            return string.Format(text, p.ToArray());
        }

        public int shipmentId = 0;
        public int shipmentDetailId = 0;
        public int TotalQty = 0;
        IndicoPackingEntities context = null;

        /// <summary>
        /// this method gets result set from GetOrderDetaildForGivenWeekView using given weekEndDate,and that week's starting data and save result data in  OrderDetailsTempTable 
        /// </summary>
        /// <param name="weekNum">week number</param>
        /// <param name="weekEndDate">ending date of the week</param>
        /// <author>rusith</author>
        public void Sync(int weekNum, DateTime weekEndDate)
        {
            var weekStartDate = weekEndDate.AddDays(-6);
            using (var indicoConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString))
            {
                try
                {
                    var indicoPackingContext = new IndicoPackingEntities();
                    var orderDetailsFormIndico = indicoConnection.Query<OrderDetailsFromIndicoModel>(string.Format("SELECT * FROM [dbo].[GetOrderDetaildForGivenWeekView] WHERE OrderDetailShipmentDate BETWEEN '{0}' AND '{1}'", weekStartDate, weekEndDate)).ToList();
                    if (orderDetailsFormIndico.Count <= 0)
                        return;
                    var id = 1;
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
                            indicoPackingContext.OrderDetailsFromIndicoes.Add(newDetail);
                            id++;
                        }
                        
                    }
                    indicoPackingContext.SaveChanges();
                    indicoPackingContext.SynchronizeOrderDetails();
                }
                catch (Exception e)
                {
                    indicoConnection.Close();
                    throw new Exception("cannot retrieve data from the database", e);
                }
            }

            
            //using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString))
            //{
            //    conn.Open();
            //    try
            //    {
            //        var indicoPackingContext = new IndicoPackingEntities();

            //        var command = new SqlCommand(string.Format("SELECT * from [dbo].[GetOrderDetaildForGivenWeekView] WHERE OrderDetailShipmentDate BETWEEN '{0}' AND '{1}'", weekStartDate, weekEndDate), conn);
            //        var dataReader = command.ExecuteReader();
            //        var i = 1;
            //        while (dataReader.HasRows && dataReader.Read())
            //        {
            //            for (var d = 1; d <= Int32.Parse(dataReader["Quentity"].ToString()); d++)
            //            {
            //                var detail = new OrderDetailsFromIndico
            //                {
            //                    ID = i,
            //                    SequenceNumber = d,
            //                    OrderID = Int32.Parse(dataReader["OrderId"].ToString()),
            //                    OrderDetailID = Int32.Parse(dataReader["OrderDetailId"].ToString()),
            //                    OrderShipmentDate = DateTime.Parse(dataReader["OrderDetailShipmentDate"].ToString()),
            //                    OrderDetailShipmentDate = DateTime.Parse(dataReader["OrderDetailShipmentDate"].ToString()),
            //                    OrderType = dataReader["OrderType"].ToString(),
            //                    Distributor = dataReader["Distributor"].ToString(),
            //                    Client = dataReader["Client"].ToString(),
            //                    PurchaseOrder = dataReader["PurchaseOrder"].ToString(),
            //                    NamePrefix = dataReader["NamePrefix"].ToString(),
            //                    Pattern = dataReader["Pattern"].ToString(),
            //                    Fabric = dataReader["Fabric"].ToString(),
            //                    Material = dataReader["Material"].ToString(),
            //                    Gender = dataReader["Gender"].ToString(),
            //                    AgeGroup = dataReader["AgeGroup"].ToString(),
            //                    SleeveShape = dataReader["SleeveShape"].ToString(),
            //                    SleeveLength = dataReader["SleeveLength"].ToString(),
            //                    ItemSubGroup = dataReader["ItemSubGroup"].ToString(),
            //                    SizeName = dataReader["SizeName"].ToString(),
            //                    Quantity = Int32.Parse(dataReader["Quentity"].ToString()),
            //                    Status = dataReader["Status"].ToString(),
            //                    PrintedCount = Int32.Parse(dataReader["PrintedCount"].ToString()),
            //                    PatternImagePath = dataReader["PatternImagePath"].ToString(),
            //                    VLImagePath = dataReader["VLImagePath"].ToString(),
            //                    Number = dataReader["Number"].ToString(),
            //                    OtherCharges = decimal.Parse(dataReader["OtherCharges"].ToString()),
            //                    Notes = dataReader["Notes"].ToString(),
            //                    PatternInvoiceNotes = dataReader["PatternInvoiceNotes"].ToString(),
            //                    JKFOBCostSheetPrice = decimal.Parse(dataReader["JKFOBCostSheetPrice"].ToString()),
            //                    IndimanCIFCostSheetPrice = decimal.Parse(dataReader["IndimanCIFCostSheetPrice"].ToString()),
            //                    HSCode = dataReader["HSCode"].ToString(),
            //                    ItemName = dataReader["ItemName"].ToString(),
            //                    PurchaseOrderNo = dataReader["PurchaseOrderNo"].ToString(),
            //                    DistributorClientAddressID = Int32.Parse(dataReader["DistributorClientAddressID"].ToString()),
            //                    DistributorClientAddressName = dataReader["DistributorClientAddressName"].ToString(),
            //                    DestinationPort = dataReader["DestinationPort"].ToString(),
            //                    ShipmentMode = dataReader["ShipmentMode"].ToString(),
            //                    PaymentMethod = dataReader["PaymentMethod"].ToString(),
            //                    WeekEndDate = weekEndDate,
            //                    WeekNumber = weekNum
            //                };
            //                indicoPackingContext.OrderDetailsFromIndicoes.Add(detail);
            //                i++;
            //            }
            //        }
            //        indicoPackingContext.SaveChanges();
            //        indicoPackingContext.SynchronizeOrderDetails(); //procedure
            //    }
            //    catch (Exception ex)
            //    {
            //        conn.Close();
            //        var exception = new Exception("cannot retrieve data from the database", ex);
            //        throw exception;
            //    }
               
            //}
        }

        //public void Sync(int weekNo, DateTime weekEndDate)
        //{
        //    SqlConnection indicoCon = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString);
        //    context = new IndicoPackingEntities();
        //    List<ShipmentDetailTemp> shipmentDetailTemp = new List<ShipmentDetailTemp>();

        //    indicoCon.Open();

        //    shipmentId = (from s in context.Shipments
        //                  where s.WeekNo == weekNo && s.WeekendDate == weekEndDate
        //                  select s.ID).FirstOrDefault();

        //    // If shipment record does not exist add one.
        //    if (shipmentId == 0)
        //    {
        //        string weeklyProductionCapacity = "SELECT TOP 1 ID " +
        //                                          "FROM WeeklyProductionCapacity " +
        //                                          "WHERE WeekNo = " + weekNo + " AND WeekendDate = '" + weekEndDate + "' ";

        //        SqlCommand cmd = new SqlCommand(weeklyProductionCapacity, indicoCon);
        //        SqlDataReader rd = cmd.ExecuteReader();

        //        Shipment shipment = new Shipment();
        //        shipment.WeekNo = weekNo;
        //        shipment.WeekendDate = weekEndDate;
        //        while (rd.Read())
        //        {
        //            shipment.IndicoWeeklyProductionCapacityID = int.Parse(rd["ID"].ToString());
        //            context.Shipments.Add(shipment);
        //            context.SaveChanges();
        //            shipmentId = shipment.ID;
        //        }
        //        rd.Close();
        //    }

        //    // Add Shipment details reordered if does not exist...
        //    string shipmentDetailRec = @"SELECT dca.ID AS DistributorClientAddressId, 
        //                                    dca.CompanyName, 
        //                                    COALESCE(dp.Name, '') AS DestinationPort, 
        //                                    COALESCE(sm.Name, '') AS ShipmentMode, 
        //                                    COALESCE(pm.Name,'') AS PaymentMethod, 
        //                                    od.ShipmentDate AS ETD,
        //                                    0 AS Qty, 0 AS QuantityFilled, 
        //                                    0 AS QuantityYetToBeFilled, '' AS InvoiceNo 
        //                                FROM OrderDetail od 
        //                                    INNER JOIN DistributorClientAddress dca  
        //                                        ON od.[DespatchTo] = dca.[ID] 
        //                                    LEFT OUTER JOIN [DestinationPort] dp 
        //                                        ON dca.[Port] = dp.[ID] 
        //                                    LEFT OUTER JOIN [ShipmentMode] sm 
        //                                        ON od.[ShipmentMode] = sm.[ID]	
        //                                    LEFT OUTER JOIN [PaymentMethod] pm 
        //                                        ON od.[PaymentMethod] = pm.[ID] 
        //                                        WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, '" + weekEndDate + "')) AND CAST('" + weekEndDate + "' as DATE)) " +
        //                                  @"GROUP BY dca.ID,
        //                                             dca.CompanyName,
        //                                             dp.Name,
        //                                             sm.Name,
        //                                             pm.Name,
        //                                             od.ShipmentDate";

        //    SqlCommand cmdSD = new SqlCommand(shipmentDetailRec, indicoCon);
        //    SqlDataReader rdrSD = cmdSD.ExecuteReader();

        //    while (rdrSD.Read())
        //    {
        //        var DCA = int.Parse(rdrSD["DistributorClientAddressId"].ToString());
        //        var shipmentMode = rdrSD["ShipmentMode"].ToString();
        //        var priceTerm = rdrSD["PaymentMethod"].ToString();
        //        var ETD = DateTime.Parse(rdrSD["ETD"].ToString());

        //        shipmentDetailId = (from sd in context.ShipmentDetails
        //                            where sd.Shipment == shipmentId && sd.IndicoDistributorClientAddress == DCA
        //                            && sd.ShipmentMode == shipmentMode && sd.PriceTerm == priceTerm && sd.ETD == ETD
        //                            select sd.ID).FirstOrDefault();

        //        // If shipmentDetail record doesn't exists.
        //        if (shipmentDetailId == 0)
        //        {
        //            ShipmentDetail shipmentDetail = new ShipmentDetail();
        //            shipmentDetail.Shipment = shipmentId;
        //            shipmentDetail.IndicoDistributorClientAddress = int.Parse(rdrSD["DistributorClientAddressId"].ToString());
        //            shipmentDetail.ShipTo = rdrSD["CompanyName"].ToString();
        //            shipmentDetail.Port = rdrSD["DestinationPort"].ToString();
        //            shipmentDetail.ShipmentMode = rdrSD["ShipmentMode"].ToString();
        //            shipmentDetail.PriceTerm = rdrSD["PaymentMethod"].ToString();
        //            shipmentDetail.ETD = DateTime.Parse(rdrSD["ETD"].ToString());
        //            shipmentDetail.Qty = int.Parse(rdrSD["Qty"].ToString());
        //            shipmentDetail.QuantityFilled = int.Parse(rdrSD["QuantityFilled"].ToString());
        //            shipmentDetail.QuantityYetToBeFilled = int.Parse(rdrSD["QuantityYetToBeFilled"].ToString());
        //            shipmentDetail.InvoiceNo = rdrSD["InvoiceNo"].ToString();

        //            shipmentDetailTemp.Add(new ShipmentDetailTemp()
        //            {
        //                ID = shipmentDetailId,
        //                Shipment = shipmentId,
        //                IndicoDistributorClientAddress = int.Parse(rdrSD["DistributorClientAddressId"].ToString()),
        //                ShipTo = rdrSD["CompanyName"].ToString(),
        //                Port = rdrSD["DestinationPort"].ToString(),
        //                ShipmentMode = rdrSD["ShipmentMode"].ToString(),
        //                PriceTerm = rdrSD["PaymentMethod"].ToString(),
        //                ETD = DateTime.Parse(rdrSD["ETD"].ToString()),
        //                Qty = int.Parse(rdrSD["Qty"].ToString())
        //            });

        //            context = new IndicoPackingEntities();
        //            context.ShipmentDetails.Add(shipmentDetail);
        //            context.SaveChanges();
        //        }
        //    }
        //    rdrSD.Close();

        //    // ShipmentDetail records
        //    var shipmentDetails = (from sd in context.ShipmentDetails
        //                           where sd.Shipment == shipmentId
        //                           select sd).ToList();

        //    // Get the orderdetails from the Indico for given week
        //    string orderDeatils = @"SELECT  o.ID AS OrderID,
        //                            od.ID AS OrderDeatilID,
        //                            s.SizeName,
        //                            odq.Qty 
        //                        FROM [Indico].[dbo].[OrderDetail] od 
        //                            INNER JOIN [Indico].[dbo].[OrderDetailQty] odq 
        //                                ON od.[ID] = odq.[OrderDetail] 
        //                            INNER JOIN [Indico].[dbo].[Order] o 
        //                                ON od.[Order] = o.ID 
        //                            INNER JOIN [Indico].[dbo].[Size] s 
        //                                ON odq.[Size] = s.[ID] 
        //                       WHERE (od.[ShipmentDate] BETWEEN DATEADD(DAY, -6, CONVERT(DATE, '" + weekEndDate + "')) AND CAST('" + weekEndDate + "' AS DATE)) " + @"AND odq.Qty != 0";

        //    SqlCommand cmdOrderDetails = new SqlCommand(orderDeatils, indicoCon);
        //    SqlDataReader rdrOrderDetails = cmdOrderDetails.ExecuteReader();

        //    int orderId = 0;
        //    int orderDetailId = 0;
        //    string sizeName = string.Empty;
        //    int qty = 0;
        //    string orderDetailSQLString = string.Empty;

        //    context = new IndicoPackingEntities();
        //    //foreach (ShipmentDetailTemp sdTemp in shipmentDetailTemp)

        //    while (rdrOrderDetails.Read())
        //    {
        //        foreach (ShipmentDetail sdTemp in context.ShipmentDetails)
        //        {
        //            orderId = int.Parse(rdrOrderDetails["OrderID"].ToString());
        //            orderDetailId = int.Parse(rdrOrderDetails["OrderDeatilID"].ToString());
        //            sizeName = rdrOrderDetails["SizeName"].ToString();
        //            qty = int.Parse(rdrOrderDetails["Qty"].ToString());

        //            orderDetailSQLString = @"SELECT o.ID AS OrderId,
        //                                    od.ID AS OrderDetailId,
        //                                    ot.[Name] AS OrderType,
        //                                    dis.[Name] AS Distributor,
        //                                    c.[Name] AS Client,
        //                                    'PO-' + CAST(o.ID AS nvarchar(47)) AS PurchaseOrder,
        //                                    vl.[NamePrefix],
        //                                    o.[ID] AS PurchaseOrder1,
        //                                    p.[Number] + ' - ' + p.[NickName] AS Pattern,
        //                                    fc.[Code] + ' - ' + fc.[NickName] AS Fabric,
        //                                    fc.[Filaments] AS Material,
        //                                    g.Name AS Gender,
        //                                    ag.Name AS AgeGroup,
        //                                    '' AS SleeveShape,
        //                                    '' AS SleeveLength,
        //                                    COALESCE(i.[Name], '') AS ItemSubGroup,
        //                                    os.Name AS [Status],
        //                                    COALESCE(
        //                                        (SELECT TOP 1 
        //                                            'http://gw.indiman.net/IndicoData/PatternTemplates/' + CAST(pti.Pattern AS nvarchar(8)) + '/' + pti.[Filename] + pti.Extension
        //                                        FROM [Indico].[dbo].[PatternTemplateImage] pti WHERE p.ID = pti.Pattern AND pti.IsHero = 1
        //                                        ), '' 
        //                                    ) AS PatternImagePath,
        //                                    COALESCE(
        //                                        (SELECT TOP 1 
        //                                            'http://gw.indiman.net/IndicoData/VisualLayout/' + CAST(vl.ID AS nvarchar(8)) + '/' + im.[Filename] + im.Extension
        //                                        FROM [Indico].[dbo].[Image] im WHERE vl.ID = im.VisualLayout AND im.IsHero = 1
        //                                        ), '' 
        //                                    ) AS VLImagePath,
        //                                    p.[Number],
        //                                    COALESCE(cs.QuotedFOBCost, 0.0) AS JKFOBCostSheetPrice,
        //                                    COALESCE(cs.QuotedCIF, 0.0) AS IndimanCIFCostSheetPrice,
        //                                    ISNULL(CAST((SELECT CASE
        //                                                        WHEN (p.[SubItem] IS NULL)
        //                                                            THEN  	('')
        //                                                        ELSE (CAST((SELECT TOP 1 hsc.[Code] FROM [Indico].[dbo].[HSCode] hsc WHERE hsc.[ItemSubCategory] = p.[SubItem] AND hsc.[Gender] = p.[Gender]) AS nvarchar(64)))
        //                                                END) AS nvarchar (64)), '') AS HSCode,
        //                                    ISNULL(CAST((SELECT CASE
        //                                                        WHEN (p.[SubItem] IS NULL)
        //                                                            THEN  	('')
        //                                                        ELSE (CAST((SELECT it.[Name] FROM [Indico].[dbo].[Item] it WHERE it.[ID] = i.[Parent]) AS nvarchar(64)))
        //                                                END) AS nvarchar (64)), '') AS ItemName,
        //                                    o.PurchaseOrderNo 
        //                            FROM [Indico].[dbo].[OrderDetail] od 
        //                                INNER JOIN [Indico].[dbo].[Order] o 
        //                                    ON od.[Order] = o.ID 
        //                                INNER JOIN [Indico].[dbo].[OrderStatus] os 
        //                                    ON o.[Status] = os.ID 
        //                                INNER JOIN [Indico].[dbo].[Company] dis 
        //                                    ON dis.[ID] = o.[Distributor]		 
        //                                INNER JOIN [Indico].[dbo].[OrderType] ot 
        //                                    ON od.[OrderType] = ot.[ID] 
        //                                INNER JOIN [Indico].[dbo].[VisualLayout] vl 
        //                                    ON od.[VisualLayout] = vl.ID	 
        //                                INNER JOIN [Indico].[dbo].[Pattern] p 
        //                                    ON od.Pattern = p.ID 
        //                                INNER JOIN [Indico].[dbo].[FabricCode] fc 
        //                                    ON od.FabricCode = fc.ID 
        //                                LEFT OUTER JOIN [Indico].[dbo].[Gender] g 
        //                                    ON p.Gender = g.ID 
        //                                LEFT OUTER JOIN [Indico].[dbo].[AgeGroup] ag 
        //                                    ON p.AgeGroup = ag.ID 
        //                                LEFT OUTER JOIN [Indico].[dbo].[Item] i 
        //                                    ON p.[SubItem] = i.ID 
        //                                 INNER JOIN [Indico].[dbo].[JobName] j 
        //                                     ON o.[Client] = j.ID
        //                                INNER JOIN [Indico].[dbo].Client c
        //                                    ON j.Client = c.ID  
        //                                LEFT OUTER JOIN [Indico].[dbo].[ShipmentMode] sm 
        //                                    ON sm.[ID] = od.ShipmentMode 
        //                                LEFT OUTER JOIN [Indico].[dbo].[PaymentMethod] pm 
        //                                    ON pm.[ID] = od.PaymentMethod	 
        //                                LEFT OUTER JOIN [Indico].[dbo].[CostSheet] cs 	
        //                                    ON p.ID = cs.Pattern 
        //                                        AND fc.ID = cs.Fabric 
        //                                WHERE  od.ID = '" + orderDetailId + "' AND od.[Order] = '" + orderId + "' AND od.DespatchTo = '" + sdTemp.IndicoDistributorClientAddress + "' AND sm.Name = '" + sdTemp.ShipmentMode + "' AND pm.Name = '" + sdTemp.PriceTerm + "' AND od.[ShipmentDate] = '" + sdTemp.ETD + "'" +
        //                                    @"ORDER BY od.ID, od.[Order]";

        //            OrderDetailBreakdown breakdown = new OrderDetailBreakdown();

        //            SqlCommand cmdOD = new SqlCommand(orderDetailSQLString, indicoCon);
        //            SqlDataReader odRdr = cmdOD.ExecuteReader();

        //            while (odRdr.Read() && odRdr.HasRows)
        //            {
        //                breakdown.AgeGroup = odRdr["AgeGroup"].ToString();
        //                breakdown.Client = odRdr["Client"].ToString();
        //                breakdown.Distributor = odRdr["Distributor"].ToString();
        //                breakdown.Fabric = odRdr["Fabric"].ToString();
        //                breakdown.Gender = odRdr["Gender"].ToString();
        //                breakdown.HSCode = odRdr["HSCode"].ToString();
        //                breakdown.IndimanCIFCostSheetPrice = decimal.Parse(odRdr["IndimanCIFCostSheetPrice"].ToString());
        //                breakdown.ItemName = odRdr["ItemName"].ToString();
        //                breakdown.ItemSubGroup = odRdr["ItemSubGroup"].ToString();
        //                breakdown.JKFOBCostSheetPrice = decimal.Parse(odRdr["JKFOBCostSheetPrice"].ToString());
        //                breakdown.Material = odRdr["Material"].ToString();
        //                breakdown.NamePrefix = odRdr["NamePrefix"].ToString();
        //                breakdown.Number = odRdr["Number"].ToString();
        //                breakdown.OrderDeatilID = int.Parse(odRdr["OrderDetailId"].ToString());
        //                breakdown.OrderID = int.Parse(odRdr["OrderId"].ToString());
        //                breakdown.OrderType = odRdr["OrderType"].ToString();
        //                breakdown.Pattern = odRdr["Pattern"].ToString();
        //                breakdown.PatternImagePath = odRdr["PatternImagePath"].ToString();
        //                breakdown.PurchaseOrder = odRdr["PurchaseOrder"].ToString();
        //                breakdown.PurchaseOrderNo = odRdr["PurchaseOrder1"].ToString();
        //                breakdown.SleeveLength = odRdr["SleeveLength"].ToString();
        //                breakdown.SleeveShape = odRdr["SleeveShape"].ToString();
        //                breakdown.Status = odRdr["Status"].ToString();
        //                breakdown.VLImagePath = odRdr["VLImagePath"].ToString();
        //            }

        //            if (odRdr.HasRows)
        //            {
        //                context = new IndicoPackingEntities();

        //                for (int i = 1; i <= qty; i++)
        //                {
        //                    if (context.OrderDeatilItems.Where(o => o.IndicoOrderID == orderId
        //                                                    && o.IndicoOrderDetailID == orderDetailId
        //                                                    && o.SizeDesc == sizeName
        //                                                    && o.SizeQty == qty
        //                                                    && o.SizeSrno == i).Count() == 0)
        //                    {
        //                        OrderDeatilItem item = new OrderDeatilItem();
        //                        item.AgeGroup = breakdown.AgeGroup;
        //                        item.Client = breakdown.Client;
        //                        item.DateScanned = null;
        //                        item.Distributor = breakdown.Distributor;
        //                        item.Fabric = breakdown.Fabric;
        //                        item.FactoryPrice = decimal.Parse("0.00");
        //                        item.Gender = breakdown.Gender;
        //                        item.HSCode = breakdown.HSCode;
        //                        item.IndicoOrderDetailID = breakdown.OrderDeatilID;
        //                        item.IndicoOrderID = breakdown.OrderID;
        //                        item.IndimanCIFCostSheetPrice = breakdown.IndimanCIFCostSheetPrice;
        //                        item.IndimanPrice = decimal.Parse("0.00");
        //                        item.Invoice = null;
        //                        item.IsPolybagScanned = false;
        //                        item.ItemName = breakdown.ItemName;
        //                        item.ItemSubGroup = breakdown.ItemSubGroup;
        //                        item.JKFOBCostSheetPrice = breakdown.JKFOBCostSheetPrice;
        //                        item.Material = breakdown.Material;
        //                        item.Notes = string.Empty;
        //                        item.OrderNumber = breakdown.OrderDeatilID;
        //                        item.OrderType = breakdown.OrderType;
        //                        item.OtherCharges = decimal.Parse("0.00");
        //                        item.Pattern = breakdown.Pattern;
        //                        item.PatternImage = breakdown.PatternImagePath;
        //                        item.PatternInvoiceNotes = string.Empty;
        //                        item.PatternNumber = breakdown.Number;
        //                        item.PaymentMethod = null;
        //                        item.PrintedCount = 0;
        //                        item.ProductNotes = string.Empty;
        //                        item.PurchaseOrder = breakdown.PurchaseOrder;
        //                        item.PurchaseOrderNo = breakdown.PurchaseOrderNo;
        //                        item.ShipmentDeatil = sdTemp.ID;
        //                        item.ShipmentDetailCarton = null;
        //                        item.SizeDesc = sizeName;
        //                        item.SizeQty = qty;
        //                        item.SizeSrno = i;
        //                        item.SleeveLength = breakdown.SleeveLength;
        //                        item.SleeveShape = breakdown.SleeveShape;
        //                        item.Status = breakdown.Status;
        //                        item.VisualLayout = breakdown.NamePrefix;
        //                        item.VLImage = breakdown.VLImagePath;

        //                        context.OrderDeatilItems.Add(item);

        //                    }
        //                    else // Update record if it is exist 
        //                    {
        //                        OrderDeatilItem item = context.OrderDeatilItems.Where(o => o.IndicoOrderID == orderId
        //                                                    && o.IndicoOrderDetailID == orderDetailId
        //                                                    && o.SizeDesc == sizeName
        //                                                    && o.SizeQty == qty
        //                                                    && o.SizeSrno == i).FirstOrDefault();

        //                        item.AgeGroup = breakdown.AgeGroup;
        //                        item.Client = breakdown.Client;
        //                        //item.DateScanned = null;
        //                        item.Distributor = breakdown.Distributor;
        //                        item.Fabric = breakdown.Fabric;
        //                        //item.FactoryPrice = decimal.Parse("0.00");
        //                        item.Gender = breakdown.Gender;
        //                        item.HSCode = breakdown.HSCode;
        //                        item.IndicoOrderDetailID = breakdown.OrderDeatilID;
        //                        item.IndicoOrderID = breakdown.OrderID;
        //                        item.IndimanCIFCostSheetPrice = breakdown.IndimanCIFCostSheetPrice;
        //                        //item.IndimanPrice = decimal.Parse("0.00");
        //                        //item.Invoice = null;
        //                        //item.IsPolybagScanned = false;
        //                        item.ItemName = breakdown.ItemName;
        //                        item.ItemSubGroup = breakdown.ItemSubGroup;
        //                        item.JKFOBCostSheetPrice = breakdown.JKFOBCostSheetPrice;
        //                        item.Material = breakdown.Material;
        //                        //item.Notes = string.Empty;
        //                        item.OrderNumber = breakdown.OrderDeatilID;
        //                        item.OrderType = breakdown.OrderType;
        //                        //item.OtherCharges = decimal.Parse("0.00");
        //                        item.Pattern = breakdown.Pattern;
        //                        item.PatternImage = breakdown.PatternImagePath;
        //                        //item.PatternInvoiceNotes = string.Empty;
        //                        item.PatternNumber = breakdown.Number;
        //                        //item.PaymentMethod = null;
        //                        //item.PrintedCount = 0;
        //                        //item.ProductNotes = string.Empty;
        //                        item.PurchaseOrder = breakdown.PurchaseOrder;
        //                        item.PurchaseOrderNo = breakdown.PurchaseOrderNo;
        //                        //item.ShipmentDeatil = shipmentDetailId;
        //                        //item.ShipmentDetailCarton = null;
        //                        item.SizeDesc = sizeName;
        //                        item.SizeQty = qty;
        //                        item.SizeSrno = i;
        //                        item.SleeveLength = breakdown.SleeveLength;
        //                        item.SleeveShape = breakdown.SleeveShape;
        //                        item.Status = breakdown.Status;
        //                        item.VisualLayout = breakdown.NamePrefix;
        //                        item.VLImage = breakdown.VLImagePath;
        //                    }
        //                    context.SaveChanges();
        //                }
        //            }
        //        }
        //    }//while  
        //    // Add total qty for each shipment in ShipmentDetail
        //    context = new IndicoPackingEntities();
        //    foreach (ShipmentDetail s in context.ShipmentDetails)
        //    {
        //        s.Qty = context.OrderDeatilItems.Where(o => o.ShipmentDeatil == s.ID).Count();
        //    }
        //    context.SaveChanges();

        //}
    }

    //class OrderDetailBreakdown
    //{
    //    public int OrderID { get; set; }
    //    public int OrderDeatilID { get; set; }
    //    public string SizeName { get; set; }
    //    public int Qty { get; set; }
    //    public int Seq { get; set; }
    //    public string OrderType { get; set; }
    //    public string Distributor { get; set; }
    //    public string Client { get; set; }
    //    public string PurchaseOrder { get; set; }
    //    public string NamePrefix { get; set; }
    //    public int PurchaseOrder1 { get; set; }
    //    public string Pattern { get; set; }
    //    public string Fabric { get; set; }
    //    public string Material { get; set; }
    //    public string Gender { get; set; }
    //    public string AgeGroup { get; set; }
    //    public string SleeveShape { get; set; }
    //    public string SleeveLength { get; set; }
    //    public string ItemSubGroup { get; set; }
    //    public int Quantity { get; set; }
    //    public int QtySequence { get; set; }
    //    public string Status { get; set; }
    //    public string PatternImagePath { get; set; }
    //    public string VLImagePath { get; set; }
    //    public string Number { get; set; }
    //    public decimal JKFOBCostSheetPrice { get; set; }
    //    public decimal IndimanCIFCostSheetPrice { get; set; }
    //    public string HSCode { get; set; }
    //    public string ItemName { get; set; }
    //    public string PurchaseOrderNo { get; set; }
    //}

    //class ShipmentDetailTemp
    //{
    //    public int ID { get; set; }
    //    public int Shipment { get; set; }
    //    public int IndicoDistributorClientAddress { get; set; }
    //    public string ShipTo { get; set; }
    //    public string Port { get; set; }
    //    public string ShipmentMode { get; set; }
    //    public string PriceTerm { get; set; }
    //    public DateTime ETD { get; set; }
    //    public int Qty { get; set; }
    //}
}
