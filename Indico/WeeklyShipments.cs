using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using IndicoPacking.Common;
using IndicoPacking.Model;
using IndicoPacking.ViewModels;
using Telerik.WinControls.UI;
using System.Threading;
using Dapper;
using IndicoPacking.CustomModels;
using Microsoft.Reporting.WinForms;

namespace IndicoPacking
{
    public partial class WeeklyShipments : Form
    {
        #region Constants

        const int BoxHeight = 130;
        const int Gap = 5;

        const int PanelWidth = 160;
        const int PanelHeight = 130;
        const int PboxWidth = 73;
        const int PboxHeight = 55;

        const UserType FillingCordinator = UserType.FillingCordinator;

        #endregion

        #region Fields

        private string _imageUrlvl;
        private string _imageUrlPattern;
        private int _boxCount = 1;
        private int _even = 1;
        private int _odd = 2;
        private readonly ContextMenuStrip _contextMenu;
        private readonly string _installedFolder = string.Empty;
        private readonly ToolStripItem _toolStripItemFillingCarton;

        private ToolStripItem toolStripItemGeneratePolybags, toolStripItemGenerateCarton, 
                              toolStripItemGenerateBatch, toolStripItemClearitems, toolStripItemClearFilledItems, 
                              toolStripItemChangeBoxSize, toolStripItemDeleteCarton = null;

        int _mainFormCurrentWidth = 1845;
        int _mainFormCurrentHeight = 1071;
        IndicoPackingEntities _context;
        Preview _preview = new Preview();
        private int _activatedCount;
        private readonly Semaphore _vlImageloadSemaphoreSemaphore =new Semaphore(1,1);
        private readonly Semaphore _patterenImageLoadSemaphore = new Semaphore(1,1); 

        #endregion

        #region Properties

        public string ImageUrivl
        {
            set
            {
                if (_imageUrlvl == value) return;
                _imageUrlvl = value;
                Task.Factory.StartNew(() =>
                {
                    _vlImageloadSemaphoreSemaphore.WaitOne();
                    picVLImage.Image = Image.FromFile(_installedFolder + @"images\loadingImage.gif");
                    try
                    {
                        var request = WebRequest.Create(value);
                        var response = request.GetResponse();
                        var stream = response.GetResponseStream();
                        picVLImage.Image = stream != null ? Image.FromStream(stream) : null;
                        response.Close();
                        if (stream != null)
                            stream.Close();
                    }
                    catch (Exception e)
                    {
                        picVLImage.Image = null;
                    }
                    _vlImageloadSemaphoreSemaphore.Release();
                }).ContinueWith((r) => { }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        public string ImageUriPattern
        {
            set
            {
                if(_imageUrlPattern == value) return;
                _imageUrlPattern = value;
                Task.Factory.StartNew(() =>
                {
                    _patterenImageLoadSemaphore.WaitOne();
                    picPatternImage.Image = Image.FromFile(_installedFolder + @"images\loadingImage.gif");
                    try
                    {
                        var request = WebRequest.Create(value);
                        var response = request.GetResponse();
                        var stream = response.GetResponseStream();
                        picPatternImage.Image = stream != null ? Image.FromStream(stream) : null;
                        response.Close();
                        if (stream != null)
                            stream.Close();
                    }
                    catch (Exception e)
                    {
                        picPatternImage.Image = null;
                    }
                    _patterenImageLoadSemaphore.Release();
                }).ContinueWith((r) => { }, TaskScheduler.FromCurrentSynchronizationContext());
             
            }
        }

        public bool SetCartonDatasource
        {
            set
            {
                if (value)
                {
                    IndicoPackingEntities context1 = new IndicoPackingEntities();
                    ddlCarton.DataSource = null;
                    ddlCarton.DataSource = context1.Cartons.ToList();
                    ddlCarton.DisplayMember = "Name";
                    ddlCarton.ValueMember = "ID";

                    ddlCarton.SelectedIndex = 0;
                }
            }
        }       

        #endregion

        #region Constructors

        public WeeklyShipments()
        {
            InitializeComponent();

            _context = new IndicoPackingEntities();

            picVLImage.SizeMode = PictureBoxSizeMode.StretchImage;
            picPatternImage.SizeMode = PictureBoxSizeMode.StretchImage;
            List<Shipment> lst = _context.Shipments.ToList();

            if (lst.Count == 0)
            {
                btnAddData.Text = "Add data for year " + DateTime.Now.Year.ToString();
            }
            if (lst.Count > 0)
            {
                LoadDropDown();
                btnAddData.Text = "Add data for year " + lst.Last().WeekendDate.AddYears(1).Year;
            }
           
            ddlCarton.DataSource = _context.Cartons.ToList();
            ddlCarton.DisplayMember = "Name";
            ddlCarton.ValueMember = "ID";

            ddlCarton.SelectedIndex = 0;           

            // Context menu for each panel box
            _contextMenu = new ContextMenuStrip();            
            toolStripItemGeneratePolybags = _contextMenu.Items.Add("Genarate Polybag Labels", null, new EventHandler(GeneratePolybagBarcodeClick));
            toolStripItemGenerateCarton = _contextMenu.Items.Add("Genarate Carton Label", null, new EventHandler(GenerateCartonBarcodeClick));
            toolStripItemGenerateBatch = _contextMenu.Items.Add("Genarate Batch Labels", null, new EventHandler(GenerateBatchlabelClick));
            _contextMenu.Items.Add("-");
            toolStripItemClearitems = _contextMenu.Items.Add("Clear Items", null, new EventHandler(ClearItemsClick));
            toolStripItemClearFilledItems = _contextMenu.Items.Add("Clear Filed Items", null, new EventHandler(ClearFilledItemsClick));
            _contextMenu.Items.Add("-");
            toolStripItemChangeBoxSize = _contextMenu.Items.Add("Change Box Size", null, new EventHandler(ChangeBoxSizeClick));
            _contextMenu.Items.Add("-");
            _toolStripItemFillingCarton = _contextMenu.Items.Add("Start Filling Carton", null, new EventHandler(FillCartonClick));
            _contextMenu.Items.Add("-");
            toolStripItemDeleteCarton = _contextMenu.Items.Add("Delete Carton", null, new EventHandler(DeleteCartonClick));

            // Hide for filling cordinator.
            if (LoginInfo.Role == FillingCordinator)
            {
                toolStripItemGeneratePolybags.Enabled = false;
                toolStripItemGenerateCarton.Enabled = false;
                toolStripItemGenerateBatch.Enabled = false;
                toolStripItemClearitems.Enabled = false;
                toolStripItemChangeBoxSize.Enabled = false;
                toolStripItemDeleteCarton.Enabled = false;
            }

            _installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            grdOrderDetailItem.SelectionChanged += grdOrderDetailItem_SelectionChanged;
            grdOrderDetailItem.MouseDown += grdOrderDetailItem_MouseDown;
            txtPONumber.KeyDown += txtPONumber_KeyDown;  
        }       
           
        private void frmMain_Load(object sender, EventArgs e)
        {           
            btnGenerateAllBatchLabels.Enabled = btnClearFilledCartons.Enabled = btnFillCarton.Enabled = btnClearAllCartonItems.Enabled = btnClearCartonArea.Enabled = btnGenerateCartonBarcods.Enabled = btnGeneratePolybagBarcods.Enabled = ddlCarton.Enabled = btnAddcarton.Enabled = btnFillingFirstScanningPolybags.Enabled = txtPONumber.Enabled = false;
            // Hide for filling cordinator.
            if (LoginInfo.Role == FillingCordinator)
                btnSynchronize.Enabled = btnAddData.Enabled = false;

            Spire.Barcode.BarcodeSettings.ApplyKey("W2Z0H-QC6SA-J17AC-UQUJ3-SA3T2");
            Resize += frmMain_Resize;
            Activated += WeeklyShipments_Activated;
            _mainFormCurrentHeight = Height;
            _mainFormCurrentWidth = Width;
        }

        void frmMain_Resize(object sender, EventArgs e)
        {
            var formMain = (Form)sender;
            DoubleBuffered = true;
            SuspendLayout();

            //if (formMain.Width <= 1845)
            //{
            //    formMain.Width = 1845;
            //}

            //if (formMain.Height <= 1071)
            //{
            //    formMain.Height = 1071;
            //}

            if (formMain.Height > 1071)
            {
                grdOrderDetailItem.Height = grdOrderDetailItem.Height + (formMain.Height - _mainFormCurrentHeight);
                pnlmain.Height = pnlmain.Height + (formMain.Height - _mainFormCurrentHeight);
                btnCancel.Top = btnCancel.Top + (formMain.Height - _mainFormCurrentHeight);

                _mainFormCurrentHeight = formMain.Height;
            }

            //if (formMain.Width > 1562)
            //{
            ddlCarton.Left = ddlCarton.Left + (formMain.Width - _mainFormCurrentWidth);
            btnAddcarton.Left = btnAddcarton.Left + (formMain.Width - _mainFormCurrentWidth);
            pnlmain.Left = pnlmain.Left + (formMain.Width - _mainFormCurrentWidth);
            btnCancel.Left = btnCancel.Left + (formMain.Width - _mainFormCurrentWidth);

            btnGenerateAllBatchLabels.Left = btnGenerateAllBatchLabels.Left + (formMain.Width - _mainFormCurrentWidth);
            groupBox1.Left = groupBox1.Left + (formMain.Width - _mainFormCurrentWidth);
            groupBox2.Left = groupBox2.Left + (formMain.Width - _mainFormCurrentWidth);
            picVLImage.Left = picVLImage.Left + (formMain.Width - _mainFormCurrentWidth);
            lblPAtternImage.Left = lblPAtternImage.Left + (Width - _mainFormCurrentWidth);
            picPatternImage.Left = picPatternImage.Left + (formMain.Width - _mainFormCurrentWidth);
            btnFillCarton.Left = btnFillCarton.Left + (formMain.Width - _mainFormCurrentWidth);
            btnFillingFirstScanningPolybags.Left = btnFillingFirstScanningPolybags.Left + formMain.Width - _mainFormCurrentWidth;
            label3.Left = label3.Left + (formMain.Width - _mainFormCurrentWidth);
            grdOrderDetailItem.Width = grdOrderDetailItem.Width + (formMain.Width - _mainFormCurrentWidth);
            grdShipmentDetails.Width = grdShipmentDetails.Width + (formMain.Width - _mainFormCurrentWidth);
            lblSearchByPO.Left = lblSearchByPO.Left + (Width - _mainFormCurrentWidth);
            txtPONumber.Left = txtPONumber.Left + (Width - _mainFormCurrentWidth);
            // btnAddData.Width = btnAddData.Width + (formMain.Width - _mainFormCurrentWidth);
            //GeneratePackingListButton.Width = GeneratePackingListButton.Width + (formMain.Width - _mainFormCurrentWidth);
            CartonColorsPanel.Left = CartonColorsPanel.Left + (formMain.Width - _mainFormCurrentWidth);

            _mainFormCurrentWidth = formMain.Width;
            //}

            DoubleBuffered = false;
            ResumeLayout();
        }

        #endregion

        #region Events 

        void WeeklyShipments_Activated(object sender, EventArgs e)
        {
            if (_activatedCount != 0)
            {
                dGVshipmentdetail_CellClick(grdShipmentDetails, new DataGridViewCellEventArgs(0, 0));
            }

            _activatedCount++;
        }
                 
        void menuButton_Click(object sender, EventArgs e)
        {
            MenuButton menuButton = (MenuButton)sender;
            _toolStripItemFillingCarton.Text = (menuButton.Tag.Equals(true)) ? "Resume Filling Carton" : "Start Filling Carton";
        }

        void FillCartonClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Panel panel = ((System.Windows.Forms.Panel)((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent);
            int shipmentDetailCartonId = int.Parse(panel.Tag.ToString());

            FillingCarton frmFC = new FillingCarton();
            frmFC.ClickedCartonShipmentDeatailCartonId = shipmentDetailCartonId;
            frmFC.MainPanel = pnlmain;
            frmFC.InstalledPath = _installedFolder;
            frmFC.GridShipment = grdShipmentDetails;
            frmFC.ShowDialog();
        }

        void ClearFilledItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to clear filled items in carton?", "Clear Filled Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Windows.Forms.Panel panel = ((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent));
                int shipmentDetailCartonId = int.Parse(panel.Tag.ToString());
                ClearFilledCartonItems(shipmentDetailCartonId, panel);

                Control button = panel.Controls.Find("mnuButton", true).FirstOrDefault();
                if (button is MenuButton)
                {
                    button.Tag = false;
                }
            }
        }

        public void btnClearFilledCartons_Click(object sender, EventArgs e)
        {
            if (grdShipmentDetails.SelectedRows.Count < 1)
            {
                MessageBox.Show("No carton found to clear", "Clear Filled Cartons", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure, you want to clear all fillled cartons?", "Clear Filled Cartons", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearCartonDetailsArea();
                // Don't remove this context, if removed it will misbehave the app.
                IndicoPackingEntities context = new IndicoPackingEntities();
                int shipmentDetail = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());

                List<OrderDeatilItem> oder = (from odi in context.OrderDeatilItems
                                              where odi.IsPolybagScanned == true && odi.ShipmentDeatil == shipmentDetail
                                              select odi).ToList();

                foreach (OrderDeatilItem item in oder)
                {
                    item.IsPolybagScanned = false;
                    UpdateShipmentDetailQty(item.ShipmentDeatil, -1);
                }

                // Status text of Parent form
                ((ParentForm)MdiParent).toolStripStatusLabel.Text = "All filled items removed from cartons.";

                context.SaveChanges();

                LoadCartonDeatils();
            }
        }

        void DeleteCartonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to delete this carton?", "Delete Carton", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Windows.Forms.Panel panel = ((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent));
                int shipmentDetailCartonId = int.Parse(panel.Tag.ToString());
                // Don't remove this context, if removed it will missbehave the app.
                IndicoPackingEntities context = new IndicoPackingEntities();

                // First remove the items from the carton
                List<OrderDeatilItem> orderDeatilItems = (from odi in context.OrderDeatilItems
                                                          where odi.ShipmentDetailCarton == shipmentDetailCartonId
                                                          select odi).ToList();

                foreach (OrderDeatilItem odi in orderDeatilItems)
                {
                    odi.ShipmentDetailCarton = null;
                    if (odi.IsPolybagScanned == true)
                    {
                        odi.IsPolybagScanned = false;
                        UpdateShipmentDetailQty(odi.ShipmentDeatil, -1);
                    }
                }

                // Reset the carton numbers
                // first get the shipmentdeatilcarton object from the database
                ShipmentDetailCarton shipmentDeatilCarton = (from o in context.ShipmentDetailCartons
                                                             where o.ID == shipmentDetailCartonId
                                                             select o).FirstOrDefault();

                List<ShipmentDetailCarton> shipmentDeatilCartons = (from o in context.ShipmentDetailCartons
                                                                    where o.ShipmentDetail == shipmentDeatilCarton.ShipmentDetail
                                                                        && o.Number > shipmentDeatilCarton.Number
                                                                    select o).ToList();
                foreach (ShipmentDetailCarton o in shipmentDeatilCartons)
                {
                    o.Number = o.Number - 1;
                }

                // Now delete the shipmentdeatilcarton
                context.ShipmentDetailCartons.Remove(shipmentDeatilCarton);

                context.SaveChanges();

                // Now simulate the top grid click event
                dGVshipmentdetail_CellClick(grdShipmentDetails, new DataGridViewCellEventArgs(0, 0));
            }
        }

        public void btnClearCartonArea_Click(object sender, EventArgs e)
        {
            if (grdShipmentDetails.SelectedRows.Count < 1)
            {
                MessageBox.Show("Nothing to clear in carton area", "Clear Carton Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure, you want to clear the carton area?", "Clear Carton Area", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Don't remove this context, if removed it will missbehave the app.
                IndicoPackingEntities context = new IndicoPackingEntities();

                // Get the ShipmentDeatilId from the top grid
                int shipmentDetailId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());

                List<OrderDeatilItem> orderDeatilItems = (from odi in context.OrderDeatilItems
                                                          where odi.ShipmentDetailCarton.HasValue && odi.ShipmentDeatil == shipmentDetailId
                                                          select odi).ToList();

                foreach (OrderDeatilItem odi in orderDeatilItems)
                {
                    odi.ShipmentDetailCarton = null;
                    if (odi.IsPolybagScanned == true)
                    {
                        odi.IsPolybagScanned = false;
                        UpdateShipmentDetailQty(odi.ShipmentDeatil, -1);
                    }
                }

                // Remove the records from ShipmentDeatilCarton table                

                foreach (ShipmentDetailCarton sdc in (from o in context.ShipmentDetailCartons
                                                      where o.ShipmentDetail == shipmentDetailId
                                                      select o).ToList())
                {
                    context.ShipmentDetailCartons.Remove(sdc);
                }

                // Status text of Parent form
                ((ParentForm)MdiParent).toolStripStatusLabel.Text = "Carton area is cleared.";

                context.SaveChanges();

                ClearCartonDetailsArea();

                PopulategrdOrderDetailItems();
            }
        }

        public void btnClearAllCartonItems_Click(object sender, EventArgs e)
        {
            if (grdShipmentDetails.SelectedRows.Count < 1)
            {
                MessageBox.Show("No carton items to clear", "Clear All Carton Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure, you want to clear all the carton items?", "Clear All Carton Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ClearCartonDetailsArea();
                // Don't remove this context, if removed it will missbehave the app.
                IndicoPackingEntities context = new IndicoPackingEntities();

                int shipmentDetailId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());

                List<OrderDeatilItem> orderDeatilItems = (from odi in context.OrderDeatilItems
                                                          where odi.ShipmentDetailCarton.HasValue && odi.ShipmentDeatil == shipmentDetailId
                                                          select odi).ToList();

                foreach (OrderDeatilItem odi in orderDeatilItems)
                {
                    odi.ShipmentDetailCarton = null;
                    if (odi.IsPolybagScanned == true)
                    {
                        odi.IsPolybagScanned = false;
                        UpdateShipmentDetailQty(odi.ShipmentDeatil, -1);
                    }
                }

                // Status text of Parent form
                ((ParentForm)MdiParent).toolStripStatusLabel.Text = "All items removed from cartons.";

                context.SaveChanges();

                PopulategrdOrderDetailItems();
                LoadCartonDeatils();
            }
        }

        void GenerateBatchlabelClick(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating batch labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                int shipmentDetailCartonId = int.Parse(((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent)).Tag.ToString());

                List<OrderDeatilItem> orderDetailItem = (from odi in _context.OrderDeatilItems
                                                         where odi.ShipmentDetailCarton == shipmentDetailCartonId
                                                         select odi).ToList();

                GeneratePDF.CreateBatchLabel(orderDetailItem ,_installedFolder);
                         
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the batch labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        void GenerateGridBatchLabelClick(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating batch labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                List<int> ids = new List<int>();
                foreach (GridViewRowInfo row in grdOrderDetailItem.SelectedRows)
                {
                    ids.Add(int.Parse(row.Cells[0].Value.ToString()));
                }

                List<OrderDeatilItem> orderDeatilItems = _context.OrderDeatilItems
                                                            .Where(o => ids.Contains(o.ID)).ToList();

                GeneratePDF.CreateBatchLabel(orderDeatilItems, _installedFolder);

            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while generating the batch labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        void GenerateGridPolybagBarcodeClick(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating polybag labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                List<int> ids = new List<int>();
                foreach (GridViewRowInfo row in grdOrderDetailItem.SelectedRows)
                {
                    ids.Add(int.Parse(row.Cells[0].Value.ToString()));
                }

                List<OrderDeatilItem> orderDeatilItems = _context.OrderDeatilItems
                                                            .Where(o => ids.Contains(o.ID)).ToList();

                // Increase the printed count
                orderDeatilItems.ToList()
                    .ForEach(c => { c.PrintedCount = c.PrintedCount + 1; });

                // Now we have the order detail items so we know the polybag items
                // Now generate the pdf with polybag labels
                GenerateBarcode.GeneratePolybagLabels(orderDeatilItems, _installedFolder);

                _context.SaveChanges();

                PopulategrdOrderDetailItems();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the polybag labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        void GeneratePolybagBarcodeClick(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating polybag labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                int shipmentDetailCartonId = int.Parse(((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent)).Tag.ToString());

                List<OrderDeatilItem> orderDeatilItems = (from odi in _context.OrderDeatilItems
                                                          where odi.ShipmentDetailCarton == shipmentDetailCartonId
                                                          select odi).ToList();

                // Increase the printed count
                orderDeatilItems.ToList()
                    .ForEach(c => { c.PrintedCount = c.PrintedCount + 1; });

                // Now we have the order detail items so we know the polybag items
                // Now generate the pdf with polybag labels
                GenerateBarcode.GeneratePolybagLabels(orderDeatilItems, _installedFolder);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the polybag labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        void GenerateCartonBarcodeClick(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating carton labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                int shipmentDetailCartonId = int.Parse(((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent)).Tag.ToString());

                List<ShipmentDetailCarton> shipmentDetailCarton = (from sdc in _context.ShipmentDetailCartons
                                                                   where sdc.ID == shipmentDetailCartonId
                                                                   select sdc).ToList();

                GenerateBarcode.GenerateCartonLabels(shipmentDetailCarton, _installedFolder);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the carton labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        public void btnGenerateCartonBarcods_Click(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating carton labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                List<int> shipmentDetailCartonIds = new List<int>();

                foreach (Control control in pnlmain.Controls)
                {
                    if (control is Panel)
                    {
                        shipmentDetailCartonIds.Add(int.Parse(((Panel)control).Tag.ToString()));
                    }
                }

                List<ShipmentDetailCarton> shipmentDetailCarton = (from sdc in _context.ShipmentDetailCartons
                                                                   where shipmentDetailCartonIds.Contains(sdc.ID)
                                                                   select sdc).ToList();

                GenerateBarcode.GenerateCartonLabels(shipmentDetailCarton, _installedFolder);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the carton labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        public void btnGeneratePolybagBarcods_Click(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating polybag labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                int shipmentDetailId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());

                List<OrderDeatilItem> orderDeatilItems = (from od in _context.OrderDeatilItems
                                                          where od.ShipmentDeatil == shipmentDetailId
                                                          select od).ToList();

                // Increase the printed count
                orderDeatilItems.ToList()
                    .ForEach(c => { c.PrintedCount = c.PrintedCount + 1; });

                GenerateBarcode.GeneratePolybagLabels(orderDeatilItems, _installedFolder);

                _context.SaveChanges();

                PopulategrdOrderDetailItems();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the polybag labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        public void btnGenerateAllBatchLabels_Click(object sender, EventArgs e)
        {
            frmProgress progress = new frmProgress();

            try
            {
                progress.Message = "Generating batch labels. Please wait...";
                progress.StartPosition = FormStartPosition.CenterScreen;
                progress.TopMost = true;
                progress.Show();
                progress.Refresh();

                int shipmentDetailId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());

                List<OrderDeatilItem> orderDetailitem = (from odi in _context.OrderDeatilItems
                                                         where odi.ShipmentDetail.ID == shipmentDetailId
                                                         select odi).ToList();

                GeneratePDF.CreateBatchLabel(orderDetailitem, _installedFolder);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured while generating the batch labels", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progress.Hide();
                progress.Dispose();
            }
        }

        void ChangeBoxSizeClick(object sender, EventArgs e)
        {
            ChangeBoxSize frm = new ChangeBoxSize();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();

            System.Windows.Forms.Panel panel = ((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent));
            int shipmentDetailCartonId = int.Parse(panel.Tag.ToString());

            Carton c = (from o in _context.Cartons
                        where o.ID == frm.CartonId
                        select o).FirstOrDefault();

            ShipmentDetailCarton sdc = (from o in _context.ShipmentDetailCartons
                                        where o.ID == shipmentDetailCartonId
                                        select o).FirstOrDefault();
            if (c != null)
            {
                c.ShipmentDetailCartons.Add(sdc);
                
                _context.SaveChanges();

                Label lblCartonInfo = panel.Controls.Find("lblCartonInfo", true).FirstOrDefault() as Label;
                if (lblCartonInfo != null)
                {
                    lblCartonInfo.Text = c.Name;
                }

                Label lblCartonItemsCount = panel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
                if (lblCartonItemsCount != null)
                {
                    lblCartonItemsCount.Text = lblCartonItemsCount.Text.Substring(0, lblCartonItemsCount.Text.IndexOf('(') - 1) + " (" + c.Qty.ToString() + ")";
                }
            }
        }

        private void ClearFilledCartonItems(int shipmentDetailCartonId, System.Windows.Forms.Panel panel)
        {
            // Don't remove this context, if removed it will missbehave the app.
            IndicoPackingEntities context = new IndicoPackingEntities();

            List<OrderDeatilItem> orderDeatilItems = (from odi in context.OrderDeatilItems
                                                      where odi.ShipmentDetailCarton == shipmentDetailCartonId && odi.IsPolybagScanned == true
                                                      select odi).ToList();

            var shipment = context.ShipmentDetailCartons.Where(s => s.ID == shipmentDetailCartonId).FirstOrDefault();        

            // Status text of Parent form
            ((ParentForm)MdiParent).toolStripStatusLabel.Text = string.Format("Filled {0} Polybag(s) removed from the carton number {1}.", orderDeatilItems.Count.ToString(), shipment.Number.ToString());                

            foreach (OrderDeatilItem odi in orderDeatilItems)
            {         
                odi.IsPolybagScanned = false;
                UpdateShipmentDetailQty(odi.ShipmentDeatil, -1);                              
            }

            context.SaveChanges();

            int filledCount = (context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId && o.IsPolybagScanned == true).ToList()).Count;
            int itemCount = (context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId).ToList()).Count;

            ChangeCartonColor(panel, filledCount, itemCount);
            RefreshCartonDeatilsAddedCount(shipmentDetailCartonId, panel);
        }

        void ClearItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to clear the carton items?", "Clear Carton", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                System.Windows.Forms.Panel panel = ((System.Windows.Forms.Panel)(((MenuButton)(((ContextMenuStrip)((ToolStripItem)sender).GetCurrentParent()).SourceControl)).Parent));
                int shipmentDetailCartonId = int.Parse(panel.Tag.ToString());
                ClearCartonItems(shipmentDetailCartonId, panel);
            }
        }

        private void ClearCartonItems(int shipmentDetailCartonId, System.Windows.Forms.Panel panel)
        {
            // Don't remove this context, if removed it will missbehave the app.
            IndicoPackingEntities context = new IndicoPackingEntities();

            List<OrderDeatilItem> orderDeatilItems = (from odi in context.OrderDeatilItems
                                                      where odi.ShipmentDetailCarton == shipmentDetailCartonId
                                                      select odi).ToList();

            var shipment = context.ShipmentDetailCartons.Where(s => s.ID == shipmentDetailCartonId).FirstOrDefault(); 

            // Status text of Parent form
            ((ParentForm)MdiParent).toolStripStatusLabel.Text = string.Format("Added {0} Item(s) cleared from the carton {1}", orderDeatilItems.Count.ToString(), shipment.Number.ToString());

            foreach (OrderDeatilItem odi in orderDeatilItems)
            {
                if (odi.IsPolybagScanned == true)
                {
                    odi.IsPolybagScanned = false;
                    UpdateShipmentDetailQty(odi.ShipmentDeatil, -1);
                }
                odi.ShipmentDetailCarton = null;                            
            }

            context.SaveChanges();

            PopulategrdOrderDetailItems();

            int filledCount = (context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId && o.IsPolybagScanned == true).ToList()).Count;
            int itemCount = (context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId).ToList()).Count;

            ChangeCartonColor(panel, filledCount, itemCount);
            RefreshCartonDeatilsAddedCount(shipmentDetailCartonId, panel);
        }

        private void dGVshipmentdetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= -1)
                return;
            ClearCartonDetailsArea();
            grdOrderDetailItem.DataSource = null;
            grdOrderDetailItem.Rows.Clear();


            if (grdShipmentDetails.Rows.Count > 0)
            {
                // Check the orderdeatil quantity count match the both databses or not. If not then ask to synchronize the databses
                /*   int shipmentId = (int)((System.Collections.Generic.KeyValuePair<int, string>)ddlWeekEndDate.SelectedValue).Key;

                    Shipment shipment = (from s in context.Shipments
                                         where s.ID == shipmentId
                                         select s).FirstOrDefault();

                    int localCount = context.OrderDeatilItems.Where(o => o.ShipmentDetail.Shipment == shipment.ID).Count();
                    int OPSCount = context.GetOrderDetailsQuatityCount(shipment.WeekNo, shipment.WeekendDate).ToList()[0].Value;

                    if (localCount != OPSCount)                    
                    {
                        MessageBox.Show("The sum of quantities of OPS and local database for this shipment does not match. Please synchronize the databases for the selected week.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }*/

                // Load Order deatils grid
                PopulategrdOrderDetailItems();

                // Load Carton details for shipment deatil
                LoadCartonDeatils();                    
            }
        }

        void grdOrderDetailItem_MouseDown(object sender, MouseEventArgs e)
        {
            var index = grdOrderDetailItem.Rows.IndexOf(grdOrderDetailItem.CurrentRow);
            if (e.Button == MouseButtons.Right && index != -1 && grdOrderDetailItem.Rows[index].IsSelected)
            {
                var contextMenuItem = new ContextMenu();
                contextMenuItem.MenuItems.Add(new MenuItem("Generate Polybag Label(s)", new EventHandler(GenerateGridPolybagBarcodeClick)));
                contextMenuItem.MenuItems.Add(new MenuItem("Generate Batch Label(s)", new EventHandler(GenerateGridBatchLabelClick)));
                contextMenuItem.Show(grdOrderDetailItem, new Point(e.X, e.Y));
            }
            else if (e.Button == MouseButtons.Left && index != -1)
            {
                var cell = grdOrderDetailItem.ElementTree.GetElementAtPoint(e.Location) as GridDataCellElement;
                if (cell == null)
                    return;
                var selectedOrderDetailItemIds =
                    grdOrderDetailItem.SelectedRows.Select(row => row.Cells["ID"].Value.ToString()).ToList();
                grdOrderDetailItem.DoDragDrop(selectedOrderDetailItemIds, DragDropEffects.Copy);
            }
               
        }

        void grdOrderDetailItem_SelectionChanged(object sender, EventArgs e)
        {
            lblSelectedRowsvalue.Text = grdOrderDetailItem.SelectedRows.Count.ToString();

           //picPatternImage.Image = null;
           //picVLImage.Image = null;

            if (grdOrderDetailItem.SelectedRows.Count > 0)
            {
                int orderDetailItemId = (int)grdOrderDetailItem.SelectedRows[0].Cells[0].Value;
                string noImgVLlocation = _installedFolder + @"images\NoImage299x261.png";
                string noImgPatternlocation = _installedFolder + @"images\NoImage299x203.png";

                foreach (GridViewRowInfo row in grdOrderDetailItem.Rows)
                {
                    if (row.Cells[0].Value.Equals(orderDetailItemId))
                    {
                        if (row.Cells["VLImage"].Value != string.Empty && row.Cells["PatternImage"].Value != string.Empty)
                        {
                            ImageUrivl = row.Cells["VLImage"].Value.ToString();
                            ImageUriPattern = row.Cells["PatternImage"].Value.ToString();
                        }
                        else if (row.Cells["VLImage"].Value != string.Empty && row.Cells["PatternImage"].Value == string.Empty)
                        {
                            ImageUrivl = row.Cells["VLImage"].Value.ToString();
                            picPatternImage.ImageLocation = noImgPatternlocation;
                        }
                        else if (row.Cells["VLImage"].Value == string.Empty && row.Cells["PatternImage"].Value != string.Empty)
                        {
                            ImageUriPattern = row.Cells["PatternImage"].Value.ToString();
                            picVLImage.ImageLocation = noImgVLlocation;
                        }

                        if (row.Cells["VLImage"].Value == string.Empty && row.Cells["PatternImage"].Value == string.Empty)
                        {
                            picPatternImage.ImageLocation = noImgPatternlocation;
                            picVLImage.ImageLocation = noImgVLlocation;
                        }

                        break;
                    }
                }
            }            
        }

        private void btnAddData_Click(object sender, EventArgs e)
        {
            btnAddData.Enabled = false;
            var currentYear = DateTime.Now.Year;

            var currentShipments = _context.Shipments.ToList();
            if (currentShipments.Any())
            {
                if (currentShipments.Count > 1)
                {
                    var lastWeek = currentShipments.Last().WeekendDate;
                    currentYear = lastWeek.Year == currentShipments[currentShipments.Count() - 2].WeekendDate.Year ?
                                                                                                lastWeek.AddYears(1).Year : lastWeek.Year;
                }
                else  currentYear = currentShipments.Last().WeekendDate.AddYears(1).Year;
            }
            var currentYearStartDate = new DateTime(currentYear, 1, 1);
            var weekCount = Utility.getWeekCountOfAYear(currentYear);
            var day = currentYearStartDate;
            while (currentYearStartDate.DayOfWeek != DayOfWeek.Tuesday)
                currentYearStartDate = currentYearStartDate.AddDays(1);
            for (var i = 1; i <= weekCount; i++)
            {
                var shipment = new Shipment {WeekNo = i, WeekendDate = day};
                _context.Shipments.Add(shipment);
                day = day.AddDays(7);
            }
            _context.SaveChanges();
            btnAddData.Text = "Add data for year " + (currentYear+1);
            btnAddData.Enabled = true;
            LoadDropDown();
        }

        private void btnAddcarton_Click(object sender, EventArgs e)
        {
            // Insert record to the ShipmentDetailCarton table
            var cartonId = int.Parse(ddlCarton.SelectedValue.ToString());

            var carton = (from c in _context.Cartons
                          where c.ID == cartonId
                          select c).FirstOrDefault();
            var sdc = new ShipmentDetailCarton
            {
                ShipmentDetail = (int) grdShipmentDetails.SelectedRows[0].Cells[0].Value,
                Number = _boxCount,
            };
            if (carton != null) carton.ShipmentDetailCartons.Add(sdc);
            _context.ShipmentDetailCartons.Add(sdc);
            _context.SaveChanges();
            AddCartonInfo(sdc.ID, sdc.Carton1, sdc.OrderDeatilItems.Count, sdc.OrderDeatilItems.Where(o => o.IsPolybagScanned == true).ToList().Count);
        }

        void p_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool formShown = false;
            int shipmentDeatilCartonId = 0;
            System.Windows.Forms.Panel panel = null;
            if (sender is System.Windows.Forms.Panel)
            {
                panel = (System.Windows.Forms.Panel)sender;
                shipmentDeatilCartonId = int.Parse(panel.Tag.ToString());
                CartonDeatils cartonDeatils = new CartonDeatils(shipmentDeatilCartonId);
                cartonDeatils.StartPosition = FormStartPosition.CenterParent;
                cartonDeatils.GridShipment = grdShipmentDetails;
                cartonDeatils.MainPanel = pnlmain;
                cartonDeatils.InstalledPath = _installedFolder;
                cartonDeatils.ShowDialog();
                formShown = true;
            }
            else if (sender is System.Windows.Forms.Label)
            {
                panel = (System.Windows.Forms.Panel)((System.Windows.Forms.Label)sender).Parent;
                shipmentDeatilCartonId = int.Parse(panel.Tag.ToString());
                CartonDeatils cartonDeatils = new CartonDeatils(shipmentDeatilCartonId);
                cartonDeatils.StartPosition = FormStartPosition.CenterParent;
                cartonDeatils.GridShipment = grdShipmentDetails;
                cartonDeatils.MainPanel = pnlmain;
                cartonDeatils.InstalledPath = _installedFolder;
                cartonDeatils.ShowDialog();
                formShown = true;
            }
            else if (sender is System.Windows.Forms.PictureBox)
            {
                panel = (System.Windows.Forms.Panel)((System.Windows.Forms.PictureBox)sender).Parent;
                shipmentDeatilCartonId = int.Parse(panel.Tag.ToString());
                CartonDeatils cartonDeatils = new CartonDeatils(shipmentDeatilCartonId);
                cartonDeatils.StartPosition = FormStartPosition.CenterParent;
                cartonDeatils.GridShipment = grdShipmentDetails;
                cartonDeatils.MainPanel = pnlmain;
                cartonDeatils.InstalledPath = _installedFolder;
                cartonDeatils.ShowDialog();
                formShown = true;
            }

            if (formShown)
            {
                // Reload the grid and carton area
                PopulategrdOrderDetailItems();
                RefreshCartonDeatilsAddedCount(shipmentDeatilCartonId, panel);
            }
        }

        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)sender;
            panel.BackColor = Color.LightSkyBlue;

            if (e.Data.GetDataPresent(typeof(List<string>)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void p_DragLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)sender;
            //panel.BackColor = Color.White;            

            Label lblCartonInfo = panel.Controls.Find("lblCartonInfo", true).FirstOrDefault() as Label;
            Label lblCartonItemsCount = panel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
            Label lblCartonFilledItemsCount = panel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

            int filledCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.IndexOf(" "), lblCartonFilledItemsCount.Text.LastIndexOf(" ") - lblCartonFilledItemsCount.Text.IndexOf(" ")));
            int itemsCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

            ChangeCartonColor(panel, filledCount, itemsCount);                                                 
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(List<string>)))
            {
                IndicoPackingEntities context = new IndicoPackingEntities();
                List<string> ids = (List<string>)e.Data.GetData(typeof(List<string>));
                int shipmentDetailCartonId = 0;
                ShipmentDetailCarton shipmentDetailCarton = null;
                Panel panel = null;

                if (sender is System.Windows.Forms.Panel)
                {
                    panel = (System.Windows.Forms.Panel)sender;
                    shipmentDetailCartonId = int.Parse(panel.Tag.ToString());                    

                    shipmentDetailCarton = (from o in context.ShipmentDetailCartons
                                            where o.ID == shipmentDetailCartonId
                                            select o).SingleOrDefault();                    
                }
                else
                {
                    MessageBox.Show("Can't find the dropped Panel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                foreach (string id in ids)
                {
                    var intID = int.Parse(id);
                    OrderDeatilItem order = (from o in context.OrderDeatilItems
                                             where o.ID == intID
                                             select o).SingleOrDefault();
                    order.ShipmentDetailCarton = shipmentDetailCarton.ID;

                    // Remove rows from grid after drop.
                    GridViewRowInfo row = grdOrderDetailItem.Rows
                        .Cast<GridViewRowInfo>()
                        .Where(r => r.Cells["ID"].Value.ToString().Equals(id))
                        .FirstOrDefault();

                    grdOrderDetailItem.Rows.Remove(row);
                }

                context.SaveChanges();

                int filledCount = context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId && o.IsPolybagScanned == true).ToList().Count;
                int itemsCount = context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == shipmentDetailCartonId).ToList().Count;

                ChangeCartonColor(panel, filledCount, itemsCount);

                // Reload the grid with data where ShipmentDetailCarton is null for OrderDeatilItems
                //PopulategrdOrderDetailItems();

                lblCurrentCountValue.Text = grdOrderDetailItem.Rows.Count.ToString();

                // Refesh the Added item count of the Panel
                RefreshCartonDeatilsAddedCount(shipmentDetailCartonId, panel);               

                // Status text of Parent form
                ((ParentForm)MdiParent).toolStripStatusLabel.Text = string.Format("Added {0} item(s) to the carton number {1}", ids.Count.ToString(), shipmentDetailCarton.Number.ToString());
            }
        }

        private void btnLoadLocal_Click(object sender, EventArgs e)
        {
            ClearCartonDetailsArea();
            grdOrderDetailItem.DataSource = null;
            grdOrderDetailItem.Rows.Clear();
            ChangeStateOfControls(false);

            var shipmentId  = (int)ddlWeekEndDate.SelectedValue;

            if (PoulateGVShipmentDetails(shipmentId) > 0)
            {
                //MessageBox.Show("Data loaded from the local database without synchronizing the databases.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Check the orderdeatil quantity count match the both databses or not. If not then ask to synchronize the databses
                Shipment shipment = (from s in _context.Shipments
                                     where s.ID == shipmentId
                                     select s).FirstOrDefault();

                int localCount = _context.OrderDeatilItems.Where(o => o.ShipmentDetail.Shipment == shipment.ID).Count();
                int OPSCount = 0;//context.GetOrderDetailsQuatityCount(shipment.WeekNo, shipment.WeekendDate).ToList()[0].Value;

                if (localCount != OPSCount)
                {
                    MessageBox.Show("The sum of quantities of OPS and local database for this shipment does not match. Please synchronize the databases for the selected week.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("There are no data to be loaded for the selected week from the local database. Try to synchronize the databases before loading from the local database for the selected week.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }           

            if (LoginInfo.Role == FillingCordinator)          
                HideControlsForUsers();
            else
                ChangeStateOfControls(true);
        }

        private void btnSynchronize_Click(object sender, EventArgs e)
        {

            var progressForm = new frmProgress
            {
                Message = "Please wait. Synchronizing in progress...",
                StartPosition = FormStartPosition.CenterScreen
            };
            progressForm.Show();
            progressForm.Refresh();
            try
            {
                ClearCartonDetailsArea();
                grdOrderDetailItem.DataSource = null;
                grdOrderDetailItem.Rows.Clear();
                ChangeStateOfControls(false);

                var shipmentId = (int)ddlWeekEndDate.SelectedValue;// var shipmentId= ddlWeekEndDate.SelectedValue as 

                var shipment = (from s in _context.Shipments
                                     where s.ID == shipmentId
                                     select s).FirstOrDefault();

                var syn = new Synchronize();
                syn.Sync(shipment.WeekNo, shipment.WeekendDate);

                PoulateGVShipmentDetails(shipmentId);

                progressForm.Hide();

                MessageBox.Show("Successfully synchronized the data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                progressForm.Hide();

                MessageBox.Show("Error occured while synchronizing the databases.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressForm.Dispose();
                ChangeStateOfControls(true);
            }
        }

        void p_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys != Keys.Control)
            {
                foreach (System.Windows.Forms.Control control in pnlmain.Controls)
                {
                    if (control is System.Windows.Forms.Panel)
                    {
                        //control.BackColor = Color.White;
                        System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)control;
                        panel.BorderStyle = BorderStyle.FixedSingle;
                        //panel.Paint += panel_Paint2;
                        //control.Paint += p_Paint2;
                        Label lblCartonInfo = panel.Controls.Find("lblCartonInfo", true).FirstOrDefault() as Label;
                        Label lblCartonItemsCount = panel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
                        Label lblCartonFilledItemsCount = panel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                        int filledCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.IndexOf(" "), lblCartonFilledItemsCount.Text.LastIndexOf(" ") - lblCartonFilledItemsCount.Text.IndexOf(" ")));
                        int itemsCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

                        ChangeCartonColor(panel, filledCount, itemsCount);
                    }
                }
            }

            if (sender is System.Windows.Forms.Panel)
            {
                System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)sender;
                panel.BackColor = SystemColors.GradientActiveCaption;
                panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                //panel.Paint += panel_Paint;
            }
            else if (sender is System.Windows.Forms.PictureBox)
            {
                System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)((System.Windows.Forms.PictureBox)sender).Parent;
                panel.BackColor = SystemColors.GradientActiveCaption;
                panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                //panel.Paint += panel_Paint;
            }
            else if (sender is System.Windows.Forms.Label)
            {
                System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)((System.Windows.Forms.Label)sender).Parent;
                panel.BackColor = SystemColors.GradientActiveCaption;
                panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                //panel.Paint += panel_Paint;
            }
        }

        void panel_Paint2(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)sender;
            if (panel.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel.ClientSize.Width - thickness,
                                                              panel.ClientSize.Height - thickness));
                }
            }
        }

        void panel_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Panel panel = (System.Windows.Forms.Panel)sender;
            if (panel.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 3;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(SystemColors.Highlight, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel.ClientSize.Width - thickness,
                                                              panel.ClientSize.Height - thickness));
                }
            }
        }

        private void ddlWeekEndDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearCartonDetailsArea();
            grdOrderDetailItem.DataSource = grdShipmentDetails.DataSource = null;
            grdShipmentDetails.Rows.Clear();
            grdOrderDetailItem.Rows.Clear();
            grdOrderDetailItem.Columns.Clear();
            picPatternImage.Image = null;
            picVLImage.Image = null;
            lblCurrentCountValue.Text = "0";
        }

        public void btnFillCarton_Click(object sender, EventArgs e)
        {
            int shipmentId = (int)ddlWeekEndDate.SelectedValue;// (int)((System.Collections.Generic.KeyValuePair<int, string>)ddlWeekEndDate.SelectedValue).Key;

            foreach (Form frm in ParentForm.MdiChildren)
            {
                if (frm is IndicoPacking.FillingCarton)
                {
                    frm.BringToFront();
                    return;
                }
            }

            FillingCarton frmFC = new FillingCarton();
            frmFC.MdiParent = ParentForm;
            frmFC.MainPanel = pnlmain;
            frmFC.ClickedCartonShipmentDeatailCartonId = null;
            frmFC.InstalledPath = _installedFolder;
            frmFC.GridShipment = grdShipmentDetails;
            frmFC.StartPosition = FormStartPosition.CenterScreen;
            frmFC.Show();
            PopulategrdOrderDetailItems();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnFillingFirstScanningPolybags_Click(object sender, EventArgs e)
        {
            foreach (Form frm in ParentForm.MdiChildren)
            {
                if (frm is IndicoPacking.FillingFirstScanningPolybags)
                {
                    frm.BringToFront();
                    return;
                }
            }

            FillingFirstScanningPolybags frmFirstScanPolybags = new FillingFirstScanningPolybags();
            frmFirstScanPolybags.StartPosition = FormStartPosition.CenterScreen;
            frmFirstScanPolybags.MdiParent = ParentForm;
            frmFirstScanPolybags.ShipmentId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["Shipment"].Value.ToString());
            frmFirstScanPolybags.MainPanel = pnlmain;
            frmFirstScanPolybags.GridShipment = grdShipmentDetails;
            frmFirstScanPolybags.InstalledPath = _installedFolder;
            frmFirstScanPolybags.Show();
        }

        void txtPONumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string poNumber = "PO-" + txtPONumber.Text;

                IndicoPackingEntities context = new IndicoPackingEntities();

                int shipment = ((System.Collections.Generic.KeyValuePair<int, string>)(ddlWeekEndDate.SelectedItem)).Key;

                var item = (from odi in context.OrderDeatilItems
                            where odi.PurchaseOrder == poNumber && odi.ShipmentDetail.Shipment == shipment
                            select odi).FirstOrDefault();

                if (item == null)
                    MessageBox.Show("Purchase order number cannot be found. Please enter without 'PO-'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    int shipmentDetailId = item.ShipmentDeatil;

                    foreach (DataGridViewRow row in grdShipmentDetails.Rows)
                    {
                        if (row.Cells["ID"].Value.Equals(shipmentDetailId))
                        {
                            row.Selected = true;
                            dGVshipmentdetail_CellClick(grdShipmentDetails, new DataGridViewCellEventArgs(0, 0));
                        }
                    }

                    int i = 0;
                    foreach (GridViewRowInfo row in grdOrderDetailItem.Rows)
                    {
                        if (row.Cells["PurchaseOrder"].Value.Equals(poNumber))
                        {
                            row.IsSelected = true;
                            GridTableElement tableElement = grdOrderDetailItem.CurrentView as GridTableElement;

                            if (tableElement != null && row != null && i == 0)
                            {
                                tableElement.ScrollToRow(row);
                            }
                            i++;
                        }
                    }
                }
            }
        }

        private void OnGeneratePackingListButtonClick(object sender, EventArgs e)
        {
            GeneratePackingListForSelectedShipmentDetail();
        }

        #endregion

        #region Methods

        #region Private

        private void AddCartonInfo(int shipmentDeatilCartonId, Carton carton, int orderDetailItemsCount, int filledCount)
        {
            var imgCartonLocation = _installedFolder + @"images\openbox.png";

            var pbxCarton = new PictureBox { Name = "picBox" };
            var p = new Panel { Name = "pnlCarton" + shipmentDeatilCartonId};
            if (filledCount == orderDetailItemsCount && filledCount != 0 && orderDetailItemsCount != 0)
            {
                imgCartonLocation = _installedFolder + @"images\closedbox.png";
            }
            pbxCarton.Image = Image.FromFile(imgCartonLocation);
            pbxCarton.AutoSize = true;
            pbxCarton.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxCarton.Width = 45;
            pbxCarton.Height = 30;
            pbxCarton.Top = p.Top + 5;
            pbxCarton.Left = p.Left;
            p.Size = new Size(PanelWidth, PanelHeight);
            p.BackColor = Color.White;
            p.Select();

     
            // Carton number
            var lblCartonNumber = new Label
            {
                Text = _boxCount.ToString(),
                Top = p.Top + PboxHeight + 4,
                Left = p.Left + 4,
                AutoSize = true,
                ForeColor = Color.DarkBlue
            };
            lblCartonNumber.Click += p_Click;
            //lblCartonNumber.MouseDoubleClick += p_MouseDoubleClick;

            // Carton name info
            var lblCartonInfo = new Label
            {
                Name = "lblCartonInfo",
                Text = carton.Name,
                Top = p.Top + PboxHeight + 10,
                Left = p.Left + 55,
                AutoSize = true
            };
            lblCartonInfo.Click += p_Click;
            lblCartonInfo.MouseDoubleClick += p_MouseDoubleClick;

            // Carton Items count
            var lblCartonItemsCount = new Label
            {
                Name = "lblCartonItemsCount",
                Text = "Added: " + orderDetailItemsCount + " (" + carton.Qty + ")",
                Top = p.Top + PboxHeight + 30,
                Left = p.Left + 55,
                AutoSize = true
            };

            lblCartonItemsCount.Click += p_Click;
            lblCartonItemsCount.MouseDoubleClick += p_MouseDoubleClick;

            // Carton Filled Items count
            Label lblCartonFilledItemsCount = new Label();
            lblCartonFilledItemsCount.Name = "lblCartonFilledItemsCount";
            lblCartonFilledItemsCount.Text = "Filled: " + filledCount.ToString() + " (" + orderDetailItemsCount.ToString() + ")";
            lblCartonFilledItemsCount.Top = p.Top + PboxHeight + 50;
            lblCartonFilledItemsCount.Left = p.Left + 55;
            lblCartonFilledItemsCount.AutoSize = true;
            lblCartonFilledItemsCount.Click += p_Click;
            lblCartonFilledItemsCount.MouseDoubleClick += p_MouseDoubleClick;

            MenuButton menuButton = new MenuButton();
            menuButton.Name = "mnuButton";
            menuButton.Top = p.Top + 5;
            menuButton.Left = p.Left + 75;
            menuButton.Size = new Size(60, 25);
            menuButton.Text = "Menu";
            menuButton.Menu = _contextMenu;
            menuButton.Click += menuButton_Click;
            menuButton.Tag = (filledCount > 0);

            Tuple<int, int> pos = GetPosition(_boxCount++);
            //p.Location = new Point(pos.Item1, pos.Item2);
            p.Top = pos.Item2;
            p.Left = pos.Item1;
            p.Controls.Add(pbxCarton);
            pbxCarton.Click += p_Click;
            pbxCarton.MouseDoubleClick += p_MouseDoubleClick;

            lblCartonNumber.Size = new Size(15, 30);
            lblCartonNumber.Font = new Font("Arial", 20, FontStyle.Bold);
            p.Controls.Add(lblCartonNumber);

            // lblCartonInfo.Size = new Size(60, 20);
            lblCartonInfo.Font = new Font("Arial", 10);
            p.Controls.Add(lblCartonInfo);
            lblCartonItemsCount.Font = new Font("Arial", 10);
            p.Controls.Add(lblCartonItemsCount);
            lblCartonFilledItemsCount.Font = new Font("Arial", 10);
            p.Controls.Add(lblCartonFilledItemsCount);
            p.Controls.Add(menuButton);

            p.Click += p_Click;
            p.MouseDoubleClick += p_MouseDoubleClick;
            // Disable drag and drop for filling cordinator.
            if (LoginInfo.Role != FillingCordinator)
            {
                p.AllowDrop = true;
                p.DragEnter += Panel_DragEnter;
                p.DragDrop += Panel_DragDrop;
                p.DragLeave += p_DragLeave;
            }
            p.Tag = shipmentDeatilCartonId;
            p.BorderStyle = BorderStyle.FixedSingle;
            if (pnlmain.VerticalScroll.Value > 0)
            {
                p.Top = p.Top - pnlmain.VerticalScroll.Value;
            }

            ChangeCartonColor(p, filledCount, orderDetailItemsCount);

            pnlmain.Controls.Add(p);

        }

        private void LoadCartonDeatils()
        {
            var shipmentDetailId = int.Parse(grdShipmentDetails.SelectedRows[0].Cells["ID"].Value.ToString());
            _context = new IndicoPackingEntities();
            var cartons = (from sc in _context.ShipmentDetailCartons
                                                  where sc.ShipmentDetail == shipmentDetailId
                                                  select sc).ToList();

            // Now iterate thorugh the cartons
            foreach (var sdc in cartons)
            {
                AddCartonInfo(sdc.ID, sdc.Carton1, sdc.OrderDeatilItems.Count, sdc.OrderDeatilItems.Where(o => o.IsPolybagScanned).ToList().Count);
            }
        }

        private void RefreshCartonDeatilsAddedCount(int shipmentDeatilCartonId, System.Windows.Forms.Panel panel)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            ShipmentDetailCarton carton = (from sc in context.ShipmentDetailCartons
                                           where sc.ID == shipmentDeatilCartonId
                                           select sc).FirstOrDefault();           

            List<OrderDeatilItem> items = (from odi in context.OrderDeatilItems
                                           where odi.ShipmentDetailCarton == shipmentDeatilCartonId && odi.IsPolybagScanned == true
                                           select odi).ToList();

            Label lblCartonItemsCount = panel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
            if (lblCartonItemsCount != null)
            {
                lblCartonItemsCount.Text = "Added: " + carton.OrderDeatilItems.Count.ToString() + " (" + carton.Carton1.Qty.ToString() + ")";
            }

            Label lblCartonFilledItemsCount = panel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;
            if (lblCartonFilledItemsCount != null)
            {
                lblCartonFilledItemsCount.Text = "Filled: " + items.Count.ToString() + " (" + carton.OrderDeatilItems.Count.ToString() + ")";
            }

            if (items.Count != carton.OrderDeatilItems.Count || items.Count == 0)
            {
                PictureBox picBox = panel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                Bitmap b = new Bitmap(_installedFolder + @"images\openbox.png");
                picBox.BackgroundImage = b;
            }

            else if (items.Count == carton.OrderDeatilItems.Count && items.Count != 0 && carton.OrderDeatilItems.Count != 0)
            {
                PictureBox picBox = panel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                Bitmap b = new Bitmap(_installedFolder + @"images\closedbox.png");
                picBox.BackgroundImage = b;
            }
        }

        private void ClearCartonDetailsArea()
        {
            _boxCount = 1;
            _even = 1;
            _odd = 2;
            pnlmain.Controls.Clear();
        }

        private void ChangeStateOfControls(bool enabled)
        {
            btnGenerateAllBatchLabels.Enabled = btnClearFilledCartons.Enabled = btnFillCarton.Enabled = btnClearAllCartonItems.Enabled = btnClearCartonArea.Enabled = btnGenerateCartonBarcods.Enabled = btnGeneratePolybagBarcods.Enabled = ddlCarton.Enabled = btnAddcarton.Enabled = btnAddData.Enabled = btnLoadLocal.Enabled = btnSynchronize.Enabled = btnFillingFirstScanningPolybags.Enabled = txtPONumber.Enabled = enabled;
        }

        private int GetWeeksInYear(int year, DateTime lastDayofYear)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            System.Globalization.Calendar cal = dfi.Calendar;
            return cal.GetWeekOfYear(lastDayofYear, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        private void LoadDropDown()
        {
            //rewritten by rusith
            var currentShipments = _context.Shipments.ToList();
            var weekNumberSource= currentShipments
                                 .ToDictionary(
                                    shipment => shipment.ID,
                                    shipment => string.Format(" week : {0}{1}year : {2}", shipment.WeekNo,shipment.WeekNo>9?"  ":"    " ,shipment.WeekendDate.Year));

            ddlWeekEndDate.DataSource = new BindingSource(weekNumberSource, null);
            ddlWeekEndDate.DisplayMember = "Value";
            ddlWeekEndDate.ValueMember = "Key";
        }

        private int PoulateGVShipmentDetails(int shipmentId)
        {
            var shipmentDeatils = (from sd in _context.ShipmentDetails
                                   where sd.Shipment == shipmentId
                                   select new ShipmentDetailView {  ID = sd.ID, 
                                                                    Shipment = sd.Shipment, 
                                                                    IndicoDistributorClientAddress = sd.IndicoDistributorClientAddress, 
                                                                    ShipTo = sd.ShipTo, 
                                                                    Port = sd.Port, 
                                                                    ShipmentMode = sd.ShipmentMode, 
                                                                    PriceTerm = sd.PriceTerm, 
                                                                    ETD = sd.ETD, 
                                                                    Qty = sd.Qty, 
                                                                    QuantityFilled = sd.QuantityFilled, 
                                                                    QuantityYetToBeFilled = sd.QuantityYetToBeFilled }).ToList();

            grdShipmentDetails.DataSource = shipmentDeatils;
            if (shipmentDeatils.Count > 0)
            {
                dGVshipmentdetail_CellClick(grdShipmentDetails, new DataGridViewCellEventArgs(0, 0));
            }

            if (grdShipmentDetails.Columns["ID"] != null)
            {
                grdShipmentDetails.Columns["ID"].Width = 65;
            }

            if (grdShipmentDetails.Columns["Shipment"] != null)
            {
                grdShipmentDetails.Columns["Shipment"].Width = 65;
            }

            if (grdShipmentDetails.Columns["Port"] != null)
            {
                grdShipmentDetails.Columns["Port"].Width = 150;
            }

            if (grdShipmentDetails.Columns["IndicoDistributorClientAddress"] != null)
            {
                grdShipmentDetails.Columns["IndicoDistributorClientAddress"].HeaderText = "Distributor Client Address";
            }

            if (grdShipmentDetails.Columns["ShipTo"] != null)
            {
                grdShipmentDetails.Columns["ShipTo"].HeaderText = "Ship To";
                grdShipmentDetails.Columns["ShipTo"].Width =140;
            }

            if (grdShipmentDetails.Columns["ShipmentMode"] != null)
            {
                grdShipmentDetails.Columns["ShipmentMode"].HeaderText = "Shipment Mode";
                grdShipmentDetails.Columns["ShipmentMode"].Width = 70;
            }

            if (grdShipmentDetails.Columns["PriceTerm"] != null)
            {
                grdShipmentDetails.Columns["PriceTerm"].HeaderText = "Price Term";
                grdShipmentDetails.Columns["PriceTerm"].Width = 70;
            }

            if (grdShipmentDetails.Columns["Qty"] != null)
            {
                grdShipmentDetails.Columns["Qty"].HeaderText = "Quantity";
                grdShipmentDetails.Columns["Qty"].Width = 70;
            }

            if (grdShipmentDetails.Columns["QuantityFilled"] != null)
            {
                grdShipmentDetails.Columns["QuantityFilled"].HeaderText = "Quantity Filled";
                grdShipmentDetails.Columns["QuantityFilled"].Width = 70;
            }

            if (grdShipmentDetails.Columns["QuantityYetToBeFilled"] != null)
            {
                grdShipmentDetails.Columns["QuantityYetToBeFilled"].HeaderText = "Quantity Yet To Be Filled";
            }

            return shipmentDeatils.Count;
        }

        private void PopulategrdOrderDetailItems()
        {
            if (grdShipmentDetails.SelectedRows.Count <= 0) return;
            var shipmentDetailId = (int)grdShipmentDetails.SelectedRows[0].Cells["ID"].Value;
            var orderDeatilItems = (from odi in _context.OrderDeatilItems
                where odi.ShipmentDeatil == shipmentDetailId && odi.ShipmentDetailCarton == null
                select
                    new
                    {
                        odi.ID,
                        odi.PurchaseOrder,
                        odi.IndicoOrderID,
                        odi.IndicoOrderDetailID,
                        odi.OrderType,
                        odi.VisualLayout,
                        odi.Pattern,
                        odi.Fabric,
                        odi.SizeDesc,
                        odi.SizeQty,
                        odi.SizeSrno,
                        odi.Distributor,
                        odi.Client,
                        odi.PrintedCount,
                        odi.PatternImage,
                        odi.VLImage
                    }).ToList();

            typeof(RadGridView).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                grdOrderDetailItem,
                new object[] { true });

            grdOrderDetailItem.DataSource = orderDeatilItems;
            lblCurrentCountValue.Text = orderDeatilItems.Count.ToString();
            //grdOrderDetailItem.Columns[0].Width =
            //    grdOrderDetailItem.Columns[0].AutoSizeMode = BestFitColumnMode.SummaryRowCells;
          //  resizeGridViewColumns(ref grdOrderDetailItem);
            if (grdOrderDetailItem.Columns["ID"] != null)
            {
                grdOrderDetailItem.Columns["ID"].Width = 65;
                grdOrderDetailItem.Columns["ID"].IsVisible = false;
            }

            if (grdOrderDetailItem.Columns["PurchaseOrder"] != null)
            {
                grdOrderDetailItem.Columns["PurchaseOrder"].HeaderText = "Purchase Order #";
                grdOrderDetailItem.Columns["PurchaseOrder"].Width = 85;
            }

            if (grdOrderDetailItem.Columns["IndicoOrderID"] != null)
            {
                grdOrderDetailItem.Columns["IndicoOrderID"].HeaderText = "Order ID";
                grdOrderDetailItem.Columns["IndicoOrderID"].Width = 85;
                grdOrderDetailItem.Columns["IndicoOrderID"].IsVisible = false;
            }

            if (grdOrderDetailItem.Columns["IndicoOrderDetailID"] != null)
            {
                grdOrderDetailItem.Columns["IndicoOrderDetailID"].HeaderText = "Order Detail ID";
                grdOrderDetailItem.Columns["IndicoOrderDetailID"].Width = 100;
            }

            if (grdOrderDetailItem.Columns["OrderType"] != null)
            {
                grdOrderDetailItem.Columns["OrderType"].HeaderText = "Order Type";
                grdOrderDetailItem.Columns["OrderType"].Width = 80;
            }

            if (grdOrderDetailItem.Columns["VisualLayout"] != null)
            {
                grdOrderDetailItem.Columns["VisualLayout"].HeaderText = "Visual Layout";
                grdOrderDetailItem.Columns["VisualLayout"].Width = 100;
            }

            if (grdOrderDetailItem.Columns["Pattern"] != null)
            {
                grdOrderDetailItem.Columns["Pattern"].Width = 100;
            }

            if (grdOrderDetailItem.Columns["Fabric"] != null)
            {
                grdOrderDetailItem.Columns["Fabric"].Width = 95;
            }

            if (grdOrderDetailItem.Columns["Distributor"] != null)
            {
                grdOrderDetailItem.Columns["Distributor"].Width = 120;
            }

            if (grdOrderDetailItem.Columns["Client"] != null)
            {
                grdOrderDetailItem.Columns["Client"].Width = 120;
            }

            if (grdOrderDetailItem.Columns["SizeDesc"] != null)
            {
                grdOrderDetailItem.Columns["SizeDesc"].HeaderText = "Size Desc";
                grdOrderDetailItem.Columns["SizeDesc"].Width = 60;
            }

            if (grdOrderDetailItem.Columns["SizeQty"] != null)
            {
                grdOrderDetailItem.Columns["SizeQty"].HeaderText = "Qty";
                grdOrderDetailItem.Columns["SizeQty"].Width = 45;
            }

            if (grdOrderDetailItem.Columns["SizeSrno"] != null)
            {
                grdOrderDetailItem.Columns["SizeSrno"].HeaderText = "Size No";
                grdOrderDetailItem.Columns["SizeSrno"].Width = 45;
            }

            if (grdOrderDetailItem.Columns["PrintedCount"] != null)
            {
                grdOrderDetailItem.Columns["PrintedCount"].HeaderText = "Printed Count";
                grdOrderDetailItem.Columns["PrintedCount"].Width = 75;
            }

            if (grdOrderDetailItem.Columns["PatternImage"] != null)
            {
                grdOrderDetailItem.Columns["PatternImage"].IsVisible = false;
            }

            if (grdOrderDetailItem.Columns["VLImage"] != null)
            {
                grdOrderDetailItem.Columns["VLImage"].IsVisible = false;
            }

            /*Image image = Image.FromFile(installedFolder + @"\images\eyeOpen.png");
                  Bitmap b = new Bitmap(@"C:\Projects\IndicoPacking\IndicoPacking\IndicoPacking\images\eyeOpen.png");
                  grdOrderDetailItem.Columns.Add("colTest", "Preview");

                  DataGridViewImageColumn columnVLPreview = new DataGridViewImageColumn();
                  columnVLPreview.HeaderText = "VL Preview";
                  columnVLPreview.Image = Image.FromFile(@"C:\Projects\IndicoPacking\IndicoPacking\IndicoPacking\images\eyeOpen.png");
                  columnVLPreview.Name = "colVLPreview";
                  grdOrderDetailItem.Columns.Add(columnVLPreview);

                  DataGridViewImageColumn columnPatternPreview = new DataGridViewImageColumn();
                  columnPatternPreview.HeaderText = "Pattern Preview";
                  columnPatternPreview.Image = Image.FromFile(@"C:\Projects\IndicoPacking\IndicoPacking\IndicoPacking\images\eyeOpen.png");
                  columnPatternPreview.Name = "colPatternPreview";
                  grdOrderDetailItem.Columns.Add(columnPatternPreview);

                 if (!grdOrderDetailItem.Columns.Contains("PatternPreview"))
                 {
                     DataGridViewImageCell imgcell = new DataGridViewImageCell();
                     DataGridViewColumn Patterncolumn = new DataGridViewColumn(imgcell);
                     Patterncolumn.Name = "PatternPreview";
                     grdOrderDetailItem.Columns.Add(Patterncolumn);
                     grdOrderDetailItem.Columns["PatternPreview"].Visible = false;

                     DataGridViewImageCell cell5 = new DataGridViewImageCell();
                     DataGridViewColumn VLcolumn = new DataGridViewColumn(imgcell);
                     VLcolumn.Name = "VLPreview";
                     VLcolumn.HeaderText = "VL Preview";
                     grdOrderDetailItem.Columns.Add(VLcolumn);

                     foreach (DataGridViewRow row in grdOrderDetailItem.Rows)
                     {
                         DataGridViewCell Patterncell = row.Cells["PatternImage"];
                         DataGridViewCell VLcell = row.Cells["VLImage"];
                         DataGridViewImageCell imgPatterncell = (DataGridViewImageCell)row.Cells["PatternPreview"];
                         DataGridViewImageCell imgVLcell = (DataGridViewImageCell)row.Cells["VLPreview"];

                         imgPatterncell.Value = (Patterncell.Value != null) ?
                             Image.FromFile(installedFolder + @"images\eyeOpen.png") :
                             Image.FromFile(installedFolder + @"images\eyeClose.png");

                         imgVLcell.Value = (VLcell.Value != null) ?
                             Image.FromFile(installedFolder + @"images\eyeOpen.png") :
                             Image.FromFile(installedFolder + @"images\eyeClose.png");
                     }
                 }   

                 grdOrderDetailItem.CellMouseEnter += grdOrderDetailItem_CellMouseEnter;
                 grdOrderDetailItem.CellMouseLeave += grdOrderDetailItem_CellMouseLeave;*/
        }

        private Tuple<int, int> GetPosition(int count)
        {
            int x;
            int y;

            if (count % 2 == 0)
            {
                x = 168;
                y = ((count - _odd++) * BoxHeight) + Gap * _even - 6;
                
                if (count == 2)
                {
                    y = 4;
                }
            }
            else
            {
                x = 3;
                y = ((count - _even++) * BoxHeight) + Gap * _even - 6;
                
                if (count == 1)
                {
                    y = 4;
                }
            }

            return new Tuple<int, int>(x, y);
        }

        private void UpdateShipmentDetailQty(int shipmentDeatil, int qty)
        {
           ShipmentDetail shipment = (from s in _context.ShipmentDetails
                                       where s.ID == shipmentDeatil
                                       select s).FirstOrDefault();
           
            shipment.QuantityFilled = shipment.QuantityFilled + qty;
            shipment.QuantityYetToBeFilled = shipment.Qty - shipment.QuantityFilled;
            _context.SaveChanges();

            UpdateFilledCount(shipment.QuantityFilled, shipment.QuantityYetToBeFilled);           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ddlCarton_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UpdateFilledCount(int filledCount, int yetToBeFilledCount)
        {
            DataGridViewRow row = grdShipmentDetails.SelectedRows[0];
            row.Cells[9].Value = filledCount;
            row.Cells[10].Value = yetToBeFilledCount;
            grdShipmentDetails.EndEdit();
            row.Cells[9].Value = filledCount;
            row.Cells[10].Value = yetToBeFilledCount;
            grdShipmentDetails.EndEdit();
        }

        private void HideControlsForUsers()
        {
            // Hide controls to filling cordinator.                  
            btnLoadLocal.Enabled = true;
            btnClearFilledCartons.Enabled = true;
            btnFillCarton.Enabled = true;
        }

        private void ChangeCartonColor(Panel carton, int filledCount, int itemsCount)
        {
            //Label lblCartonInfo = carton.Controls.Find("lblCartonInfo", true).FirstOrDefault() as Label;
            //Label lblCartonItemsCount = carton.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
            //Label lblCartonFilledItemsCount = carton.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

            //int filledCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.IndexOf(" "), lblCartonFilledItemsCount.Text.LastIndexOf(" ") - lblCartonFilledItemsCount.Text.IndexOf(" ")));
            //int itemsCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

            if (filledCount == 0)
            {
                carton.BackColor = Constants.NOT_FILLED_CARTON;
            }
            else if (filledCount >= 1 && itemsCount != filledCount)
            {
                carton.BackColor = Constants.HALF_FILLED_CARTON;
            }
            else if (itemsCount == filledCount)
            {
                carton.BackColor = Constants.FILLED_CARTON;
            }
        }
       
        #endregion

        private void resizeGridViewColumns(ref Telerik.WinControls.UI.RadGridView redGridView)
        {
            if (redGridView == null) return;
            foreach (var column in redGridView.Columns)
            {
                column.Width = column.Name.Length*8;
            }
        }

        private void GeneratePackingListForSelectedShipmentDetail()
        {

            if (grdShipmentDetails.SelectedRows.Count <= 0)
                return;
            var shipmentDetail = grdShipmentDetails.SelectedRows[0].Cells["ID"].Value as int?;
            if (shipmentDetail == null || shipmentDetail < 1)
                return;
            var cartons = _context.ShipmentDetailCartons.Where(sdc => sdc.ShipmentDetail == shipmentDetail).ToList();
            if (cartons.Count < 1)
                return;
            var dialog = new CartonSelectWindow(cartons);
            var dialogResult = dialog.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;
            List<ShipmentDetailCartonModel> details;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoPacking"].ConnectionString))
            {
                details = connection.Query<ShipmentDetailCartonModel>(string.Format("EXEC [dbo].[SPC_GetPackingListDetailForGivenCartons] {0},'{1}'", shipmentDetail, dialog.SelectedCartons.Aggregate("", (current, id) => current + "," + id))).ToList();
            }
            var packingListReport = new LocalReport { ReportPath = @"Reports\PackingListReport.rdlc" };
            var packingListDataSource = new ReportDataSource("PackingListData", details);
            packingListReport.DataSources.Add(packingListDataSource);

            try
            {

                while (true)
                {
                    var renderedReport = new byte[] { };
                    try
                    {
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        renderedReport = packingListReport.Render("PDF",
                            @"<DeviceInfo><OutputFormat>PDF</OutputFormat><HumanReadablePDF>False</HumanReadablePDF></DeviceInfo>",
                            out mimeType,
                            out encoding,
                            out fileNameExtension,
                            out streamids,
                            out warnings);
                    }
                    catch (Exception)
                    {
                        throw new Exception("CRR");
                    }
                    var date = DateTime.Now;
                    var fileName = string.Format("PackingListReport_{0}_{1}_{2}_{3}_{4}_{5}_.pdf", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
                    try
                    {
                        using (var fileStream = new FileStream(@"Reports\" + fileName, FileMode.Create))
                        {
                            fileStream.Write(renderedReport, 0, renderedReport.Length);
                        }
                    }
                    catch (IOException)
                    {

                        var messageResult = MessageBox.Show(
                            string.Format(
                                "Cannot write report file ({0}) to the disk.\nmake sure any program not using it",
                                 fileName), "cannot write the report file to the disk", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        if (messageResult == DialogResult.Retry)
                            continue;
                        throw new Exception("CWF");
                    }

                    if (File.Exists(@"Reports\" + fileName))
                    {
                        System.Diagnostics.Process.Start(@"Reports\" + fileName);
                    }
                    break;
                }

            }
            catch (Exception e)
            {
                if (e.Message == "CRR")
                    MessageBox.Show("An error occurred. when trying to generate the report",
                        "cannot generate the report", MessageBoxButtons.OK, MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);

            }

        }

        #endregion
    }
}
