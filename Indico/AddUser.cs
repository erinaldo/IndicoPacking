using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking;
using IndicoPacking.Common;
using IndicoPacking.Model;
using System.Text.RegularExpressions;

namespace IndicoPacking
{
    public partial class AddUser : Form
    {
        #region Constants

        int PASSWORD_MINIMUM_CHARACTER_COUNT = 6;

        #endregion

        #region Properties

        public int UserId { get; set; }

        #endregion

        #region Fields

        private bool isChecked = false;
        
        IndicoPackingEntities context = null;

        User userAlredyExist = null;

        #endregion

        #region Constructors

        public AddUser()
        {
            InitializeComponent();
        }
     
        private void frmAddUser_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            if(LoginInfo.Role == UserType.AppAdmin)
                this.cmbRole.DataSource = context.Roles.Where(u => u.ID != 1).ToList();
            else if (LoginInfo.Role == UserType.JkAdmin)
                this.cmbRole.DataSource = context.Roles.Where(u => u.ID == (int)UserType.JkAdmin || u.ID == (int)UserType.FillingCordinator).ToList();
            else
                this.cmbRole.DataSource = context.Roles.Where(u => u.ID == (int)UserType.IndimanAdmin).ToList();

            //this.cmbRole.DataSource = context.Roles.ToList();   
            this.cmbRole.DisplayMember = "Name";
            this.cmbRole.ValueMember = "ID";
            this.cmbRole.SelectedIndex = 0;
                      

            // Load data to user status drop down
            this.cmbStatus.DataSource = context.UserStatus.ToList();
            this.cmbStatus.DisplayMember = "Name";
            this.cmbStatus.ValueMember = "ID";
            this.cmbStatus.SelectedIndex = 0;           

            rdoMale.Checked = true;

            this.txtPassword.TextChanged += txtPassword_TextChanged;
            this.txtConfirmPassword.TextChanged += txtPassword_TextChanged;

            this.txtPhone.TextChanged += txtPhone_TextChanged;
            this.txtMobile.TextChanged += txtPhone_TextChanged;

            this.txtEmail.TextChanged += txtEmail_TextChanged;

            this.cmbStatus.Enabled = false;             

            //Get the user need to be edited
            this.userAlredyExist = context.Users.Where(u => u.ID == UserId).FirstOrDefault();

            if (userAlredyExist != null)
            {
                this.txtName.Text = userAlredyExist.Name;
                this.txtUsername.Text = userAlredyExist.Username;
                this.txtEmail.Text = userAlredyExist.EmailAddress;
                this.txtPhone.Text = userAlredyExist.OfficeTelephoneNumber;
                this.txtMobile.Text = userAlredyExist.MobileTelephoneNumber;
                this.cmbRole.SelectedValue = int.Parse(userAlredyExist.Role.ToString());
                this.cmbStatus.SelectedValue = int.Parse(userAlredyExist.Status.ToString());
                if (userAlredyExist.GenderMale == true)
                {
                    this.rdoMale.Checked = true;
                }
                else
                    this.rdoFemale.Checked = true;
            }
        }
                            
        #endregion

        #region Events

        void txtEmail_TextChanged(object sender, EventArgs e)
        {
            this.lblIsEmailValid.Text = "";
        }

        void txtPhone_TextChanged(object sender, EventArgs e)
        {
            this.lblPhoneError.Text = "";
        }

        void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.lblPasswordLength.Text = "(Password must have at least 6 characters.)";
            lblPasswordLength.ForeColor = System.Drawing.Color.Black;
            this.lblMatchPassword.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ValidateFormEntries())
            {                
                // Now add user to the database
                User user = new User();                
                user.Name = this.txtName.Text.Trim();
                user.Username = this.txtUsername.Text.Trim();
                user.PasswordSalt = Convert.ToBase64String(DateTime.Now.ToLocalTime().Ticks.ToString().GetBytes());
                user.Password = Security.EncryptPassword(this.txtPassword.Text.Trim(), user.PasswordSalt);
                user.EmailAddress = this.txtEmail.Text.Trim();
                user.OfficeTelephoneNumber = this.txtPhone.Text.Trim();
                user.MobileTelephoneNumber = this.txtMobile.Text.Trim();
                user.Role = ((Role)(this.cmbRole.SelectedItem)).ID;
                user.Status = ((UserStatu)(this.cmbStatus.SelectedItem)).ID;
                user.CreatedDate = DateTime.Now;

                isChecked = rdoMale.Checked;
                if (isChecked)
                    user.GenderMale = true;
                else
                    user.GenderMale = false;

                if (userAlredyExist == null)
                {
                    this.context.Users.Add(user);
                    this.context.SaveChanges();
                }

                else
                {
                    userAlredyExist.Name = this.txtName.Text.Trim();
                    userAlredyExist.Username = this.txtUsername.Text.Trim();
                    userAlredyExist.PasswordSalt = Convert.ToBase64String(DateTime.Now.ToLocalTime().Ticks.ToString().GetBytes());
                    userAlredyExist.Password = Security.EncryptPassword(this.txtPassword.Text.Trim(), user.PasswordSalt);
                    userAlredyExist.EmailAddress = this.txtEmail.Text.Trim();
                    userAlredyExist.OfficeTelephoneNumber = this.txtPhone.Text.Trim();
                    userAlredyExist.MobileTelephoneNumber = this.txtMobile.Text.Trim();
                    userAlredyExist.Role = ((Role)(this.cmbRole.SelectedItem)).ID;
                    userAlredyExist.Status = ((UserStatu)(this.cmbStatus.SelectedItem)).ID;
                    userAlredyExist.CreatedDate = DateTime.Now;

                    isChecked = rdoMale.Checked;
                    if (isChecked)
                        userAlredyExist.GenderMale = true;
                    else
                        userAlredyExist.GenderMale = false; 

                    this.context.SaveChanges();
                }

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private bool ValidateFormEntries()
        {
            bool isAllFormFieldEntriesValid = false;
            bool isNameValid = true, isUsernameValid = true, isPasswordValid = true, isConfimPasswordValid = true, isPhoneValid = true, isEmailValid = true, isPasswordLengthValid = true, isPasswordMatched = true;
           
            rfvName.Clear();
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                rfvName.SetError(txtName, "Name is required");
                isNameValid = false;
            }

            rfvUserName.Clear();
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                rfvUserName.SetError(txtUsername, "Username is required");
                isUsernameValid = false;
            }

            rfvPassword.Clear();
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                rfvPassword.SetError(txtPassword, "Password is required");
                isPasswordValid = false;
            }

            rfvConfirmPassword.Clear();
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                rfvConfirmPassword.SetError(txtConfirmPassword, "ConfirmPassword is required");
                isConfimPasswordValid = false;
            }

            if (txtPassword.TextLength < PASSWORD_MINIMUM_CHARACTER_COUNT && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblPasswordLength.Text = "Password too short, at least 6 characters required";
                lblPasswordLength.ForeColor = System.Drawing.Color.Red;
                isPasswordLengthValid = false;
            }

            if (!string.IsNullOrWhiteSpace(txtConfirmPassword.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text) && this.txtPassword.Text != this.txtConfirmPassword.Text)
            {
                lblMatchPassword.Text = "Passwords did not match";
                lblMatchPassword.ForeColor = System.Drawing.Color.Red;
                isPasswordMatched = false;
            }

            rfvEmail.Clear();
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                rfvEmail.SetError(txtEmail, "Email is required");
                isEmailValid = false;
            }

            bool isEmail = Regex.IsMatch(this.txtEmail.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail && !string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                this.lblIsEmailValid.Text = "Enter a valid email address";
                this.lblIsEmailValid.ForeColor = System.Drawing.Color.Red;
                isEmailValid = false;
            }

            rfvPhone.Clear();
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                rfvPhone.SetError(txtPhone, "Phone number is required");
                isPhoneValid = false;
            }

            if (isNameValid && isUsernameValid && isPasswordValid && isConfimPasswordValid && isPhoneValid && isEmailValid && isPasswordLengthValid && isPasswordMatched)
            {
                isAllFormFieldEntriesValid = true;
            }

            return isAllFormFieldEntriesValid;
        }

        #endregion
    }
}
