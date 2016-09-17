using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        IndicoPackingEntities _context;
        int _shipmentDeatilCartonId;
        int _orderDetailItemId;
        Label _lblCartonFilledItemsCount;
        int _filledCount;
        int _countSupposedToFill;

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
                var request = WebRequest.Create(value);

                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    this.picVLImage.Image = Bitmap.FromStream(stream);
                }
            }
        }

        #endregion

        #region Constructors

        public FillingCarton()
        {
            InitializeComponent();
            this.picVLImage.SizeMode = PictureBoxSizeMode.StretchImage;

            this.grdItems.MouseClick += grdItems_MouseClick;
        }

        private void frmFillingCarton_Load(object sender, EventArgs e)
        {
            _context = new IndicoPackingEntities();

            this.ActiveControl = this.txtBarcode;
            this.HideUnHideControls(false);
            this.txtBarcode.Validated += txtBarcode_Validated;
            this.grdItems.ReadOnly = true;
            this.grdItems.DoubleClick += grdItems_DoubleClick;
            this.grdItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.grdItems.Click += grdItems_Click;
            this.txtPolybag.Validated += txtPolybag_Validated;
            this.txtPolybag.Visible = false;
            this.txtBarcode.Visible = true;
            this.txtBarcode.Focus();
            this.lblStartScanPolybags.Visible = false;
            this.lblStartScanPolybagSinhala.Visible = false;
        }

        #endregion

        #region Events

        void grdItems_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.grdItems.HitTest(e.X, e.Y).RowIndex > -1 && this.grdItems.SelectedRows.Count > 0)
            {
                IndicoPackingEntities context = new IndicoPackingEntities();
                List<int> ids = new List<int>();
                foreach (DataGridViewRow row in this.grdItems.SelectedRows)
                {
                    ids.Add(int.Parse(row.Cells[0].Value.ToString()));
                }

                List<OrderDeatilItem> orderDeatilItems = context.OrderDeatilItems
                                                            .Where(o => ids.Contains(o.ID)).ToList();

                ContextMenu contextMenu = new ContextMenu();

                if (orderDeatilItems.Count == orderDeatilItems.Where(o => o.IsPolybagScanned == true).ToList().Count)
                {
                    contextMenu.MenuItems.Add(new MenuItem("Remove Filled", new EventHandler(this.removeFilledItemsClick)));
                    if (LoginInfo.Role != FillingCordinator)
                        contextMenu.MenuItems.Add(new MenuItem("Remove", new EventHandler(this.removeItemsClick)));
                    contextMenu.Show(grdItems, new Point(e.X, e.Y));
                }
                else
                {
                    if (LoginInfo.Role != FillingCordinator)
                    {
                        contextMenu.MenuItems.Add(new MenuItem("Remove", new EventHandler(this.removeItemsClick)));
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

                foreach (DataGridViewRow row in this.grdItems.SelectedRows)
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
                        " " + (int.Parse(lblCartonItemsCountTemp.Text.Substring(lblCartonItemsCountTemp.Text.IndexOf(" "), lblCartonItemsCountTemp.Text.LastIndexOf(" ") - lblCartonItemsCountTemp.Text.IndexOf(" "))) - this.grdItems.SelectedRows.Count).ToString() +
                        lblCartonItemsCountTemp.Text.Substring(lblCartonItemsCountTemp.Text.LastIndexOf(" "));

                    if (item.IsPolybagScanned == true)
                    {
                        UpdateShipmentDetailQty(item.ShipmentDeatil, -1);
                        item.IsPolybagScanned = false;
                        this.lblItemsFilled.Text = (int.Parse(this.lblItemsFilled.Text) - 1).ToString();
                    }

                    item.ShipmentDetailCarton = null;                                      
                    this.lblItemsyetToBeFilled.Text = (int.Parse(this.lblItemsyetToBeFilled.Text) - 1).ToString();                                                                                  
                }
                _context.SaveChanges();

                _filledCount = int.Parse(this.lblItemsFilled.Text);
                _countSupposedToFill = int.Parse(this.lblItemsyetToBeFilled.Text);

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

                this.grdItems.DataSource = orderDetailsItems;
                this.GrdColumnHeaders();

                this.lblItemsInCarton.Text = this.grdItems.RowCount.ToString();
                this.HighlightFilledRows(_shipmentDeatilCartonId);
                this.ChangeCartonColor(cartonPanel, _filledCount, this.grdItems.RowCount);
            }
        }

        void removeFilledItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to remove the filled polybag(s) from the carton?" + Environment.NewLine + "ඔබට පෙට්ටියේ ඇසිරු පොලිබැග ඉවත් කිරීමට අවශ්‍යද?", "Remove Filled Polybag(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in this.grdItems.SelectedRows)
                {
                    _orderDetailItemId = int.Parse(row.Cells[0].Value.ToString());

                    OrderDeatilItem item = (from odi in _context.OrderDeatilItems
                                            where odi.ID == _orderDetailItemId
                                            select odi).FirstOrDefault();

                    item.IsPolybagScanned = false;
                    row.DefaultCellStyle.BackColor = Color.White;
                    UpdateShipmentDetailQty(item.ShipmentDeatil, -1);

                    this.lblItemsFilled.Text = (int.Parse(this.lblItemsFilled.Text) - 1).ToString();
                    this.lblItemsyetToBeFilled.Text = (int.Parse(this.lblItemsyetToBeFilled.Text) + 1).ToString();
                }

                _context.SaveChanges();

                Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + ClickedCartonShipmentDeatailCartonId.ToString(), true).FirstOrDefault() as Panel;
                if (cartonPanel != null)
                {
                    Label lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                    // Update the Main panel filled items label
                    lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                        " " + (int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) - this.grdItems.SelectedRows.Count).ToString() +
                        lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" "));// Filled 50(55)
                }

                _filledCount = int.Parse(this.lblItemsFilled.Text);
                _countSupposedToFill = int.Parse(this.lblItemsyetToBeFilled.Text);

                this.ChangeCartonColor(cartonPanel, _filledCount, this.grdItems.RowCount);

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
            this.txtPolybag.Focus();
        }

        void grdItems_DoubleClick(object sender, EventArgs e)
        {
            this.txtPolybag.Focus();

            if (this.grdItems.ReadOnly == true)
                return;
        }

        void txtBarcode_Validated(object sender, EventArgs e)
        {
            string scannedText = ((TextBox)sender).Text;
            this.txtBarcode.Text = string.Empty;
            this.txtBarcode.Focus();
                                  
            if (scannedText != string.Empty)
            {
                this.lblErrorMsg.Text = string.Empty;
                this.lblErrorMsgSinhala.Text = string.Empty;
                this.lblErrorMsgSinhala.Location = new Point(21, 377);

                try
                {
                    _shipmentDeatilCartonId = int.Parse(scannedText.Replace("CARTON", ""));
                }
                catch(Exception)
                {
                    this.lblErrorMsg.Text = "Carton information can't be extracted from the barcode.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කිරීම දෝෂ සහිතයි. පෙට්ටියේ දත්ත බාකොඩයෙන් ගත නොහැක.";
                    //MessageBox.Show("Carton information can't be extracted from the barcode." + Environment.NewLine + "ස්කෑන් කිරීම දෝෂ සහිතයි. පෙට්ටියේ දත්ත බාකොඩයෙන් ගත නොහැක.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ShipmentDetailCarton carton = (from o in _context.ShipmentDetailCartons
                                               where o.ID == _shipmentDeatilCartonId
                                               select o).FirstOrDefault(); 
                if (carton == null)
                {
                    this.lblErrorMsg.Text = "This carton does not belong to any shipment within the system. Invalid carton scanned.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පෙට්ටිය මෙම ලිපිනියට යැවීමට අදාල නොවේ.";
                    //MessageBox.Show("This carton does not belong to any shipment within the system. Invalid carton scanned." + Environment.NewLine + "ස්කෑන් කරන ලද පෙට්ටිය මෙම ලිපිනියට යැවීමට අදාල නොවේ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check main panel has the child panel with tag value shipmentDeatilCartonId. If not show error messages
                Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + _shipmentDeatilCartonId.ToString(), true).FirstOrDefault() as Panel;
                if (cartonPanel != null && ClickedCartonShipmentDeatailCartonId != null && ClickedCartonShipmentDeatailCartonId != _shipmentDeatilCartonId)
                {
                    this.lblErrorMsg.MaximumSize = new Size(5000, 62);
                    this.lblErrorMsgSinhala.MaximumSize = new Size(5000, 62);
                    this.lblErrorMsgSinhala.Location = new Point(21, 417);
                    
                    var clickedCarton = _context.ShipmentDetailCartons.Where(c => c.ID == ClickedCartonShipmentDeatailCartonId).FirstOrDefault();

                    this.lblErrorMsg.Text = string.Format("You're trying to fill the carton number {0}. Scanned carton is number {1}." + Environment.NewLine + "Please scan the correct carton.", clickedCarton.Number.ToString(), carton.Number.ToString());
                    this.lblErrorMsgSinhala.Text = string.Format("ඔබ පිරවීමට උත්සහ කරන ලද්දේ පෙට්ටි අංක {0}. ස්කෑන් කරන ලද්දේ පෙට්ටි අංක {1}." + Environment.NewLine + "කරුණාකර නිවැරදි පෙට්ටිය ස්කෑන් කරන්න.", clickedCarton.Number.ToString(), carton.Number.ToString());
                    //MessageBox.Show(string.Format("You're trying to fill the carton number {0}. Scanned carton is number {1}. Please scan the correct carton." + Environment.NewLine + Environment.NewLine + "ඔබ පිරවීමට උත්සහ කරන ලද්දේ පෙට්ටි අංක {0}. ස්කෑන් කරන ලද්දේ පෙට්ටි අංක {1}. කරුණාකර නිවැරදි පෙට්ටිය ස්කෑන් කරන්න.", clickedCarton.Number.ToString(), carton.Number.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (cartonPanel == null)
                {
                    this.lblErrorMsg.Text = "This carton does not belong to this shipment.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන පෙට්ටිය මෙම ලිපිනියට අදාල නොවේ.";
                    //MessageBox.Show("This carton does not belong to this shipment." + Environment.NewLine + "ස්කෑන් කරන පෙට්ටිය මෙම ලිපිනියට අදාල නොවේ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClickedCartonShipmentDeatailCartonId = _shipmentDeatilCartonId;

                this.HideUnHideControls(true);
                this.txtBarcode.Visible = false;
                this.txtPolybag.Visible = true;

                // Get the filled items label
                _lblCartonFilledItemsCount = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                // Load the grid              
                var orderDetailsItems = (from odi in _context.OrderDeatilItems
                                            where odi.ShipmentDetailCarton == _shipmentDeatilCartonId
                                            select new { odi.ID, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderNumber, odi.OrderType, odi.VisualLayout, odi.Pattern, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.Distributor, odi.Client, odi.PrintedCount, odi.PatternImage, odi.VLImage }).ToList();

                this.grdItems.DataSource = orderDetailsItems;

                this.lblStartScanPolybags.Visible = true;
                this.lblStartScanPolybagSinhala.Visible = true;
                this.lblStartScanPolybags.Text = (HighlightFilledRows(_shipmentDeatilCartonId) > 0)? "Please scan the next polybag..." : "Please start scanning polybags...";
                this.lblStartScanPolybagSinhala.Text = (this.lblStartScanPolybags.Text == "Please scan the next polybag..." || this.lblStartScanPolybags.Text == "Continue Scan Polybags")? "මීලග පොලිබෑගය ස්කෑන් කරන්න..." : "පොලිබෑග් ස්කෑන් කිරීම අරබන්න...";

                this.lblCarton.Text = _shipmentDeatilCartonId.ToString();
                this.lblItemsInCarton.Text = grdItems.Rows.Count.ToString();

                this.GrdColumnHeaders();

                this.txtPolybag.Focus();
            }
        }

        void txtPolybag_Validated(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;
            bool found = false;

            if (str != string.Empty)
            {
                this.lblErrorMsg.Text = string.Empty;
                this.lblErrorMsgSinhala.Text = string.Empty;

                // Check thhe scanned text has valid orderdetailid or not
                try
                {
                    _orderDetailItemId = int.Parse(str.Replace("POLYBAG", ""));
                }
                catch (Exception)
                {
                    this.lblErrorMsg.Text = "Polybag information can't be extracted from the barcode.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කිරීම දෝෂ සහිතයි.";
                    //MessageBox.Show("Polybag information can't be extracted from the barcode." + Environment.NewLine + "ස්කෑන් කිරීම දෝෂ සහිතයි.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtPolybag.Text = string.Empty;
                    this.txtPolybag.Focus();
                    return;
                }

                foreach (DataGridViewRow row in grdItems.Rows)
                {
                    if (row.Cells[0].Value.Equals(_orderDetailItemId))
                    {
                        OrderDeatilItem order = (from odi in _context.OrderDeatilItems
                                                 where odi.ID == _orderDetailItemId
                                                 select odi).SingleOrDefault();                        

                        // Check this polubag already filled or not
                        if (order.IsPolybagScanned)
                        {
                            this.lblErrorMsg.Text = "Scanned polybag already scanned and filled to the carton.";
                            this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පොලි බෑගය මෙම පෙට්ටියට මීට ඉහත අසුරා ඇත.";
                            //MessageBox.Show("Scanned polybag already scanned and filled to the carton." + Environment.NewLine + "ස්කෑන් කරන ලද පොලි බෑගය මෙම පෙට්ටියට මීට ඉහත අසුරා ඇත.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.txtPolybag.Text = string.Empty;
                            this.txtPolybag.Focus();
                            return;
                        }

                        row.DefaultCellStyle.BackColor = Color.Aqua;
                        int scannedCount = (this.lblItemsFilled.Text == string.Empty)? 1 : (int.Parse(this.lblItemsFilled.Text) + 1);
                        this.lblItemsFilled.Text = scannedCount.ToString();
                        this.lblItemsyetToBeFilled.Text = (grdItems.Rows.Count - scannedCount).ToString();
                        this.grdItems.FirstDisplayedScrollingRowIndex = row.Index;
                        this.grdItems.ClearSelection();

                        picVLImage.Image = null;

                        if (row.Cells["VLImage"].Value != string.Empty) 
                        {                         
                            ImageURIVL = row.Cells["VLImage"].Value.ToString();                        
                        }

                        else if (row.Cells["VLImage"].Value == string.Empty && row.Cells["PatternImage"].Value != string.Empty)
                        {
                            ImageURIVL = row.Cells["PatternImage"].Value.ToString();
                        }

                        else if (row.Cells["VLImage"].Value == string.Empty && row.Cells["PatternImage"].Value == string.Empty)
                        {
                            this.picVLImage.ImageLocation = InstalledPath + @"images\NoImage299x203.png";
                        }
                        
                        order.IsPolybagScanned = true;
                        order.DateScanned = DateTime.Now;

                        _context.SaveChanges();

                        // Update the top grid cells
                        this.UpdateShipmentDetailQty(order.ShipmentDeatil, 1);

                        // Get the Scan polybag carton 
                        Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + order.ShipmentDetailCarton1.ID.ToString(), true).FirstOrDefault() as Panel;
                        if (cartonPanel != null)
                        {
                            Label lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;
                            int filledCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) + 1;
                            int countSupposedToFill = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

                            // Update the Main panel filled items label
                            lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                                " " + (int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" "))) + 1).ToString() +
                                lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" "));// Filled 50(55)

                            if (filledCount == countSupposedToFill && filledCount != 0 && countSupposedToFill != 0)
                            {
                                PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                                Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                                picBox.BackgroundImage = b;
                            }

                            this.ChangeCartonColor(cartonPanel, filledCount, this.grdItems.RowCount);

                            // Change the menu item name to Resume
                            Control button = cartonPanel.Controls.Find("mnuButton", true).FirstOrDefault();
                            button.Tag = true;
                        }

                        found = true;
                        this.lblStartScanPolybags.Text = "Please scan the next polybag...";
                        this.lblStartScanPolybagSinhala.Text = "මීලග පොලිබෑගය ස්කෑන් කරන්න...";

                        break;
                    }              
                }

                this.txtPolybag.Text = string.Empty;
                this.txtPolybag.Focus();

                if (!found)
                {
                    this.lblErrorMsg.Text = "Scanned polybag does not belong to this carton.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පොලිබෑගය මෙම පෙට්ටියට අදාල නොවේ.";
                    //MessageBox.Show("Scanned polybag does not belong to this carton" + Environment.NewLine + "ස්කෑන් කරන ලද පොලිබෑගය මෙම පෙට්ටියට අදාල නොවේ.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

            if (this.grdItems.Columns["PatternImage"] != null)
            {
                this.grdItems.Columns["PatternImage"].Visible = false;
            }

            if (this.grdItems.Columns["VLImage"] != null)
            {
                this.grdItems.Columns["VLImage"].Visible = false;
            }
        }

        private int HighlightFilledRows(int shipmentDeatilCartonId)
        {
            this.lblStartScanPolybags.Text = "Continue Scan Polybags";
            this.lblStartScanPolybagSinhala.Text = "පොලිබෑග් ස්කෑන් කිරීම අරබන්න...";

            List<OrderDeatilItem> items = (from odi in _context.OrderDeatilItems
                                           where odi.ShipmentDetailCarton == shipmentDeatilCartonId && odi.IsPolybagScanned == true
                                           select odi).ToList();

            foreach (OrderDeatilItem item in items)
            {
                foreach (DataGridViewRow row in this.grdItems.Rows)
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
                    this.picVLImage.ImageLocation = InstalledPath + @"images\NoImage.PNG";
                }
            }

            this.lblItemsFilled.Text = items.Count.ToString();
            this.lblItemsyetToBeFilled.Text = (grdItems.Rows.Count - items.Count).ToString();
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
            //this.btnCancel.Visible = unhide;
            this.lblCarton.Visible = unhide;
            this.lblCartonNumber.Visible = unhide;
            this.lblItemsFilled.Visible = unhide;
            this.lblItemsInCarton.Visible = unhide;
            this.lblItemsyetToBeFilled.Visible = unhide;
            this.lblLastAddedItem.Visible = unhide;
            this.label1.Visible = unhide;
            this.label2.Visible = unhide;
            this.label3.Visible = unhide;
            this.grdItems.Visible = unhide;
            this.picVLImage.Visible = unhide;
            this.lblMessage.Visible = !unhide;
            this.lblMessageSinhala.Visible = !unhide;
            this.txtBarcode.Visible = !unhide;
        }

        private void UpdateShipmentDetailQty(int shipmentDeatil, int qty)
        {
            ShipmentDetail shipment = (from s in _context.ShipmentDetails
                                       where s.ID == shipmentDeatil
                                       select s).FirstOrDefault();

            shipment.QuantityFilled = shipment.QuantityFilled + qty;
            shipment.QuantityYetToBeFilled = shipment.Qty - shipment.QuantityFilled;
            _context.SaveChanges();

            DataGridViewRow row = this.GridShipment.SelectedRows[0];
            row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
            row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
            this.GridShipment.EndEdit();
            row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
            row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
            this.GridShipment.EndEdit();
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

        #endregion  
    }
}
