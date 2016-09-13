using IndicoPacking.Model;
using IndicoPacking.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    public partial class RemovedInvoiceItems : Form
    {
        #region Fields

        IndicoPackingEntities context = null;
        List<int> allIds = new List<int>(); 

        #endregion

        #region Properties

        public int ShipmentDetailId { get; set; }

        public RadGridView gridInvoiceOrders { get; set; }

        public List<int> AllIds 
        { 
            get { return allIds; } 
        }

        public bool IsCancelledHit { get; set; }

        public bool IsGroupByQty { get; set; }

        #endregion

        #region Constructors

        public RemovedInvoiceItems()
        {
            InitializeComponent();

            this.IsCancelledHit = false;
        }

        private void RemovedInvoiceItems_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            if (!IsGroupByQty)
            {
                List<int> idsToAdd = new List<int>();
                var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                    where odi.Invoice == null && odi.ShipmentDeatil == ShipmentDetailId
                                    select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno }).ToList();

                foreach (var item in orderDetails)
                {
                    GridViewRowInfo row = this.gridInvoiceOrders.Rows
                        .Cast<GridViewRowInfo>()
                        .Where(r => r.Cells["ID"].Value.ToString().Equals(item.ID.ToString()))
                        .FirstOrDefault();

                    if (row == null)
                    {
                        idsToAdd.Add(item.ID);
                    }
                }

                this.gridRemovedItems.DataSource = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                                    where idsToAdd.Contains(odi.ID)
                                                    select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno }).ToList(); ;
            }
            else if (IsGroupByQty)
            {
                List<int> idsToAdd = new List<int>();
                var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                    where odi.Invoice == null && odi.ShipmentDeatil == ShipmentDetailId
                                    select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                foreach (var item in orderDetails)
                {
                    GridViewRowInfo row = this.gridInvoiceOrders.Rows
                        .Cast<GridViewRowInfo>()
                        .Where(r => r.Cells["IndicoOrderID"].Value.ToString().Equals(item.IndicoOrderID.ToString()))
                        .FirstOrDefault();

                    if (row == null)
                    {
                        idsToAdd.Add(item.IndicoOrderID);
                    }
                }

                this.gridRemovedItems.DataSource = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                    where idsToAdd.Contains(odi.IndicoOrderID)
                                                    select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

            }

            this.lblItemCount.Text = this.gridRemovedItems.Rows.Count.ToString();
        }

        #endregion

        #region Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsGroupByQty)
            {
                foreach (GridViewRowInfo row in this.gridInvoiceOrders.Rows)
                {
                    this.allIds.Add(int.Parse(row.Cells["ID"].Value.ToString()));
                }

                foreach (GridViewRowInfo row in this.gridRemovedItems.SelectedRows)
                {
                    this.allIds.Add(int.Parse(row.Cells["ID"].Value.ToString()));
                }
            }
            else if (IsGroupByQty)
            {
                foreach (GridViewRowInfo row in this.gridInvoiceOrders.Rows)
                {
                    this.allIds.Add(int.Parse(row.Cells["IndicoOrderID"].Value.ToString()));
                }

                foreach (GridViewRowInfo row in this.gridRemovedItems.SelectedRows)
                {
                    this.allIds.Add(int.Parse(row.Cells["IndicoOrderID"].Value.ToString()));
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsCancelledHit = true;
            this.Close();
        }

        #endregion        
    }
}
