using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using IndicoPacking.Model;
using IndicoPacking.ViewModels;
using IndicoPacking.Common;
using IndicoPacking.CustomModels;
using IndicoPacking.DAL.Base.Implementation;
using IndicoPacking.DAL.Objects.Implementation;
using IndicoPacking.Tools;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    enum InvoiceFor
    {
        Indiman = 0,
        Factory = 1,
        IndimanNew = 2
    }

    public partial class AddInvoice : IndicoPackingForm
    {
        #region Fields

        int _shipmentDetailId;
        private string installedFolder = string.Empty;
        private List<DistributorClientAddressBo> _distributorClientAddress;

        private int _lastWidth;
        private int _lastHegiht;

        #region Resize Rules (Collection of Controls)

        private readonly List<Control> _toLeftControls;
        private readonly List<Control> _steachControls;
        private readonly List<Control> _toBottomControls;
        
        #endregion
        
        #endregion

        #region Properties

        public int InvoiceId { get; set; }

        public int TypeOfInvoice { get; set; }

        #endregion

        #region Constructors

        public AddInvoice()
        {
            InitializeComponent();

            _toLeftControls = new List<Control> { btnInvoiceSummary ,btnInvoiceDetail,btnCombinedInvoice};
            _steachControls = new List<Control> { gridOrderDetail };
            _toBottomControls =  new List<Control> { btnCancel , btnSave, btnSaveAndPrint };

            var context = new IndicoPackingEntities();

            installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            LoadDropdown();
            using (var unit = new UnitOfWork())
            {
                var distributorClientAddresses = unit.DistributorClientAddressRepository.Get().ToList();
                _distributorClientAddress = distributorClientAddresses;
                var lstShipTo = distributorClientAddresses.OrderBy(a=>a.CompanyName)
                    .Select(b=> new ShipToView { ID = b.ID, CompanyName = b.CompanyName }).ToList();

                lstShipTo.Insert(0, new ShipToView { ID = 0, CompanyName = "Please Select..." });
                cmbShipTo.DataSource = lstShipTo;
                cmbShipTo.DisplayMember = "CompanyName";
                cmbShipTo.ValueMember = "ID";
                cmbShipTo.SelectedIndex = 0;

                // Load Mode drop down
                var lstMode = unit.ShipmentModeRepository.Get().Select(b=> new ShipmentModeView { ID = b.ID, Name = b.Name }).ToList(); 

                lstMode.Insert(0, new ShipmentModeView { ID = 0, Name = "Please Select..." });
                cmbMode.DataSource = lstMode;
                cmbMode.DisplayMember = "Name";
                cmbMode.ValueMember = "ID";
                cmbMode.SelectedIndex = 0;

                // Load BillTo drop down
                var lstBillTo = distributorClientAddresses.OrderBy(b => b.CompanyName).Select(b => new BillToView { ID = b.ID, CompanyName = b.CompanyName}).ToList();

                lstBillTo.Insert(0, new BillToView { ID = 0, CompanyName = "Please Select..." });
                cmbBillTo.DataSource = lstBillTo;
                cmbBillTo.DisplayMember = "CompanyName";
                cmbBillTo.ValueMember = "ID";
                cmbBillTo.SelectedIndex = 0;

                // Load Port drop down   
                var lstPort = (from b in context.Ports
                               select new PortView { ID = b.ID, Name = b.Name }).ToList();

                lstPort.Insert(0, new PortView { ID = 0, Name = "Please Select..." });
                cmbPort.DataSource = lstPort;
                cmbPort.DisplayMember = "Name";
                cmbPort.ValueMember = "ID";
                cmbPort.SelectedIndex = 0;

                // Load Bank drop down    
                var lst = (from b in context.Banks
                           select new BankView { ID = b.ID, Name = b.Name }).ToList();

                lst.Insert(0, new BankView { ID = 0, Name = "Please Select..." });
                cmbBank.DataSource = lst;
                cmbBank.DisplayMember = "Name";
                cmbBank.ValueMember = "ID";
                cmbBank.SelectedIndex = 0;

                // Load Status drop down    
                cmbStatus.DataSource = context.InvoiceStatus.ToList();
                cmbStatus.DisplayMember = "Name";
                cmbStatus.ValueMember = "ID";
                cmbStatus.SelectedIndex = 0;

                // Set invoice date to current date
                dtInvoiceDate.Value = DateTime.Now;

                cmbWeek.SelectedIndexChanged += cmbWeek_SelectedIndexChanged;
                cmbShipmentKey.SelectedIndexChanged += cmbShipmentKey_SelectedIndexChanged;
                cmbBillTo.SelectedIndexChanged += cmbBillTo_SelectedIndexChanged;
                cmbShipTo.SelectedIndexChanged += cmbShipTo_SelectedIndexChanged;
                rbWithGrroupByQty.CheckedChanged += rbWithGrroupByQty_CheckedChanged;

                // Hide invoice generate PDF buttons
                btnInvoiceDetail.Visible = false;
                btnInvoiceSummary.Visible = false;
                btnCombinedInvoice.Visible = false;
            }

         
        }

        private void AddInvoice_Load(object sender, EventArgs e)
        {
            //var context = new IndicoPackingEntities();
            _lastHegiht = Height;
            _lastWidth = Width;

            if (InvoiceId > 0)
            {
                using (var unit = new UnitOfWork())
                {
                    var currentInvoice = unit.InvoiceRepository.Get(InvoiceId);

                    if (currentInvoice != null)
                    {
                        // Disable fields when edit
                        cmbWeek.Enabled = false;
                        dtETD.Enabled = false;
                        cmbShipmentKey.Visible = false;
                        txtInvoiceNumber.Enabled = false;
                        dtInvoiceDate.Enabled = false;

                        //Enable Invoice Pdf generate buttons
                        btnInvoiceDetail.Visible = true;
                        btnInvoiceSummary.Visible = true;
                        btnCombinedInvoice.Visible = true;

                        cmbWeek.SelectedIndex = int.Parse(currentInvoice.ObjShipmentDetail.ObjShipment.WeekNo.ToString()) - 1;
                        dtETD.Value = currentInvoice.ShipmentDate;
                        lblShipmentKeyEdit.Text = currentInvoice.ObjShipmentDetail.ShipTo;

                        switch (TypeOfInvoice)
                        {
                            case (int)InvoiceFor.Factory:
                                txtInvoiceNumber.Text = currentInvoice.FactoryInvoiceNumber;
                                dtInvoiceDate.Value = currentInvoice.FactoryInvoiceDate;
                                btnSaveAndPrint.Visible = false; // hide save and print for factory invoice 
                                break;
                            case (int)InvoiceFor.Indiman:
                                if (currentInvoice.IndimanInvoiceNumber != null)
                                {
                                    txtInvoiceNumber.Text = currentInvoice.IndimanInvoiceNumber;
                                    dtInvoiceDate.Value = currentInvoice.IndimanInvoiceDate ?? DateTime.MinValue;
                                }
                                else
                                {
                                    txtInvoiceNumber.Enabled = true;
                                    dtInvoiceDate.Enabled = true;
                                }
                                break;
                        }

                        txtAWDNumber.Text = currentInvoice.AWBNumber;
                        cmbStatus.SelectedValue = int.Parse(currentInvoice.Status.ToString());
                        cmbPort.SelectedValue = int.Parse(currentInvoice.Port.ToString());
                        cmbMode.SelectedValue = int.Parse(currentInvoice.ShipmentMode.ToString());
                        cmbShipTo.SelectedValue = int.Parse(currentInvoice.ShipTo.ToString());
                        cmbBillTo.SelectedValue = int.Parse(currentInvoice.BillTo.ToString());
                        cmbBank.SelectedValue = int.Parse(currentInvoice.Bank.ToString());

                        if (TypeOfInvoice == (int) InvoiceFor.Indiman)
                        {
                            CourierChargsInput.Text = currentInvoice.CourierCharges == null ? "" : currentInvoice.CourierCharges.GetValueOrDefault().ToString();
                        }
                    }
                }
            }
         

            // Disable fields for indiman invoice
            if (TypeOfInvoice == (int) InvoiceFor.Indiman)
            {
                cmbStatus.Enabled = false;
                cmbPort.Enabled = false;
                cmbMode.Enabled = false;
                cmbShipTo.Enabled = false;
                cmbBillTo.Enabled = false;
                cmbBank.Enabled = false;
                btnAddRemovedItems.Visible = false;
                btnInvoiceDetail.Visible = false;
                btnInvoiceSummary.Visible = false;
                btnCombinedInvoice.Visible = false;
            }
            else
            {
                label3.Visible = CourierChargsInput.Visible = false;
            }

            // Remove buttonclick
            gridOrderDetail.CommandCellClick += gridOrderDetail_CommandCellClick;
            //Factory Price cell value changed
            gridOrderDetail.CellValueChanged += gridOrderDetail_CellValueChanged;           

            lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();
            rbWithGrroupByQty.Checked = true;
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

            if (rbWithGrroupByQty.Checked)
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
            else if (rbWithoutGroupByQty.Checked)
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

            GridViewRowInfo clickedRow = gridOrderDetail.Rows[e.RowIndex];

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
            gridOrderDetail.Rows.Remove(clickedRow);
            
            context.SaveChanges();

            lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();
        }

        private void cmbWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            gridOrderDetail.Rows.Clear();
            gridOrderDetail.Columns.Clear();
            gridOrderDetail.Rows.Clear();
            gridOrderDetail.DataSource = null;
            cmbShipmentKey.DataSource = null;

            if(cmbBank.SelectedIndex > -1)
            {
                cmbBank.SelectedIndex = 0;
                cmbShipTo.SelectedIndex = 0;
                cmbMode.SelectedIndex = 0;
                cmbBillTo.SelectedIndex = 0;
                cmbPort.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                lblBillToAddress.Text = "";
                lblShipToAddress.Text = "";
            }

            int shipmentId = (int)((System.Collections.Generic.KeyValuePair<int, string>)cmbWeek.SelectedValue).Key;
          
            var shipmentdetail = (from s in context.GetShipmentKeysViews
                                    where s.Shipment == shipmentId
                                    select new ShipmentKeyView { ID = s.ID, ShipTo = s.ShipTo, Shipment = s.Shipment, ETD = s.ETD, Port = s.Port, ShipmentMode = s.ShipmentMode, AvailableQuantity = s.AvailableQuantity }).ToList();            

            if (shipmentdetail != null && shipmentdetail.Count > 0)
            {
                dtETD.Value = shipmentdetail[0].ETD;
                cmbShipmentKey.DataSource = shipmentdetail;                

                cmbShipmentKey.Columns["Qty"].IsVisible = false;
                gridOrderDetail.Columns["Factory Price"].EnableExpressionEditor = true;
            }                         
        }

        private void cmbShipmentKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbShipmentKey.SelectedIndex > -1)
            {
                var selectedItem = (GridViewDataRowInfo)cmbShipmentKey.SelectedItem;
                var key = (ShipmentKeyView)selectedItem.DataBoundItem;

                cmbShipTo.SelectedValue = cmbShipTo.FindStringExact(key.ShipTo.ToString());
                cmbBillTo.SelectedValue = cmbBillTo.FindStringExact(key.ShipTo.ToString());
                cmbMode.SelectedValue = cmbMode.FindStringExact(key.ShipmentMode.ToString());
                cmbPort.SelectedValue = cmbPort.FindStringExact(key.Port.ToString());

                // Load order detail items 
                _shipmentDetailId = key.ID;
                using (var unit = new UnitOfWork())
                {
                    var shipmentDetail = unit.ShipmentDetailRepository.Get(_shipmentDetailId);
                    var distributorName = shipmentDetail.ShipTo;
                    var addressesForDistributor = _distributorClientAddress.Where(a => a.CompanyName.ToLower().Trim() == distributorName.ToLower()).OrderBy(o=>o.CompanyName).ToList();
                    var otherAddresses = new List<DistributorClientAddressBo>();
                    _distributorClientAddress.ForEach(d=> {if(!addressesForDistributor.Contains(d)) {otherAddresses.Add(d);} });
                    otherAddresses = otherAddresses.OrderBy(o => o.CompanyName).ToList();
                    var addresses = addressesForDistributor.Concat(otherAddresses).ToList();

                    var lstBillTo = addresses.Select(b => new BillToView { ID = b.ID, CompanyName = b.CompanyName }).ToList();

                    lstBillTo.Insert(0, new BillToView { ID = 0, CompanyName = "Please Select..." });
                    cmbBillTo.DataSource = lstBillTo;
                    cmbBillTo.DisplayMember = "CompanyName";
                    cmbBillTo.ValueMember = "ID";
                    cmbBillTo.SelectedIndex = 0;

                    var lstShipTo = addresses
                    .Select(b => new ShipToView { ID = b.ID, CompanyName = b.CompanyName }).ToList();

                    lstShipTo.Insert(0, new ShipToView { ID = 0, CompanyName = "Please Select..." });
                    cmbShipTo.DataSource = lstShipTo;
                    cmbShipTo.DisplayMember = "CompanyName";
                    cmbShipTo.ValueMember = "ID";
                    cmbShipTo.SelectedIndex = 0;
                }

                 

                gridOrderDetail.CreateCell += gridOrderDetail_CreateCell;                

                gridOrderDetail.Columns.Clear();
                      
                // Default checked radio button
                rbWithoutGroupByQty.Checked = true;

                if (rbWithGrroupByQty.Checked)
                {
                    OrderDetailGroupByQty();
                }
                else if (rbWithoutGroupByQty.Checked)
                {
                    OrderDetailWithoutGroupByQty();
                }
                lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();              
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

            if (cmbShipTo.SelectedIndex > 0)
            {
                int distributerClientAddressId = int.Parse(cmbShipTo.SelectedValue.ToString());

                DistributorClientAddress dca = (from d in context.DistributorClientAddresses
                                                where d.ID == distributerClientAddressId
                                                select d).FirstOrDefault();

                lblShipToAddress.Text = AddressToString(dca);
            }
        }

        private void cmbBillTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            if (cmbShipTo.SelectedIndex > 0)
            {
                int distributerClientAddressId = int.Parse(cmbBillTo.SelectedValue.ToString());

                DistributorClientAddress dca = (from d in context.DistributorClientAddresses
                                                where d.ID == distributerClientAddressId
                                                select d).FirstOrDefault();

                lblBillToAddress.Text = AddressToString(dca);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;
            btnSave.Enabled = false;
            SaveChanges();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rbWithGrroupByQty_CheckedChanged(object sender, EventArgs e)
        {
            if (rbWithGrroupByQty.Checked)
            {
                OrderDetailGroupByQty();
                lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();
            }
            else if (rbWithoutGroupByQty.Checked)
            {
                OrderDetailWithoutGroupByQty();
                lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();
            }
        }

        private void btnAddRemovedItems_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Before adding removed rows have to save the changes done to the grid. Do you wish to save and continue?", "Add Removed Item(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ValidateForm())
                {
                    SaveChanges();
                }
                else
                {
                    return;
                }

                IndicoPackingEntities context = new IndicoPackingEntities();

                RemovedInvoiceItems removedItems = new RemovedInvoiceItems();
                removedItems.ShipmentDetailId = _shipmentDetailId;
                removedItems.gridInvoiceOrders = gridOrderDetail;
                removedItems.StartPosition = FormStartPosition.CenterScreen;
                if (rbWithGrroupByQty.Checked)
                    removedItems.IsGroupByQty = true;
                removedItems.ShowDialog();

                if (!removedItems.IsCancelledHit)
                {
                    gridOrderDetail.Columns.Clear();

                    if (TypeOfInvoice == (int)InvoiceFor.Factory)
                    {
                        if (rbWithoutGroupByQty.Checked)
                        {
                            var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                                where removedItems.AllIds.Contains(odi.ID)
                                                select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, FactoryPrice = odi.FactoryPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges }).ToList();

                            gridOrderDetail.DataSource = orderDetails;
                            LoadGridOrdeDetailColumns();

                            if (orderDetails != null && orderDetails.Count > 0)
                            {
                                int i = 0;
                                foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                                {
                                    row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                                    row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                                    i++;
                                }
                            }

                            gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                            gridOrderDetail.Columns["ID"].IsVisible = false;
                            gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";
                            gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                            gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";

                            gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                        }
                        else if (rbWithGrroupByQty.Checked)
                        {
                            var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                where removedItems.AllIds.Contains(odi.IndicoOrderID)
                                                select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                            gridOrderDetail.DataSource = orderDetails;
                            LoadGridOrdeDetailColumns();

                            if (orderDetails != null && orderDetails.Count > 0)
                            {
                                int i = 0;
                                foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                                {
                                    row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                                    row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                                    i++;
                                }
                            }

                            gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                        }
                    }
                    else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                    {
                        var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                            where removedItems.AllIds.Contains(odi.ID)
                                            select new { odi.ID, odi.PurchaseOrder, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderType, odi.VisualLayout, odi.Distributor, odi.Client, odi.Pattern, odi.Fabric, odi.Gender, odi.AgeGroup, odi.SleeveShape, odi.SleeveLength, odi.SizeDesc, odi.SizeQty, odi.SizeSrno, odi.FactoryPrice, odi.IndimanPrice, odi.OtherCharges }).ToList();

                        gridOrderDetail.DataSource = orderDetails;
                        LoadGridOrdeDetailColumns();

                        if (orderDetails != null && orderDetails.Count > 0)
                        {
                            int i = 0;
                            foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                            {
                                row.Cells["Indiman Price"].Value = orderDetails[i].IndimanPrice.ToString();
                                i++;
                            }
                        }

                        gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                        gridOrderDetail.Columns["Other Charges"].IsVisible = false;
                        gridOrderDetail.Columns["TotalPrice"].IsVisible = false;
                    }
                }

                lblItemCount.Text = gridOrderDetail.Rows.Count.ToString();
            }
        }

        private void btnApplyCostSheetPrice_Click_1(object sender, EventArgs e)
        {
            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                {
                    if (double.Parse(row.Cells["JKFOBCostSheetPrice"].Value.ToString()) != 0.00)
                        row.Cells["Factory Price"].Value = row.Cells["JKFOBCostSheetPrice"].Value.ToString();
                }
            }
            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                foreach (GridViewRowInfo row in gridOrderDetail.Rows)
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

            cmbWeek.DataSource = new BindingSource(weekendSource, null);
            cmbWeek.DisplayMember = "Value";    
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
            gridOrderDetail.Columns.Clear();

            IndicoPackingEntities context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (InvoiceId != 0)
            {
                currentInvoice = context.Invoices.Where(i => i.ID == InvoiceId).FirstOrDefault();
                _shipmentDetailId = currentInvoice.ShipmentDetail;
            }

            
            if (gridOrderDetail.MasterTemplate.SummaryRowsBottom.Count < 1)
            {
                var qtyTotal = new GridViewSummaryItem("Qty", "Total :   {0}", GridAggregateFunction.Sum);
                var amountTotal = new GridViewSummaryItem("Amount", "Total : {0}", GridAggregateFunction.Sum);
                gridOrderDetail.MasterTemplate.SummaryRowsBottom.Add(new GridViewSummaryRowItem(new[] { qtyTotal,amountTotal }));
            }
           

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                // When editing the invoice load orderdetails 
                if (currentInvoice == null)
                {
                    var orderDetailsWithGroupByFactory = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityGroupByForFactories
                                                          where odi.ShipmentDeatil == _shipmentDetailId && odi.Invoice == null
                                                          select new GroupByQtyFactoryView { PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType,
                                                              VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup,
                                                              SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, Qty = odi.Qty, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes}).ToList();

                    gridOrderDetail.DataSource = orderDetailsWithGroupByFactory;
                    LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
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

                    gridOrderDetail.DataSource = orderDetailsWithGroupByFactory;

                    LoadGridOrdeDetailColumns();

                    // iterate thorugh the grid rows
                    if (orderDetailsWithGroupByFactory != null && orderDetailsWithGroupByFactory.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                        {
                            row.Cells["Factory Price"].Value = orderDetailsWithGroupByFactory[i].FactoryPrice.ToString();
                            row.Cells["Other Charges"].Value = orderDetailsWithGroupByFactory[i].OtherCharges.ToString();
                            row.Cells["NotesColumn"].Value = orderDetailsWithGroupByFactory[i].Notes.ToString();
                            i++;
                        }
                    }
                }     
           
                gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                gridOrderDetail.Columns["Notes"].IsVisible = false;
                gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "Costsheet Price";

                foreach (GridViewColumn column in gridOrderDetail.Columns)
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
                    var connection = IndicoPackingConnection;
                    var orderDetails = connection.Query(string.Format("SELECT * FROM [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman] WHERE Invoice = {0} AND IndimanPrice = 0 ",currentInvoice.ID));
                    connection.Close();
                    var orderDetailsWithGroupByIndiman = orderDetails.Select(odi => new { odi.PurchaseOrder, odi.IndicoOrderID, odi.IndicoOrderDetailID, odi.OrderType, odi.VisualLayout, odi.Distributor, odi.Client, odi.Pattern, odi.Fabric, odi.Gender, odi.AgeGroup, odi.SleeveShape, odi.SleeveLength, odi.Qty, odi.Notes, odi.FactoryPrice, odi.IndimanPrice, odi.OtherCharges, odi.JKFOBCostSheetPrice, odi.IndimanCIFCostSheetPrice, odi.ProductNotes, odi.PatternNotes }).ToList();

                    gridOrderDetail.DataSource = orderDetailsWithGroupByIndiman;
                    LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                    {
                        row.Cells["Indiman Price"].Value = "0.00";
                    }
                }
                else
                {
                    var connection = IndicoPackingConnection;
                    var orderDetailsWithGroupByIndiman = connection.Query<OrderDetailsWithGroupByIndimanModel>(string.Format("SELECT * FROM [dbo].[GetInvoiceOrderDetailItemsWithQuatityGroupByForIndiman] WHERE Invoice = {0} AND IndimanPrice != 0", currentInvoice.ID)).ToList();
                    connection.Close();

                    gridOrderDetail.DataSource = orderDetailsWithGroupByIndiman;
                    LoadGridOrdeDetailColumns();

                    if (orderDetailsWithGroupByIndiman.Count > 0)
                    {
                        var i = 0;
                        foreach (var row in gridOrderDetail.Rows)
                        {
                            row.Cells["Indiman Price"].Value = orderDetailsWithGroupByIndiman[i].IndimanPrice.ToString();
                            row.Cells["Other Charges"].Value = orderDetailsWithGroupByIndiman[i].OtherCharges.ToString();
                            i++;
                        }
                    }
                }    

                gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                gridOrderDetail.Columns["NotesColumn"].IsVisible = false;

                gridOrderDetail.Columns["FactoryPrice"].HeaderText = "Factory Price";
                gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "JK Costsheet Price";

                if (gridOrderDetail.Columns["JKFOBCostSheetPrice"]!=null)
                {
                    gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].HeaderText = "Costsheet Price";
                }
               

                foreach (var column in gridOrderDetail.Columns)
                {
                    if (column.Name == "Indiman Price")
                        return;
                    column.ReadOnly = true;
                }
            }
        }

        private void OrderDetailWithoutGroupByQty()
        {
            gridOrderDetail.Columns.Clear();

            IndicoPackingEntities context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (InvoiceId != 0)
            {
                currentInvoice = context.Invoices.Where(i => i.ID == InvoiceId).FirstOrDefault();
                _shipmentDetailId = currentInvoice.ShipmentDetail;
            }
            if (gridOrderDetail.MasterTemplate.SummaryRowsBottom.Count < 1)
            {
                var amountTotal = new GridViewSummaryItem("Amount", "Total : {0}", GridAggregateFunction.Sum);
                gridOrderDetail.MasterTemplate.SummaryRowsBottom.Add(new GridViewSummaryRowItem(new[] { amountTotal }));
            }
            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                if (currentInvoice == null)
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where odi.ShipmentDeatil == _shipmentDetailId && odi.Invoice == null
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, FactoryPrice = odi.FactoryPrice, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, Notes = odi.Notes }).ToList();

                    gridOrderDetail.DataSource = orderDetails;
                    LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
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

                    gridOrderDetail.DataSource = orderDetails;
                    LoadGridOrdeDetailColumns();

                    if (orderDetails != null && orderDetails.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                        {
                            row.Cells["Factory Price"].Value = orderDetails[i].FactoryPrice.ToString();
                            row.Cells["Other Charges"].Value = orderDetails[i].OtherCharges.ToString();
                            i++;
                        }
                    }
                }

                gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].IsVisible = false;
                gridOrderDetail.Columns["ID"].IsVisible = false;
                gridOrderDetail.Columns["FactoryPrice"].IsVisible = false;
                gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                gridOrderDetail.Columns["Notes"].IsVisible = false;
                gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "Costsheet Price";
                gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";
                gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";  


                foreach (GridViewColumn column in gridOrderDetail.Columns)
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

                    gridOrderDetail.DataSource = orderDetails;
                    LoadGridOrdeDetailColumns();

                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                    {
                        row.Cells["Indiman Price"].Value = "0.00";
                    }
                }
                else
                {
                    var orderDetails = (from odi in context.GetInvoiceOrderDetailItemsWithQuatityBreakdowns
                                        where odi.Invoice == currentInvoice.ID && odi.IndimanPrice != 0
                                        select new OrderDetailView { ID = odi.ID, PurchaseOrder = odi.PurchaseOrder, IndicoOrderID = odi.IndicoOrderID, IndicoOrderDetailID = odi.IndicoOrderDetailID, OrderType = odi.OrderType, VisualLayout = odi.VisualLayout, Distributor = odi.Distributor, Client = odi.Client, Pattern = odi.Pattern, Fabric = odi.Fabric, Gender = odi.Gender, AgeGroup = odi.AgeGroup, SleeveShape = odi.SleeveShape, SleeveLength = odi.SleeveLength, SizeDesc = odi.SizeDesc, SizeQty = odi.SizeQty, SizeSrno = odi.SizeSrno, Notes = odi.Notes, FactoryPrice = odi.FactoryPrice, IndimanPrice = odi.IndimanPrice, OtherCharges = odi.OtherCharges, JKFOBCostSheetPrice = odi.JKFOBCostSheetPrice, IndimanCIFCostSheetPrice = odi.IndimanCIFCostSheetPrice }).ToList();

                    gridOrderDetail.DataSource = orderDetails;
                    LoadGridOrdeDetailColumns();

                    if (orderDetails != null && orderDetails.Count > 0)
                    {
                        int i = 0;
                        foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                        {
                            row.Cells["Indiman Price"].Value = orderDetails[i].IndimanPrice.ToString();
                            i++;
                        }
                    }
                }

                gridOrderDetail.Columns["IndimanPrice"].IsVisible = false;
                gridOrderDetail.Columns["ID"].IsVisible = false;
                gridOrderDetail.Columns["Factory Price"].IsVisible = false;
                //gridOrderDetail.Columns["Other Charges"].IsVisible = false;
                //gridOrderDetail.Columns["TotalPrice"].IsVisible = false;
                gridOrderDetail.Columns["NotesColumn"].IsVisible = false;
                gridOrderDetail.Columns["JKFOBCostSheetPrice"].HeaderText = "JK Costsheet Price";
                gridOrderDetail.Columns["IndimanCIFCostSheetPrice"].HeaderText = "Costsheet Price";
                gridOrderDetail.Columns["FactoryPrice"].HeaderText = "Factory Price";
                gridOrderDetail.Columns["SizeQty"].HeaderText = "Size Qty";
                gridOrderDetail.Columns["SizeSrno"].HeaderText = "Size Srno";
                gridOrderDetail.Columns["SizeDesc"].HeaderText = "Size Desc";  

                foreach (GridViewColumn column in gridOrderDetail.Columns)
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
            otherChargesColumn.ParentGrid = gridOrderDetail;
            gridOrderDetail.Columns.Add(otherChargesColumn);

            FactoryPriceColumn factoryPriceColumn = new FactoryPriceColumn("Factory Price");
            factoryPriceColumn.Width = 120;
            factoryPriceColumn.ParentGrid = gridOrderDetail;
            gridOrderDetail.Columns.Add(factoryPriceColumn);

            IndimanPriceColumn indimanPriceColumn = new IndimanPriceColumn("Indiman Price");
            indimanPriceColumn.Width = 120;
            indimanPriceColumn.ParentGrid = gridOrderDetail;
            gridOrderDetail.Columns.Add(indimanPriceColumn);
            
            gridOrderDetail.MasterView.TableHeaderRow.MinHeight = 50;
            gridOrderDetail.AutoSizeRows = true;

            gridOrderDetail.Columns["IndicoOrderID"].IsVisible = false;
            gridOrderDetail.Columns["IndicoOrderDetailID"].HeaderText = "Order Detail ID";
            gridOrderDetail.Columns["IndicoOrderDetailID"].Width = 100;
            gridOrderDetail.Columns["OrderType"].HeaderText = "Order Type";
            gridOrderDetail.Columns["OrderType"].Width = 80;
            gridOrderDetail.Columns["VisualLayout"].HeaderText = "Visual Layout";
            gridOrderDetail.Columns["AgeGroup"].HeaderText = "Age Group";
            gridOrderDetail.Columns["SleeveShape"].HeaderText = "Sleeve Shape";
            gridOrderDetail.Columns["SleeveLength"].HeaderText = "Sleeve Length";
            gridOrderDetail.Columns["OtherCharges"].HeaderText = "Other Charges";
            gridOrderDetail.Columns["Factory Price"].AllowFiltering = false;
            gridOrderDetail.Columns["Indiman Price"].AllowFiltering = false;
            gridOrderDetail.AllowDragToGroup = false;
            gridOrderDetail.Columns["OtherCharges"].IsVisible = false;
            gridOrderDetail.Columns["SleeveShape"].IsVisible = false;
            gridOrderDetail.Columns["SleeveLength"].IsVisible = false;
               
            // Add Total Price column
            GridViewTextBoxColumn totalPrice = new GridViewTextBoxColumn();
            totalPrice.Name = "TotalPrice";
            totalPrice.HeaderText = "Total Price";
            totalPrice.TextAlignment = ContentAlignment.MiddleRight;
            gridOrderDetail.MasterTemplate.Columns.Add(totalPrice);

            gridOrderDetail.Columns["TotalPrice"].ReadOnly = true;
            gridOrderDetail.Columns["TotalPrice"].Width = 100;

            // Add Ammount column
            GridViewTextBoxColumn ammount = new GridViewTextBoxColumn();
            ammount.Name = "Amount";
            ammount.HeaderText = "Amount";
            ammount.TextAlignment = ContentAlignment.MiddleRight;
            gridOrderDetail.MasterTemplate.Columns.Add(ammount);

            gridOrderDetail.Columns["Amount"].ReadOnly = true;
            gridOrderDetail.Columns["Amount"].Width = 100;

            // Add Notes column
            GridViewTextBoxColumn notesColumn = new GridViewTextBoxColumn();
            notesColumn.Name = "NotesColumn";
            notesColumn.HeaderText = "Notes";
            notesColumn.TextAlignment = ContentAlignment.MiddleLeft;
            gridOrderDetail.MasterTemplate.Columns.Add(notesColumn);

            gridOrderDetail.Columns["NotesColumn"].Width = 100;

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                // Add remove button column
                GridViewCommandColumn removeColumn = new GridViewCommandColumn();
                removeColumn.Name = "RemoveColumn";
                removeColumn.UseDefaultText = true;
                removeColumn.DefaultText = "Remove";
                removeColumn.FieldName = "ID";
                removeColumn.HeaderText = "";
                gridOrderDetail.MasterTemplate.Columns.Add(removeColumn);

                gridOrderDetail.Columns["RemoveColumn"].Width = 80;
                gridOrderDetail.Columns["Indiman Price"].IsVisible = false;
            }            
        }

        private void SaveChanges()
        {
            var context = new IndicoPackingEntities();
            Invoice currentInvoice = null;

            if (InvoiceId != 0)
            {
                currentInvoice = context.Invoices.FirstOrDefault(i => i.ID == InvoiceId);
                if (currentInvoice != null)
                    _shipmentDetailId = currentInvoice.ShipmentDetail;
            }

            if (currentInvoice == null)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    currentInvoice = new Invoice
                    {
                        ShipmentDetail = _shipmentDetailId,
                        ShipmentDate = dtETD.Value,
                        CreatedDate = DateTime.Now,
                        AWBNumber = txtAWDNumber.Text,
                        LastModifiedBy = LoginInfo.UserID,
                        ModifiedDate = DateTime.Now,
                        Status = ((InvoiceStatu)(cmbStatus.SelectedItem)).ID,
                        ShipTo = ((ShipToView)(cmbShipTo.SelectedItem)).ID,
                        BillTo = ((BillToView)(cmbBillTo.SelectedItem)).ID,
                        ShipmentMode = ((ShipmentModeView)(cmbMode.SelectedItem)).ID,
                        Port = ((PortView)(cmbPort.SelectedItem)).ID,
                        Bank = ((BankView)(cmbBank.SelectedItem)).ID,
                        FactoryInvoiceNumber = txtInvoiceNumber.Text,
                        FactoryInvoiceDate = dtInvoiceDate.Value,
                    };

                    currentInvoice = context.Invoices.Add(currentInvoice);
                }
            }
            else
            {
                if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    currentInvoice.IndimanInvoiceNumber = txtInvoiceNumber.Text;
                    currentInvoice.IndimanInvoiceDate = dtInvoiceDate.Value;
                }

                currentInvoice.AWBNumber = txtAWDNumber.Text;
                currentInvoice.LastModifiedBy = LoginInfo.UserID;
                currentInvoice.ModifiedDate = DateTime.Now;
                currentInvoice.Status = ((InvoiceStatu)(cmbStatus.SelectedItem)).ID;
                currentInvoice.ShipTo = ((ShipToView)(cmbShipTo.SelectedItem)).ID;
                currentInvoice.BillTo = ((BillToView)(cmbBillTo.SelectedItem)).ID;
                currentInvoice.ShipmentMode = ((ShipmentModeView)(cmbMode.SelectedItem)).ID;
                currentInvoice.Port = ((PortView)(cmbPort.SelectedItem)).ID;
                currentInvoice.Bank = ((BankView)(cmbBank.SelectedItem)).ID;
                int cc;
                currentInvoice.CourierCharges = int.TryParse(CourierChargsInput.Text, out cc) ? cc : 0;
            }

            var orderDetailItems = (from odi in context.OrderDeatilItems
                                    where odi.ShipmentDeatil == _shipmentDetailId
                                    select odi).ToList();

            if (rbWithoutGroupByQty.Checked)
            {
                int orderId;


                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    foreach (var row in gridOrderDetail.Rows)
                    {
                        orderId = int.Parse(row.Cells["ID"].Value.ToString());

                        var item = orderDetailItems.FirstOrDefault(o => o.ID == orderId);

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
                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
                    {
                        orderId = int.Parse(row.Cells["ID"].Value.ToString());
                        //sizeName = row.Cells["SizeDesc"].Value.ToString();
                        //sizeSeqNumber = int.Parse(row.Cells["SizeSrno"].Value.ToString());

                        OrderDeatilItem item = orderDetailItems.Where(o => o.ID == orderId).FirstOrDefault();

                        if (item != null)
                        {
                            item.IndimanPrice = (row.Cells["Indiman Price"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Indiman Price"].Value.ToString());
                            item.OtherCharges = (row.Cells["Other Charges"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Other Charges"].Value.ToString());
                        }
                    }
                }
            }
            else if (rbWithGrroupByQty.Checked)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    int orderId;
                    decimal factoryPrice;
                    decimal otherCharges;
                    var orderDetailId = 0;


                    foreach (var row in gridOrderDetail.Rows)
                    {
                        List<OrderDeatilItem> item;
                        if (InvoiceId != 0)
                        {
                            orderId = int.Parse(row.Cells["IndicoOrderID"].Value.ToString());
                            factoryPrice = decimal.Parse(row.Cells["FactoryPrice"].Value.ToString());
                            otherCharges = decimal.Parse(row.Cells["OtherCharges"].Value.ToString());
                            orderDetailId = int.Parse(row.Cells["IndicoOrderDetailID"].Value.ToString());
                            item = orderDetailItems.Where(o => o.IndicoOrderDetailID == orderDetailId).ToList();

                            //item = orderDetailItems.Where(o => o.IndicoOrderID == orderId && o.FactoryPrice == factoryPrice && o.OtherCharges == otherCharges).ToList();
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

                    foreach (GridViewRowInfo row in gridOrderDetail.Rows)
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
                                oderdetailitem.OtherCharges = (row.Cells["Other Charges"].Value == null) ? (decimal?)0.0 : decimal.Parse(row.Cells["Other Charges"].Value.ToString());
                            }
                        }
                    }
                }
            }

            context.SaveChanges();
            if (currentInvoice != null)
                InvoiceId = currentInvoice.ID;

            //Update OrderDetail status,Order Status in Indico Database (to Shipped)
            try
            {
                if (InvoiceId == 0 && currentInvoice != null)
                {
                    using (var packingConnection = IndicoPackingConnection)
                    using (var connection = IndicoConnection)
                    {
                        var shipmentDetails = packingConnection.Query("SELECT odi.IndicoOrderDetailID AS OrderDetailId  " +
                                                                      "FROM[dbo].[OrderDeatilItem] odi  " +
                                                                      "WHERE odi.Invoice = " + currentInvoice.ID +
                                                                      "GROUP BY odi.IndicoOrderDetailID").Select(s => new { s.OrderDetailId }).ToList();
                        if (shipmentDetails.Count < 1)
                            return;
                        var shipmentDetailsparameterBuilder = new StringBuilder();
                        foreach (var shipmentDetail in shipmentDetails)
                        {
                            shipmentDetailsparameterBuilder.Append(shipmentDetail.OrderDetailId + ",");
                        }

                        var query = string.Format("EXEC [dbo].[SPC_UpdateOrderStatusToShipped] '{0}'", shipmentDetailsparameterBuilder.ToString().Trim(','));
                        connection.Execute(query);
                    }
                }
            }
            catch (Exception e)
            {
                IndicoPackingLog.GetObject().Log(e, "Unable to update Order Detail status in the Indico database ");
                MessageBox.Show("Unable to update Order Detail status in the Indico database ", "Unable to update Indico database .", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            InvoiceId = currentInvoice.ID;
        }


        private string AddressToString(DistributorClientAddress dca)
        {
            return dca==null ? "" 
                : string.Format("{0} {1} {2} {3} {4} {5}", dca.CompanyName, dca.Address, dca.PostCode, dca.Suburb, dca.State, dca.Country1.Name);
        }

        #endregion

        private void ManageControlsOnResize()
        {
            if (_toLeftControls != null && _toLeftControls.Count > 0)
            {
                foreach (var control in _toLeftControls)
                    control.Left = control.Left + (Width - _lastWidth);
            }

            if (_steachControls != null && _steachControls.Count > 0)
            {
                foreach (var control in _steachControls)
                {
                    control.Width = control.Width + (Width - _lastWidth);
                    control.Height = control.Height + (Height - _lastHegiht);
                }
            }

            if (_toBottomControls != null && _toBottomControls.Count > 0)
            {
                foreach (var control in _toBottomControls)
                    control.Top = control.Top + (Height - _lastHegiht);
            }
        }

        private void OnSizeChanged(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SuspendLayout();

            ManageControlsOnResize();

            _lastWidth = Width;
            _lastHegiht = Height;

            DoubleBuffered = false;
            ResumeLayout();
        }

        private void Save(bool close)
        {
            if (!ValidateForm()) return;
            btnSave.Enabled = false;
            SaveChanges();
            if(close)
                Close();
        }

        private void OnSaveAndPrintClick(object sender, EventArgs e)
        {
            Save(false);
            GeneratePDF.GenerateInvoices(InvoiceId, InstalledFolder, "IndimanInvoiceDetail.rdl", "IndimanInvoiceDetail", ReportType.Indiman);
            Close();
        }
    }  
}
