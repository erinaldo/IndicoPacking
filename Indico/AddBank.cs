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
    public partial class AddBank : Form
    {
        #region Properties

        public int BankId { get; set; }

        #endregion

        #region Constructors

        public AddBank()
        {
            InitializeComponent();
        }

        private void AddBank_Load(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            var lst = (from b in context.Countries
                       select new CountryView { ID = b.ID, ShortName = b.ShortName }).ToList();

            lst.Insert(0, new CountryView { ID = 0, ShortName = "Please Select..." });
            this.cmbCountry.DataSource = lst;
            this.cmbCountry.DisplayMember = "ShortName";
            this.cmbCountry.ValueMember = "ID";
            this.cmbCountry.SelectedIndex = 0;

            Bank bank = context.Banks.Where(u => u.ID == BankId).FirstOrDefault();

            if (bank != null)
            {
                this.txtBankName.Text = bank.Name.ToString();
                this.txtAccountNumber.Text = bank.AccountNo.ToString();
                this.txtBranch.Text = bank.Branch.ToString();
                this.txtNumber.Text = bank.Number.ToString();
                this.txtAddress.Text = bank.Address.ToString();
                this.txtCity.Text = bank.City.ToString();
                this.txtState.Text = bank.State.ToString();
                this.txtPostCode.Text = bank.Postcode.ToString();
                this.txtSwiftCode.Text = bank.SwiftCode.ToString();
                this.cmbCountry.SelectedValue = int.Parse(bank.Country.ToString());
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

                Bank bank = null;

                if (BankId == 0)
                {
                    bank = new Bank();
                    bank.Name = this.txtBankName.Text;
                    bank.AccountNo = this.txtAccountNumber.Text;
                    bank.Branch = this.txtBranch.Text;
                    bank.Number = this.txtNumber.Text;
                    bank.Address = this.txtAddress.Text;
                    bank.City = this.txtCity.Text;
                    bank.State = this.txtState.Text;
                    bank.Postcode = this.txtPostCode.Text;
                    bank.SwiftCode = this.txtSwiftCode.Text;
                    bank.Country = ((IndicoPacking.ViewModels.CountryView)(this.cmbCountry.SelectedItem)).ID;

                    context.Banks.Add(bank);
                }
                else
                {
                    bank = context.Banks.Where(u => u.ID == BankId).FirstOrDefault();

                    bank.Name = this.txtBankName.Text;
                    bank.AccountNo = this.txtAccountNumber.Text;
                    bank.Branch = this.txtBranch.Text;
                    bank.Number = this.txtNumber.Text;
                    bank.Address = this.txtAddress.Text;
                    bank.City = this.txtCity.Text;
                    bank.State = this.txtState.Text;
                    bank.Postcode = this.txtPostCode.Text;
                    bank.SwiftCode = this.txtSwiftCode.Text;
                    bank.Country = ((IndicoPacking.ViewModels.CountryView)(this.cmbCountry.SelectedItem)).ID;
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
            bool isNameValid = true, isAccountNumberValid = true, isBranchValid = true, isCountryValid = true;

            rfvBankName.Clear();
            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                rfvBankName.SetError(txtBankName, "Name is required");
                isNameValid = false;
            }

            rfvAccountNumber.Clear();
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                rfvAccountNumber.SetError(txtAccountNumber, "Account Number is required");
                isAccountNumberValid = false;
            }

            rfvBranch.Clear();
            if (string.IsNullOrWhiteSpace(txtBranch.Text))
            {
                rfvBranch.SetError(txtBranch, "Branch is required");
                isBranchValid = false;
            }

            rfvCountry.Clear();
            if (this.cmbCountry.SelectedIndex == 0)
            {
                rfvCountry.SetError(cmbCountry, "Country is required");
                isCountryValid = false;
            }

            if (isNameValid && isAccountNumberValid && isBranchValid && isCountryValid)
            {
                isAllFormFieldEntriesValid = true;
            }

            return isAllFormFieldEntriesValid;
        }

        #endregion
    }
}
