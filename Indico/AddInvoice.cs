using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Model;
using IndicoPacking.ViewModels;
using IndicoPacking.Common;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    enum InvoiceFor
    {
        Indiman = 0,
        Factory = 1,
        IndimanNew = 2
    }

    public partial class AddInvoice : Form
    {
        #region Fields

        int shipmentDetailId;
        private string installedFolder = string.Empty;

        #endregion

        #region Properties

        public int InvoiceId { get; set; }

        public int TypeOfInvoice { get; set; }

        #endregion

        #region Constructors

        public AddInvoice()
        {
            InitializeComponent();

            IndicoPackingEntities context = new IndicoPackingEntities();

            installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            LoadDropdown();           

            // Load shipto drop down
            var lstShipTo = (from b in context.DistributorClientAddresses
                             select new ShipToView { ID = b.ID, CompanyName = b.CompanyName }).ToList();

            lstShipTo.Insert(0, new ShipToView { ID = 0, CompanyName = "Please Select..." });
            this.cmbShipTo.DataSource = lstShipTo;
            this.cmbShipTo.DisplayMember = "CompanyName";
            this.cmbShipTo.ValueMember = "ID";
            this.cmbShipTo.SelectedIndex = 0;

            // Load Mode drop down
            var lstMode = (from b in context.ShipmentModes
                       select new ShipmentModeView { ID = b.ID, Name = b.Name }).ToList();

            lstMode.Insert(0, new ShipmentModeView { ID = 0, Name = "Please Select..." });
            this.cmbMode.DataSource = lstMode;
            this.cmbMode.DisplayMember = "Name";
            this.cmbMode.ValueMember = "ID";
            this.cmbMode.SelectedIndex = 0;

            // Load BillTo drop down
            var lstBillTo = (from b in context.DistributorClientAddresses
                             select new BillToView { ID = b.ID, CompanyName = b.CompanyName }).ToList();

            lstBillTo.Insert(0, new BillToView { ID = 0, CompanyName = "Please Select..." });
            this.cmbBillTo.DataSource = lstBillTo;
            this.cmbBillTo.DisplayMember = "CompanyName";
            this.cmbBillTo.ValueMember = "ID";
            this.cmbBillTo.SelectedIndex = 0;

            // Load Port drop down   
            var lstPort = (from b in context.Ports
                           select new PortView { ID = b.ID, Name = b.Name }).ToList();

            lstPort.Insert(0, new PortView { ID = 0, Name = "Please Select..." });
            this.cmbPort.DataSource = lstPort;
            this.cmbPort.DisplayMember = "Name";
            this.cmbPort.ValueMember = "ID";
            this.cmbPort.SelectedIndex = 0;

            // Load Bank drop down    
            var lst = (from b in context.Banks
                          select new BankView { ID = b.ID, Name = b.Name }).ToList();

            lst.Insert(0, new BankView { ID = 0, Name = "Please Select..." });
            this.cmbBank.DataSource = lst;
            this.cmbBank.DisplayMember = "Name";
            this.cmbBank.ValueMember = "ID";
            this.cmbBank.SelectedIndex = 0;

            // Load Status dropdown    
            this.cmbStatus.DataSource = context.InvoiceStatus.ToList();
            this.cmbStatus.DisplayMember = "Name";
            this.cmbStatus.ValueMember = "ID";
            this.cmbStatus.SelectedIndex = 0;

            // Set invoice date to current date
            this.dtInvoiceDate.Value = DateTime.Now;

            this.cmbWeek.SelectedIndexChanged += cmbWeek_SelectedIndexChanged;
            this.cmbShipmentKey.SelectedIndexChanged += cmbShipmentKey_SelectedIndexChanged;
            this.cmbBillTo.SelectedIndexChanged += cmbBillTo_SelectedIndexChanged;
            this.cmbShipTo.SelectedIndexChanged += cmbShipTo_SelectedIndexChanged;
            this.rbWithGrroupByQty.CheckedChanged += rbWithGrroupByQty_CheckedChanged;

            // Hide invoice generate PDF buttuns
            this.btnInvoiceDetail.Visible = false;
            this.btnInvoiceSummary.Visible = false;
            this.btnCombinedInvoice.Visible = false;
        }

        private void AddInvoice_Load(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            // Load existing invoice details to form
            Invoice currentInvoice = context.Invoices.Where(u => u.ID == InvoiceId).FirstOrDefault();

            if (currentInvoice != null)
            {
                // Disable fields when edit
                this.cmbWeek.Enabled = false;
                this.dtETD.Enabled = false;
                this.cmbShipmentKey.Visible = false;
                this.txtInvoiceNumber.Enabled = false;
                this.dtInvoiceDate.Enabled = false;

                //Enable Invoice Pdf genrate buttons
                this.btnInvoiceDetail.Visible = true;
                this.btnInvoiceSummary.Visible = true;
                this.btnCombinedInvoice.Visible = true;

                this.cmbWeek.SelectedIndex = int.Parse(currentInvoice.ShipmentDetail1.Shipment1.WeekNo.ToString()) - 1;
                this.dtETD.Value = currentInvoice.ShipmentDate;
                this.lblShipmentKeyEdit.Text = currentInvoice.ShipmentDetail1.ShipTo.ToString();

                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    this.txtInvoiceNumber.Text = currentInvoice.FactoryInvoiceNumber.ToString();
                    this.dtInvoiceDate.Value = currentInvoice.FactoryInvoiceDate;
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    if (currentInvoice.IndimanInvoiceNumber != null)
                    {
                        this.txtInvoiceNumber.Text = currentInvoice.IndimanInvoiceNumber.ToString();
                        this.dtInvoiceDate.Value = currentInvoice.IndimanInvoiceDate ?? DateTime.MinValue;
                    }
                    else
                    {
                        this.txtInvoiceNumber.Enabled = true;
                        this.dtInvoiceDate.Enabled = true;
                    }
                }

                this.txtAWDNumber.Text = currentInvoice.AWBNumber.ToString();
                this.cmbStatus.SelectedValue = int.Parse(currentInvoice.Status.ToString());
                this.cmbPort.SelectedValue = int.Parse(currentInvoice.Port.ToString());
                this.cmbMode.SelectedValue = int.Parse(currentInvoice.ShipmentMode.ToString());
                this.cmbShipTo.SelectedValue = int.Parse(currentInvoice.ShipTo.ToString());
                this.cmbBillTo.SelectedValue = int.Parse(currentInvoice.BillTo.ToString());
                this.cmbBank.SelectedValue = int.Parse(currentInvoice.Bank.ToString());                
            }

            // Disable fields for indiman invoice
            if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                this.cmbStatus.Enabled = false;
                this.cmbPort.Enabled = false;
                this.cmbMode.Enabled = false;
                this.cmbShipTo.Enabled = false;
                this.cmbBillTo.Enabled = false;
                this.cmbBank.Enabled = false;
                this.btnAddRemovedItems.Visible = false;
                this.btnInvoiceDetail.Visible = false;
                this.btnInvoiceSummary.Visible = false;
                this.btnCombinedInvoice.Visible = false;
            }

            // Remove buttonclick
            this.gridOrderDetail.CommandCellClick += gridOrderDetail_CommandCellClick;
            //Factory Price cell value changed
            this.gridOrderDetail.CellValueChanged += gridOrderDetail_CellValueChanged;           

            this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();
            this.rbWithGrroupByQty.Checked = true;
        }          

        #endregion

        #region Events

        void gridOrderDetail_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            decimal factroryPrice = 0;
            decimal indimanprice = 0;
            decimal otherCharges = 0;
            decimal totalPrice = 0;
            int quantity = 0;

            if (this.rbWithGrroupByQty.Checked)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Factory Price")
                    {
                        // Calculate total price
                        otherCharges = (gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value.ToString());
                        factroryPrice = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (factroryPrice + otherCharges).ToString();

                        //Calculate ammount
                        totalPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString());
                        quantity = int.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Qty"].Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (totalPrice * quantity).ToString();
                    }
                    else if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Other Charges")
                    {
                        // Calculate total price
                        factroryPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["Factory Price"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Factory Price"].Value.ToString());
                        otherCharges = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (factroryPrice + otherCharges).ToString();

                        //Calculate ammount
                        totalPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString());
                        quantity = int.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Qty"].Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (totalPrice * quantity).ToString();
                    }
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Indiman Price")
                    {
                        // Calculate total price
                        otherCharges = (gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value.ToString());
                        indimanprice = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (indimanprice + otherCharges).ToString();

                        //Calculate ammount
                        totalPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString());
                        quantity = int.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Qty"].Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (totalPrice * quantity).ToString();
                    }
                    else if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Other Charges")
                    {
                        // Calculate total price
                        indimanprice = (gridOrderDetail.Rows[e.RowIndex].Cells["Indiman Price"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Indiman Price"].Value.ToString());
                        otherCharges = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (indimanprice + otherCharges).ToString();

                        //Calculate ammount
                        totalPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString());
                        quantity = int.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Qty"].Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (totalPrice * quantity).ToString();
                    }
                }
            }
            else if (this.rbWithoutGroupByQty.Checked)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Factory Price")
                    {
                        // Calculate total price
                        otherCharges = (gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value.ToString());
                        factroryPrice = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (factroryPrice + otherCharges).ToString();
                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (factroryPrice + otherCharges).ToString();
                    }
                    else if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Other Charges")
                    {
                        // Calculate total price
                        factroryPrice = (gridOrderDetail.Rows[e.RowIndex].Cells["Factory Price"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Factory Price"].Value.ToString());
                        otherCharges = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (factroryPrice + otherCharges).ToString();
                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (factroryPrice + otherCharges).ToString();
                    }
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Indiman Price")
                    {
                        // Calculate total price
                        otherCharges = (gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Other Charges"].Value.ToString());
                        indimanprice = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (indimanprice + otherCharges).ToString();
                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (indimanprice + otherCharges).ToString();
                    }
                    else if (gridOrderDetail.Columns[e.ColumnIndex].Name == "Other Charges")
                    {
                        // Calculate total price
                        indimanprice = (gridOrderDetail.Rows[e.RowIndex].Cells["Indiman Price"].Value == null) ? (decimal)0.0 : decimal.Parse(gridOrderDetail.Rows[e.RowIndex].Cells["Indiman Price"].Value.ToString());
                        otherCharges = decimal.Parse(e.Value.ToString());

                        gridOrderDetail.Rows[e.RowIndex].Cells["TotalPrice"].Value = (indimanprice + otherCharges).ToString();
                        gridOrderDetail.Rows[e.RowIndex].Cells["Amount"].Value = (indimanprice + otherCharges).ToString();
                    }
                }
            }
        }     

        void gridOrderDetail_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            GridViewRowInfo clickedRow = this.gridOrderDetail.Rows[e.RowIndex];

            if (InvoiceId != 0)
            {
                if (rbWithoutGroupByQty.Checked)
                {
                    int orderId = ((IndicoPacking.ViewModels.OrderDetailView)(clickedRow.DataBoundItem)).ID;

                    OrderDeatilItem item = context.OrderDeatilItems.Where(o => o.ID == orderId).FirstOrDefault();

                    item.Invoice = null;
                    item.FactoryPrice = null;
                    item.OtherCharges = (decimal)0.00;
                    item.Notes = string.Empty;
                }
                else if (rbWithGrroupByQty.Checked)
                {
                    int indicoOrderId = ((IndicoPacking.ViewModels.GroupByQtyFactoryView)(clickedRow.DataBoundItem)).IndicoOrderID;
                    decimal factoryPrice = decimal.Parse(((IndicoPacking.ViewModels.GroupByQtyFactoryView)(clickedRow.DataBoundItem)).FactoryPrice.ToString());
                    decimal OtherCharges = decimal.Parse(((IndicoPacking.ViewModels.GroupByQtyFactoryView)(clickedRow.DataBoundItem)).OtherCharges.ToString());

                    List<OrderDeatilItem> item = context.OrderDeatilItems.Where(o => o.IndicoOrderID == indicoOrderId && o.FactoryPrice == factoryPrice && o.OtherCharges == OtherCharges).ToList();
                    foreach (OrderDeatilItem itm in item)
                    {
                        itm.Invoice = null;
                        itm.FactoryPrice = null;
                        itm.OtherCharges = (decimal)0.00;
                        itm.Notes = string.Empty;
                    }
                }
            }
            this.gridOrderDetail.Rows.Remove(clickedRow);
            
            context.SaveChanges();

            this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();
        }

        private void cmbWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            this.gridOrderDetail.Rows.Clear();
            this.gridOrderDetail.Columns.Clear();
            this.gridOrderDetail.Rows.Clear();
            this.gridOrderDetail.DataSource = null;
            this.cmbShipmentKey.DataSource = null;

            if(this.cmbBank.SelectedIndex > -1)
            {
                this.cmbBank.SelectedIndex = 0;
                this.cmbShipTo.SelectedIndex = 0;
                this.cmbMode.SelectedIndex = 0;
                this.cmbBillTo.SelectedIndex = 0;
                this.cmbPort.SelectedIndex = 0;
                this.cmbStatus.SelectedIndex = 0;
                this.lblBillToAddress.Text = "";
                this.lblShipToAddress.Text = "";
            }

            int shipmentId = (int)((System.Collections.Generic.KeyValuePair<int, string>)this.cmbWeek.SelectedValue).Key;
          
            var shipmentdetail = (from s in context.GetShipmentKeysViews
                                    where s.Shipment == shipmentId
                                    select new ShipmentKeyView { ID = s.ID, ShipTo = s.ShipTo, Shipment = s.Shipment, ETD = s.ETD, Port = s.Port, ShipmentMode = s.ShipmentMode, AvailableQuantity = s.AvailableQuantity }).ToList();            

            if (shipmentdetail != null && shipmentdetail.Count > 0)
            {
                this.dtETD.Value = shipmentdetail[0].ETD;
                this.cmbShipmentKey.DataSource = shipmentdetail;                

                this.cmbShipmentKey.Columns["Qty"].IsVisible = false;
                this.gridOrderDetail.Columns["Factory Price"].EnableExpressionEditor = true;
            }                         
        }

        private void cmbShipmentKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbShipmentKey.SelectedIndex > -1)
            {
                Telerik.WinControls.UI.GridViewDataRowInfo selectedItem = (Telerik.WinControls.UI.GridViewDataRowInfo)this.cmbShipmentKey.SelectedItem;
                ShipmentKeyView key = (ShipmentKeyView)selectedItem.DataBoundItem;

                this.cmbShipTo.SelectedValue = this.cmbShipTo.FindStringExact(key.ShipTo.ToString());
                this.cmbBillTo.SelectedValue = this.cmbBillTo.FindStringExact(key.ShipTo.ToString());
                this.cmbMode.SelectedValue = this.cmbMode.FindStringExact(key.ShipmentMode.ToString());
                this.cmbPort.SelectedValue = this.cmbPort.FindStringExact(key.Port.ToString());

                // Load order detail items 
                shipmentDetailId = key.ID;

                this.gridOrderDetail.CreateCell += gridOrderDetail_CreateCell;                

                this.gridOrderDetail.Columns.Clear();
                      
                // Default checked radio button
                this.rbWithoutGroupByQty.Checked = true;

                if (rbWithGrroupByQty.Checked)
                {
                    OrderDetailGroupByQty();
                }
                else if (rbWithoutGroupByQty.Checked)
                {
                    OrderDetailWithoutGroupByQty();
                }

                this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();              
            }
        }

        void gridOrderDetail_CreateCell(object sender, Telerik.WinControls.UI.GridViewCreateCellEventArgs e)
        {
            if (e.Column.Name == "Factory Price" && e.CellType == typeof(GridHeaderCellElement))
            {
                e.CellType = typeof(FactoryPriceHeaderCellElement);
            }
            else if (e.Column.Name == "Indiman Price" && e.CellType == typeof(GridHeaderCellElement))
            {
                e.CellType = typeof(IndimanPriceHeaderCellElement);
            }
            else if (e.Column.Name == "Other Charges" && e.CellType == typeof(GridHeaderCellElement))
            {
                e.CellType = typeof(OtherChargesHeaderCellElement);
            }
        }

        private void cmbShipTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            if (this.cmbShipTo.SelectedIndex > 0)
            {
                int distributerClientAddressId = int.Parse(this.cmbShipTo.SelectedValue.ToString());

                DistributorClientAddress dca = (from d in context.DistributorClientAddresses
                                                where d.ID == distributerClientAddressId
                                                select d).FirstOrDefault();

                lblShipToAddress.Text = string.Empty;
                lblShipToAddress.Text = dca.Address.ToString();
            }
        }

        private void cmbBillTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            if (this.cmbShipTo.SelectedIndex > 0)
            {
                int distributerClientAddressId = int.Parse(this.cmbBillTo.SelectedValue.ToString());

                DistributorClientAddress dca = (from d in context.DistributorClientAddresses
                                                where d.ID == distributerClientAddressId
                                                select d).FirstOrDefault();

                lblBillToAddress.Text = string.Empty;
                lblBillToAddress.Text = dca.Address.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateForm())
            {
                this.btnSave.Enabled = false;
                this.SaveChanges();
                this.Close();
            }           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbWithGrroupByQty_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWithGrroupByQty.Checked)
            {
                OrderDetailGroupByQty();
                this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();
            }
            else if (rbWithoutGroupByQty.Checked)
            {
                OrderDetailWithoutGroupByQty();
                this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();
            }
        }

        private void btnAddRemovedItems_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Before adding removed rows have to save the changes done to the grid. Do you wish to save and continue?", "Add Removed Item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.ValidateForm())
                {
                    this.SaveChanges();
                }
                else
                {
                    return;
                }

                IndicoPackingEntities context = new IndicoPackingEntities();

                RemovedInvoiceItems removedItems = new RemovedInvoiceItems();
                removedItems.ShipmentDetailId = shipmentDetailId;
                removedItems.gridInvoiceOrders = this.gridOrderDetail;
                removedItems.StartPosition = FormStartPosition.CenterScreen;
                if (this.rbWithGrroupByQty.Checked)
                    removedItems.IsGroupByQty = true;
                removedItems.ShowDialog();

                if (!removedItems.IsCancelledHit)
                {
                    this.gridOrderDetail.Columns.Clear();

                    if (TypeOfInvoice == (int)InvoiceFor.Factory)
                    {
                        if (this.rbWithoutGroupByQty.Checked)
                        {
                            var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                                where removedItems.AllIds.Contains(odi.ID)
                                                select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, FactoryPrice = odi.FactoryPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges }).ToList();

                            this.gridOrderDetail.DataSource = orderDetails;
                            this.LoadGridOrdeDetailColumns();

                            if (orderDetails != null && orderDetails.Count > 0)
                            {
                                int i = 0;
                                foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                                {
                                    row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                                    row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                                    i++;
                                }
                            }

                            this.gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                            this.gridOrderDetail.Columns["ID"].IsVisible = false;
                            this.gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";
                            this.gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                            this.gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";

                            this.gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                        }
                        else if (this.rbWithGrroupByQty.Checked)
                        {
                            var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                where removedItems.AllIds.Contains(odi.IndicoOrderID)
                                                select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                            this.gridOrderDetail.DataSource = orderDetails;
                            this.LoadGridOrdeDetailColumns();

                            if (orderDetails != null && orderDetails.Count > 0)
                            {
                                int i = 0;
                                foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                                {
                                    row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                                    row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                                    i++;
                                }
                            }

                            this.gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                        }
                    }
                    else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                    {
                        var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                            where removedItems.AllIds.Contains(odi.ID)
                                            select new { odi.ID, odi.PurchaseOrder, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderType, odi.VisualLayout, odi.Distributor, odi.Client, odi.Pattern, odi.Fabric, odi.Gender, odi.AgeGroup, odi.SleeveShape, odi.SleeveLength, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.FactoryPrice, odi.IndimanPrice, odi.OtherCharges }).ToList();

                        this.gridOrderDetail.DataSource = orderDetails;
                        this.LoadGridOrdeDetailColumns();

                        if (orderDetails != null && orderDetails.Count > 0)
                        {
                            int i = 0;
                            foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                            {
                                row.Cells["Indiman Price"].Value = orderDetails[i].IndimanPrice.ToString();
                                i++;
                            }
                        }

                        this.gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                        this.gridOrderDetail.Columns["Other Charges"].IsVisible = false;
                        this.gridOrderDetail.Columns["TotalPrice"].IsVisible = false;
                    }
                }

                this.lblItemCount.Text = this.gridOrderDetail.Rows.Count.ToString();
            }
        }

        private void btnApplyCostSheetPrice_Click_1(object sender, EventArgs e)
        {
            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                {
                    if (double.Parse(row.Cells["JKFOBCostSheetPrice"].Value.ToString()) != 0.00)
                        row.Cells["Factory Price"].Value = row.Cells["JKFOBCostSheetPrice"].Value.ToString();
                }
            }
            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                {
                    if (double.Parse(row.Cells["IndimanCIFCostSheetPrice"].Value.ToString()) != 0.00)
                        row.Cells["Indiman Price"].Value = row.Cells["IndimanCIFCostSheetPrice"].Value.ToString();
                }
            }
        }

        private void btnInvoiceSummary_Click(object sender, EventArgs e)
        {
            GeneratePDF.GenerateJKInvoiceSummary(InvoiceId, Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")));
        }

        private void btnInvoiceDetail_Click(object sender, EventArgs e)
        {
            GeneratePDF.GenerateJKInvoiceDetail(InvoiceId, Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")));
        }

        private void btnCombinedInvoice_Click(object sender, EventArgs e)
        {
            GeneratePDF.CombinedInvoice(InvoiceId, Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")));
        }

        #endregion

        #region Methods

        private void LoadDropdown()
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            // Load week dropdown
            List<Shipment> lst = context.Shipments.ToList();
            Dictionary<int, string> weekendSource = new Dictionary<int, string>();

            string Value = string.Empty;
            foreach (Shipment obj in lst)
            {
                Value = obj.WeekNo.ToString() + " / " + obj.WeekendDate.Year.ToString();
                weekendSource.Add(obj.ID, Value);
            }

            this.cmbWeek.DataSource = new BindingSource(weekendSource, null);
            this.cmbWeek.DisplayMember = "Value";    
        }

        private bool ValidateForm()
        {
            bool isFormvalid = false;
            bool isInvoiceNumber = true, isPort = true, isMode = true, isShipTo = true, isBillTo = true, isBank = true; 

            rfvInvoiceNumber.Clear();
            if (string.IsNullOrWhiteSpace(txtInvoiceNumber.Text))
            {
                rfvInvoiceNumber.SetError(txtInvoiceNumber, "Invoice number is required.");
                isInvoiceNumber = false;
            }

            rfvPort.Clear();
            if (cmbPort.SelectedIndex == 0 || cmbPort.SelectedValue == null)
            {
                rfvPort.SetError(cmbPort, "Port is required.");
                isPort = false;
            }

            rfvMode.Clear();
            if (cmbMode.SelectedIndex == 0 || cmbMode.SelectedValue == null)
            {
                rfvMode.SetError(cmbMode, "Mode is required.");
                isMode = false;
            }

            rfvShipTo.Clear();
            if (cmbShipTo.SelectedIndex == 0 || cmbShipTo.SelectedValue == null)
            {
                rfvShipTo.SetError(cmbShipTo, "ShipTo is required.");
                isShipTo = false;
            }

            rfvBillTo.Clear();
            if (cmbBillTo.SelectedIndex == 0 || cmbBillTo.SelectedValue == null)
            {
                rfvBillTo.SetError(cmbBillTo, "BillTo is required.");
                isBillTo = false;
            }

            rfvBank.Clear();
            if (cmbBank.SelectedIndex == 0)
            {
                rfvBank.SetError(cmbBank, "Bank is required.");
                isBank = false;
            }

            if (isInvoiceNumber && isPort && isMode && isShipTo && isBillTo && isBank)
            {
                isFormvalid = true;
            }

            return isFormvalid;
        }                                        

        private void OrderDetailGroupByQty()
        {
            this.gridOrderDetail.Columns.Clear();

            IndicoPackingEntities context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (this.InvoiceId != 0)
            {
                currentInvoice = context.Invoices.Where(i => i.ID == InvoiceId).FirstOrDefault();
                shipmentDetailId = currentInvoice.ShipmentDetail;
            }

            if(TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                // When editing the invoice load orderdetails 
                if (currentInvoice == null)
                {
                    var orderDetailsWithGroupByFactory = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                          where odi.ShipmentDeatil == shipmentDetailId && odi.Invoice == null
                                                          select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                    this.gridOrderDetail.DataSource = orderDetailsWithGroupByFactory;
                    this.LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        row.Cells["Factory Price"].Value = "0.00";
                        row.Cells["Other Charges"].Value = "0.00";
                    }
                }
                else
                {
                    var orderDetailsWithGroupByFactory = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                          where odi.Invoice == currentInvoice.ID
                                                          select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                    this.gridOrderDetail.DataSource = orderDetailsWithGroupByFactory;

                    this.LoadGridOrdeDetailColumns();

                    // iterate thorugh the grid rows
                    if (orderDetailsWithGroupByFactory != null && orderDetailsWithGroupByFactory.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                        {
                            row.Cells["Factory Price"].Value = orderDetailsWithGroupByFactory[i].FactoryPrice.ToString();
                            row.Cells["Other Charges"].Value = orderDetailsWithGroupByFactory[i].OtherCharges.ToString();
                            row.Cells["NotesColumn"].Value = orderDetailsWithGroupByFactory[i].Notes.ToString();
                            i++;
                        }
                    }
                }     
           
                this.gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["Notes"].IsVisible = false;
                this.gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "Costsheet Price";

                foreach (GridViewColumn column in this.gridOrderDetail.Columns)
                {
                    if (column.Name == "Factory Price")
                    {
                        return;
                    }
                    else if (column.Name == "Other Charges")
                    {
                        return;
                    }
                    else if (column.Name == "Notes")
                    {
                        return;
                    }
                    else
                        column.ReadOnly = true;
                }
            }

            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                if (currentInvoice != null && currentInvoice.IndimanInvoiceNumber == null)
                {
                    var orderDetailsWithGroupByIndiman = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimen
                                                          where odi.Invoice == currentInvoice.ID && odi.IndimanPrice == 0
                                                          select new { odi.PurchaseOrder, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderType, odi.VisualLayout, odi.Distributor, odi.Client, odi.Pattern, odi.Fabric, odi.Gender, odi.AgeGroup, odi.SleeveShape, odi.SleeveLength, odi.Qty, odi.Notes, odi.FactoryPrice, odi.IndimanPrice, odi.OtherCharges, odi.JKFOBCostSheetPrice, odi.IndimanCIFCostSheetPrice }).ToList();

                    this.gridOrderDetail.DataSource = orderDetailsWithGroupByIndiman;
                    this.LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        row.Cells["Indiman Price"].Value = "0.00";
                    }
                }
                else
                {
                    var orderDetailsWithGroupByIndiman = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForIndimen
                                                          where odi.Invoice == currentInvoice.ID && odi.IndimanPrice != 0
                                                          select new { odi.PurchaseOrder, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderType, odi.VisualLayout, odi.Distributor, odi.Client, odi.Pattern, odi.Fabric, odi.Gender, odi.AgeGroup, odi.SleeveShape, odi.SleeveLength, odi.Qty, odi.Notes, odi.FactoryPrice, odi.IndimanPrice, odi.OtherCharges, odi.JKFOBCostSheetPrice, odi.IndimanCIFCostSheetPrice }).ToList();

                    this.gridOrderDetail.DataSource = orderDetailsWithGroupByIndiman;
                    this.LoadGridOrdeDetailColumns();

                    // iterate thorugh the grid rows
                    if (orderDetailsWithGroupByIndiman != null && orderDetailsWithGroupByIndiman.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                        {
                            row.Cells["Indiman Price"].Value = orderDetailsWithGroupByIndiman[i].IndimanPrice.ToString();
                            i++;
                        }
                    }
                }    

                this.gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                this.gridOrderDetail.Columns["Other Charges"].IsVisible = false;
                this.gridOrderDetail.Columns["TotalPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["NotesColumn"].IsVisible = false;

                this.gridOrderDetail.Columns["FactoryPrice"].HeaderText = "Factory Price";
                this.gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "JK Costsheet Price";
                this.gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].HeaderText = "Costsheet Price";

                foreach (GridViewColumn column in this.gridOrderDetail.Columns)
                {
                    if (column.Name == "Indiman Price")
                    {
                        return;
                    }               
                    else
                        column.ReadOnly = true;
                }
            }
        }

        private void OrderDetailWithoutGroupByQty()
        {
            this.gridOrderDetail.Columns.Clear();

            IndicoPackingEntities context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (this.InvoiceId != 0)
            {
                currentInvoice = context.Invoices.Where(i => i.ID == InvoiceId).FirstOrDefault();
                shipmentDetailId = currentInvoice.ShipmentDetail;
            }

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                if (currentInvoice == null)
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where odi.ShipmentDeatil == shipmentDetailId && odi.Invoice == null
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                    this.gridOrderDetail.DataSource = orderDetails;
                    this.LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        row.Cells["Factory Price"].Value = "0.00";
                        row.Cells["Other Charges"].Value = "0.00";
                    }
                }
                else
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where  odi.Invoice == currentInvoice.ID
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                    this.gridOrderDetail.DataSource = orderDetails;
                    this.LoadGridOrdeDetailColumns();

                    if (orderDetails != null && orderDetails.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                        {
                            row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                            row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                            i++;
                        }
                    }
                }

                this.gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["ID"].IsVisible = false;
                this.gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["Notes"].IsVisible = false;
                this.gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "Costsheet Price";
                this.gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                this.gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";
                this.gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";  

                foreach (GridViewColumn column in this.gridOrderDetail.Columns)
                {
                    if (column.Name == "Factory Price") 
                    {
                        return;                        
                    }
                    else if (column.Name == "Other Charges")
                    {
                        return;
                    }
                    else if (column.Name == "Notes")
                    {
                        return;
                    }
                    else
                        column.ReadOnly = true;
                }
            }
                                   
            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                if (currentInvoice != null && currentInvoice.IndimanInvoiceNumber == null)
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where odi.Invoice == currentInvoice.ID && odi.IndimanPrice == 0
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, Notes = odi.Notes, FactoryPrice = odi.FactoryPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanCIFCostSheetPrice = odi.IndimanCIFCostSheetPrice }).ToList();

                    this.gridOrderDetail.DataSource = orderDetails;
                    this.LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        row.Cells["Indiman Price"].Value = "0.00";
                    }
                }
                else
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where odi.Invoice == currentInvoice.ID && odi.IndimanPrice != 0
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, Notes = odi.Notes, FactoryPrice = odi.FactoryPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanCIFCostSheetPrice = odi.IndimanCIFCostSheetPrice }).ToList();

                    this.gridOrderDetail.DataSource = orderDetails;
                    this.LoadGridOrdeDetailColumns();

                    if (orderDetails != null && orderDetails.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                        {
                            row.Cells["Indiman Price"].Value = orderDetails[i].IndimanPrice.ToString();
                            i++;
                        }
                    }
                }

                this.gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["ID"].IsVisible = false;
                this.gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                this.gridOrderDetail.Columns["Other Charges"].IsVisible = false;
                this.gridOrderDetail.Columns["TotalPrice"].IsVisible = false;
                this.gridOrderDetail.Columns["NotesColumn"].IsVisible = false;
                this.gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "JK Costsheet Price";
                this.gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].HeaderText = "Costsheet Price";
                this.gridOrderDetail.Columns["FactoryPrice"].HeaderText = "Factory Price";
                this.gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                this.gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";
                this.gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";  

                foreach (GridViewColumn column in this.gridOrderDetail.Columns)
                {
                    if (column.Name == "Indiman Price")
                    {
                        return;
                    }
                    else
                        column.ReadOnly = true;
                }
            }                                      
        }

        private void LoadGridOrdeDetailColumns()
        {
            OtherChargesColumn otherChargesColumn = new OtherChargesColumn("Other Charges");
            otherChargesColumn.Width = 120;
            otherChargesColumn.ParentGrid = this.gridOrderDetail;
            this.gridOrderDetail.Columns.Add(otherChargesColumn);

            FactoryPriceColumn factoryPriceColumn = new FactoryPriceColumn("Factory Price");
            factoryPriceColumn.Width = 120;
            factoryPriceColumn.ParentGrid = this.gridOrderDetail;
            this.gridOrderDetail.Columns.Add(factoryPriceColumn);

            IndimanPriceColumn indimanPriceColumn = new IndimanPriceColumn("Indiman Price");
            indimanPriceColumn.Width = 120;
            indimanPriceColumn.ParentGrid = this.gridOrderDetail;
            this.gridOrderDetail.Columns.Add(indimanPriceColumn);
            
            this.gridOrderDetail.MasterView.TableHeaderRow.MinHeight = 50;
            this.gridOrderDetail.AutoSizeRows = true;

            this.gridOrderDetail.Columns["IndicoOrderID"].IsVisible = false;
            this.gridOrderDetail.Columns["IndicoOrderDetailID"].HeaderText = "Order Detail ID";
            this.gridOrderDetail.Columns["IndicoOrderDetailID"].Width = 100;
            this.gridOrderDetail.Columns["OrderType"].HeaderText = "Order Type";
            this.gridOrderDetail.Columns["OrderType"].Width = 80;
            this.gridOrderDetail.Columns["VisualLayout"].HeaderText = "Visual Layout";
            this.gridOrderDetail.Columns["AgeGroup"].HeaderText = "Age Group";
            this.gridOrderDetail.Columns["SleeveShape"].HeaderText = "Sleeve Shape";
            this.gridOrderDetail.Columns["SleeveLength"].HeaderText = "Sleeve Length";
            this.gridOrderDetail.Columns["OtherCharges"].HeaderText = "Other Charges";
            this.gridOrderDetail.Columns["Factory Price"].AllowFiltering = false;
            this.gridOrderDetail.Columns["Indiman Price"].AllowFiltering = false;
            this.gridOrderDetail.AllowDragToGroup = false;
            this.gridOrderDetail.Columns["OtherCharges"].IsVisible = false;
            this.gridOrderDetail.Columns["SleeveShape"].IsVisible = false;
            this.gridOrderDetail.Columns["SleeveLength"].IsVisible = false;
               
            // Add Total Price column
            GridViewTextBoxColumn totalPrice = new GridViewTextBoxColumn();
            totalPrice.Name = "TotalPrice";
            totalPrice.HeaderText = "Total Price";
            totalPrice.TextAlignment = ContentAlignment.MiddleRight;
            this.gridOrderDetail.MasterTemplate.Columns.Add(totalPrice);

            this.gridOrderDetail.Columns["TotalPrice"].ReadOnly = true;
            this.gridOrderDetail.Columns["TotalPrice"].Width = 100;

            // Add Ammount column
            GridViewTextBoxColumn ammount = new GridViewTextBoxColumn();
            ammount.Name = "Amount";
            ammount.HeaderText = "Amount";
            ammount.TextAlignment = ContentAlignment.MiddleRight;
            this.gridOrderDetail.MasterTemplate.Columns.Add(ammount);

            this.gridOrderDetail.Columns["Amount"].ReadOnly = true;
            this.gridOrderDetail.Columns["Amount"].Width = 100;

            // Add Notes column
            GridViewTextBoxColumn notesColumn = new GridViewTextBoxColumn();
            notesColumn.Name = "NotesColumn";
            notesColumn.HeaderText = "Notes";
            notesColumn.TextAlignment = ContentAlignment.MiddleLeft;
            this.gridOrderDetail.MasterTemplate.Columns.Add(notesColumn);

            this.gridOrderDetail.Columns["NotesColumn"].Width = 100;

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                // Add remove button column
                GridViewCommandColumn removeColumn = new GridViewCommandColumn();
                removeColumn.Name = "RemoveColumn";
                removeColumn.UseDefaultText = true;
                removeColumn.DefaultText = "Remove";
                removeColumn.FieldName = "ID";
                removeColumn.HeaderText = "";
                this.gridOrderDetail.MasterTemplate.Columns.Add(removeColumn);

                this.gridOrderDetail.Columns["RemoveColumn"].Width = 80;
                this.gridOrderDetail.Columns["Indiman Price"].IsVisible = false;
            }            
        }

        private void SaveChanges()
        {
            IndicoPackingEntities context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (InvoiceId != 0)
            {
                
                currentInvoice = context.Invoices.FirstOrDefault(i => i.ID == InvoiceId);
                shipmentDetailId = currentInvoice.ShipmentDetail;
            }

            if (currentInvoice == null)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    currentInvoice = new Invoice();
                    currentInvoice.ShipmentDetail = this.shipmentDetailId;
                    currentInvoice.ShipmentDate = this.dtETD.Value;
                    currentInvoice.CreatedDate = DateTime.Now;
                    currentInvoice.AWBNumber = this.txtAWDNumber.Text;
                    currentInvoice.LastModifiedBy = LoginInfo.UserID;
                    currentInvoice.ModifiedDate = DateTime.Now;
                    currentInvoice.Status = ((InvoiceStatu)(this.cmbStatus.SelectedItem)).ID;
                    currentInvoice.ShipTo = ((IndicoPacking.ViewModels.ShipToView)(this.cmbShipTo.SelectedItem)).ID;
                    currentInvoice.BillTo = ((IndicoPacking.ViewModels.BillToView)(this.cmbBillTo.SelectedItem)).ID;
                    currentInvoice.ShipmentMode = ((IndicoPacking.ViewModels.ShipmentModeView)(this.cmbMode.SelectedItem)).ID;
                    currentInvoice.Port = ((IndicoPacking.ViewModels.PortView)(this.cmbPort.SelectedItem)).ID;
                    currentInvoice.Bank = ((IndicoPacking.ViewModels.BankView)(this.cmbBank.SelectedItem)).ID;
                    currentInvoice.FactoryInvoiceNumber = this.txtInvoiceNumber.Text;
                    currentInvoice.FactoryInvoiceDate = this.dtInvoiceDate.Value;

                    context.Invoices.Add(currentInvoice);
                }
            }
            else
            {
                if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    currentInvoice.IndimanInvoiceNumber = this.txtInvoiceNumber.Text;
                    currentInvoice.IndimanInvoiceDate = this.dtInvoiceDate.Value;
                }

                currentInvoice.AWBNumber = this.txtAWDNumber.Text;
                currentInvoice.LastModifiedBy = LoginInfo.UserID;
                currentInvoice.ModifiedDate = DateTime.Now;
                currentInvoice.Status = ((InvoiceStatu)(this.cmbStatus.SelectedItem)).ID;
                currentInvoice.ShipTo = ((IndicoPacking.ViewModels.ShipToView)(this.cmbShipTo.SelectedItem)).ID;
                currentInvoice.BillTo = ((IndicoPacking.ViewModels.BillToView)(this.cmbBillTo.SelectedItem)).ID;
                currentInvoice.ShipmentMode = ((IndicoPacking.ViewModels.ShipmentModeView)(this.cmbMode.SelectedItem)).ID;
                currentInvoice.Port = ((IndicoPacking.ViewModels.PortView)(this.cmbPort.SelectedItem)).ID;
                currentInvoice.Bank = ((IndicoPacking.ViewModels.BankView)(this.cmbBank.SelectedItem)).ID;
            }

            var orderDetailItems = (from odi in context.OrderDeatilItems
                                    where odi.ShipmentDeatil == this.shipmentDetailId
                                    select odi).ToList();

            if (rbWithoutGroupByQty.Checked)
            {
                int orderId = 0;
                //int orderDetailId = 0;
                //string sizeName = string.Empty;
                //int sizeSeqNumber = 0;

                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        orderId = int.Parse(row.Cells["ID"].Value.ToString());
                        //sizeName = row.Cells["SizeDesc"].Value.ToString();
                        //sizeSeqNumber = int.Parse(row.Cells["SizeSrno"].Value.ToString());

                        OrderDeatilItem item = orderDetailItems.Where(o => o.ID == orderId).FirstOrDefault();

                        if (item != null)
                        {
                            item.FactoryPrice = (row.Cells["Factory Price"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Factory Price"].Value.ToString());
                            item.OtherCharges = (row.Cells["Other Charges"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Other Charges"].Value.ToString());
                            item.Notes = (row.Cells["NotesColumn"].Value == null) ? string.Empty : (row.Cells["NotesColumn"].Value.ToString());

                            currentInvoice.OrderDeatilItems.Add(item);
                        }
                    }
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        orderId = int.Parse(row.Cells["ID"].Value.ToString());
                        //sizeName = row.Cells["SizeDesc"].Value.ToString();
                        //sizeSeqNumber = int.Parse(row.Cells["SizeSrno"].Value.ToString());

                        OrderDeatilItem item = orderDetailItems.Where(o => o.ID == orderId).FirstOrDefault();

                        if (item != null)
                        {
                            item.IndimanPrice = (row.Cells["Indiman Price"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Indiman Price"].Value.ToString());
                        }
                    }
                }
            }
            else if (rbWithGrroupByQty.Checked)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    int orderId = 0;
                    decimal factoryPrice = 0.00M;
                    decimal otherCharges = 0.00M;
                    List<OrderDeatilItem> item = null;

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        if (InvoiceId != 0)
                        {
                            orderId = int.Parse(row.Cells["IndicoOrderID"].Value.ToString());
                            factoryPrice = decimal.Parse(row.Cells["FactoryPrice"].Value.ToString());
                            otherCharges = decimal.Parse(row.Cells["OtherCharges"].Value.ToString());

                            item = orderDetailItems.Where(o => o.IndicoOrderID == orderId && o.FactoryPrice == factoryPrice && o.OtherCharges == otherCharges).ToList();
                        }
                        else
                        {
                            orderId = int.Parse(row.Cells["IndicoOrderID"].Value.ToString());

                            item = orderDetailItems.Where(o => o.IndicoOrderID == orderId).ToList();
                        }

                        foreach (OrderDeatilItem oderdetailitem in item)
                        {
                            if (oderdetailitem != null)
                            {
                                oderdetailitem.FactoryPrice = (row.Cells["Factory Price"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Factory Price"].Value.ToString());
                                oderdetailitem.OtherCharges = (row.Cells["Other Charges"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Other Charges"].Value.ToString());
                                oderdetailitem.Notes = (row.Cells["NotesColumn"].Value == null) ? string.Empty : (row.Cells["NotesColumn"].Value.ToString());

                                currentInvoice.OrderDeatilItems.Add(oderdetailitem);
                            }
                        }
                    }
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    int orderId = 0;
                    decimal factoryPrice = 0.00M;
                    decimal indimanPrice = 0.00M;
                    decimal otherCharges = 0.00M;
                    List<OrderDeatilItem> item = null;

                    // Check indiman invoice number exist in the Invoice table when enter new indiman invoice
                    Invoice indimanInvoice = null;
                    IndicoPackingEntities context1 = new IndicoPackingEntities();
                    indimanInvoice = context1.Invoices.Where(i => i.ID == InvoiceId).FirstOrDefault();

                    foreach (GridViewRowInfo row in this.gridOrderDetail.Rows)
                    {
                        if (indimanInvoice.IndimanInvoiceNumber != null)
                        {
                            orderId = int.Parse(row.Cells["IndicoOrderID"].Value.ToString());
                            factoryPrice = decimal.Parse(row.Cells["FactoryPrice"].Value.ToString());
                            indimanPrice = decimal.Parse(row.Cells["IndimanPrice"].Value.ToString());
                            otherCharges = decimal.Parse(row.Cells["OtherCharges"].Value.ToString());

                            item = orderDetailItems.Where(o => o.IndicoOrderID == orderId && o.FactoryPrice == factoryPrice && o.IndimanPrice == indimanPrice && o.OtherCharges == otherCharges).ToList();
                        }
                        else
                        {
                            orderId = int.Parse(row.Cells["IndicoOrderID"].Value.ToString());

                            item = orderDetailItems.Where(o => o.IndicoOrderID == orderId).ToList();
                        }

                        foreach (OrderDeatilItem oderdetailitem in item)
                        {
                            if (oderdetailitem != null)
                            {
                                oderdetailitem.IndimanPrice = (row.Cells["Indiman Price"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Indiman Price"].Value.ToString());
                            }
                        }
                    }
                }
            }

            context.SaveChanges();

            // Set the added invoice number to InvoiceId
            this.InvoiceId = currentInvoice.ID;
        }

        #endregion                                
    }  
}
