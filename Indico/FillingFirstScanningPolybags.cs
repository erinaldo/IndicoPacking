using IndicoPacking.Common;
using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndicoPacking
{
    public partial class FillingFirstScanningPolybags : Form
    {
        #region Constants

        const int BOX_HEIGHT = 130;
        const int GAP = 5; 

        const int PANEL_WIDTH = 220; 
        const int PANEL_HEIGHT = 130;
        const int PBOX_WIDTH = 73;
        const int PBOX_HEIGHT = 55;       

        #endregion

        #region Properties

        public string InstalledPath { get; set; }

        public Panel MainPanel { get; set; }

        public DataGridView GridShipment { get; set; }

        public int ShipmentId { get; set; }

        #endregion

        #region Fields

        int shipmentDetail = 0;
        int orderDetailItemId = 0;
        private int boxCount = 1;
        private int even = 1;
        private int odd = 2;
        
        #endregion

        #region Constructors

        public FillingFirstScanningPolybags()
        {
            InitializeComponent();            
        }

        private void FillingFirstScanningPolybags_Load(object sender, EventArgs e)
        {            
            this.ActiveControl = this.txtPolybag;
            this.HideUnHideControls(false);
            this.txtPolybag.Focus();

            this.txtPolybag.Validated += txtPolybag_Validated;
        }
        
        #endregion

        #region Events

        void txtPolybag_Validated(object sender, EventArgs e)
        {
            string str = ((TextBox)sender).Text;

            if (str != string.Empty)
            {
                // Check the scanned text has valid orderdetailid or not
                try
                {
                    orderDetailItemId = int.Parse(str.Replace("POLYBAG", ""));
                }
                catch (Exception)
                {
                    this.lblErrorMsg.Text = "Polybag information can't be extracted from the barcode.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කිරීම දෝෂ සහිතයි.";
                    this.ErrorLabel();
                    return;
                }

                IndicoPackingEntities context = new IndicoPackingEntities();

                OrderDeatilItem order = (from odi in context.OrderDeatilItems
                                         where odi.ID == orderDetailItemId
                                         select odi).SingleOrDefault();

                if (order.ShipmentDetail.Shipment != ShipmentId)
                {
                    this.HideUnHideControls(true);
                    this.lblErrorMsg.Text = string.Empty;
                    this.lblErrorMsgSinhala.Text = string.Empty;

                    this.lblErrorMsg.Text = "Scanned polybag doesn't belong to this shipment.";
                    this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පොලි බෑගය මෙම ලිපිනියට අදාල නොවේ.";
                    this.ErrorLabel();
                    return;
                }

                if(order != null)
                {
                    this.HideUnHideControls(true);
                    this.lblErrorMsg.Text = string.Empty;
                    this.lblErrorMsgSinhala.Text = string.Empty;

                    if (shipmentDetail != order.ShipmentDeatil)
                    {
                        boxCount = 1;
                        odd = 2;
                        even = 1;
                        this.pnlFillingCartons.Controls.Clear();
                    }

                    if (!this.pnlFillingCartons.HasChildren)
                        this.LoadCartonDeatils(order.ShipmentDeatil);

                    // Check this polubag already filled or not
                    if (order.IsPolybagScanned)
                    {
                        this.lblErrorMsg.Text = "Scanned polybag already scanned and filled to the carton.";
                        this.lblErrorMsgSinhala.Text = "ස්කෑන් කරන ලද පොලි බෑගය මීට ඉහත අසුරා ඇත.";
                        this.ErrorLabel();
                        return;
                    }

                    if (order.ShipmentDetailCarton1 == null)
                    {
                        this.lblErrorMsg.Text = "This polybag yet to be assigned to a carton.";
                        this.lblErrorMsgSinhala.Text = "මෙම පොලිබැගය පෙට්ටියකට අනුකුල කර නැත.";
                        this.ErrorLabel();
                        return;
                    }                    

                    this.lblCartonNo.Text = order.ShipmentDetailCarton1.Number.ToString();
                    order.IsPolybagScanned = true;
                    order.DateScanned = DateTime.Now;
                    
                    this.UpdateShipmentDetailQty(order.ShipmentDeatil, 1);

                    context.SaveChanges();

                    this.lblMessage.Visible = true;
                    this.lblMessageSinhala.Visible = true;

                    this.lblMessage.Text = "Please scan the next polybag...";
                    this.lblMessageSinhala.Text = "මීලග පොලිබෑගය ස්කෑන් කරන්න...";                                      

                    this.lblETD.Text = order.ShipmentDetail.ETD.ToString("d");
                    this.lblPort.Text = order.ShipmentDetail.Port.ToString();
                    this.lblPriceTerm.Text = order.ShipmentDetail.PriceTerm.ToString();
                    this.lblShipTo.Text = order.ShipmentDetail.ShipTo.ToString();

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

                        this.ChangeCartonColor(cartonPanel, filledCount, countSupposedToFill);

                        // Change the menu item name to Resume
                        Control button = cartonPanel.Controls.Find("mnuButton", true).FirstOrDefault();
                        button.Tag = true;                        
                    }

                    List<OrderDeatilItem> items = context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == order.ShipmentDetailCarton).ToList();
                    List<OrderDeatilItem> filledItems = context.OrderDeatilItems.Where(o => o.ShipmentDetailCarton == order.ShipmentDetailCarton && o.IsPolybagScanned == true).ToList();

                    foreach (Control control in this.pnlFillingCartons.Controls)
                    {
                        if (control is Panel)
                        {
                            Label lblCartonInfo = control.Controls.Find("lblCartonInfo", true).FirstOrDefault() as Label;
                            Label lblCartonItemsCount = control.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
                            Label lblCartonFilledItemsCount = control.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;                           

                            int filledCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.IndexOf(" "), lblCartonFilledItemsCount.Text.LastIndexOf(" ") - lblCartonFilledItemsCount.Text.IndexOf(" ")));
                            int itemsCount = int.Parse(lblCartonFilledItemsCount.Text.Substring(lblCartonFilledItemsCount.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim());

                            this.ChangeCartonColor(control as Panel, filledCount, itemsCount);

                            if (order.ShipmentDetailCarton1.ID == int.Parse(control.Tag.ToString()))
                            {                                
                                lblCartonItemsCount.Text = "Added: " + items.Count.ToString() + " (" + order.ShipmentDetailCarton1.Carton1.Qty.ToString() + ")";
                                lblCartonFilledItemsCount.Text = "Filled: " + filledItems.Count.ToString() + " (" + items.Count.ToString() + ")";
                                control.BackColor = Constants.LAST_FILLED_CARTON;

                                if (items.Count == filledItems.Count && items.Count != 0 && filledItems.Count != 0)
                                {
                                    PictureBox picBox = control.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                                    Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                                    picBox.BackgroundImage = b;
                                    control.BackColor = Constants.FILLED_CARTON;
                                }
                            }                            
                        }                       
                    }  

                    shipmentDetail = order.ShipmentDeatil;
                    this.txtPolybag.Text = string.Empty;
                    this.txtPolybag.Focus();
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void ErrorLabel()
        {
            this.HideUnHideControls(false);
            this.pnlFillingCartons.Visible = true;
            this.lblErrorMsg.ForeColor = Color.Red;
            this.lblErrorMsgSinhala.ForeColor = Color.Red;  
            this.txtPolybag.Text = string.Empty;
            this.txtPolybag.Focus();      
        }

        private void HideUnHideControls(bool unhide)
        {
            this.lblCartonNo.Visible = unhide;
            this.lblETD.Visible = unhide;
            this.lblPort.Visible = unhide;
            this.lblPriceTerm.Visible = unhide;
            this.lblShipTo.Visible = unhide;
            this.lblETDVw.Visible = unhide;
            this.lblPolybagBelongsTo.Visible = unhide;
            this.lblPortVw.Visible = unhide;
            this.lblPriceTermVw.Visible = unhide;
            this.lblShipToVw.Visible = unhide;
            this.lblCartonNoDetail.Visible = unhide;
            this.lblCartonNoDetailSinhala.Visible = unhide;
            this.lblMessage.Visible = !unhide;
            this.lblMessageSinhala.Visible = !unhide;
        }

        private void UpdateShipmentDetailQty(int shipmentDeatil, int qty)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            ShipmentDetail shipment = (from s in context.ShipmentDetails
                                       where s.ID == shipmentDeatil
                                       select s).FirstOrDefault();

            shipment.QuantityFilled = shipment.QuantityFilled + qty;
            shipment.QuantityYetToBeFilled = shipment.Qty - shipment.QuantityFilled;
            context.SaveChanges();

            foreach (DataGridViewRow row in this.GridShipment.Rows)
            {
                if (int.Parse(row.Cells["ID"].Value.ToString()) == shipmentDeatil)
                {
                    row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
                    row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
                    this.GridShipment.EndEdit();
                    row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
                    row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
                    this.GridShipment.EndEdit();
                    break;
                }
            }              
        }

        private void AddCartonInfo(ShipmentDetailCarton shipmentDeatilCarton, Carton carton, int orderDetailItemsCount, int filledCount)
        {
            string imgCartonLocation = InstalledPath + @"images\openbox.png";

            PictureBox pbxCarton = new PictureBox();
            pbxCarton.Name = "picBox";
            Panel p = new Panel();
            p.Name = "pnlCarton" + shipmentDeatilCarton.ID.ToString();
            if (filledCount == orderDetailItemsCount && filledCount != 0 && orderDetailItemsCount != 0)
            {
                imgCartonLocation = InstalledPath + @"images\closedbox.png";
            }           

            Bitmap b = new Bitmap(imgCartonLocation);
            p.Size = new Size(PANEL_WIDTH, PANEL_HEIGHT);
            pbxCarton.Size = new Size(PBOX_WIDTH, PBOX_HEIGHT);
            pbxCarton.Top = p.Top + 5;
            pbxCarton.Left = p.Left + 5;
            p.BackColor = Color.White;
            pbxCarton.BackgroundImage = b;
            p.Select();

            // Carton number
            Label lblCartonNumber = new Label();
            lblCartonNumber.Name = "lblCartonNumber";
            lblCartonNumber.Text = shipmentDeatilCarton.Number.ToString();
            lblCartonNumber.Top = p.Top + PBOX_HEIGHT + 18;
            lblCartonNumber.Left = p.Left;
            lblCartonNumber.AutoSize = true;
            lblCartonNumber.ForeColor = Color.DarkBlue;

            // Carton name info
            Label lblCartonInfo = new Label();
            lblCartonInfo.Name = "lblCartonInfo";
            lblCartonInfo.Text = carton.Name;
            lblCartonInfo.Top = p.Top + 5; // +PBOX_HEIGHT + 10;
            lblCartonInfo.Left = p.Left + 80;
            lblCartonInfo.AutoSize = true;

            // Carton Items count
            Label lblCartonItemsCount = new Label();
            lblCartonItemsCount.Name = "lblCartonItemsCount";
            lblCartonItemsCount.Text = "Added: " + orderDetailItemsCount.ToString() + " (" + carton.Qty.ToString() + ")";
            lblCartonItemsCount.Top = p.Top + 25; //PBOX_HEIGHT + 30;
            lblCartonItemsCount.Left = p.Left + 80;
            lblCartonItemsCount.AutoSize = true;

            // Carton Filled Items count
            Label lblCartonFilledItemsCount = new Label();
            lblCartonFilledItemsCount.Name = "lblCartonFilledItemsCount";
            lblCartonFilledItemsCount.Text = "Filled: " + filledCount.ToString() + " (" + orderDetailItemsCount.ToString() + ")";
            lblCartonFilledItemsCount.Top = p.Top + 45; //PBOX_HEIGHT + 50;
            lblCartonFilledItemsCount.Left = p.Left + 80;
            lblCartonFilledItemsCount.AutoSize = true;

            // Carton Ship To
            string shipTo = shipmentDeatilCarton.ShipmentDetail1.ShipTo.ToString();
            if(shipTo.Length > 20)
                shipTo = ReplaceString(shipTo);

            Label lblCartonShipTo = new Label();
            lblCartonShipTo.Name = "lblCartonShipTo";            
            lblCartonShipTo.Text = shipTo;
            lblCartonShipTo.Top = p.Top + PBOX_HEIGHT + 15;
            lblCartonShipTo.Left = p.Left + 60;
            lblCartonShipTo.AutoSize = true;

            // Carton Port
            string port = shipmentDeatilCarton.ShipmentDetail1.Port.ToString();
            if (port.Length > 20)
                port = ReplaceString(port);

            Label lblCartonPort = new Label();
            lblCartonPort.Name = "lblCartonShipTo";
            lblCartonPort.Text = port;
            lblCartonPort.Top = p.Top + PBOX_HEIGHT + 35;
            lblCartonPort.Left = p.Left + 60;
            lblCartonPort.AutoSize = true;

            // Carton ETD
            Label lblCartonETD = new Label();
            lblCartonETD.Name = "lblCartonShipTo";
            lblCartonETD.Text = shipmentDeatilCarton.ShipmentDetail1.ETD.ToString("d");
            lblCartonETD.Top = p.Top + PBOX_HEIGHT + 55;
            lblCartonETD.Left = p.Left + 60;
            lblCartonETD.AutoSize = true;

            Tuple<int, int> pos = GetPosition(boxCount++);
            //p.Location = new Point(pos.Item1, pos.Item2);
            p.Top = pos.Item2;
            p.Left = pos.Item1;
            p.Controls.Add(pbxCarton);

            lblCartonNumber.Size = new Size(28, 30);
            lblCartonNumber.Font = new Font("Arial", 28, FontStyle.Bold);
            p.Controls.Add(lblCartonNumber);

            // lblCartonInfo.Size = new Size(60, 20);
            lblCartonInfo.Font = new Font("Arial", 11);
            p.Controls.Add(lblCartonInfo);
            lblCartonItemsCount.Font = new Font("Arial", 11);
            p.Controls.Add(lblCartonItemsCount);
            lblCartonFilledItemsCount.Font = new Font("Arial", 11);
            p.Controls.Add(lblCartonFilledItemsCount);
            lblCartonShipTo.Font = new Font("Arial", 8, FontStyle.Bold);
            p.Controls.Add(lblCartonShipTo);
            lblCartonPort.Font = new Font("Arial", 8, FontStyle.Bold);
            p.Controls.Add(lblCartonPort);
            lblCartonETD.Font = new Font("Arial", 8, FontStyle.Bold);
            p.Controls.Add(lblCartonETD);

            p.Tag = shipmentDeatilCarton.ID;
            p.BorderStyle = BorderStyle.FixedSingle;
            if (pnlFillingCartons.VerticalScroll.Value > 0)
            {
                p.Top = p.Top - pnlFillingCartons.VerticalScroll.Value;
            }

            this.ChangeCartonColor(p, filledCount, orderDetailItemsCount);
            pnlFillingCartons.Controls.Add(p);
        }

        private Tuple<int, int> GetPosition(int count)
        {
            int x;
            int y;

            if (count % 2 == 0)
            {
                x = 230; //208
                y = ((count - odd++) * BOX_HEIGHT) + GAP * even - 6;

                if (count == 2)
                {
                    y = 4;
                }
            }
            else
            {
                x = 3;
                y = ((count - even++) * BOX_HEIGHT) + GAP * even - 6;

                if (count == 1)
                {
                    y = 4;
                }
            }

            return new Tuple<int, int>(x, y);
        }

        private void LoadCartonDeatils(int shipmentDetailId)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();
            List<ShipmentDetailCarton> cartons = (from sc in context.ShipmentDetailCartons
                                                  where sc.ShipmentDetail == shipmentDetailId
                                                  select sc).ToList();

            //// Now iterate thorugh the cartons
            for (int i = 0; i < cartons.Count; i++)
            {
                ShipmentDetailCarton sdc = cartons[i];
                AddCartonInfo(sdc, sdc.Carton1, sdc.OrderDeatilItems.Count, sdc.OrderDeatilItems.Where(o => o.IsPolybagScanned == true).ToList().Count);
            }
        }

        private string ReplaceString(string input)
        {
            string output = string.Empty;

            // replace remaining single digits with ...            
            output = input.Substring(0, 20) + "....";

            return output;
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
