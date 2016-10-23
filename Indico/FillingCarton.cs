using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
using IndicoPacking.Model;
using IndicoPacking.Common;

namespace IndicoPacking
{
    public partial class FillingCarton : Form
    {
        #region Constants

        const UserType FillingCordinator = UserType.FillingCordinator;

        #endregion

        #region Fields

        private readonly string _installedFolder = string.Empty;
        private string _imageUrlvl;
        IndicoPackingEntities _context;
        int _shipmentDeatilCartonId;
        int _orderDetailItemId;
        Label _lblCartonFilledItemsCount;
        int _filledCount;
        int _countSupposedToFill;

        private readonly Semaphore _vlImageloadSemaphoreSemaphore = new Semaphore(1, 1);

        #endregion

        #region Properties

        public Panel MainPanel { get; set; }

        public DataGridView GridShipment { get; set; }

        public int? ClickedCartonShipmentDeatailCartonId { get; set; }

        public string InstalledPath { get; set; }

        public string ImageURIVL
        {
            set
            {
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
                        stream?.Close();
                    }
                    catch (Exception e)
                    {
                        picVLImage.Image = null;
                    }
                    _vlImageloadSemaphoreSemaphore.Release();
                }).ContinueWith((r) => { }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            //set
            //{
            //    var request = WebRequest.Create(value);

            //    using (var response = request.GetResponse())
            //    using (var stream = response.GetResponseStream())
            //    {
            //        picVLImage.Image = Bitmap.FromStream(stream);
            //    }
            //}
        }

        #endregion

        #region Constructors

        public FillingCarton()
        {
            InitializeComponent();
            picVLImage.SizeMode = PictureBoxSizeMode.StretchImage;
            _installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));
            grdItems.MouseClick += grdItems_MouseClick;
        }

        private void frmFillingCarton_Load(object sender, EventArgs e)
        {
            _context = new IndicoPackingEntities();

            ActiveControl = txtBarcode;
            HideUnHideControls(false);
            txtBarcode.Validated += txtBarcode_Validated;
            grdItems.ReadOnly = true;
            grdItems.DoubleClick += grdItems_DoubleClick;
            grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdItems.Click += grdItems_Click;
            txtPolybag.Validated += txtPolybag_Validated;
            txtPolybag.Visible = false;
            txtBarcode.Visible = true;
            txtBarcode.Focus();
            lblStartScanPolybags.Visible = false;
            lblStartScanPolybagSinhala.Visible = false;
        }

        #endregion

        #region Events

        void grdItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && grdItems.HitTest(e.X, e.Y).RowIndex > -1 && grdItems.SelectedRows.Count > 0)
            {
                IndicoPackingEntities context = new IndicoPackingEntities();
                List<int> ids = new List<int>();
                foreach (DataGridViewRow row in grdItems.SelectedRows)
                {
                    ids.Add(int.Parse(row.Cells[0].Value.ToString()));
                }

                List<OrderDeatilItem> orderDeatilItems = context.OrderDeatilItems
                                                            .Where(o => ids.Contains(o.ID)).ToList();

                ContextMenu contextMenu = new ContextMenu();

                if (orderDeatilItems.Count == orderDeatilItems.Where(o => o.IsPolybagScanned == true).ToList().Count)
                {
                    contextMenu.MenuItems.Add(new MenuItem("Remove Filled", new EventHandler(removeFilledItemsClick)));
                    if (LoginInfo.Role != FillingCordinator)
                        contextMenu.MenuItems.Add(new MenuItem("Remove", new EventHandler(removeItemsClick)));
                    contextMenu.Show(grdItems, new Point(e.X, e.Y));
                }
                else
                {
                    if (LoginInfo.Role != FillingCordinator)
                    {
                        contextMenu.MenuItems.Add(new MenuItem("Remove", new EventHandler(removeItemsClick)));
                        contextMenu.Show(grdItems, new Point(e.X, e.Y));
                    }
                }
            }
        }

        void removeItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to remove the item(s)?", "Remove Item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + ClickedCartonShipmentDeatailCartonId.ToString(), true).FirstOrDefault() as Panel;

                foreach (DataGridViewRow row in grdItems.SelectedRows)
                {
                    _orderDetailItemId = int.Parse(row.Cells[0].Value.ToString());

                    OrderDeatilItem item = (from odi in _context.OrderDeatilItems
                                            where odi.ID == _orderDetailItemId
                                            select odi).SingleOrDefault();

                    ShipmentDetailCarton carton = (from sc in _context.ShipmentDetailCartons
                                                   where sc.ID == item.ShipmentDetailCarton1.ID
                                                   select sc).FirstOrDefault();

                    //Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + item.ShipmentDetailCarton1.ID.ToString(), true).FirstOrDefault() as Panel;

                    if (item.IsPolybagScanned)
                    {
                        if (cartonPanel != null)
                        {
                            Label lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                            // Update the Main panel filled items label
                            lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                                " " + (int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) - 1).ToString() +
                                lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" "));// Filled 50(55)                      
                        }
                    }

                    Label lblCartonItemsCountTemp = cartonPanel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;

                    // Update the Main panel items count label
                    lblCartonItemsCountTemp.Text = lblCartonItemsCountTemp.Text.Substring(0, lblCartonItemsCountTemp.Text.IndexOf(" ")) +
                        " " + (int.Parse(lblCartonItemsCountTemp.Text.Substring(lblCartonItemsCountTemp.Text.IndexOf(" "), lblCartonItemsCountTemp.Text.LastIndexOf(" ") - lblCartonItemsCountTemp.Text.IndexOf(" "))) - grdItems.SelectedRows.Count).ToString() +
                        lblCartonItemsCountTemp.Text.Substring(lblCartonItemsCountTemp.Text.LastIndexOf(" "));

                    if (item.IsPolybagScanned == true)
                    {
                        UpdateShipmentDetailQty(item.ShipmentDeatil, -1);
                        item.IsPolybagScanned = false;
                        lblItemsFilled.Text = (int.Parse(lblItemsFilled.Text) - 1).ToString();
                    }

                    item.ShipmentDetailCarton = null;
                    lblItemsyetToBeFilled.Text = (int.Parse(lblItemsyetToBeFilled.Text) - 1).ToString();
                }
                _context.SaveChanges();

                _filledCount = int.Parse(lblItemsFilled.Text);
                _countSupposedToFill = int.Parse(lblItemsyetToBeFilled.Text);

                if (_filledCount != _countSupposedToFill)
                {
                    PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    Bitmap b = new Bitmap(InstalledPath + @"images\openbox.png");
                    picBox.BackgroundImage = b;
                }
                else if (_filledCount == _countSupposedToFill && _filledCount != 0)
                {
                    PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                    picBox.BackgroundImage = b;
                }

                var orderDetailsItems = (from odi in _context.OrderDeatilItems
                                         where odi.ShipmentDetailCarton == _shipmentDeatilCartonId
                                         select new { odi.ID, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderNumber, odi.OrderType, odi.VisualLayout, odi.Pattern, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.Distributor, odi.Client, odi.PrintedCount, odi.PatternImage, odi.VLImage }).ToList();

                grdItems.DataSource = orderDetailsItems;
                GrdColumnHeaders();

                lblItemsInCarton.Text = grdItems.RowCount.ToString();
                HighlightFilledRows(_shipmentDeatilCartonId);
                ChangeCartonColor(cartonPanel, _filledCount, grdItems.RowCount);
            }
        }

        void removeFilledItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to remove the filled polybag(s) from the carton?" + Environment.NewLine + "ඔබට පෙට්ටියේ ඇසිරු පොලිබැග ඉවත් කිරීමට අවශ්‍යද?", "Remove Filled Polybag(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in grdItems.SelectedRows)
                {
                    _orderDetailItemId = int.Parse(row.Cells[0].Value.ToString());

                    OrderDeatilItem item = (from odi in _context.OrderDeatilItems
                                            where odi.ID == _orderDetailItemId
                                            select odi).FirstOrDefault();

                    item.IsPolybagScanned = false;
                    row.DefaultCellStyle.BackColor = Color.White;
                    UpdateShipmentDetailQty(item.ShipmentDeatil, -1);

                    lblItemsFilled.Text = (int.Parse(lblItemsFilled.Text) - 1).ToString();
                    lblItemsyetToBeFilled.Text = (int.Parse(lblItemsyetToBeFilled.Text) + 1).ToString();
                }

                _context.SaveChanges();

                Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + ClickedCartonShipmentDeatailCartonId.ToString(), true).FirstOrDefault() as Panel;
                if (cartonPanel != null)
                {
                    Label lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                    // Update the Main panel filled items label
                    lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                        " " + (int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) - grdItems.SelectedRows.Count).ToString() +
                        lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" "));// Filled 50(55)
                }

                _filledCount = int.Parse(lblItemsFilled.Text);
                _countSupposedToFill = int.Parse(lblItemsyetToBeFilled.Text);

                ChangeCartonColor(cartonPanel, _filledCount, grdItems.RowCount);

                if (_filledCount != _countSupposedToFill)
                {
                    PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    Bitmap b = new Bitmap(InstalledPath + @"images\openbox.png");
                    picBox.BackgroundImage = b;
                }
                else if (_filledCount == _countSupposedToFill && _filledCount != 0)
                {
                    PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                    picBox.BackgroundImage = b;
                }
            }
        }

        void grdItems_Click(object sender, EventArgs e)
        {
            txtPolybag.Focus();
        }

        void grdItems_DoubleClick(object sender, EventArgs e)
        {
            txtPolybag.Focus();

            if (grdItems.ReadOnly == true)
                return;
        }

        void txtBarcode_Validated(object sender, EventArgs e)
        {
            var scannedText = ((TextBox)sender).Text;
            ClearAndFocusTextBox(txtBarcode);

            if (string.IsNullOrWhiteSpace(scannedText))
                return;

            ClearLabels(lblErrorMsg,lblErrorMsgSinhala);
            lblErrorMsgSinhala.Location = new Point(21, 377);

            try
            {
                _shipmentDeatilCartonId = int.Parse(scannedText.Replace("CARTON", ""));
            }
            catch (Exception)
            {
                SetErrorMessage("Carton information can't be extracted from the barcode.", "ස්කෑන් කිරීම දෝෂ සහිතයි. පෙට්ටියේ දත්ත බාකොඩයෙන් ගත නොහැක.");
                return;
            }

            var carton = (from o in _context.ShipmentDetailCartons
                where o.ID == _shipmentDeatilCartonId
                select o).FirstOrDefault();
            if (carton == null)
            {
                SetErrorMessage("This carton does not belong to any shipment within the system. Invalid carton scanned.", "ස්කෑන් කරන ලද පෙට්ටිය මෙම ලිපිනියට යැවීමට අදාල නොවේ.");
                return;
            }

            var cartonPanel = MainPanel.Controls.Find("pnlCarton" + _shipmentDeatilCartonId, true).FirstOrDefault() as Panel;
            if (cartonPanel != null && ClickedCartonShipmentDeatailCartonId != null && ClickedCartonShipmentDeatailCartonId != _shipmentDeatilCartonId)
            {
                lblErrorMsg.MaximumSize = new Size(5000, 62);
                lblErrorMsgSinhala.MaximumSize = new Size(5000, 62);
                lblErrorMsgSinhala.Location = new Point(21, 417);

                var clickedCarton = _context.ShipmentDetailCartons.FirstOrDefault(c => c.ID == ClickedCartonShipmentDeatailCartonId);

                SetErrorMessage(string.Format("You're trying to fill the carton number {0}. Scanned carton is number {1}." + Environment.NewLine + "Please scan the correct carton.", clickedCarton.Number, carton.Number),
                    string.Format("ඔබ පිරවීමට උත්සහ කරන ලද්දේ පෙට්ටි අංක {0}. ස්කෑන් කරන ලද්දේ පෙට්ටි අංක {1}." + Environment.NewLine + "කරුණාකර නිවැරදි පෙට්ටිය ස්කෑන් කරන්න.", clickedCarton.Number, carton.Number));
                return;
            }
            if (cartonPanel == null)
            {
                SetErrorMessage("This carton does not belong to this shipment.", "ස්කෑන් කරන පෙට්ටිය මෙම ලිපිනියට අදාල නොවේ.");
                return;
            }

            ClickedCartonShipmentDeatailCartonId = _shipmentDeatilCartonId;

            HideUnHideControls(true);
            txtBarcode.Visible = false;
            txtPolybag.Visible = true;

            // Get the filled items label
            _lblCartonFilledItemsCount = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

            // Load the grid              
            var orderDetailsItems = (from odi in _context.OrderDeatilItems
                where odi.ShipmentDetailCarton == _shipmentDeatilCartonId
                select new { odi.ID, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderNumber, odi.OrderType, odi.VisualLayout, odi.Pattern, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.Distributor, odi.Client, odi.PrintedCount, odi.PatternImage, odi.VLImage }).ToList();

            grdItems.DataSource = orderDetailsItems;

            lblStartScanPolybags.Visible = true;
            lblStartScanPolybagSinhala.Visible = true;
            lblStartScanPolybags.Text = (HighlightFilledRows(_shipmentDeatilCartonId) > 0) ? "Please scan the next polybag..." : "Please start scanning polybags...";
            lblStartScanPolybagSinhala.Text = (lblStartScanPolybags.Text == "Please scan the next polybag..." || lblStartScanPolybags.Text == "Continue Scan Polybags") ? "මීලග පොලිබෑගය ස්කෑන් කරන්න..." : "පොලිබෑග් ස්කෑන් කිරීම අරබන්න...";

            lblCarton.Text = _shipmentDeatilCartonId.ToString();
            lblItemsInCarton.Text = grdItems.Rows.Count.ToString();

            GrdColumnHeaders();

            txtPolybag.Focus();
        }

        void txtPolybag_Validated(object sender, EventArgs e)
        {
            var scannedText = ((TextBox)sender).Text;
            var found = false;

            if (string.IsNullOrWhiteSpace(scannedText))
                return;
            ClearLabels(lblErrorMsg,lblErrorMsgSinhala);

            try
            {
                _orderDetailItemId = int.Parse(scannedText.Replace("POLYBAG", ""));
            }
            catch (Exception)
            {
                SetErrorMessage("Polybag information can't be extracted from the barcode.", "ස්කෑන් කිරීම දෝෂ සහිතයි.");
                ClearAndFocusTextBox(txtPolybag);
                return;
            }

            foreach (DataGridViewRow row in grdItems.Rows)
            {
                if (!row.Cells[0].Value.Equals(_orderDetailItemId))
                    continue;

                var orderDetailItem = (from odi in _context.OrderDeatilItems
                    where odi.ID == _orderDetailItemId
                    select odi).SingleOrDefault();

                if(orderDetailItem ==null)
                    continue;
                if (orderDetailItem.IsPolybagScanned)
                {
                    SetErrorMessage("Scanned polybag already scanned and filled to the carton.", "ස්කෑන් කරන ලද පොලි බෑගය මෙම පෙට්ටියට මීට ඉහත අසුරා ඇත.");
                    ClearAndFocusTextBox(txtPolybag);
                    return;
                }

                row.DefaultCellStyle.BackColor = Color.Aqua;
                var scannedCount = (lblItemsFilled.Text == string.Empty) ? 1 : (int.Parse(lblItemsFilled.Text) + 1);
                lblItemsFilled.Text = scannedCount.ToString();
                lblItemsyetToBeFilled.Text = (grdItems.Rows.Count - scannedCount).ToString();
                grdItems.FirstDisplayedScrollingRowIndex = row.Index;
                grdItems.ClearSelection();

                picVLImage.Image = null;

                if ((string)row.Cells["VLImage"].Value != string.Empty)
                {
                    ImageURIVL = row.Cells["VLImage"].Value.ToString();
                }

                else if ((string)row.Cells["VLImage"].Value == string.Empty && (string)row.Cells["PatternImage"].Value != string.Empty)
                {
                    ImageURIVL = row.Cells["PatternImage"].Value.ToString();
                }

                else if ((string)row.Cells["VLImage"].Value == string.Empty && (string)row.Cells["PatternImage"].Value == string.Empty)
                {
                    picVLImage.ImageLocation = InstalledPath + @"images\NoImage299x203.png";
                }

                orderDetailItem.IsPolybagScanned = true;
                orderDetailItem.DateScanned = DateTime.Now;

                _context.SaveChanges();

                UpdateShipmentDetailQty(orderDetailItem.ShipmentDeatil, 1);

                var cartonPanel = MainPanel.Controls.Find("pnlCarton" + orderDetailItem.ShipmentDetailCarton1.ID.ToString(), true).FirstOrDefault() as Panel;
                if (cartonPanel != null)
                {
                    var lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;
                    var filledCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) + 1;
                    var countSupposedToFill = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

                    // Update the Main panel filled items label
                    lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                                                         " " + (int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) + 1).ToString() +
                                                         lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" "));// Filled 50(55)

                    if (filledCount == countSupposedToFill && filledCount != 0 && countSupposedToFill != 0)
                    {
                        var picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                        var b = new Bitmap(InstalledPath + @"images\closedbox.png");
                        picBox.BackgroundImage = b;
                    }

                    ChangeCartonColor(cartonPanel, filledCount, grdItems.RowCount);

                    // Change the menu item name to Resume
                    var button = cartonPanel.Controls.Find("mnuButton", true).FirstOrDefault();
                    button.Tag = true;
                }

                found = true;
                lblStartScanPolybags.Text = "Please scan the next polybag...";
                lblStartScanPolybagSinhala.Text = "මීලග පොලිබෑගය ස්කෑන් කරන්න...";

                break;
            }

            txtPolybag.Text = string.Empty;
            txtPolybag.Focus();

            if (!found)
            {
                lblErrorMsg.Text = "Scanned polybag does not belong to this carton.";
                lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පොලිබෑගය මෙම පෙට්ටියට අදාල නොවේ.";
                //MessageBox.Show("Scanned polybag does not belong to this carton" + Environment.NewLine + "ස්කෑන් කරන ලද පොලිබෑගය මෙම පෙට්ටියට අදාල නොවේ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void GrdColumnHeaders()
        {
            if (grdItems.Columns["ID"] != null)
            {
                grdItems.Columns["ID"].Width = 65;
                grdItems.Columns["ID"].Visible = false;
            }

            if (grdItems.Columns["IndicoOrderID"] != null)
            {
                grdItems.Columns["IndicoOrderID"].HeaderText = "Order ID";
                grdItems.Columns["IndicoOrderID"].Width = 65;
            }

            if (grdItems.Columns["IndicoOrderDetailID"] != null)
            {
                grdItems.Columns["IndicoOrderDetailID"].HeaderText = "Order Deatil ID";
                grdItems.Columns["IndicoOrderDetailID"].Width = 80;
            }

            if (grdItems.Columns["OrderNumber"] != null)
            {
                grdItems.Columns["OrderNumber"].HeaderText = "Order Number";
                grdItems.Columns["OrderNumber"].Width = 65;
            }

            if (grdItems.Columns["OrderType"] != null)
            {
                grdItems.Columns["OrderType"].HeaderText = "Order Type";
                grdItems.Columns["OrderType"].Width = 70;
            }

            if (grdItems.Columns["VisualLayout"] != null)
            {
                grdItems.Columns["VisualLayout"].HeaderText = "Visual Layout";
                grdItems.Columns["VisualLayout"].Width = 60;
            }

            if (grdItems.Columns["SizeDesc"] != null)
            {
                grdItems.Columns["SizeDesc"].HeaderText = "Size Desc";
                grdItems.Columns["SizeDesc"].Width = 60;
            }

            if (grdItems.Columns["SizeQty"] != null)
            {
                grdItems.Columns["SizeQty"].HeaderText = "Qty";
                grdItems.Columns["SizeQty"].Width = 45;
            }

            if (grdItems.Columns["SizeSrno"] != null)
            {
                grdItems.Columns["SizeSrno"].HeaderText = "Size No";
                grdItems.Columns["SizeSrno"].Width = 45;
            }

            if (grdItems.Columns["Distributor"] != null)
            {
                grdItems.Columns["Distributor"].Width = 155;
            }

            if (grdItems.Columns["Client"] != null)
            {
                grdItems.Columns["Client"].Width = 155;
            }

            if (grdItems.Columns["PrintedCount"] != null)
            {
                grdItems.Columns["PrintedCount"].Visible = false;
            }

            if (grdItems.Columns["PatternImage"] != null)
            {
                grdItems.Columns["PatternImage"].Visible = false;
            }

            if (grdItems.Columns["VLImage"] != null)
            {
                grdItems.Columns["VLImage"].Visible = false;
            }
        }

        private int HighlightFilledRows(int shipmentDeatilCartonId)
        {
            lblStartScanPolybags.Text = "Continue Scan Polybags";
            lblStartScanPolybagSinhala.Text = "පොලිබෑග් ස්කෑන් කිරීම අරබන්න...";

            List<OrderDeatilItem> items = (from odi in _context.OrderDeatilItems
                                           where odi.ShipmentDetailCarton == shipmentDeatilCartonId && odi.IsPolybagScanned == true
                                           select odi).ToList();

            foreach (OrderDeatilItem item in items)
            {
                foreach (DataGridViewRow row in grdItems.Rows)
                {
                    if (int.Parse(row.Cells[0].Value.ToString()) == item.ID)
                    {
                        row.DefaultCellStyle.BackColor = Color.Aqua;
                        break;
                    }
                }
            }

            var res = items.OrderByDescending(t => t.DateScanned).FirstOrDefault();

            if (res != null)
            {
                DateTime? result = Convert.ToDateTime(res.DateScanned);

                OrderDeatilItem order = (from odi in _context.OrderDeatilItems
                                         where odi.IsPolybagScanned == true && result == odi.DateScanned
                                         select odi).FirstOrDefault();

                picVLImage.Image = null;

                if (order != null && order.VLImage != string.Empty)
                {
                    ImageURIVL = order.VLImage.ToString();
                }

                else if (order != null && order.VLImage == string.Empty && order.PatternImage != string.Empty)
                {
                    ImageURIVL = order.PatternImage.ToString();
                }

                else if (order != null && order.VLImage == string.Empty && order.PatternImage == string.Empty)
                {
                    picVLImage.ImageLocation = InstalledPath + @"images\NoImage.PNG";
                }
            }

            lblItemsFilled.Text = items.Count.ToString();
            lblItemsyetToBeFilled.Text = (grdItems.Rows.Count - items.Count).ToString();
            _lblCartonFilledItemsCount.Text = "Filled: " + items.Count.ToString() + " (" + grdItems.Rows.Count.ToString() + ")";

            Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + shipmentDeatilCartonId.ToString(), true).FirstOrDefault() as Panel;
            if (cartonPanel != null)
            {
                if (items.Count == grdItems.Rows.Count && items.Count != 0 && grdItems.Rows.Count != 0)
                {
                    PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                    picBox.BackgroundImage = b;
                }
            }
            return items.Count;
        }

        private void HideUnHideControls(bool unhide)
        {
            //btnCancel.Visible = unhide;
            lblCarton.Visible = unhide;
            lblCartonNumber.Visible = unhide;
            lblItemsFilled.Visible = unhide;
            lblItemsInCarton.Visible = unhide;
            lblItemsyetToBeFilled.Visible = unhide;
            lblLastAddedItem.Visible = unhide;
            label1.Visible = unhide;
            label2.Visible = unhide;
            label3.Visible = unhide;
            grdItems.Visible = unhide;
            picVLImage.Visible = unhide;
            lblMessage.Visible = !unhide;
            lblMessageSinhala.Visible = !unhide;
            txtBarcode.Visible = !unhide;
        }

        private void UpdateShipmentDetailQty(int shipmentDeatil, int qty)
        {
            ShipmentDetail shipment = (from s in _context.ShipmentDetails
                                       where s.ID == shipmentDeatil
                                       select s).FirstOrDefault();

            shipment.QuantityFilled = shipment.QuantityFilled + qty;
            shipment.QuantityYetToBeFilled = shipment.Qty - shipment.QuantityFilled;
            _context.SaveChanges();

            DataGridViewRow row = GridShipment.SelectedRows[0];
            row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
            row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
            GridShipment.EndEdit();
            row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
            row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
            GridShipment.EndEdit();
        }

        private void ChangeCartonColor(Panel carton, int filledCount, int itemsCount)
        {
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

        private void ClearAndFocusTextBox(TextBox textBox)
        {
            if (textBox == null)
                return;
            textBox.Clear();
            textBox.Focus();
        }

        private void ClearLabels(params Label[] labels)
        {
            if (labels == null || labels.Length < 1)
                return;
            foreach (var label in labels)
            {
                label.Text = string.Empty;
            }
        }

        private void SetErrorMessage(string english, string sinhala)
        {
            lblErrorMsg.Text = english;
            lblErrorMsgSinhala.Text = sinhala;
        }

        #endregion  
    }
}
