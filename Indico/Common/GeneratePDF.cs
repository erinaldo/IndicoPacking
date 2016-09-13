using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using IndicoPacking.ViewModels;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Diagnostics;

namespace IndicoPacking.Common
{
    #region Enum

    public enum ReportType
    {
        Detail = 0,
        Summary,
        Combined,
        Indiman
    }

    #endregion

    static class GeneratePDF
    {       
        #region Fields

        private static string polybaglabels;
        private static string cartonlabels;
        private static string batchlabel;
        private static string jkinvoice;
        private static string jkinvoicesummary;
        private static string indimaninvoice;

        #endregion

        #region Properties

        public static string InstalledFolder { get;  set; }

        private static string PolyBagLabels
        {
            get
            {
                if (polybaglabels == null)
                {
                    StreamReader rdr = new StreamReader(InstalledFolder + @"Templates\PolyBagLabel.html");
                    polybaglabels = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return polybaglabels;
            }
        }

        private static string CartonLabels
        {
            get
            {
                if (cartonlabels == null)
                {
                    StreamReader rdr = new StreamReader(InstalledFolder + @"Templates\CartonLabel.html");
                    cartonlabels = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return cartonlabels;
            }
        }

        private static string BatchLabel
        {
            get
            {
                if (batchlabel == null)
                {
                    StreamReader rdr = new StreamReader(InstalledFolder + @"Templates\BatchHTML.html");
                    batchlabel = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return batchlabel;
            }
        }

        private static string JKInvoiceDetail
        {
            get
            {
                if (jkinvoice == null)
                {
                    StreamReader rdr = new StreamReader(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")) + @"Templates/JKInvoiceDetail.html");
                    jkinvoice = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return jkinvoice;
            }
        }

        private static string JKInvoiceSummary
        {
            get
            {
                if (jkinvoicesummary == null)
                {
                    StreamReader rdr = new StreamReader(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")) + @"Templates/JKInvoiceSummary.html");
                    jkinvoicesummary = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return jkinvoicesummary;
            }
        }

        private static string IndimanInvoice
        {
            get
            {
                 if (indimaninvoice == null)
                {
                    StreamReader rdr = new StreamReader(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")) + @"Templates/IndimanInvoiceDetail.html");
                    indimaninvoice = rdr.ReadToEnd();
                    rdr.Close();
                    rdr = null;
                }
                return indimaninvoice;
            }
        }

        #endregion

        #region Methods

        public static string CreateIndimanInvoiceHTML(Invoice objInvoice)
        {
            string indimaninvoicehtmlstring = IndimanInvoice;
            string invoicedetail = string.Empty;
            int recordcount = 0;
            string finalrecord = string.Empty;
            int masterqty = 0;
            decimal masteramount = 0;
            decimal total = 0;

            string invNo = (objInvoice.IndimanInvoiceNumber != null) ? objInvoice.IndimanInvoiceNumber : "0";
            string date = (objInvoice.IndimanInvoiceDate != null) ? Convert.ToDateTime(objInvoice.IndimanInvoiceDate.Value).ToString("dd MMMM yyyy") : DateTime.Now.ToString("dd MMMM yyyy");

            IndicoPackingEntities context = new IndicoPackingEntities();

            DistributorClientAddress address = (from a in context.DistributorClientAddresses
                                                where a.ID == objInvoice.ShipTo
                                                select a).FirstOrDefault();

            DistributorClientAddress billTo = (from a in context.DistributorClientAddresses
                                               where a.ID == objInvoice.BillTo
                                               select a).FirstOrDefault();

            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$invoiceno$>", invNo);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$invoicedate$>", date);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$shipcompanyname$>", address.CompanyName);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$shipcompanyaddress$>", address.Address + "  " + address.State);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$shipcompanypostalcode$>", address.PostCode + "  " + address.Country1.ShortName);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$shipcompanycontact$>", address.ContactName + "  " + address.ContactPhone);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$shipmentmode$>", objInvoice.ShipmentMode1.Name);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$awbno$>", objInvoice.AWBNumber);

            List<IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByDistributorForIndiman>> lstWeeklyAddressDetils = context.GetWeeklyAddressDetailsByDistributorForIndimen.Where(o => o.ID == objInvoice.ID).GroupBy(o => o.Distributor).ToList();

            invoicedetail = "<table cellpadding=\"1\" cellspacing=\"0\" style=\"font-size: 6px\" border=\"0.5\" width=\"100%\"><tr>" +
                            "<th style=\"line-height:7px;\" width=\"10%\" align=\"center\"><b>Order</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"5%\" align=\"center\"><b>Total</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"18%\" align=\"center\"><b>Description</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"32%\" align=\"center\"><b>Particulars</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"6%\" align=\"center\"><b>Type</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"6%\" align=\"center\"><b>Price</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"6%\" align=\"center\"><b>Other/Ch</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"6%\" align=\"center\"><b>Total Price</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"6%\" align=\"center\"><b>Amount</b></th>" +
                            "<th style=\"line-height:7px;\" width=\"5%\" align=\"center\"><b>Notes</b></th></tr>";

            int rowIndex = 1;
            double rowCount = 0;
            int pageCount = 0;
            int firstPageCount = 28;
            int otherPagesCount = 35;
            int itemCount = 0;

            foreach (IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByDistributorForIndiman> distributor in lstWeeklyAddressDetils)
            {
                int totoalqty = 0;
                invoicedetail += "<tr style=\"height:-2px;\"><td style=\"line-height:10px;\" colspan=\"10\"><h6 style=\"padding-left:5px\"><b>" + distributor.Key + "</b></h6></td></tr>";
                rowIndex++;
                rowCount++;
                itemCount++;

                List<IndicoPacking.Model.GetWeeklyAddressDetailsByDistributorForIndiman> lstOrderDetailsGroup = distributor.ToList();

                foreach (IndicoPacking.Model.GetWeeklyAddressDetailsByDistributorForIndiman item in lstOrderDetailsGroup)
                {                                                                 
                    string orderdetailqty = string.Empty;
                    orderdetailqty += item.SizeQuantities;
                    total = (decimal)(item.Amount);
                    masteramount = masteramount + total;
                    totoalqty = totoalqty + (int)item.Qty;
                    string PatternNo = item.Pattern.ToString().Substring(0, item.Pattern.ToString().LastIndexOf('-')).Trim();
                    string FabricName = item.Fabric.ToString().Substring(item.Fabric.ToString().IndexOf('-') + 1).Trim();
                    string NickName = item.Pattern.ToString().Substring(item.Pattern.ToString().IndexOf('-') + 1).Trim();                   

                    if (((item.VisualLayout + " " + item.Client).Count() > 27) || ((PatternNo + " " + FabricName).Count() > 46)) //32
                    {
                        rowCount = rowCount + 0.5;
                    }
                    if (((orderdetailqty.Trim().Substring(0, orderdetailqty.Length - 2)).Count() > 27) || (NickName.Count() > 46))
                    {
                        rowCount = rowCount + 0.5;
                    }       

                    invoicedetail += "<tr><td style=\"line-height:10px;\" border=\"0\" width=\"10%\">" + item.PurchaseOrder + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\" width=\"5%\">" + item.Qty + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\"  width=\"18%\">" + item.VisualLayout + " "+ item.Client +"<br/>"+ orderdetailqty.Trim().Substring(0, orderdetailqty.Length - 2) + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\" width=\"32%\">" + PatternNo + " " + FabricName + "<br/>" + NickName + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\"  width=\"6%\">" + item.OrderType + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\"  width=\"6%\">" + item.IndimanPrice + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\" width=\"6%\">" + item.OtherCharges + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\" width=\"6%\">" + item.TotalPrice + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\" width=\"6%\">" + total.ToString("0.00") + "</td>" +
                                        "<td style=\"line-height:10px;\" border=\"0\"  width=\"5%\">" + item.Notes + " </td></tr>";

                    rowCount++;
                    rowIndex++;
                    itemCount++;

                    if ((pageCount == 0 && rowCount >= firstPageCount) || (pageCount != 0 && rowCount >= otherPagesCount))
                    {
                        invoicedetail += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:60px;color: #FFFFFF;\" colspan=\"10\"><h6>INDICO</h6></td></tr>";
                        rowIndex = 0;
                        rowCount = 0;
                        itemCount = 0;
                        pageCount++;
                    }                
                }

                recordcount = recordcount + 1;
                masterqty = masterqty + totoalqty;
            }

            invoicedetail += "</table>";

            if (lstWeeklyAddressDetils.Count == recordcount)
            {
                finalrecord += "<table border=\"0.5\"><tr><td style=\"line-height:10px;\" border=\"0\">Total:</td><td style=\"line-height:10px;\" border=\"0\">" + masterqty + "</td><td style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" +
                               "<td style=\"line-height:10px;\" border=\"0\">&nbsp;</td><td style=\"line-height:10px;\" border=\"0\">&nbsp;</td><td style=\"line-height:10px;\" border=\"0\">&nbsp;</td><td style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" +
                               "<td style=\"line-height:10px;\" border=\"0\"></td></tr></table>";
            }

            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$pricedetails$>", invoicedetail);
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$finalrecord$>", finalrecord);

            decimal gst = ((masteramount * 10) / 100);
            decimal totalgst = gst + masteramount;

            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$subtotal$>", masteramount.ToString("0.00"));
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$gst$>", gst.ToString("0.00"));
            indimaninvoicehtmlstring = indimaninvoicehtmlstring.Replace("<$totgst$>", totalgst.ToString("0.00"));            

            return indimaninvoicehtmlstring;
        }

        private static string CreateJKInvoiceDetailHTML(Invoice objInvoice)
        {
            string jkinvoicedetailhtmlstring = JKInvoiceDetail;
            string invoicedetail = string.Empty;
            int recordcount = 0;
            string finalrecord = string.Empty;
            int masterqty = 0;
            decimal masteramount = 0;
            decimal total = 0;
            int rowIndex = 1;
            int rowIndexCount = 20;

            IndicoPackingEntities context = new IndicoPackingEntities();

            DistributorClientAddress address = (from a in context.DistributorClientAddresses
                                                where a.ID == objInvoice.ShipTo
                                                select a).FirstOrDefault();

            DistributorClientAddress billTo = (from a in context.DistributorClientAddresses
                                                where a.ID == objInvoice.BillTo
                                                select a).FirstOrDefault();

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$invoiceno$>", objInvoice.FactoryInvoiceNumber);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipmentdate$>", objInvoice.FactoryInvoiceDate.ToString("dd MMMM yyyy"));
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipcompanyname$>", address.CompanyName);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipcompanyaddress$>", address.Address + "  " + address.State);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipcompanypostalcode$>", address.PostCode + "  " + address.Country1.ShortName);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipcompanycontact$>", address.ContactName + "  " + address.ContactPhone);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$shipmentmode$>", objInvoice.ShipmentMode1.Name);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$awbno$>", objInvoice.AWBNumber);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$port$>", (billTo.Port != null && billTo.Port > 0) ? billTo.Port1.Name : string.Empty);

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$billtocompanyname$>", billTo.CompanyName);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$billtocompanyname$>", billTo.Address);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$billtocompanystate$>", billTo.Suburb + "  " + billTo.State + "  " + billTo.PostCode);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$billtocompanycountry$>", billTo.Country1.ShortName);

            invoicedetail = "<table cellpadding=\"1\" cellspacing=\"0\" style=\"font-size: 7px\" width=\"100%\"><tr>" +
                            "<th border=\"0\" width=\"9%\"><b>Type</b></th>" + //13
                            "<th border=\"0\" width=\"10%\"><b>Qty</b></th>" + //5
                            "<th border=\"0\" width=\"20%\"><b>Sizes</b></th>" + //25
                            "<th border=\"0\"  width=\"34%\"><b>Description</b></th>" + //35
                            "<th border=\"0\" width=\"6%\"><b>Price</b></th>" +
                            "<th border=\"0\" width=\"7%\"><b>Other/Ch</b></th>" +
                            "<th border=\"0\" width=\"6%\"><b>Total</b></th>" +
                            "<th border=\"0\" width=\"8%\"><b>AMOUNT</b></th></tr>";

            List<IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByDistributor>> lstAddressOrderDetails = context.GetWeeklyAddressDetailsByDistributors.Where(o => o.ID == objInvoice.ID).GroupBy(o => o.Distributor).ToList();
            foreach (IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByDistributor> distributor in lstAddressOrderDetails)
            {
                int totoalqty = 0;
                decimal totalamount = 0;
                invoicedetail += "<tr style=\"height:-2px;\" border=\"0.5\"><td style=\"line-height:10px;\" colspan=\"8\"><h6 style=\"padding-left:5px\"><b>" + distributor.Key /*address.CompanyName*/ + "</b></h6></td></tr>";
                rowIndex++;

                List<IndicoPacking.Model.GetWeeklyAddressDetailsByDistributor> lstOrderDetailsGroup = distributor.ToList();

                foreach (IndicoPacking.Model.GetWeeklyAddressDetailsByDistributor item in lstOrderDetailsGroup)
                {
                        string orderdetailqty = string.Empty;
                        orderdetailqty += item.SizeQuantities; 
                        total = (decimal)(item.Amount);
                        totalamount = totalamount + total;
                        masteramount = masteramount + total;
                        totoalqty = totoalqty + (int)item.Qty;

                        invoicedetail += "<tr style=\"height:-2px;\">" +
                                         "<td style=\"line-height:10px;\" width=\"9%\">" + item.OrderType + "<br>" + item.PurchaseOrderNo + "</td>" + //13
                                         "<td style=\"line-height:10px;\" width=\"10%\">" + item.Qty + "  " + item.ItemName + "</td>" + //5
                                         "<td style=\"line-height:10px;\" width=\"20%\">" + item.VisualLayout + " " + item.Client + "<br>" + orderdetailqty.Trim().Substring(0, orderdetailqty.Length - 2) + "</td>" + //item.client(ic) //25
                                         "<td style=\"line-height:10px;\" width=\"34%\">" + item.Fabric + " " + item.Material + "</td>" +
                                         "<td style=\"line-height:10px;\" width=\"6%\">" + ((decimal)item.FactoryPrice).ToString("0.00") + "</td>" + //35
                                         "<td style=\"line-height:10px;\" width=\"7%\" >" + ((decimal)item.OtherCharges).ToString("0.00") + "</td>" +
                                         "<td style=\"line-height:10px;\" width=\"6%\" >" + ((decimal)item.TotalPrice).ToString("0.00") + "</td>" +
                                         "<td style=\"line-height:10px;\" width=\"6%\" >" + total.ToString("0.00") + "</td></tr>" +
                                         "<tr><td style=\"line-height:0px;\" colspan=\"8\" border=\"0.5\"></td></tr>";

                        rowIndex++;

                        if (rowIndex >= rowIndexCount) //16
                        {
                            invoicedetail += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:50px;color: #FFFFFF;\" colspan=\"8\"><h6>INDICO</h6></td></tr>";
                            rowIndex = 0;
                            rowIndexCount = 27;
                        }
                    }
                //}
                invoicedetail += "<tr style=\"height:2px;\"><td  style=\"line-height:-2px;color: #F0FFFF;\" colspan=\"8\"></td>"; //9colspan
                invoicedetail += "<tr>" +
                                 "<td  style=\"line-height:8px;\" border=\"0\" width=\"9%\">&nbsp;</td>" + //13
                                 "<td  style=\"line-height:8px;\" border=\"0\" width=\"10%\"><b>" + totoalqty + "</b></td>" + //5
                                 "<td  style=\"line-height:8px;\" border=\"0\" width=\"20%\">&nbsp;</td>" + //25
                                 "<td style=\"line-height:8px;\" border=\"0\" width=\"34%\">&nbsp;</td>" + //35
                                 "<td style=\"line-height:8px;\" border=\"0\" width=\"6%\">&nbsp;</td>" +
                                 "<td style=\"line-height:8px;\" border=\"0\" width=\"7%\">&nbsp;</td>" + //IC
                                 "<td style=\"line-height:8px;\" border=\"0\" width=\"6%\">&nbsp;</td>" + //IC
                                 "<td  style=\"line-height:8px;\" border=\"0\" width=\"8%\">" + totalamount.ToString("0.00") + "</td>" +
                                 "</tr>";

                recordcount = recordcount + 1;
                masterqty = masterqty + totoalqty;

                if (lstAddressOrderDetails.Count != recordcount)
                {
                    invoicedetail += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:2px;color: #FFFFFF;\" colspan=\"8\"><h6>INDICO</h6></td>"; //7 colspan
                    rowIndex++;

                    if (rowIndex >= rowIndexCount) 
                    {
                        invoicedetail += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:50px;color: #FFFFFF;\" colspan=\"8\"><h6>INDICO</h6></td></tr>"; //7 colspan
                        rowIndex = 0;
                        rowIndexCount = 27;
                    }
                }
            }

            invoicedetail += "</table>";

            if (lstAddressOrderDetails.Count == recordcount)
            {
                finalrecord = "<table border=\"0.5\">" +
                              "<tr>" +
                              "<td width=\"9%\" style=\"line-height:0px;\">&nbsp;</td>" + //13
                              "<td width=\"10%\" style=\"line-height:0px;\">&nbsp;</td>" + //5
                              "<td width=\"20%\" style=\"line-height:0px;\">&nbsp;</td>" + //25
                              "<td width=\"34%\" style=\"line-height:0px;\">&nbsp;</td>" + //35
                              "<td width=\"6%\" style=\"line-height:0px;\">&nbsp;</td>" +
                              "<td width=\"7%\" style=\"line-height:0px;\">&nbsp;</td>" +//IC
                              "<td width=\"6%\" style=\"line-height:0px;\">&nbsp;</td>" +//IC
                              "<td width=\"8%\" style=\"line-height:0px;\">&nbsp;</td>" +
                              "</tr>";
                finalrecord += "<tr>" +
                               "<td width=\"9%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" + //13
                               "<td width=\"10%\" style=\"line-height:10px;\" border=\"0\">" + masterqty + "</td>" +//5
                                "<td width=\"20%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" + //25
                               "<td width=\"34%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" + //35
                               "<td width=\"6%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" +
                               "<td width=\"7%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" + //IC
                               "<td width=\"6%\" style=\"line-height:10px;\" border=\"0\">&nbsp;</td>" +  //IC
                               "<td width=\"8%\" style=\"line-height:10px;\" border=\"0\">" + masteramount.ToString("0.00") + "</td>" +
                               "</tr>" +
                               "</table>";
            }

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$pricedetails$>", invoicedetail);
            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$finalrecord$>", finalrecord);

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$bankname$>", objInvoice.Bank1.Name);

            string country = (objInvoice.Bank1.Country != null || objInvoice.Bank1.Country > 0) ? objInvoice.Bank1.Country1.ShortName : string.Empty;

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$bankaddress$>", objInvoice.Bank1.Address + "  " + objInvoice.Bank1.City + "  " + objInvoice.Bank1.State + "  " + objInvoice.Bank1.Postcode + "  " + country);

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$accountnumber$>", objInvoice.Bank1.AccountNo);

            jkinvoicedetailhtmlstring = jkinvoicedetailhtmlstring.Replace("<$swiftcode$>", objInvoice.Bank1.SwiftCode);

            //string textFilePath = IndicoConfiguration.AppConfiguration.PathToDataFolder + @"\Temp\invoice_" + objInvoice.ID.ToString() + ".html";
            //System.IO.StreamWriter file = new System.IO.StreamWriter(textFilePath, false);
            //file.WriteLine(jkinvoicedetailhtmlstring);
            //file.Close();

            return jkinvoicedetailhtmlstring;
        }

        private static string CreateJKInvoiceSummaryHTML(Invoice objInvoice)
        {
            string jkinvoicesummaryhtmlstring = JKInvoiceSummary;
            string invoicesummary = string.Empty;
            int masterqty = 0;
            decimal masteramount = 0;
            int rowIndex = 1;

            IndicoPackingEntities context = new IndicoPackingEntities();

            DistributorClientAddress address = (from a in context.DistributorClientAddresses
                                                where a.ID == objInvoice.ShipTo
                                                select a).FirstOrDefault();

            DistributorClientAddress billTo = (from a in context.DistributorClientAddresses
                                               where a.ID == objInvoice.BillTo
                                               select a).FirstOrDefault();

            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$invoicenumber$>", objInvoice.FactoryInvoiceNumber);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$shipmentdate$>", objInvoice.FactoryInvoiceDate.ToString("dd MMMM yyyy"));
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$mode$>", objInvoice.ShipmentMode1.Name);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$awbno$>", objInvoice.AWBNumber);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$companyname$>", address.CompanyName);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$companyaddress$>", address.Address + "  " + address.State);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$companypostalcodecountry$>", address.PostCode + "  " + address.Country1.ShortName);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$companycontact$>", address.ContactName + "  " + address.ContactPhone);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$port$>", (billTo.Port != null && billTo.Port > 0) ? billTo.Port1.Name : string.Empty);

            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$billtocompanyname$>", billTo.CompanyName);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$billtocompanyname$>", billTo.Address);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$billtocompanystate$>", billTo.Suburb + "  " + billTo.State + "  " + billTo.PostCode);
            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$billtocompanycountry$>", billTo.Country1.ShortName);        

            invoicesummary = "<table width=\"100%\" style=\"font-size:6px;\" cellpadding=\"1\" cellspacing=\"0\"><tr>" +
                             "<th width=\"35%\">Filaments</th>" +
                             "<th width=\"20%\">Item Name</th>" +
                             "<th width=\"10%\">Gender</th>" +
                             "<th width=\"15%\">Item Sub Cat</th>" +
                             "<th width=\"10%\">Qty</th>" +
                             "<th width=\"10%\">Amount</th></tr>" +
                             "<tr><td style=\"line-height:0px;\" colspan=\"6\" border=\"0.5\"></td></tr>";

            List<IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByHSCode>> lstAddressOrderDetails = context.GetWeeklyAddressDetailsByHSCodes.Where(o => o.ID == objInvoice.ID).GroupBy(o => (o.HSCode != null) ? o.HSCode : string.Empty).ToList();
            foreach (IGrouping<string, IndicoPacking.Model.GetWeeklyAddressDetailsByHSCode> hscode in lstAddressOrderDetails)
            {
                string hscodetext = (!string.IsNullOrEmpty(hscode.Key)) ? hscode.Key : string.Empty;
                int qty = 0;
                decimal amount = 0;
                decimal total = 0;
                rowIndex++;

                invoicesummary += "<tr><td colspan=\"6\"><b>" + hscodetext + "</b></td></tr>";

               List<IndicoPacking.Model.GetWeeklyAddressDetailsByHSCode> lstOrderDetailsGroup = hscode.ToList();

                foreach (IndicoPacking.Model.GetWeeklyAddressDetailsByHSCode item in lstOrderDetailsGroup)
                {                 
                    qty = qty + (int)item.Qty;
                    total = (decimal)item.TotalPrice;
                    amount = amount + total;

                    invoicesummary += "<tr><td style=\"line-height:12px;\" width=\"35%\">" + item.Material + "</td>" +
                                        "<td style=\"line-height:12px;\" width=\"20%\">" + item.ItemName + "</td>" +
                                        "<td style=\"line-height:12px;\" width=\"10%\">" + item.Gender + "</td>" +
                                        "<td style=\"line-height:12px;\" width=\"15%\">" + item.ItemSubGroup + "</td>" +
                                        "<td style=\"line-height:12px;\" width=\"10%\">" + item.Qty + "</td>" +
                                        "<td style=\"line-height:12px;\" width=\"10%\">" + ((decimal)item.TotalPrice).ToString("0.00") + "</td></tr>";

                    rowIndex++;

                    if (rowIndex >= 37)
                    {
                        invoicesummary += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:50px;color: #FFFFFF;\" colspan=\"6\"><h6>INDICO</h6></td></tr>";
                        rowIndex = 0;
                    }
                }

                masterqty = masterqty + qty;
                masteramount = masteramount + amount;
                invoicesummary += "<tr><td style=\"line-height:12px;\" width=\"35%\"></td>" +
                                  "<td width=\"20%\"></td>" +
                                  "<td style=\"line-height:12px;\" width=\"10%\"></td>" +
                                  "<td style=\"line-height:12px;\" width=\"15%\"></td>" +
                                  "<td style=\"line-height:12px;\" width=\"10%\"><b>" + qty.ToString() + "</b></td>" +
                                  "<td style=\"line-height:12px;\" width=\"10%\"><b>" + amount.ToString("0.00") + "</b></td></tr>";

                invoicesummary += "<tr><td width=\"35%\"></td>" +
                                  "<td width=\"20%\"></td>" +
                                  "<td width=\"10%\"></td>" +
                                  "<td width=\"15%\"></td>" +
                                  "<td width=\"10%\" style=\"line-height:0px;\" border=\"0.5\"></td>" +
                                  "<td width=\"10%\" style=\"line-height:0px;\" border=\"0.5\"></td></tr>";

                if (rowIndex >= 37)
                {
                    invoicesummary += "<tr style=\"height:5px;\"><td border=\"0\" style=\"line-height:50px;color: #FFFFFF;\" colspan=\"6\"><h6>INDICO</h6></td></tr>";
                    rowIndex = 0;
                }
            }

            invoicesummary += "<tr><td style=\"line-height:12px;\" width=\"35%\"></td>" +
                              "<td style=\"line-height:12px;\" width=\"20%\"></td>" +
                              "<td style=\"line-height:12px;\" width=\"10%\"></td>" +
                              "<td style=\"line-height:12px;\" width=\"15%\"></td>" +
                              "<td style=\"line-height:12px;\" width=\"10%\"><b>" + masterqty.ToString() + "</b></td>" +
                              "<td style=\"line-height:12px;\" width=\"10%\"><b>" + masteramount.ToString("0.00") + "</b></td></tr>";
            invoicesummary += "<tr><td width=\"35%\"></td>" +
                              "<td width=\"20%\"></td>" +
                              "<td width=\"10%\"></td>" +
                              "<td width=\"15%\"></td>" +
                              "<td width=\"10%\" style=\"line-height:0px;\" border=\"0.5\"></td><td width=\"10%\" style=\"line-height:0px;\" border=\"0.5\"></td></tr>";
            invoicesummary += "</table>";

            jkinvoicesummaryhtmlstring = jkinvoicesummaryhtmlstring.Replace("<$invoicesummary$>", invoicesummary);

            //string textFilePath = IndicoConfiguration.AppConfiguration.PathToDataFolder + @"\Temp\invoice_" + objInvoice.ID.ToString() + ".html";
            //System.IO.StreamWriter file = new System.IO.StreamWriter(textFilePath, false);
            //file.WriteLine(jkinvoicesummaryhtmlstring);
            //file.Close();

            return jkinvoicesummaryhtmlstring;
        }

        public static string PrintPolyBagBarcode(string imagelocation)
        {
            string directorypath = imagelocation + Guid.NewGuid();
            List<string> lstfilepaths = new List<string>();
            string tempfilepath = string.Empty;

            string polybagbarcodes = imagelocation + "PolyBag_Barcodes.pdf";

            try
            {
                if (Directory.Exists(directorypath))
                {
                    Directory.Delete(directorypath, true);
                }
                else
                {
                    Directory.CreateDirectory(directorypath);
                }

                if (File.Exists(polybagbarcodes))
                {
                    File.Delete(polybagbarcodes);
                }

                List<string> lstFileNames = Directory.GetFiles(imagelocation).Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime).Select(m => m.FullName).ToList();

                foreach (string imagePath in lstFileNames)
                {
                    tempfilepath = directorypath + "\\" + Guid.NewGuid() + ".pdf";

                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempfilepath, FileMode.Create));
                    document.AddKeywords("paper airplanes");

                    //float marginBottom = 12;
                    //float lineHeight = 14;
                    //float pageMargin = 20;
                    float pageHeight = (float)100.0;
                    float pageWidth = (float)295.0;

                    document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                    document.SetMargins(0, 0, 0, 0);

                    // Open the document for writing content
                    document.Open();

                    string htmlText = GeneratePDF.CreatePolyBagBarcodelHTML(imagePath);
                    HTMLWorker htmlWorker = new HTMLWorker(document);
                    htmlWorker.Parse(new StringReader(htmlText));

                    document.Close();

                    lstfilepaths.Add(tempfilepath);

                }

                if (lstfilepaths.Count > 0)
                {
                    CombineAndSavePdf(polybagbarcodes, lstfilepaths);
                }

                if (Directory.Exists(directorypath))
                {
                    Thread t = new Thread(new ThreadStart(() => DeleteDirectory(directorypath)));
                    t.Start();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the plolybag labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return polybagbarcodes;
        }

        public static string PrintCartonBarcode(string imagelocation)
        {
            string directorypath = imagelocation + Guid.NewGuid();
            List<string> lstfilepaths = new List<string>();
            string tempfilepath = string.Empty;
            bool IsFileOpen = false; 

            string polybagbarcodes = imagelocation + "Carton_Barcodes.pdf";

            FileStream stream = null;

            if (File.Exists(polybagbarcodes))
            {
                try
                {
                    stream = File.Open(polybagbarcodes, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException)
                {
                    MessageBox.Show(string.Format("Carton barcode label is open. Please close it and generate again the barcode label."), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IsFileOpen = true;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            if (!IsFileOpen)
            {
                try
                {
                    if (Directory.Exists(directorypath))
                    {
                        Directory.Delete(directorypath, true);
                    }
                    else
                    {
                        Directory.CreateDirectory(directorypath);
                    }

                    if (File.Exists(polybagbarcodes))
                    {
                        File.Delete(polybagbarcodes);
                    }

                    List<string> lstFileNames = Directory.GetFiles(imagelocation).Select(f => new FileInfo(f)).OrderBy(f => f.CreationTime).Select(m => m.FullName).ToList();

                    foreach (string imagePath in lstFileNames)
                    {
                        tempfilepath = directorypath + "\\" + Guid.NewGuid() + ".pdf";

                        Document document = new Document();
                        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempfilepath, FileMode.Create));
                        document.AddKeywords("paper airplanes");

                        //float marginBottom = 12;
                        //float lineHeight = 14;
                        //float pageMargin = 20;
                        float pageHeight = (float)255.0;
                        float pageWidth = (float)385.0;

                        document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                        document.SetMargins(0, 0, 0, 0);

                        // Open the document for writing content
                        document.Open();

                        string htmlText = GeneratePDF.CreateCartonBarcodelHTML(imagePath);
                        HTMLWorker htmlWorker = new HTMLWorker(document);
                        htmlWorker.Parse(new StringReader(htmlText));

                        document.Close();

                        lstfilepaths.Add(tempfilepath);
                    }

                    if (lstfilepaths.Count > 0)
                    {
                        CombineAndSavePdf(polybagbarcodes, lstfilepaths);
                    }

                    if (Directory.Exists(directorypath))
                    {
                        Thread t = new Thread(new ThreadStart(() => DeleteDirectory(directorypath)));
                        t.Start();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured while generating the carton label", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return polybagbarcodes;
        }   

        private static void DeleteDirectory(string directorypath)
        {
            // get a write lock on scCacheKeys
            //IndicoPage.RWLock.AcquireWriterLock(Timeout.Infinite);

            bool isDelete = true;

            while (isDelete)
            {
                try
                {
                    Directory.Delete(directorypath, true);
                    isDelete = false;
                }
                catch (Exception)
                {
                    isDelete = true;

                }
                finally
                {

                    if (!isDelete)
                    {
                        // release the lock
                        //IndicoPage.RWLock.ReleaseWriterLock();
                    }

                }
            }

        }

        private static void CombineAndSavePdf(string savePath, List<string> lstPdfFiles)
        {
            using (Stream outputPdfStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfSmartCopy copy = new PdfSmartCopy(document, outputPdfStream);
                document.Open();
                PdfReader reader;
                int totalPageCnt;
                PdfStamper stamper;
                string[] fieldNames;
                foreach (string file in lstPdfFiles)
                {
                    reader = new PdfReader(file);
                    totalPageCnt = reader.NumberOfPages;
                    for (int pageCnt = 0; pageCnt < totalPageCnt; )
                    {
                        //have to create a new reader for each page or PdfStamper will throw error
                        reader = new PdfReader(file);
                        stamper = new PdfStamper(reader, outputPdfStream);
                        fieldNames = new string[stamper.AcroFields.Fields.Keys.Count];
                        stamper.AcroFields.Fields.Keys.CopyTo(fieldNames, 0);
                        foreach (string name in fieldNames)
                        {
                            stamper.AcroFields.RenameField(name, name + "_file" + pageCnt.ToString());
                        }

                        copy.AddPage(copy.GetImportedPage(reader, ++pageCnt));
                    }

                    copy.FreeReader(reader);
                }

                document.Close();
            }
        }

        private static string CreatePolyBagBarcodelHTML(string imagePath)
        {
            InstalledFolder = imagePath.Substring(0, imagePath.LastIndexOf("Data"));
            string bolybagbarcode = PolyBagLabels;

            bolybagbarcode = bolybagbarcode.Replace("<$image$>", "<img src=\"" + imagePath + "\" alt=\"Smiley face\" height=\"" + 95 + "\" width=\"" + 265 + "\" >");

            return bolybagbarcode;
        }

        private static string CreateCartonBarcodelHTML(string imagePath)
        {
            //string cartonbarcode = imagePath.Substring(0, imagePath.IndexOf("\\Data")) + "\\Templates\\CartonLabel.html"; //CartonLabels;
            InstalledFolder = imagePath.Substring(0, imagePath.LastIndexOf("Data"));
            string cartonbarcode = CartonLabels;

            cartonbarcode = cartonbarcode.Replace("<$image$>", "<img src=\"" + imagePath + "\" alt=\"Smiley face\" height=\"" + 255 + "\" width=\"" + 285 + "\" >");

            return cartonbarcode;
        }

        private static string CreateBatchLabelHTML(OrderDeatilItem objOrderDetail, DateTime weekenddate, string imagePath)
        {
            InstalledFolder = imagePath;
            string batchlabel = BatchLabel;
            string order = (!string.IsNullOrEmpty(objOrderDetail.PurchaseOrder)) ? objOrderDetail.PurchaseOrder : "PO-" + objOrderDetail.IndicoOrderID.ToString();

            batchlabel = batchlabel.Replace("<$Order$>", order);
            batchlabel = batchlabel.Replace("<$VisualLayout$>", objOrderDetail.VisualLayout);
            batchlabel = batchlabel.Replace("<$Pattern$>", objOrderDetail.PatternNumber);
            batchlabel = batchlabel.Replace("<$qty$>", objOrderDetail.SizeQty.ToString());
            batchlabel = batchlabel.Replace("<$weekenddate$>", weekenddate.ToString("d"));

            return batchlabel;           
        }

        public static string CreateBatchLabel(List<OrderDeatilItem> items/*int order*/, string imagelocation)
        {
            string directorypath = imagelocation + @"/Data/BatchLabelImages/" + Guid.NewGuid();
            List<string> lstfilepaths = new List<string>();
            string tempfilepath = string.Empty;

            string batchlabelfile = imagelocation + @"/Data/BatchLabelImages/" + "Batch_Label_" + DateTime.Now.ToString("dd MMMM yyyy") + "_" + ".pdf";

            try
            {
                if (Directory.Exists(directorypath))
                {
                    Directory.Delete(directorypath, true);
                }
                else
                {
                    Directory.CreateDirectory(directorypath);
                }

                if (File.Exists(batchlabelfile))
                {
                    File.Delete(batchlabelfile);
                }

              /*  OrderDetailBO objOrderDetail = new OrderDetailBO();
                objOrderDetail.Order = order;

                List<OrderDetailBO> lstOrderDetails = objOrderDetail.SearchObjects(); */

               // foreach (OrderDetailBO objOD in lstOrderDetails)
                foreach (OrderDeatilItem item in items)
                {
                   /* OrderDetailQtyBO objOrderDetailQty = new OrderDetailQtyBO();
                    objOrderDetailQty.OrderDetail = objOD.ID;

                    List<OrderDetailQtyBO> lstOrderDetailsqty = objOrderDetailQty.SearchObjects();

                    List<WeeklyProductionCapacityBO> lstWeeklyProductionCapacity = (new WeeklyProductionCapacityBO()).SearchObjects().Where(o => o.WeekendDate > objOD.SheduledDate && o.WeekendDate <= objOD.SheduledDate.AddDays(7)).ToList();
                    */

                    tempfilepath = directorypath + "\\" + Guid.NewGuid() + ".pdf";

                    Document document = new Document();
                    PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(tempfilepath, FileMode.Create));
                    document.AddKeywords("paper airplanes");

                    //float marginBottom = 12;
                    //float lineHeight = 14;
                    //float pageMargin = 20;
                    float pageHeight = (float)108.0;
                    float pageWidth = (float)50.0;

                    document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                    document.SetMargins(0, 0, 0, 0);

                    // Open the document for writing content
                    document.Open();

                    string htmlText = GeneratePDF.CreateBatchLabelHTML(item, Convert.ToDateTime(item.ShipmentDetail.Shipment1.WeekendDate), imagelocation);
                    HTMLWorker htmlWorker = new HTMLWorker(document);
                    htmlWorker.Parse(new StringReader(htmlText));

                    document.Close();

                    lstfilepaths.Add(tempfilepath);
                }

                if (lstfilepaths.Count > 0)
                {
                    CombineAndSavePdf(batchlabelfile, lstfilepaths);
                }

                if (Directory.Exists(directorypath))
                {
                    Thread t = new Thread(new ThreadStart(() => DeleteDirectory(directorypath)));
                    t.Start();
                }

                GenerateBarcode.DownloadPDFFile(batchlabelfile);

            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating Batch Labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return batchlabelfile;
        }

        public static string CombinedInvoice(int invoice, string dataFolder)
        {
            /*InvoiceBO objInvoice = new InvoiceBO();
            objInvoice.ID = invoice;
            objInvoice.GetObject();*/

            IndicoPackingEntities context = new IndicoPackingEntities();

            Invoice objInvoice = (from i in context.Invoices
                                  where i.ID == invoice
                                  select i).FirstOrDefault();

            string jkinvoicesummarypath = string.Empty;

            jkinvoicesummarypath = dataFolder + @"Data\Invoices\" + "JKCombineInvoice_" + objInvoice.ID.ToString() + "_" + ".pdf";

            try
            {

                if (File.Exists(jkinvoicesummarypath))
                {
                    File.Delete(jkinvoicesummarypath);
                }

                List<string> lstfilespath = new List<string>();

                // Generated indico detail pdf path
                string invoicedetailpath = GenerateJKInvoiceDetail(invoice, dataFolder);

                lstfilespath.Add(invoicedetailpath);

                // Generated indico summary pdf path
                string invoicesummarypath = GenerateJKInvoiceSummary(invoice, dataFolder);

                lstfilespath.Add(invoicesummarypath);

                CombineAndSavePdf(jkinvoicesummarypath, lstfilespath);
            }
            catch (Exception ex)
            {
                //IndicoLogging.log.Error("Error occured while creating combined invoice from GenerateOdsPdf", ex);
            }
            return jkinvoicesummarypath;
        }

        public static string GenerateJKInvoiceSummary(int invoice, string dataFolder)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            Invoice objInvoice = (from i in context.Invoices                                
                                  where i.ID == invoice
                                  select i).FirstOrDefault();

            string jkinvoicesummarypath = string.Empty;

            try
            {
                jkinvoicesummarypath = dataFolder + @"Data\Invoices\" + "JKInvoiceSummary_" + objInvoice.ID.ToString() + "_" + ".pdf";//dataFolder + @"Data\Invoices\" + "JKInvoiceSummary_" + objInvoice.ID.ToString() + "_" + ".pdf";

                if (File.Exists(jkinvoicesummarypath))
                {
                    try
                    {
                        File.Delete(jkinvoicesummarypath);
                    } catch
                    {}
                }

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(jkinvoicesummarypath, FileMode.Create));
                document.AddKeywords("paper airplanes");

                //float marginBottom = 12;
                //float lineHeight = 14;
                //float pageMargin = 20;
                float pageHeight = iTextSharp.text.PageSize.A4.Height;
                float pageWidth = iTextSharp.text.PageSize.A4.Width;

                document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                document.SetMargins(0, 0, 0, 0);

                writer.PageEvent = new PDFFooter("INVOICE SUMMARY", false);

                document.Open();

                //// Open the document for writing content
                //document.Open();
                //// Get the top layer and write some text
                //contentByte = writer.DirectContent;

                //contentByte.BeginText();
                //string content = string.Empty;
                //string pageNo = string.Empty;
                ////string page = "Page " + writer.getpa();


                ////Header
                //contentByte.SetFontAndSize(PDFFontBold, 10);
                //content = "INVOICE SUMMARY";
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, content, pageMargin, (pageHeight - pageMargin - lineHeight), 0);

                //// set Page Number
                //contentByte.SetFontAndSize(PDFFont, 8);
                //pageNo = "Page " + writer.PageNumber.ToString();
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, pageNo, (pageWidth - 30), (pageHeight - pageMargin - lineHeight), 0);

                //contentByte.EndText();

                //// Top Line
                //contentByte.SetLineWidth(0.5f);
                //contentByte.SetColorStroke(BaseColor.BLACK);
                //contentByte.MoveTo(pageMargin, (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.LineTo((pageWidth - pageMargin), (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.Stroke();

                string htmlText = GeneratePDF.CreateJKInvoiceSummaryHTML(objInvoice);
                HTMLWorker htmlWorker = new HTMLWorker(document);
                htmlWorker.Parse(new StringReader(htmlText));

                document.Close();
            }
            catch (Exception ex)
            {
                //IndicoLogging.log.Error("Error occured while Generate Pdf jkinvoicesummarypath in GenerateOdsPdf Class", ex);
            }

            return jkinvoicesummarypath;
        }

        public static string GenerateJKInvoiceDetail(int invoice, string dataFolder)
        {
            string jkinvoicedetailpath = string.Empty;

            IndicoPackingEntities context = new IndicoPackingEntities();

            Invoice objInvoice = (from i in context.Invoices
                                  where i.ID == invoice
                                  select i).FirstOrDefault();

            try
            {
                jkinvoicedetailpath = dataFolder + @"Data\Invoices\" + "JKInvoiceDetail_" + objInvoice.ID.ToString() + "_" + ".pdf";

                if (File.Exists(jkinvoicedetailpath))
                {
                    File.Delete(jkinvoicedetailpath);
                }

                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(jkinvoicedetailpath, FileMode.Create));
                document.AddKeywords("paper airplanes");

                //float marginBottom = 12;
                //float lineHeight = 14;
                //float pageMargin = 20;
                float pageHeight = iTextSharp.text.PageSize.A4.Height;
                float pageWidth = iTextSharp.text.PageSize.A4.Width;

                document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                document.SetMargins(0, 0, 0, 0);

                // Open the document for writing content
                writer.PageEvent = new PDFFooter("COMMERCIAL INVOICE", false);

                document.Open();

                // Get the top layer and write some text
                //contentByte = writer.DirectContent;

                //contentByte.BeginText();
                //string content = string.Empty;
                //string pageNo = string.Empty;
                ////string page = "Page " + writer.getpa();


                ////Header
                //contentByte.SetFontAndSize(PDFFontBold, 10);
                //content = "COMMERCIAL INVOICE";
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, content, pageMargin, (pageHeight - pageMargin - lineHeight), 0);

                //// set Page Number
                //contentByte.SetFontAndSize(PDFFont, 8);
                //pageNo = "Page " + writer.PageNumber.ToString();
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, pageNo, (pageWidth - 30), (pageHeight - pageMargin - lineHeight), 0);

                //contentByte.EndText();

                //// Top Line
                //contentByte.SetLineWidth(0.5f);
                //contentByte.SetColorStroke(BaseColor.BLACK);
                //contentByte.MoveTo(pageMargin, (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.LineTo((pageWidth - pageMargin), (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.Stroke();

                string htmlText = GeneratePDF.CreateJKInvoiceDetailHTML(objInvoice);
                HTMLWorker htmlWorker = new HTMLWorker(document);
                htmlWorker.Parse(new StringReader(htmlText));

                document.Close();
            }
            catch (Exception ex)
            {
                //IndicoLogging.log.Error("Error occured while Generate Pdf JKInvoiceOrderDetail in GenerateOdsPdf Class", ex);
            }

            return jkinvoicedetailpath;
        }

        public static string GenerateIndimanInvoice(int invoice, string dataFolder)
        {
            //InvoiceBO objInvoice = new InvoiceBO();
            //objInvoice.ID = invoice;
            //objInvoice.GetObject();

            string temppath = string.Empty;

            IndicoPackingEntities context = new IndicoPackingEntities();

            Invoice objInvoice = (from i in context.Invoices
                                  where i.ID == invoice
                                  select i).FirstOrDefault();

            Document document = new Document();

            try
            {
                temppath = dataFolder + @"Data\Invoices\" + "IndimanInvoiceDetail_" + objInvoice.ID.ToString() + "_" + ".pdf";

                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(temppath, FileMode.Create));
                document.AddKeywords("paper airplanes");

                //float marginBottom = 12;
                //float lineHeight = 14;
                //float pageMargin = 20;
                float pageHeight = iTextSharp.text.PageSize.A4.Height;
                float pageWidth = iTextSharp.text.PageSize.A4.Width;

                document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
                document.SetMargins(0, 0, 0, 0);

                writer.PageEvent = new PDFFooter("Indico Manufacturing Pty Ltd", true);

                // Open the document for writing content
                document.Open();
                // Get the top layer and write some text
                //contentByte = writer.DirectContent;

                //contentByte.BeginText();
                //string content = string.Empty;
                //string content_1 = string.Empty;
                //////string page = "Page " + writer.GetPageReference();

                //////Header
                //contentByte.SetFontAndSize(PDFFont, 12);
                //content = "Indico Manufacturing Pty Ltd";
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, content, pageMargin, (pageHeight - pageMargin - lineHeight), 0);

                //contentByte.SetFontAndSize(PDFFont, 6);
                //content_1 = "Suit 43,125 Highbury Road, Burwood, VIC 3125";
                //contentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, content_1, pageMargin, (pageHeight - pageMargin - 22), 0);

                //contentByte.EndText();
                //// Top Line
                //contentByte.SetLineWidth(0.5f);
                //contentByte.SetColorStroke(BaseColor.BLACK);
                //contentByte.MoveTo(pageMargin, (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.LineTo((pageWidth - pageMargin), (pageHeight - pageMargin - lineHeight - marginBottom - 4));
                //contentByte.Stroke();

                string htmlText = GeneratePDF.CreateIndimanInvoiceHTML(objInvoice);
                HTMLWorker htmlWorker = new HTMLWorker(document);
                htmlWorker.Parse(new StringReader(htmlText));

                writer.Close();
            }
            catch (Exception ex)
            {
                //IndicoLogging.log.Error("Error occured while Generate Pdf indimaninvoicepath in GenerateOdsPdf Class", ex);
            }
            finally
            {
                document.Close();
            }

            return temppath;
        }

        public static void GenerateInvoices(int invoiceId, string dataFolder, string rdlFileName, string invoiceName, ReportType type)
        {
            using (ReportViewer rpt = new ReportViewer())
            {
                bool IsFileOpen = false;                

                rpt.ProcessingMode = ProcessingMode.Local;

                rpt.LocalReport.ReportPath = dataFolder + @"Data\Reports\" + rdlFileName;
                
                IndicoPackingEntities context = new IndicoPackingEntities();

                rpt.LocalReport.DataSources.Clear();
                rpt.LocalReport.DataSources.Add(new ReportDataSource("InvoiceHeader", context.InvoiceHeaderDetailsViews.Where(i => i.ID == invoiceId)));
                
                switch(type)
                {
                    case ReportType.Detail:       
                        rpt.LocalReport.DataSources.Add(new ReportDataSource("OrderByDistributor", context.GetWeeklyAddressDetailsByDistributors.Where(i => i.ID == invoiceId)));
                        break;
                    case ReportType.Summary:
                        rpt.LocalReport.DataSources.Add(new ReportDataSource("ByHSCode", context.GetWeeklyAddressDetailsByHSCodes.Where(i => i.ID == invoiceId)));
                        break;
                    case ReportType.Combined:
                        rpt.LocalReport.DataSources.Add(new ReportDataSource("OrderByDistributor", context.GetWeeklyAddressDetailsByDistributors.Where(i => i.ID == invoiceId)));
                        rpt.LocalReport.DataSources.Add(new ReportDataSource("ByHSCode", context.GetWeeklyAddressDetailsByHSCodes.Where(i => i.ID == invoiceId)));
                        break;
                    case ReportType.Indiman:
                        rpt.LocalReport.DataSources.Add(new ReportDataSource("OrderByDistributorForIndiman", context.GetWeeklyAddressDetailsByDistributorForIndimen.Where(i => i.ID == invoiceId)));
                        break;
                    default:
                        break;
                }

                string mimeType, encoding, extension, deviceInfo;
                string[] streamids;
                Microsoft.Reporting.WinForms.Warning[] warnings;
                string format = "PDF";

                deviceInfo = "<DeviceInfo>" +
                "<SimplePageHeaders>True</SimplePageHeaders>" +
                "</DeviceInfo>";

                byte[] bytes = rpt.LocalReport.Render(format, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
   
                string temppath = dataFolder + @"Data\Invoices\" + invoiceName + ".pdf";

                FileStream stream = null;

                if (File.Exists(temppath))
                {
                    try
                    {
                        stream = File.Open(temppath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(string.Format("{0} is opened in PDF viewer. Please close it before opening another one.", invoiceName + ".pdf"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsFileOpen = true;                        
                    }
                    finally
                    {
                        if (stream != null)
                            stream.Close();
                    }
                }

                if (File.Exists(temppath) && !IsFileOpen)
                {
                    File.Delete(temppath);
                }

                if (!IsFileOpen)
                {
                    using (FileStream fs = new FileStream(temppath, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }

                while (File.Exists(temppath))
                {
                    Thread.Sleep(1000);
                    System.Diagnostics.Process.Start(temppath);
                    break;
                }
            }
           
        }

        #endregion
    }
}
