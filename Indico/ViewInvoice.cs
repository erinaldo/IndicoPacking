using IndicoPacking.Common;
using IndicoPacking.Model;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    public partial class ViewInvoice : Form
    {
        #region Fields

        readonly IndicoPackingEntities _context;
        private readonly string _installedFolder;

        #endregion

        #region Properties

        public int TypeOfInvoice { get; set; }

        #endregion

        #region Constructors

        public ViewInvoice()
        {
            InitializeComponent();
            _context = new IndicoPackingEntities();

            _installedFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            this.gridInvoices.CommandCellClick += gridInvoices_CommandCellClick;
        }

        private void ViewInvoice_Load(object sender, EventArgs e)
        {
            this.LoadGridInvoice();
            this.AddCustomColumn();
        }

        #endregion

        #region Events      

        void gridInvoices_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;

            GridViewRowInfo clickedRow = this.gridInvoices.Rows[cell.RowIndex];
            int invoiceId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (this.gridInvoices.Columns["editColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["editColumn"].Index && cell.RowIndex >= 0)
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

            if (this.gridInvoices.Columns["deleteColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["deleteColumn"].Index && cell.RowIndex >= 0)
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

            if (this.gridInvoices.Columns["newInvoiceColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["newInvoiceColumn"].Index && cell.RowIndex >= 0)
            {
                AddInvoice addInvoice = new AddInvoice();
                addInvoice.StartPosition = FormStartPosition.CenterScreen;
                addInvoice.InvoiceId = invoiceId;
                addInvoice.TypeOfInvoice = 0;
                addInvoice.Text = "New Indiman Invoice";
                addInvoice.ShowDialog();
            }

            if (this.gridInvoices.Columns["invoiceSummaryColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["invoiceSummaryColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, _installedFolder, "JKInvoiceSummary.rdl","JKInvoiceSummary", ReportType.Summary);
                }                
            }

            if (this.gridInvoices.Columns["invoiceDetailColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["invoiceDetailColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, _installedFolder, "JKInvoiceDetail.rdl", "JKInvoiceDetail", ReportType.Detail);
                }
                else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, _installedFolder, "IndimanInvoiceDetail.rdl", "IndimanInvoiceDetail", ReportType.Indiman);
                }
            }

            if (this.gridInvoices.Columns["combinedInvoiceColumn"] != null && cell.ColumnIndex == this.gridInvoices.Columns["combinedInvoiceColumn"].Index && cell.RowIndex >= 0)
            {
                if (TypeOfInvoice == (int)InvoiceFor.Factory)
                {
                    GeneratePDF.GenerateInvoices(invoiceId, _installedFolder, "JKCombinedInvoice.rdl", "JKCombinedInvoice", ReportType.Combined);
                }               
            }

            this.gridInvoices.DataSource = null;
            this.gridInvoices.Columns.Clear();
            this.LoadGridInvoice();

            // Add custom column
            this.AddCustomColumn();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
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

                this.gridInvoices.Columns["editColumn"].IsVisible = false;
                this.gridInvoices.Columns["invoiceDetailColumn"].IsVisible = false;
                this.gridInvoices.Columns["newInvoiceColumn"].Width = 80;
            }
        }

        private void LoadGridInvoice()
        {
            if (TypeOfInvoice == (int)InvoiceFor.Factory)
            {
                this.gridInvoices.DataSource = _context.InvoiceDetailsViews.ToList();

                this.gridInvoices.Columns["IndimanInvoiceNumber"].IsVisible = false;
                this.gridInvoices.Columns["IndimanInvoiceDate"].IsVisible = false;
            }
            else if (TypeOfInvoice == (int)InvoiceFor.Indiman)
            {
                this.gridInvoices.DataSource = (from i in _context.InvoiceDetailsViews
                                                where i.IndimanInvoiceNumber != null
                                                select i).ToList();
            }
            else if (TypeOfInvoice == (int)InvoiceFor.IndimanNew)
            {
                this.gridInvoices.DataSource = (from i in _context.InvoiceDetailsViews
                                                where i.IndimanInvoiceNumber == null
                                                select i).ToList();
            }

            this.gridInvoices.Columns["ID"].IsVisible = false;
            this.gridInvoices.Columns["BillTo"].IsVisible = false;
            this.gridInvoices.Columns["LastModifiedBy"].IsVisible = false;
            this.gridInvoices.Columns["ModifiedDate"].IsVisible = false;

            this.gridInvoices.Columns["FactoryInvoiceDate"].HeaderText = "Invoice Date";
            this.gridInvoices.Columns["FactoryInvoiceNumber"].HeaderText = "Invoice Number";
            this.gridInvoices.Columns["IndimanInvoiceNumber"].HeaderText = "Indiman Invoice Number";
            this.gridInvoices.Columns["IndimanInvoiceDate"].HeaderText = "Indiman Invoice Date";
            //this.gridInvoices.Columns["ShipmentDetail"].HeaderText = "Week";
            this.gridInvoices.Columns["ShipmentDate"].HeaderText = "ETD";
            this.gridInvoices.Columns["CompanyName"].HeaderText = "Ship To";
            this.gridInvoices.Columns["PortName"].HeaderText = "Port";
            this.gridInvoices.Columns["ShipmentModeName"].HeaderText = "Shipment Mode";
            this.gridInvoices.Columns["AWBNumber"].HeaderText = "AWB Number";
            this.gridInvoices.Columns["StatusName"].HeaderText = "Status";
        }

        #endregion
    }
}
