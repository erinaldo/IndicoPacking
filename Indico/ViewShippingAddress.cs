using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IndicoPacking.Common;
using Telerik.WinControls.UI;
using System.Threading.Tasks;
using IndicoPacking.Tools;
namespace IndicoPacking
{
    public partial class ViewShippingAddress : IndicoPackingForm
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

            LoadGridShippingAddresses();
            AddCustomColumn();

            gridShippingAddress.CommandCellClick += gridShippingAddress_CommandCellClick;
        }

        void gridShippingAddress_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            var cell = (GridCommandCellElement)sender;
            var clickedRow = gridShippingAddress.Rows[cell.RowIndex];

            var distributoeClientAddressId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (gridShippingAddress.Columns["editColumn"] != null && e.ColumnIndex == gridShippingAddress.Columns["editColumn"].Index && e.RowIndex >= 0)
            {
                var frmAddShippngAddress = new AddShippingAddress();
                frmAddShippngAddress.StartPosition = FormStartPosition.CenterScreen;
                frmAddShippngAddress.Text = "Edit Shipping Address";
                frmAddShippngAddress.DistributorClientAddressId = distributoeClientAddressId;
                frmAddShippngAddress.ShowDialog();
            }
            else if (gridShippingAddress.Columns["deleteColumn"] != null && e.ColumnIndex == gridShippingAddress.Columns["deleteColumn"].Index && e.RowIndex >= 0)
            {
                try
                {
                    var context = new IndicoPackingEntities();
                    var address = context.DistributorClientAddresses.Find(distributoeClientAddressId);
                    if (address != null)
                    {
                        context.DistributorClientAddresses.Remove(address);
                    }
                }
                catch (Exception){/*ignored*/}
             
                //context.DistributorClientAddresses.Remove((from u in context.DistributorClientAddresses
                //                                           where u.ID == distributoeClientAddressId
                //                                           select u).FirstOrDefault());

                //context.SaveChanges();
            }

            gridShippingAddress.DataSource = null;
            gridShippingAddress.Columns.Clear();
            gridShippingAddress.Rows.Clear();

            LoadGridShippingAddresses();
            AddCustomColumn();
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
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
            gridShippingAddress.MasterTemplate.Columns.Add(editColumn);

            GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
            deleteColumn.Name = "deleteColumn";
            deleteColumn.UseDefaultText = true;
            deleteColumn.DefaultText = "Delete";
            deleteColumn.FieldName = "delete";
            deleteColumn.HeaderText = "";
            gridShippingAddress.MasterTemplate.Columns.Add(deleteColumn); 
        }

        private void LoadGridShippingAddresses()
        {
            context = new IndicoPackingEntities();

            gridShippingAddress.DataSource = context.DistributorClientAddresses.ToList();

            gridShippingAddress.Columns["ID"].IsVisible = false;
            gridShippingAddress.Columns["EmailAddress"].IsVisible = false;
            gridShippingAddress.Columns["AddressType"].IsVisible = false;
            gridShippingAddress.Columns["IsAdelaideWarehouse"].IsVisible = false;
            gridShippingAddress.Columns["IndicoDistributorClientAddressId"].IsVisible = false;
            gridShippingAddress.Columns["Port"].IsVisible = false;
            gridShippingAddress.Columns["Country1"].IsVisible = false;
            gridShippingAddress.Columns["Port1"].IsVisible = false;
            gridShippingAddress.Columns["Invoices"].IsVisible = false;
            gridShippingAddress.Columns["Invoices1"].IsVisible = false;
        }

        private void SynchronizeAddresses(Control button,RadGridView grid)
        {
            try
            {
                button.Invoke(new Action(() => button.Enabled = false));
                button.Invoke(new Action(() => button.Text = "Synchronizing .."));

                Synchronize.SynchronizeDistributorClientAddresses();
                
                grid.Invoke(new Action(() =>
                {
                    var context = new IndicoPackingEntities();
                    grid.DataSource = context.DistributorClientAddresses.Select(s => new { s.ID, s.Address, s.Suburb, s.PostCode, s.Country, s.ContactName, s.ContactPhone, s.CompanyName, s.State });
                }));

                button.Invoke(new Action(() => button.Enabled = true));
                button.Invoke(new Action(() => button.Text = "Synchronize"));
            }

            catch (Exception e)
            {
                IndicoPackingLog.GetObject().Log(e, "Unable to synchronize Addresses");
                MessageBox.Show("Unable to synchronize addresses", "Unable to synchronize", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                try
                {
                    button.Invoke(new Action(() => button.Enabled = true));
                    button.Invoke(new Action(() => button.Text = "Synchronize"));
                }
                catch (Exception) {/*ignored*/ }
            }
        }

        private string CreateUpdateQuery(string id,Dictionary<string,object> set )
        {
            var setPart = new StringBuilder("SET");
            foreach (var s in set)
            {
                setPart.Append(string.Format(" {0} = '{1}'", s.Key, s.Value));
            }
            return "UPDATE [dbo].[DistributorClientAddress] dbo d";
        }

        #endregion

        private void OnSynchronizeButtonClick(object sender, EventArgs e)
        {
            var requestResult = MessageBox.Show("Do you want to synchronize Addresses with OPS", "Confirm to continue", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (requestResult != DialogResult.Yes)
                return;

            var button = (Button)sender;
            Task.Factory.StartNew(() => { SynchronizeAddresses(button, gridShippingAddress); });

        }
    }
}
