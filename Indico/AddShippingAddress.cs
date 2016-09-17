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

namespace IndicoPacking
{
    public partial class AddShippingAddress : Form
    {
        #region Properties

        public int DistributorClientAddressId { get; set; }

        #endregion

        #region Constructors

        public AddShippingAddress()
        {
            InitializeComponent();

            IndicoPackingEntities context = new IndicoPackingEntities();

            var lst = (from b in context.Countries
                       select new CountryView { ID = b.ID, ShortName = b.ShortName }).ToList();

            lst.Insert(0, new CountryView { ID = 0, ShortName = "Please Select..." });
            this.cmbCountry.DataSource = lst;
            this.cmbCountry.DisplayMember = "ShortName";
            this.cmbCountry.ValueMember = "ID";
            this.cmbCountry.SelectedIndex = 0;

            var lstPort = (from b in context.Ports
                           select new PortView { ID = b.ID, Name = b.Name }).ToList();

            lstPort.Insert(0, new PortView { ID = 0, Name = "Please Select..." });
            this.cmbPort.DataSource = lstPort;
            this.cmbPort.DisplayMember = "Name";
            this.cmbPort.ValueMember = "ID";
            this.cmbPort.SelectedIndex = 0;
        }

        private void AddShippingAddress_Load(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            DistributorClientAddress address = context.DistributorClientAddresses.Where(u => u.ID == DistributorClientAddressId).FirstOrDefault();

            if (address != null)
            {
                this.txtAddress.Text = address.Address;
                this.txtSuburb.Text = address.Suburb;
                this.txtPostCode.Text = address.PostCode;
                this.txtContactName.Text = address.ContactName;
                this.txtContactPhone.Text = address.ContactPhone;
                this.txtCompanyName.Text = address.CompanyName;
                this.txtState.Text = address.State;
                this.txtEmail.Text = address.EmailAddress;

                if (address.AddressType == 1)
                {
                    rbBusiness.Checked = true;                    
                }
                else if (address.AddressType == 0)
                {
                    rbResidential.Checked = true;
                }

                if (address.IsAdelaideWarehouse)
                {
                    cbxIsAdelaideWarehouse.Checked = true;
                }
                else
                    cbxIsAdelaideWarehouse.Checked = false;

                this.cmbPort.SelectedIndex = int.Parse(address.Port.ToString());
                this.cmbCountry.SelectedIndex = int.Parse(address.Country.ToString());
            }            
        }

        #endregion

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                IndicoPackingEntities context = new IndicoPackingEntities();
                DistributorClientAddress address = null;

                if (DistributorClientAddressId == 0)
                {
                    address = new DistributorClientAddress();
                    address.Address = this.txtAddress.Text;
                    address.Suburb = this.txtSuburb.Text;
                    address.PostCode = this.txtPostCode.Text;
                    address.ContactName = this.txtContactName.Text;
                    address.ContactPhone = this.txtContactPhone.Text;
                    address.CompanyName = this.txtCompanyName.Text;
                    address.State = this.txtState.Text;
                    address.EmailAddress = this.txtEmail.Text;

                    if (rbBusiness.Checked)
                    {
                        address.AddressType = 1;
                    }
                    else if (rbResidential.Checked)
                    {
                        address.AddressType = 0;
                    }

                    if (cbxIsAdelaideWarehouse.Checked)
                    {
                        address.IsAdelaideWarehouse = true;
                    }
                    else
                        address.IsAdelaideWarehouse = false;

                    address.Port = ((IndicoPacking.ViewModels.PortView)(this.cmbPort.SelectedItem)).ID;
                    address.Country = ((IndicoPacking.ViewModels.CountryView)(this.cmbCountry.SelectedItem)).ID;

                    context.DistributorClientAddresses.Add(address);
                }
                else
                {
                    address = context.DistributorClientAddresses.Where(u => u.ID == DistributorClientAddressId).FirstOrDefault();

                    address.Address = this.txtAddress.Text;
                    address.Suburb = this.txtSuburb.Text;
                    address.PostCode = this.txtPostCode.Text;
                    address.ContactName = this.txtContactName.Text;
                    address.ContactPhone = this.txtContactPhone.Text;
                    address.CompanyName = this.txtCompanyName.Text;
                    address.State = this.txtState.Text;
                    address.EmailAddress = this.txtEmail.Text;

                    if (rbBusiness.Checked)
                    {
                        address.AddressType = 1;
                    }
                    else if (rbResidential.Checked)
                    {
                        address.AddressType = 0;
                    }

                    if (cbxIsAdelaideWarehouse.Checked)
                    {
                        address.IsAdelaideWarehouse = true;
                    }
                    else
                        address.IsAdelaideWarehouse = false;

                    address.Port = ((IndicoPacking.ViewModels.PortView)(this.cmbPort.SelectedItem)).ID;
                    address.Country = ((IndicoPacking.ViewModels.CountryView)(this.cmbCountry.SelectedItem)).ID;
                }

                context.SaveChanges();

                this.Close();
            }
        }

        #endregion   
     
        #region Methods

        private bool ValidateForm()
        {
            bool isAllFormFieldEntriesValid = false;
            bool isAddressValid = true, isCompanyNameValid = true, isContactNameValid = true, isContactPhoneValid = true, isPostCodeValid = true, isSuburbValid = true, isCountryValid = true, isPortValid =true;

            rfvAddress.Clear();
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                rfvAddress.SetError(txtAddress, "Address is required");
                isAddressValid = false;
            }

            rfvCompanyname.Clear();
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                rfvCompanyname.SetError(txtCompanyName, "Company Name is required");
                isCompanyNameValid = false;
            }

            rfvContactName.Clear();
            if (string.IsNullOrWhiteSpace(txtContactName.Text))
            {
                rfvContactName.SetError(txtContactName, "Contact Name is required");
                isContactNameValid = false;
            }

            rfvContactPhone.Clear();
            if (string.IsNullOrWhiteSpace(txtContactPhone.Text))
            {
                rfvContactPhone.SetError(txtContactPhone, "Contact Phone is required");
                isContactPhoneValid = false;
            }

            rfvPostCode.Clear();
            if (string.IsNullOrWhiteSpace(txtPostCode.Text))
            {
                rfvPostCode.SetError(txtPostCode, "Post Code is required");
                isPostCodeValid = false;
            }

            rfvSuberb.Clear();
            if (string.IsNullOrWhiteSpace(txtSuburb.Text))
            {
                rfvSuberb.SetError(txtSuburb, "Suberb is required");
                isSuburbValid = false;
            }

            rfvCountry.Clear();
            if (cmbCountry.SelectedIndex == 0)
            {
                rfvCountry.SetError(cmbCountry, "Country is required");
                isCountryValid = false;
            }

            rfvPort.Clear();
            if (string.IsNullOrWhiteSpace(txtSuburb.Text))
            {
                rfvPort.SetError(cmbPort, "Port is required");
                isPortValid = false;
            }

            if (isAddressValid && isCompanyNameValid && isContactNameValid && isContactPhoneValid && isPostCodeValid && isSuburbValid && isCountryValid && isPortValid)
            {
                isAllFormFieldEntriesValid = true;
            }

            return isAllFormFieldEntriesValid;
        }

        #endregion            
    }
}
