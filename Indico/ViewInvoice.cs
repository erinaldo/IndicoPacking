using IndicoPacking.Common;
using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    public partial class ViewInvoice : IndicoPackingForm
    {
        #region Fields

        readonly IndicoPackingEntities _context;

        #endregion

        #region Properties

        public int TypeOfInvoice { get; set; }

        #endregion

        #region Constructors

        public ViewInvoice()
        {
            InitializeComponent();
            _context = new IndicoPackingEntities();

            //_installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            gridInvoices.CommandCellClick += gridInvoices_CommandCellClick;
        }

        private void ViewInvoice_Load(object sender, EventArgs e)
        {
            LoadGridInvoice();
            AddCustomColumn();
        }

        #endregion

        #region Events      

        void gridInvoices_CommandCellClick(object sender, EventArgs e)
        {
            var cell = (GridCommandCellElement)sender;

            var clickedRow = gridInvoices.Rows[cell.RowIndex];
            var invoiceId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (gridInvoices.Columns["editColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["editColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    // Handle Edit                
                    AddInvoice addInvoice = new AddInvoice();
                    addInvoice.StartPosition = FormStartPosition.CenterScreen;
                    addInvoice.InvoiceId = invoiceId;
                    addInvoice.TypeOfInvoice = 1;
                    addInvoice.Text = "Edit Factory Invoice";
                    addInvoice.ShowDialog();
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    AddInvoice addInvoice = new AddInvoice();
                    addInvoice.StartPosition = FormStartPosition.CenterScreen;
                    addInvoice.InvoiceId = invoiceId;
                    addInvoice.TypeOfInvoice = 0;
                    addInvoice.Text = "Edit Indiman Invoice";
                    addInvoice.ShowDialog();
                }
            }

            if (gridInvoices.Columns["deleteColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["deleteColumn"].Index && cell.RowIndex >= 0)
            {
                // Handle Delete                
                if (MessageBox.Show("Are you sure, you want to delete this invoice?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Remove from Invoice table
                    _context.Invoices.Remove((from u in _context.Invoices
                                             where u.ID == invoiceId
                                             select u).FirstOrDefault());

                    // Set null invoice column and price column from order detail table
                    List<OrderDeatilItem> orderDetail = _context.OrderDeatilItems.Where(o => o.Invoice == invoiceId).ToList();

                    foreach (OrderDeatilItem odi in orderDetail)
                    {
                        odi.Invoice = null;
                        odi.FactoryPrice = null;
                        odi.IndimanPrice = null;
                        odi.OtherCharges = (decimal)0.00;
                    }

                    _context.SaveChanges();
                }
            }

            if (gridInvoices.Columns["newInvoiceColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["newInvoiceColumn"].Index && cell.RowIndex >= 0)
            {
                AddInvoice addInvoice = new AddInvoice();
                addInvoice.StartPosition = FormStartPosition.CenterScreen;
                addInvoice.InvoiceId = invoiceId;
                addInvoice.TypeOfInvoice = 0;
                addInvoice.Text = "New Indiman Invoice";
                addInvoice.ShowDialog();
            }

            if (gridInvoices.Columns["invoiceSummaryColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["invoiceSummaryColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, InstalledFolder, "JKInvoiceSummary.rdl","JKInvoiceSummary", ReportType.Summary);
                }                
            }

            if (gridInvoices.Columns["invoiceDetailColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["invoiceDetailColumn"].Index && cell.RowIndex >= 0)
            {
                switch (TypeOfInvoice)
                {
                    case (int)InvoiceFor.Factory:
                        GeneratePDF.GenerateInvoices(invoiceId, InstalledFolder, "JKInvoiceDetail.rdl", "JKInvoiceDetail", ReportType.Detail);
                        break;
                    case (int)InvoiceFor.Indiman:
                        GeneratePDF.GenerateInvoices(invoiceId, InstalledFolder, "IndimanInvoiceDetail.rdl", "IndimanInvoiceDetail", ReportType.Indiman);
                        break;
                }
            }

            if (gridInvoices.Columns["combinedInvoiceColumn"] != null && cell.ColumnIndex == gridInvoices.Columns["combinedInvoiceColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, InstalledFolder, "JKCombinedInvoice.rdl", "JKCombinedInvoice", ReportType.Combined);
                }               
            }

            gridInvoices.DataSource = null;
            gridInvoices.Columns.Clear();
            LoadGridInvoice();

            // Add custom column
            AddCustomColumn();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Private Methods

        private void AddCustomColumn()
        {
            GridViewCommandColumn invoiceDetailColumn = new GridViewCommandColumn();
            invoiceDetailColumn.Name = "invoiceDetailColumn";
            invoiceDetailColumn.UseDefaultText = true;
            invoiceDetailColumn.DefaultText = "Invoice Detail";
            invoiceDetailColumn.FieldName = "invoiceDetail";
            invoiceDetailColumn.HeaderText = "";
            invoiceDetailColumn.Width = 100;
            gridInvoices.MasterTemplate.Columns.Add(invoiceDetailColumn);

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                GridViewCommandColumn invoiceSummaryColumn = new GridViewCommandColumn();
                invoiceSummaryColumn.Name = "invoiceSummaryColumn";
                invoiceSummaryColumn.UseDefaultText = true;
                invoiceSummaryColumn.DefaultText = "Invoice Summary";
                invoiceSummaryColumn.FieldName = "invoiceSummary";
                invoiceSummaryColumn.HeaderText = "";
                invoiceSummaryColumn.Width = 100;
                gridInvoices.MasterTemplate.Columns.Add(invoiceSummaryColumn);

                GridViewCommandColumn combinedInvoiceColumn = new GridViewCommandColumn();
                combinedInvoiceColumn.Name = "combinedInvoiceColumn";
                combinedInvoiceColumn.UseDefaultText = true;
                combinedInvoiceColumn.DefaultText = "Combined Invoice";
                combinedInvoiceColumn.FieldName = "combinedInvoice";
                combinedInvoiceColumn.HeaderText = "";
                combinedInvoiceColumn.Width = 100;
                gridInvoices.MasterTemplate.Columns.Add(combinedInvoiceColumn);
            }

            GridViewCommandColumn editColumn = new GridViewCommandColumn();
            editColumn.Name = "editColumn";
            editColumn.UseDefaultText = true;
            editColumn.DefaultText = "Edit";
            editColumn.FieldName = "edit";
            editColumn.HeaderText = "";
            gridInvoices.MasterTemplate.Columns.Add(editColumn);

            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
                deleteColumn.Name = "deleteColumn";
                deleteColumn.UseDefaultText = true;
                deleteColumn.DefaultText = "Delete";
                deleteColumn.FieldName = "delete";
                deleteColumn.HeaderText = "";
                gridInvoices.MasterTemplate.Columns.Add(deleteColumn);
            }
            else if (TypeOfInvoice == (int)InvoiceFor.IndimanNew)
            {
                GridViewCommandColumn newInvoiceColumn = new GridViewCommandColumn();
                newInvoiceColumn.Name = "newInvoiceColumn";
                newInvoiceColumn.UseDefaultText = true;
                newInvoiceColumn.DefaultText = "New Invoice";
                newInvoiceColumn.FieldName = "newInvoice";
                newInvoiceColumn.HeaderText = "";
                gridInvoices.MasterTemplate.Columns.Add(newInvoiceColumn);

                gridInvoices.Columns["editColumn"].IsVisible = false;
                gridInvoices.Columns["invoiceDetailColumn"].IsVisible = false;
                gridInvoices.Columns["newInvoiceColumn"].Width = 80;
            }
        }

        private void LoadGridInvoice()
        {
            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                gridInvoices.DataSource = _context.InvoiceDetailsViews.ToList();

                gridInvoices.Columns["IndimanInvoiceNumber"].IsVisible = false;
                gridInvoices.Columns["IndimanInvoiceDate"].IsVisible = false;
            }
            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                gridInvoices.DataSource = (from i in _context.InvoiceDetailsViews
                                                where i.IndimanInvoiceNumber != null
                                                select i).ToList();
            }
            else if (TypeOfInvoice == (int)InvoiceFor.IndimanNew)
            {
                gridInvoices.DataSource = (from i in _context.InvoiceDetailsViews
                                                where i.IndimanInvoiceNumber == null
                                                select i).ToList();
            }

            if (TypeOfInvoice == (int) InvoiceFor.Factory)
            {
                gridInvoices.Columns["Qty"].IsVisible = false;
                gridInvoices.Columns["TotalAmount"].IsVisible = false;
                gridInvoices.Columns["CourierCharges"].IsVisible = false;
            }
            else
            {
                gridInvoices.Columns["TotalAmount"].HeaderText = "Total Amount";
                gridInvoices.Columns["CourierCharges"].HeaderText = "Courier Charges";
            }
            gridInvoices.Columns["ID"].IsVisible = false;
            gridInvoices.Columns["BillTo"].IsVisible = false;
            gridInvoices.Columns["LastModifiedBy"].IsVisible = false;
            gridInvoices.Columns["ModifiedDate"].IsVisible = false;

            gridInvoices.Columns["FactoryInvoiceDate"].HeaderText = "Invoice Date";
            gridInvoices.Columns["FactoryInvoiceNumber"].HeaderText = "Invoice Number";
            gridInvoices.Columns["IndimanInvoiceNumber"].HeaderText = "Indiman Invoice Number";
            gridInvoices.Columns["IndimanInvoiceDate"].HeaderText = "Indiman Invoice Date";
            //gridInvoices.Columns["ShipmentDetail"].HeaderText = "Week";
            gridInvoices.Columns["ShipmentDate"].HeaderText = "ETD";
            gridInvoices.Columns["CompanyName"].HeaderText = "Ship To";
            gridInvoices.Columns["PortName"].HeaderText = "Port";
            gridInvoices.Columns["ShipmentModeName"].HeaderText = "Shipment Mode";
            gridInvoices.Columns["AWBNumber"].HeaderText = "AWB Number";
            gridInvoices.Columns["StatusName"].HeaderText = "Status";


        }

        #endregion
    }
}
