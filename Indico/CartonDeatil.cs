using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Model;
using IndicoPacking.Common;

namespace IndicoPacking
{
    public partial class CartonDeatils : Form
    {
        #region Constants

        const int FILLING_CORDINATOR = 2;

        #endregion

        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Properties

        public int ShipmentDeatilCartonId { get; set; }

        public Panel MainPanel { get; set; }

        public string InstalledPath { get; set; }

        public DataGridView GridShipment { get; set; }

        #endregion     

        #region Constructors
       
        public CartonDeatils()
        {
            InitializeComponent();
        }

        public CartonDeatils(int shipmentDeatilCartonId)
        {
            InitializeComponent();

            ShipmentDeatilCartonId = shipmentDeatilCartonId;

            context = new IndicoPackingEntities();
        }

        #endregion

        #region Events

        private void CartonDeatils_Load(object sender, EventArgs e)
        {
            int shipmentDeatilCarton = ShipmentDeatilCartonId;
            var query = (from odi in context.OrderDeatilItems
                         where odi.ShipmentDetailCarton == shipmentDeatilCarton
                         select new { odi.ID, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderNumber, odi.OrderType, odi.VisualLayout, odi.Pattern, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.Distributor, odi.Client }).ToList();

            if (query.Count > 0)
            {
                lblQtyFiledCount.Text = query.Count.ToString();

                this.lblNoItemsMessage.Visible = false;
                this.grdCartonDeatils.Visible = true;
                this.lblQtyFiledCount.Visible = true;
                this.lblQtyFilled.Visible = true;

                typeof(DataGridView).InvokeMember(
                       "DoubleBuffered",
                       BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                       null,
                       this.grdCartonDeatils,
                       new object[] { true });

                this.grdCartonDeatils.DataSource = query;

                if (grdCartonDeatils.Columns["ID"] != null)
                {
                    grdCartonDeatils.Columns["ID"].Width = 65;
                }

                if (grdCartonDeatils.Columns["IndicoOrderID"] != null)
                {
                    grdCartonDeatils.Columns["IndicoOrderID"].HeaderText = "Order ID";
                    grdCartonDeatils.Columns["IndicoOrderID"].Width = 65;
                }

                if (grdCartonDeatils.Columns["IndicoOrderDetailID"] != null)
                {
                    grdCartonDeatils.Columns["IndicoOrderDetailID"].HeaderText = "Order Deatil ID";
                    grdCartonDeatils.Columns["IndicoOrderDetailID"].Width = 80;
                }

                if (grdCartonDeatils.Columns["OrderNumber"] != null)
                {
                    grdCartonDeatils.Columns["OrderNumber"].HeaderText = "Order Number";
                    grdCartonDeatils.Columns["OrderNumber"].Width = 65;
                }

                if (grdCartonDeatils.Columns["OrderType"] != null)
                {
                    grdCartonDeatils.Columns["OrderType"].HeaderText = "Order Type";
                    grdCartonDeatils.Columns["OrderType"].Width = 70;
                }

                if (grdCartonDeatils.Columns["VisualLayout"] != null)
                    grdCartonDeatils.Columns["VisualLayout"].HeaderText = "Visual Layout";

                if (grdCartonDeatils.Columns["SizeDesc"] != null)
                {
                    grdCartonDeatils.Columns["SizeDesc"].HeaderText = "Size Desc";
                    grdCartonDeatils.Columns["SizeDesc"].Width = 60;
                }

                if (grdCartonDeatils.Columns["SizeQty"] != null)
                {
                    grdCartonDeatils.Columns["SizeQty"].HeaderText = "Qty";
                    grdCartonDeatils.Columns["SizeQty"].Width = 45;
                }

                if (grdCartonDeatils.Columns["SizeSrno"] != null)
                {
                    grdCartonDeatils.Columns["SizeSrno"].HeaderText = "Size No";
                    grdCartonDeatils.Columns["SizeSrno"].Width = 45;
                }

                if(LoginInfo.Role != FILLING_CORDINATOR)
                     this.grdCartonDeatils.MouseClick += grdCartonDeatils_MouseClick;
            }
            else
            {
                this.lblNoItemsMessage.Visible = true;
                this.grdCartonDeatils.Visible = false;
                this.lblQtyFiledCount.Visible = false;
                this.lblQtyFilled.Visible = false;
            }
        }

        void grdCartonDeatils_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.grdCartonDeatils.HitTest(e.X, e.Y).RowIndex > -1 && this.grdCartonDeatils.SelectedRows.Count > 0)
            {
                if (this.grdCartonDeatils.Rows[grdCartonDeatils.HitTest(e.X, e.Y).RowIndex].Selected && this.grdCartonDeatils.Visible == true)
                {
                    ContextMenu contextMenu = new ContextMenu();
                    contextMenu.MenuItems.Add(new MenuItem("Remove", new EventHandler(this.removeItemsClick)));
                    contextMenu.Show(this.grdCartonDeatils, new Point(e.X, e.Y));
                }
            }
        }

        void removeItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to remove the item(s)?", "Clear Item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in this.grdCartonDeatils.SelectedRows)
                {
                    int orderDetailItemId = int.Parse(row.Cells["ID"].Value.ToString());

                    OrderDeatilItem item = (from odi in context.OrderDeatilItems
                                            where odi.ID == orderDetailItemId
                                            select odi).FirstOrDefault();

                    if (item.IsPolybagScanned == true)
                    {
                        this.UpdateShipmentDetailQty(item.ShipmentDeatil, -1);
                        item.IsPolybagScanned = false;
                    }

                    item.ShipmentDetailCarton = null;
                                        
                }

                context.SaveChanges();

                Panel cartonPanel = MainPanel.Controls.Find("pnlCarton" + ShipmentDeatilCartonId.ToString(), true).FirstOrDefault() as Panel;
                if (cartonPanel != null)
                {
                    Label lblCartontemsCountTemp = cartonPanel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
                    Label lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                    // Update the Main panel items count label
                    lblCartontemsCountTemp.Text = lblCartontemsCountTemp.Text.Substring(0, lblCartontemsCountTemp.Text.IndexOf(" ")) +
                         " " + (int.Parse(lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.IndexOf(" "), lblCartontemsCountTemp.Text.LastIndexOf(" ") - lblCartontemsCountTemp.Text.IndexOf(" "))) - this.grdCartonDeatils.SelectedRows.Count).ToString() +
                         lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.LastIndexOf(" "));
                   
                    // Update the Main panel filled items label
                    int filledItemsCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" ")));
                    filledItemsCount = (filledItemsCount == 0) ? 0 : context.OrderDeatilItems.Where(o => o.IsPolybagScanned == true && o.ShipmentDetailCarton == ShipmentDeatilCartonId).ToList().Count; //filledItemsCount - this.grdCartonDeatils.SelectedRows.Count;

                    int addedItemsCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim().ToString()) - this.grdCartonDeatils.SelectedRows.Count;
                    lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                           " " + filledItemsCount.ToString() +
                           " (" + addedItemsCount.ToString() + ")";
                           
                    int filledCount = int.Parse(lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.IndexOf(" "), lblCartontemsCountTemp.Text.LastIndexOf(" ") - lblCartontemsCountTemp.Text.IndexOf(" ")));
                    int countSupposedToFill = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" ")));

                    if (filledCount == countSupposedToFill && filledCount != 0 && countSupposedToFill != 0)
                    {
                        PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                        Bitmap b = new Bitmap(InstalledPath + @"images\closedbox.png");
                        picBox.BackgroundImage = b;
                    }
                    else if (filledCount == 0)
                    {
                        PictureBox picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                        Bitmap b = new Bitmap(InstalledPath + @"images\openbox.png");
                        picBox.BackgroundImage = b;
                    }
                }
                
                this.CartonDeatils_Load(this, new EventArgs());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

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

        #endregion
    }
}
