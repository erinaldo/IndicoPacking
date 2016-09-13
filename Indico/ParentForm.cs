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
using System.Configuration;

namespace IndicoPacking
{
    enum LoggedUser
    {
        APP_ADMIN = 1,
        INDIMAN_ADMIN = 2,
        JK_ADMIN = 3,
        FILLING_CORDINATOR = 4        
    }

    public partial class ParentForm : Form
    {
        #region Constants

        // Define DoubleClick...
        const int WM_NCLBUTTONDBLCLK = 163;
        // Define LeftButtonDown event...
        const int WM_NCLBUTTONDOWN = 161;
        // Define MOVE action...
        const int WM_SYSCOMMAND = 274;
        // Define that the WM_NCLBUTTONDOWN is at TitleBar...
        const int HTCAPTION = 2;
        // Trap MOVE action...
        const int SC_MOVE = 61456;

        #endregion

        #region Fields

        private int childFormNumber = 0;

        #endregion

        #region Properties

        public ComboBox Shipmentddlcarton { get; set; }

        #endregion

        #region Constructors

        public ParentForm()
        {
            InitializeComponent();
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;            

            // Disable functionalities to filling cordinator.
            if (LoginInfo.Role == (int)LoggedUser.FILLING_CORDINATOR)
            {
                this.generateLabelsToolStripMenuItem.Visible = false;
                this.clearToolStripMenuItem.Visible = false;
                this.cartonsToolStripMenuItem.Visible = false;
                this.usersToolStripMenuItem.Visible = false;
                this.invoicesToolStripMenuItem.Visible = false;
                this.viewMenu.Visible = false;
                this.toolStrip.Visible = false;
                this.portToolStripMenuItem.Visible = false;
                this.modeToolStripMenuItem.Visible = false;
                this.shipToToolStripMenuItem.Visible = false;
                this.bankToolStripMenuItem.Visible = false;
            }
            else if (LoginInfo.Role == (int)LoggedUser.INDIMAN_ADMIN)
            {
                this.generateLabelsToolStripMenuItem.Visible = false;
                this.clearToolStripMenuItem.Visible = false;
                this.cartonsToolStripMenuItem.Visible = false;
                this.newFactoryInvoiceToolStripMenuItem.Visible = false;
                this.viewFactoryInvoicesToolStripMenuItem.Visible = false;
                this.viewMenu.Visible = false;
                this.toolStrip.Visible = false;
                this.portToolStripMenuItem.Visible = false;
                this.modeToolStripMenuItem.Visible = false;
                this.shipToToolStripMenuItem.Visible = false;
                this.bankToolStripMenuItem.Visible = false;
                this.shipmentsToolStripMenuItem.Visible = false;
            }    
          
            // Disable indiman invoice for other users
            if (LoginInfo.Role == (int)LoggedUser.JK_ADMIN)
            {
                this.newIndimanInvoiceToolStripMenuItem.Visible = false;
                this.viewIndimanInvoiceToolStripMenuItem.Visible = false;
            }

            // Protect the connection string. Enable below code in production server
           // Utility.ProtectConnectionString();
        }
                             
        #endregion

        #region Private Methods

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
        
        #endregion

        #region Protected Methods

        protected override void WndProc(ref Message m)
        {
            // Disable moving TitleBar...
            if (((m.Msg == WM_SYSCOMMAND)
                        && (m.WParam.ToInt32() == SC_MOVE)))
            {
                return;
            }

            // Track whether clicked on TitleBar...
            if (((m.Msg == WM_NCLBUTTONDOWN)
                        && (m.WParam.ToInt32() == HTCAPTION)))
            {
                return;
            }

            // Disable double click on TitleBar...
            if ((m.Msg == WM_NCLBUTTONDBLCLK))
            {
                return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Public Methods

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ParentForm frmParent = new ParentForm();
           // frmParent.MaximizeBox = false;
            frmParent.IsMdiContainer = true;
            //Login frmLogin = new Login();
            //frmLogin.ShowDialog();
            Application.Run(frmParent);
        }

        #endregion

        #region Events

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void newShipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeeklyShipments frmWeeklyShipments = new WeeklyShipments();
            frmWeeklyShipments.TopMost = true;
            frmWeeklyShipments.MdiParent = this;
            frmWeeklyShipments.Show();
        }

        private void addCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddCarton)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddCarton frmAEC = new AddCarton();
            frmAEC.StartPosition = FormStartPosition.CenterScreen;
            frmAEC.MdiParent = this;
            frmAEC.ParentMDIForm = this;
            frmAEC.Show();
        }

        private void viewCartonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.CartonSizeDetails)
                {
                    frm.BringToFront();
                    return;
                }
            }

            CartonSizeDetails frmCSD = new CartonSizeDetails();
            frmCSD.StartPosition = FormStartPosition.CenterScreen;
            frmCSD.MdiParent = this;
            frmCSD.Type = 3;
            frmCSD.Show();
        }

        private void modifyCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.CartonSizeDetails)
                {
                    frm.BringToFront();
                    return;
                }
            }

            CartonSizeDetails frmCSD = new CartonSizeDetails();
            frmCSD.StartPosition = FormStartPosition.CenterScreen;
            frmCSD.MdiParent = this;
            frmCSD.ParentMDIForm = this;
            frmCSD.Type = 1;
            frmCSD.Text = "Modify Carton";
            frmCSD.Show();
        }

        private void deleteCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.CartonSizeDetails)
                {
                    frm.BringToFront();
                    return;
                }
            }

            CartonSizeDetails frmCSD = new CartonSizeDetails();
            frmCSD.StartPosition = FormStartPosition.CenterScreen;
            frmCSD.MdiParent = this;
            frmCSD.ParentMDIForm = this;
            frmCSD.Type = 2;
            frmCSD.Text = "Delete Carton";
            frmCSD.Show();
        }

        private void allCartonLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnGenerateCartonBarcods_Click(null, new EventArgs());
            }
        }

        private void allPolybagLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnGeneratePolybagBarcods_Click(null, new EventArgs());
            }
        }

        private void allBatchLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnGenerateAllBatchLabels_Click(null, new EventArgs());
            }
        }

        private void startResumeFillingCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnFillCarton_Click(null, new EventArgs());
            }
        }

        private void cartonAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnClearCartonArea_Click(null, new EventArgs());
            }
        }

        private void allCartonItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnClearAllCartonItems_Click(null, new EventArgs());
            }
        }

        private void allFilledCartonItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form activeChild = this.ActiveMdiChild;

            // if there is a active chile then we can find the events that we need to fire here
            if (activeChild != null && activeChild is IndicoPacking.WeeklyShipments)
            {
                IndicoPacking.WeeklyShipments child = (IndicoPacking.WeeklyShipments)activeChild;
                child.btnClearFilledCartons_Click(null, new EventArgs());
            }
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddUser)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddUser addUsr = new AddUser();
            addUsr.StartPosition = FormStartPosition.CenterScreen;
            addUsr.MdiParent = this;
            addUsr.Show();
        }

        private void viewUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewUsers)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewUsers viewUsers = new ViewUsers();
            viewUsers.Type = 3;
            viewUsers.StartPosition = FormStartPosition.CenterScreen;
            viewUsers.MdiParent = this;
            viewUsers.Show();
        }

        private void modifyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewUsers)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewUsers viewUsers = new ViewUsers();
            viewUsers.Type = 1;
            viewUsers.Text = "Modify User";
            viewUsers.StartPosition = FormStartPosition.CenterScreen;
            viewUsers.MdiParent = this;
            viewUsers.Show();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewUsers)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewUsers viewUsers = new ViewUsers();
            viewUsers.Type = 2;
            viewUsers.Text = "Delete User";
            viewUsers.StartPosition = FormStartPosition.CenterScreen;
            viewUsers.MdiParent = this;
            viewUsers.Show();
        }

        private void newFactoryInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddInvoice frmNewInvoice = new AddInvoice();
            frmNewInvoice.StartPosition = FormStartPosition.CenterScreen;
            frmNewInvoice.MdiParent = this;
            frmNewInvoice.TypeOfInvoice = 1;
            frmNewInvoice.Text = "New Factory Invoice";
            frmNewInvoice.Show();
        }

        private void viewFactoryInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewInvoice frmViewInvoice = new ViewInvoice();
            frmViewInvoice.StartPosition = FormStartPosition.CenterScreen;
            frmViewInvoice.MdiParent = this;
            frmViewInvoice.TypeOfInvoice = 1;
            frmViewInvoice.Text = "View Factory Invoices";
            frmViewInvoice.Show();
        }

        private void newIndimanInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewInvoice frmViewInvoice = new ViewInvoice();
            frmViewInvoice.StartPosition = FormStartPosition.CenterScreen;
            frmViewInvoice.MdiParent = this;
            frmViewInvoice.TypeOfInvoice = 2;
            frmViewInvoice.Text = "New Indiman Invoice";
            frmViewInvoice.Show();
        }

        private void viewIndimanInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewInvoice frmViewInvoice = new ViewInvoice();
            frmViewInvoice.StartPosition = FormStartPosition.CenterScreen;
            frmViewInvoice.MdiParent = this;
            frmViewInvoice.TypeOfInvoice = 0;
            frmViewInvoice.Text = "View Indiman Invoices";
            frmViewInvoice.Show();
        }

        private void addBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddBank)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddBank frmAddBank = new AddBank();
            frmAddBank.StartPosition = FormStartPosition.CenterScreen;
            frmAddBank.MdiParent = this;
            frmAddBank.Show();
        }

        private void viewBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewBank)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewBank frmViewBank = new ViewBank();
            frmViewBank.StartPosition = FormStartPosition.CenterScreen;
            frmViewBank.MdiParent = this;
            frmViewBank.Show();
        }

        private void addPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddPort)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddPort frmAddPort = new AddPort();
            frmAddPort.StartPosition = FormStartPosition.CenterScreen;
            frmAddPort.MdiParent = this;
            frmAddPort.Show();
        }

        private void viewPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewPorts)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewPorts frmviewPorts = new ViewPorts();
            frmviewPorts.StartPosition = FormStartPosition.CenterScreen;
            frmviewPorts.MdiParent = this;
            frmviewPorts.Show();
        }

        private void addShipmentModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddShipmentMode)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddShipmentMode frmAddShipmentMode = new AddShipmentMode();
            frmAddShipmentMode.StartPosition = FormStartPosition.CenterScreen;
            frmAddShipmentMode.MdiParent = this;
            frmAddShipmentMode.Show();
        }

        private void viewShipmentModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewShipmentMode)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewShipmentMode frmViewShipmentModes = new ViewShipmentMode();
            frmViewShipmentModes.StartPosition = FormStartPosition.CenterScreen;
            frmViewShipmentModes.MdiParent = this;
            frmViewShipmentModes.Show();
        }

        private void addShippingAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.AddShippingAddress)
                {
                    frm.BringToFront();
                    return;
                }
            }

            AddShippingAddress frmAddShippingAddress = new AddShippingAddress();
            frmAddShippingAddress.StartPosition = FormStartPosition.CenterScreen;
            frmAddShippingAddress.MdiParent = this;
            frmAddShippingAddress.Show();
        }

        private void viewShippingAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is IndicoPacking.ViewShippingAddress)
                {
                    frm.BringToFront();
                    return;
                }
            }

            ViewShippingAddress frmViewShippingAddress = new ViewShippingAddress();
            frmViewShippingAddress.StartPosition = FormStartPosition.CenterScreen;
            frmViewShippingAddress.MdiParent = this;
            frmViewShippingAddress.Show();
        }

        #endregion                                                                              

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
