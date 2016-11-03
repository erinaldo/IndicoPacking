using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dapper;
using iTextSharp.text.pdf;
using IndicoPacking.Common;
using Telerik.WinControls.UI;
using IndicoPacking.CustomModels;
using IndicoPacking;
using IndicoPacking.DAL.Base.Implementation;
using IndicoPacking.DAL.Objects.Implementation;
using System.Threading.Tasks;
using IndicoPacking.Tools;
using DAL = IndicoPacking.DAL.Objects.Implementation;
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

            this.LoadGridShippingAddresses();
            this.AddCustomColumn();

            this.gridShippingAddress.CommandCellClick += gridShippingAddress_CommandCellClick;
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
                    using (var unit = new UnitOfWork())
                    {
                        unit.DistributorClientAddressRepository.Delete(distributoeClientAddressId);
                        unit.Complete();
                    }
                }
                catch (Exception){/*ignored*/}
             
                //context.DistributorClientAddresses.Remove((from u in context.DistributorClientAddresses
                //                                           where u.ID == distributoeClientAddressId
                //                                           select u).FirstOrDefault());

                //context.SaveChanges();
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

        private void SynchronizeAddresses(Control button,RadGridView grid)
        {
            try
            {
                button.Invoke(new Action(() => button.Enabled = false));
                button.Invoke(new Action(() => button.Text = "Getting Data.."));


                using (var indicoConnection = IndicoConnection)
                {
                    var distributorClientAddressesFromIndico = indicoConnection.Query<DistributorClientAddressFromIndico>(@"
                                                                                        SELECT dca.ID AS IndicoDistributorClientAddressId
		                                                                                    ,dca.[Address]
		                                                                                    ,dca.[Suburb]
		                                                                                    ,dca.[PostCode]
		                                                                                    ,dca.[Country]
		                                                                                    ,dca.[ContactName]
		                                                                                    ,dca.[ContactPhone]
		                                                                                    ,dca.[CompanyName]
		                                                                                    ,dca.[State]
		                                                                                    ,dca.[Port]
		                                                                                    ,dca.[EmailAddress]
		                                                                                    ,dca.[AddressType]
		                                                                                    ,dca.[IsAdelaideWarehouse]
		                                                                                    ,d.Name AS DistributorName
	                                                                                    FROM [dbo].[DistributorClientAddress] dca
		                                                                                    INNER JOIN [dbo].[Company] d
			                                                                                    ON dca.Distributor = d.ID
	                                                                                    WHERE dca.[Address] != 'tba'").ToList();
                    if (distributorClientAddressesFromIndico.Count <= 0)
                        return;
                    button.Invoke(new Action(() => button.Text = "Synchronizing.."));
                    using (var unit = new UnitOfWork())
                    {
                        var localAddresses = unit.DistributorClientAddressRepository.Get().ToList();
                        foreach (var indicoAddress in distributorClientAddressesFromIndico)
                        {
                            var id = indicoAddress.IndicoDistributorClientAddressId;
                            var addresses = localAddresses.Where(localAddress => localAddress.IndicoDistributorClientAddressId == id).ToList();
                            if (addresses.Count < 1)
                            {
                                int? newPort = null;
                                var portForAddress = unit.PortRepository.Where(new { IndicoPortId = indicoAddress.Port }).FirstOrDefault();
                                if (portForAddress == null)
                                {
                                    var portfromIndico = indicoConnection.Query("SELECT * FROM [dbo].[DestinationPort] WHERE ID = " + indicoAddress.Port).Select(s => new { s.ID, s.Name, s.Description }).FirstOrDefault();
                                    if (portfromIndico != null)
                                    {
                                        newPort = unit.PortRepository.Add(new PortBo() { Name = portfromIndico.Name, Description = portfromIndico.Description, IndicoPortId = portfromIndico.ID });
                                    }
                                }
                                var newAddress = new DistributorClientAddressBo
                                {
                                    Address = indicoAddress.Address,
                                    AddressType = indicoAddress.AddressType,
                                    CompanyName = indicoAddress.CompanyName,
                                    ContactName = indicoAddress.ContactName,
                                    ContactPhone = indicoAddress.ContactPhone,
                                    Country = indicoAddress.Country,
                                    EmailAddress = indicoAddress.EmailAddress,
                                    IndicoDistributorClientAddressId = indicoAddress.IndicoDistributorClientAddressId,
                                    IsAdelaideWarehouse = indicoAddress.IsAdelaideWarehouse,
                                    Port = newPort ?? portForAddress.ID,
                                    PostCode = indicoAddress.PostCode,
                                    State = indicoAddress.State,
                                    DistributorName = indicoAddress.DistributorName,
                                    Suburb = indicoAddress.Suburb
                                };
                                unit.DistributorClientAddressRepository.Add(newAddress);
                                continue;
                            }
                            foreach (var localAddress in localAddresses.Where(localAddress => localAddress.IndicoDistributorClientAddressId == id))
                            {
                                if (indicoAddress.Address != localAddress.Address)
                                    localAddress.Address = indicoAddress.Address;

                                if (indicoAddress.IsAdelaideWarehouse != localAddress.IsAdelaideWarehouse)
                                    localAddress.IsAdelaideWarehouse = indicoAddress.IsAdelaideWarehouse;

                                if (indicoAddress.AddressType != localAddress.AddressType)
                                    localAddress.AddressType = indicoAddress.AddressType;

                                if (indicoAddress.CompanyName != localAddress.CompanyName)
                                    localAddress.CompanyName = indicoAddress.CompanyName;

                                if (indicoAddress.ContactName != localAddress.ContactName)
                                    localAddress.ContactName = indicoAddress.ContactName;

                                if (indicoAddress.ContactPhone != localAddress.ContactPhone)
                                    localAddress.ContactPhone = indicoAddress.ContactPhone;

                                if (indicoAddress.Country != localAddress.Country)
                                    localAddress.Country = indicoAddress.Country;

                                if (indicoAddress.DistributorName != localAddress.DistributorName)
                                    localAddress.DistributorName = indicoAddress.DistributorName;

                                if (indicoAddress.EmailAddress != localAddress.EmailAddress)
                                    localAddress.EmailAddress = indicoAddress.EmailAddress;


                                var port =localAddress.ObjPort;
                                if (port == null && indicoAddress.Port.GetValueOrDefault() > 0)
                                {
                                    var portforIndicoId = unit.PortRepository.Where(new { IndicoPortId = indicoAddress.Port }).ToList();
                                    if (portforIndicoId.Count < 1)
                                    {
                                        var portfromIndico = indicoConnection.Query("SELECT * FROM [dbo].[DestinationPort] WHERE ID = " + indicoAddress.Port).Select(s => new { s.ID, s.Name, s.Description }).FirstOrDefault();
                                        if (portfromIndico != null)
                                        {
                                            var newId = unit.PortRepository.Add(new PortBo() { Name = portfromIndico.Name, Description = portfromIndico.Description, IndicoPortId = portfromIndico.ID });
                                            localAddress.Port = newId;
                                        }
                                    }
                                }
                                else if (port != null)
                                {
                                    if (port.IndicoPortId != indicoAddress.Port)
                                    {
                                        var portfromIndico = indicoConnection.Query("SELECT * FROM [dbo].[DestinationPort] WHERE ID = " + indicoAddress.Port).Select(s => new { s.ID, s.Name, s.Description }).FirstOrDefault();
                                        if (portfromIndico != null)
                                        {
                                            var newId = unit.PortRepository.Add(new PortBo() { Name = portfromIndico.Name, Description = portfromIndico.Description, IndicoPortId = portfromIndico.ID });
                                            localAddress.Port = newId.GetValueOrDefault();
                                        }
                                    }
                                }

                                if (indicoAddress.PostCode != localAddress.PostCode)
                                    localAddress.PostCode = indicoAddress.PostCode;
                                if (indicoAddress.State != localAddress.State)
                                    localAddress.State = indicoAddress.State;
                                if (indicoAddress.Suburb != localAddress.Suburb)
                                    localAddress.Suburb = indicoAddress.Suburb;
                            }
                        }

                        unit.Complete();

                        grid.Invoke(new Action(() =>
                        {
                            using (var unito = new UnitOfWork())
                            {
                                grid.DataSource = unito.DistributorClientAddressRepository.Get().Select(s => new { s.ID, s.Address, s.Suburb, s.PostCode, s.Country, s.ContactName, s.ContactPhone, s.CompanyName, s.State });
                            }
                        }));
                    }
                }

                button.Invoke(new Action(() => button.Enabled = true));
                button.Invoke(new Action(() => button.Text = "Synchronize"));
            }

            catch (Exception e)
            {
                IndicoPackingLog.GetObject().Log(e,"Unable to synchronize Addresses");
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
