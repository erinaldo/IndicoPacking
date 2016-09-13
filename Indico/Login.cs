using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Common;
using IndicoPacking.Model;

namespace IndicoPacking
{
    public partial class Login : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Constructors

        public Login()
        {
            InitializeComponent();
            context = new IndicoPackingEntities();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin")) + @"images\logo_login.png");
            this.picLogo.SizeMode = PictureBoxSizeMode.CenterImage;
            this.picLogo.BackgroundImage = b;
        }

        #endregion      

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtUsername.Text) && string.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                MessageBox.Show("Username and password cannot be empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check the credentials
            // First get the username record            
            User user = context.Users.Where(u => u.Username.ToLower() == this.txtUsername.Text.ToLower().Trim()).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("Username or password invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string password = Security.EncryptPassword(this.txtPassword.Text.Trim(), user.PasswordSalt);

            foreach(User u in context.Users)
            {
                if (u.Username.ToLower() == this.txtUsername.Text.ToLower().Trim() && u.Password == password)
                {
                    context = new IndicoPackingEntities();
                    User loggedUser = context.Users.Where(o => o.ID == u.ID).FirstOrDefault();
                    loggedUser.DateLastLogin = DateTime.Now;
                    context.SaveChanges();

                    this.Hide();

                    // Add LoggedUser Information to LoginIfo
                    LoginInfo.UserID = loggedUser.ID;
                    LoginInfo.Role = loggedUser.Role;
                    
                    return;
                }
            }

            MessageBox.Show("Username or password invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}
