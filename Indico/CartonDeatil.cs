using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using IndicoPacking.Model;
using IndicoPacking.Common;

namespace IndicoPacking
{
    public partial class CartonDeatils : Form
    {
        #region Constants

        const UserType FillingCordinator = UserType.FillingCordinator;

        #endregion

        #region Fields

        readonly IndicoPackingEntities _context;

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

            _context = new IndicoPackingEntities();
        }

        #endregion

        #region Events

        private void CartonDeatils_Load(object sender, EventArgs e)
        {
            int shipmentDeatilCarton = ShipmentDeatilCartonId;
            var query = (from odi in _context.OrderDeatilItems
                         where odi.ShipmentDetailCarton == shipmentDeatilCarton
                         select new { odi.ID, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderNumber, odi.OrderType, odi.VisualLayout, odi.Pattern, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.Distributor, odi.Client }).ToList();

            if (query.Count > 0)
            {
                lblQtyFiledCount.Text = query.Count.ToString();

                lblNoItemsMessage.Visible = false;
                grdCartonDeatils.Visible = true;
                lblQtyFiledCount.Visible = true;
                lblQtyFilled.Visible = true;

                typeof(DataGridView).InvokeMember(
                       "DoubleBuffered",
                       BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                       null,
                       grdCartonDeatils,
                       new object[] { true });

                grdCartonDeatils.DataSource = query;

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
                    grdCartonDeatils.Columns["IndicoOrderDetailID"].HeaderText = "Order Detail ID";
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

                if(LoginInfo.Role != FillingCordinator)
                     grdCartonDeatils.MouseClick += grdCartonDeatils_MouseClick;
            }
            else
            {
                lblNoItemsMessage.Visible = true;
                grdCartonDeatils.Visible = false;
                lblQtyFiledCount.Visible = false;
                lblQtyFilled.Visible = false;
            }
        }

        void grdCartonDeatils_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || grdCartonDeatils.HitTest(e.X, e.Y).RowIndex <= -1 || grdCartonDeatils.SelectedRows.Count <= 0)
                return;
            if (!grdCartonDeatils.Rows[grdCartonDeatils.HitTest(e.X, e.Y).RowIndex].Selected || grdCartonDeatils.Visible != true)
                return;
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Remove",RemoveItemsClick));
            contextMenu.Show(grdCartonDeatils, new Point(e.X, e.Y));
        }

        void RemoveItemsClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to remove the item(s)?", "Clear Item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            foreach (var item in from DataGridViewRow row in grdCartonDeatils.SelectedRows select int.Parse(row.Cells["ID"].Value.ToString()) into orderDetailItemId select (from odi in _context.OrderDeatilItems
                where odi.ID == orderDetailItemId
                select odi).FirstOrDefault())
            {
                if (item.IsPolybagScanned)
                {
                    UpdateShipmentDetailQty(item.ShipmentDeatil, -1);
                    item.IsPolybagScanned = false;
                }

                item.ShipmentDetailCarton = null;
            }

            _context.SaveChanges();

            var cartonPanel = MainPanel.Controls.Find("pnlCarton" + ShipmentDeatilCartonId, true).FirstOrDefault() as Panel;
            if (cartonPanel != null)
            {
                var lblCartontemsCountTemp = cartonPanel.Controls.Find("lblCartonItemsCount", true).FirstOrDefault() as Label;
                var lblCartonFilledItemsCountTemp = cartonPanel.Controls.Find("lblCartonFilledItemsCount", true).FirstOrDefault() as Label;

                // Update the Main panel items count label
                lblCartontemsCountTemp.Text = lblCartontemsCountTemp.Text.Substring(0, lblCartontemsCountTemp.Text.IndexOf(" ")) +
                                              " " + (int.Parse(lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.IndexOf(" "), lblCartontemsCountTemp.Text.LastIndexOf(" ") - lblCartontemsCountTemp.Text.IndexOf(" "))) - grdCartonDeatils.SelectedRows.Count) +
                                              lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.LastIndexOf(" "));
                   
                // Update the Main panel filled items label
                int filledItemsCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" "), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ") - lblCartonFilledItemsCountTemp.Text.IndexOf(" ")));
                filledItemsCount = (filledItemsCount == 0) ? 0 : _context.OrderDeatilItems.Where(o => o.IsPolybagScanned  && o.ShipmentDetailCarton == ShipmentDeatilCartonId).ToList().Count; //filledItemsCount - grdCartonDeatils.SelectedRows.Count;

                int addedItemsCount = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ")).Replace("(", string.Empty).Replace(")", string.Empty).Trim().ToString()) - grdCartonDeatils.SelectedRows.Count;
                lblCartonFilledItemsCountTemp.Text = lblCartonFilledItemsCountTemp.Text.Substring(0, lblCartonFilledItemsCountTemp.Text.IndexOf(" ")) +
                                                     " " + filledItemsCount +
                                                     " (" + addedItemsCount + ")";
                           
                var filledCount = int.Parse(lblCartontemsCountTemp.Text.Substring(lblCartontemsCountTemp.Text.IndexOf(" ", StringComparison.Ordinal), lblCartontemsCountTemp.Text.LastIndexOf(" ") - lblCartontemsCountTemp.Text.IndexOf(" ")));
                var countSupposedToFill = int.Parse(lblCartonFilledItemsCountTemp.Text.Substring(lblCartonFilledItemsCountTemp.Text.IndexOf(" ", StringComparison.Ordinal), lblCartonFilledItemsCountTemp.Text.LastIndexOf(" ", StringComparison.Ordinal) - lblCartonFilledItemsCountTemp.Text.IndexOf(" ", StringComparison.Ordinal)));

                if (filledCount == countSupposedToFill && filledCount != 0 && countSupposedToFill != 0)
                {
                    var picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    var b = new Bitmap(InstalledPath + @"images\closedbox.png");
                    picBox.BackgroundImage = b;
                }
                else if (filledCount == 0)
                {
                    var picBox = cartonPanel.Controls.Find("picBox", true).FirstOrDefault() as PictureBox;
                    var b = new Bitmap(InstalledPath + @"images\openbox.png");
                    if (picBox != null) picBox.BackgroundImage = b;
                }
            }
                
            CartonDeatils_Load(this, new EventArgs());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void UpdateShipmentDetailQty(int shipmentDeatil, int qty)
        {
            var context = new IndicoPackingEntities();

            var shipment = (from s in context.ShipmentDetails
                                       where s.ID == shipmentDeatil
                                       select s).FirstOrDefault();

            if (shipment == null)
                return;
            shipment.QuantityFilled = shipment.QuantityFilled + qty;
            shipment.QuantityYetToBeFilled = shipment.Qty - shipment.QuantityFilled;
            context.SaveChanges();

            foreach (DataGridViewRow row in GridShipment.Rows)
            {
                if (int.Parse(row.Cells["ID"].Value.ToString()) == shipmentDeatil)
                {
                    row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
                    row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
                    GridShipment.EndEdit();
                    row.Cells["QuantityFilled"].Value = shipment.QuantityFilled;
                    row.Cells["QuantityYetToBeFilled"].Value = shipment.QuantityYetToBeFilled;
                    GridShipment.EndEdit();
                    break;
                }
            }
        }

        #endregion
    }
}
