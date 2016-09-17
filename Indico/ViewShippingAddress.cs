using IndicoPacking.Model;
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
    public partial class ViewShippingAddress : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Constructors

        public ViewShippingAddress()
        {
            InitializeComponent();          
        }

        private void ViewShippingAddress_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            this.LoadGridShippingAddresses();
            this.AddCustomColumn();

            this.gridShippingAddress.CommandCellClick += gridShippingAddress_CommandCellClick;
        }

        void gridShippingAddress_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;
            GridViewRowInfo clickedRow = this.gridShippingAddress.Rows[cell.RowIndex];

            int distributoeClientAddressId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (this.gridShippingAddress.Columns["editColumn"] != null && e.ColumnIndex == this.gridShippingAddress.Columns["editColumn"].Index && e.RowIndex >= 0)
            {
                AddShippingAddress frmAddShippngAddress = new AddShippingAddress();
                frmAddShippngAddress.StartPosition = FormStartPosition.CenterScreen;
                frmAddShippngAddress.Text = "Edit Shipping Address";
                frmAddShippngAddress.DistributorClientAddressId = distributoeClientAddressId;
                frmAddShippngAddress.ShowDialog();
            }
            else if (this.gridShippingAddress.Columns["deleteColumn"] != null && e.ColumnIndex == this.gridShippingAddress.Columns["deleteColumn"].Index && e.RowIndex >= 0)
            {
                context.DistributorClientAddresses.Remove((from u in context.DistributorClientAddresses
                                                           where u.ID == distributoeClientAddressId
                                                           select u).FirstOrDefault());

                context.SaveChanges();
            }

            this.gridShippingAddress.DataSource = null;
            this.gridShippingAddress.Columns.Clear();
            this.gridShippingAddress.Rows.Clear();

            this.LoadGridShippingAddresses();
            this.AddCustomColumn();
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Private Methods

        private void AddCustomColumn()
        {
            GridViewCommandColumn editColumn = new GridViewCommandColumn();
            editColumn.Name = "editColumn";
            editColumn.UseDefaultText = true;
            editColumn.DefaultText = "Edit";
            editColumn.FieldName = "edit";
            editColumn.HeaderText = "";
            this.gridShippingAddress.MasterTemplate.Columns.Add(editColumn);

            GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
            deleteColumn.Name = "deleteColumn";
            deleteColumn.UseDefaultText = true;
            deleteColumn.DefaultText = "Delete";
            deleteColumn.FieldName = "delete";
            deleteColumn.HeaderText = "";
            this.gridShippingAddress.MasterTemplate.Columns.Add(deleteColumn); 
        }

        private void LoadGridShippingAddresses()
        {
            context = new IndicoPackingEntities();

            this.gridShippingAddress.DataSource = context.DistributorClientAddresses.ToList();

            this.gridShippingAddress.Columns["ID"].IsVisible = false;
            this.gridShippingAddress.Columns["EmailAddress"].IsVisible = false;
            this.gridShippingAddress.Columns["AddressType"].IsVisible = false;
            this.gridShippingAddress.Columns["IsAdelaideWarehouse"].IsVisible = false;
            this.gridShippingAddress.Columns["IndicoDistributorClientAddressId"].IsVisible = false;
            this.gridShippingAddress.Columns["Port"].IsVisible = false;
            this.gridShippingAddress.Columns["Country1"].IsVisible = false;
            this.gridShippingAddress.Columns["Port1"].IsVisible = false;
            this.gridShippingAddress.Columns["Invoices"].IsVisible = false;
            this.gridShippingAddress.Columns["Invoices1"].IsVisible = false;
        }

        #endregion       
        
    }
}
