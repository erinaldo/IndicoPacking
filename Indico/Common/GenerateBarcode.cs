using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Model;

namespace IndicoPacking.Common
{
    static class GenerateBarcode
    {
        #region fields

        private static int maxODCount = 9;

        #endregion

        public static void GeneratePolybagLabels(List<OrderDeatilItem> lstOrderDeatilItems, string installedFolder)
        {
            string imageLocation = installedFolder + @"Data\PolybagImages\";
            Directory.CreateDirectory(imageLocation);
            string tempPath = imageLocation + "temp.jpg";

            Spire.Barcode.BarcodeSettings settings = new Spire.Barcode.BarcodeSettings();
            settings.Type = Spire.Barcode.BarCodeType.Code39;
            settings.BackColor = Color.White;
            settings.ForeColor = Color.Black;
            settings.ImageWidth = 400;
            settings.ImageHeight = 100;
            settings.Code128SetMode = Spire.Barcode.Code128SetMode.Auto;
            settings.HasBorder = false;
            settings.ResolutionType = Spire.Barcode.ResolutionType.Printer;
            settings.ShowText = false;
            settings.DpiX = 96;
            settings.DpiY = 96;

            Spire.Barcode.BarCodeGenerator barcode = new Spire.Barcode.BarCodeGenerator(settings);

            foreach (OrderDeatilItem odi in lstOrderDeatilItems)
            {
                Bitmap lblBM = new Bitmap(348, 113);
                using (Graphics gfx = Graphics.FromImage(lblBM))
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    gfx.FillRectangle(brush, 0, 0, 378, 113);
                    gfx.SmoothingMode = SmoothingMode.HighQuality;
                    gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                }

                lblBM.Save(tempPath);

                GenerateBarcode.GeneratePolybagLabel(lblBM, odi, "POLYBAG" + odi.ID.ToString(), imageLocation + ("POLYBAG" + odi.ID.ToString()).Replace('/', '_') + ".jpg", settings, barcode);

                lblBM.Dispose();
                File.Delete(tempPath);
            }

            string pdfpath = GeneratePDF.PrintPolyBagBarcode(imageLocation);

            foreach (string imagePath in Directory.GetFiles(imageLocation, "*.jpg"))
            {
                File.Delete(imagePath);
            }

            DownloadPDFFile(pdfpath);
         }

        public static void GenerateCartonLabels(List<ShipmentDetailCarton> lstShipmentDeatilCartons, string installedFolder)  
         {
             var imageLocation = installedFolder + @"Data\CartonImages\"; 
             Directory.CreateDirectory(imageLocation);
             string tempPath = imageLocation + "temp.jpg";

             Spire.Barcode.BarcodeSettings settings = new Spire.Barcode.BarcodeSettings();
             settings.Type = Spire.Barcode.BarCodeType.Code39;
             settings.BackColor = Color.White;
             settings.ForeColor = Color.Black;
             settings.ImageWidth = 400;
             settings.ImageHeight = 100;
             settings.Code128SetMode = Spire.Barcode.Code128SetMode.Auto;
             settings.HasBorder = false;
             settings.ResolutionType = Spire.Barcode.ResolutionType.Printer;
             settings.ShowText = false;
             settings.DpiX = 96;
             settings.DpiY = 96;

             Spire.Barcode.BarCodeGenerator barcode = new Spire.Barcode.BarCodeGenerator(settings);

             foreach (ShipmentDetailCarton obj in lstShipmentDeatilCartons)
             {
                 Bitmap lblBM = new Bitmap(400, 275);
                 using (Graphics gfx = Graphics.FromImage(lblBM))
                 using (SolidBrush brush = new SolidBrush(Color.White))
                 {
                     gfx.FillRectangle(brush, 0, 0, 400, 275); 
                     gfx.SmoothingMode = SmoothingMode.HighQuality; 
                     gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                     gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
                 }

                 lblBM.Save(tempPath);

                 string labelText = string.Empty;
                 string qty = string.Empty;
                 List<KeyValuePair<int, string>> listOrderDetails = new List<KeyValuePair<int, string>>();

                 IndicoPackingEntities context = new IndicoPackingEntities();
                 List<GetCartonLabelInfo> lst = context.GetCartonLabelInfoes.Where(i => i.ID == obj.ID).ToList(); 

                 GenerateBarcode.GenerateCartonLabel(lblBM, obj.Number.ToString(), lst, "CARTON" + obj.ID.ToString(), imageLocation, settings, barcode); 

                lblBM.Dispose();
                File.Delete(tempPath);
            }

            string pdfpath = GeneratePDF.PrintCartonBarcode(imageLocation);

            foreach (string imagePath in Directory.GetFiles(imageLocation, "*.jpg"))
            {
                File.Delete(imagePath);
            }

            DownloadPDFFile(pdfpath);
        }

        public static void DownloadPDFFile(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string outputName = System.Text.RegularExpressions.Regex.Replace(fileInfo.Name, @"\W+", "_");
            outputName = System.Text.RegularExpressions.Regex.Replace(outputName, "_pdf", ".pdf");

            if (File.Exists(filePath))
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                process.StartInfo = startInfo;
                startInfo.FileName = filePath;
                process.Start();
            }
            else
            {
                MessageBox.Show("Generated file can't be found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GeneratePolybagLabel(Bitmap bmLabel, OrderDeatilItem item, string labelText, string savingPath, Spire.Barcode.BarcodeSettings settings, Spire.Barcode.BarCodeGenerator barcode)
        {  
            try
            {
                settings.Data = labelText;

                Graphics graphics = Graphics.FromImage(bmLabel);
                graphics.DrawString(item.Distributor + " - " + item.Client, new Font("Calibri", 8, FontStyle.Regular), Brushes.Black, 10, 10);
                //graphics.DrawString(clientName, new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 135, 10);
                graphics.DrawString("Order: " + item.IndicoOrderID, new Font("Calibri", 9, FontStyle.Bold), Brushes.Black, 10, 22);
                graphics.DrawString(item.VisualLayout, new Font("Calibri", 9, FontStyle.Bold), Brushes.Black, 100, 22);
                graphics.DrawString("Size: " + item.SizeDesc, new Font("Calibri", 9, FontStyle.Bold), Brushes.Black, 180, 22);
                graphics.DrawString("Qty: " + item.SizeQty.ToString(), new Font("Calibri", 9, FontStyle.Bold), Brushes.Black, 250, 22);
                graphics.DrawString(item.Pattern, new Font("Calibri", 8), Brushes.Black, 10, 34);
                graphics.DrawImage(barcode.GenerateImage(), 10, 50, 300, 50); //  ("Order: " + orderNumber, new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 50, 10);
                graphics.DrawString(labelText.Replace("Polybag-", string.Empty), new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 250, 100);

                bmLabel.Save(savingPath, bmLabel.RawFormat);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the barcode image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GenerateCartonLabel(Bitmap bmLabel, string cartonNo, List<GetCartonLabelInfo> lst, string labelText, string savingLocation, Spire.Barcode.BarcodeSettings settings, Spire.Barcode.BarCodeGenerator barcode /*DistributorClientAddressBO objDistributorClientAddress, string ShipmentMode*/)
        {
            int orderDetailCount = lst.Count;
            int cartonLabelCount = (orderDetailCount % maxODCount == 0) ? orderDetailCount / maxODCount : (orderDetailCount / maxODCount) + 1;
            settings.Data = labelText;

            try
            {
                int i = 0;
                int l = 0;

                for (int k = 0; k < cartonLabelCount; k++)
                {
                    Bitmap originalBM = new Bitmap(bmLabel);
                    Graphics graphics = Graphics.FromImage(originalBM);

                    graphics.DrawString("Carton #: " + cartonNo + ((cartonLabelCount > 1) ? " - " + (k + 1).ToString() : ""), new Font("Calibri", 22, FontStyle.Bold), Brushes.Black, 10, 5);
                    graphics.DrawString("Invoice #:", new Font("Calibri", 12, FontStyle.Bold), Brushes.Black, 210, 15);
                    graphics.DrawString("PO #", new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 10, 40);
                    graphics.DrawString("VL #", new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 60, 40);
                    graphics.DrawString("Size & Qty", new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 110, 40);
                    graphics.DrawString("Client", new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 210, 40);
                    graphics.DrawString("Distributor", new Font("Calibri", 8, FontStyle.Bold), Brushes.Black, 310, 40);

                    float yValue = 40;

                    if (lst.Count > 0)
                    {
                        graphics.DrawString((lst[0].FactoryInvoiceNumber == null) ? "Not Yet Assigned" : lst[0].FactoryInvoiceNumber, new Font("Calibri", 12, FontStyle.Bold), Brushes.Black, 280, 15);
                    }

                    yValue += 15;
                    l = ((l + 9) < lst.Count) ? (l + 9) : lst.Count; 

                    for (int j = i; j < l; j++)
                    {
                        lst[i].SizeQuantities = lst[i].SizeQuantities.Substring(0, lst[i].SizeQuantities.Length - 1).Substring(0, Math.Min(28, lst[i].SizeQuantities.Length - 1));
                        graphics.DrawString(lst[i].PurchaseOrder, new Font("Calibri", 8), Brushes.Black, 10, yValue);
                        graphics.DrawString(lst[i].VisualLayout, new Font("Calibri", 8), Brushes.Black, 60, yValue);
                        graphics.DrawString(lst[i].SizeQuantities, new Font("Calibri", 6), Brushes.Black, 110, yValue);
                        graphics.DrawString(lst[i].Client.ToString().Substring(0, Math.Min(15, lst[i].Client.Length)), new Font("Calibri", 8), Brushes.Black, 210, yValue);
                        graphics.DrawString(lst[i].Distributor.ToString().Substring(0, Math.Min(15, lst[i].Distributor.Length)), new Font("Calibri", 8), Brushes.Black, 310, yValue);
                        yValue += 15;
                        i++;
                    }

                    graphics.DrawString("Qty: " + lst.Sum(x => x.Total).ToString(), new Font("Calibri", 14, FontStyle.Bold), Brushes.Black, 300, 215);

                    graphics.DrawImage(barcode.GenerateImage(), 10, 200, 250, 50);
                    originalBM.Save(savingLocation + labelText.Replace('/', '_') + "-" + k + ".jpg", originalBM.RawFormat);
                    graphics.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while generating the carton barcode image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
