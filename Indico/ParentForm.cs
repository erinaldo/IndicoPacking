using System;
using System.Linq;
using System.Windows.Forms;
using IndicoPacking.Common;

namespace IndicoPacking
{
    public partial class ParentForm : Form
    {
        #region Constructors

        public ParentForm()
        {
            InitializeComponent();
        }

        private void ParentForm_Load(object sender, EventArgs e)
        {
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Location = Screen.PrimaryScreen.WorkingArea.Location;            

            switch (LoginInfo.Role)
            {
                case UserType.FillingCordinator:
                    generateLabelsToolStripMenuItem.Visible = false;
                    clearToolStripMenuItem.Visible = false;
                    cartonsToolStripMenuItem.Visible = false;
                    usersToolStripMenuItem.Visible = false;
                    invoicesToolStripMenuItem.Visible = false;
                    viewMenu.Visible = false;
                    portToolStripMenuItem.Visible = false;
                    modeToolStripMenuItem.Visible = false;
                    shipToToolStripMenuItem.Visible = false;
                    bankToolStripMenuItem.Visible = false;
                    break;
                case UserType.JkAdmin:
                    newIndimanInvoiceToolStripMenuItem.Visible = false;
                    viewIndimanInvoiceToolStripMenuItem.Visible = false;
                    break;
            }

        }
                             
        #endregion

        #region Public Methods

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var frmParent = new ParentForm ();
            var login = new Login();
            var result = login.ShowDialog();
            if (result != DialogResult.OK)
                Application.Exit();
            Application.Run(frmParent);
            
        }

        #endregion

        #region Events

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {          
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
            foreach (var childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void newShipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmWeeklyShipments = new WeeklyShipments {TopMost = true, MdiParent = this};
            frmWeeklyShipments.Show();
        }

        private void addCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddCarton>())
            {
                frm.BringToFront();
                return;
            }

            var frmAec = new AddCarton {StartPosition = FormStartPosition.CenterScreen, MdiParent = this, ParentMDIForm = this};
            frmAec.Show();
        }

        private void viewCartonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<CartonSizeDetails>())
            {
                frm.BringToFront();
                return;
            }

            var frmCsd = new CartonSizeDetails {StartPosition = FormStartPosition.CenterScreen, MdiParent = this, Type = 3};
            frmCsd.Show();
        }

        private void modifyCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<CartonSizeDetails>())
            {
                frm.BringToFront();
                return;
            }

            var frmCsd = new CartonSizeDetails {StartPosition = FormStartPosition.CenterScreen, MdiParent = this, ParentMDIForm = this, Type = 1, Text = "Modify Carton"};
            frmCsd.Show();
        }

        private void deleteCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<CartonSizeDetails>())
            {
                frm.BringToFront();
                return;
            }

            var frmCsd = new CartonSizeDetails {StartPosition = FormStartPosition.CenterScreen, MdiParent = this, ParentMDIForm = this, Type = 2, Text = "Delete Carton"};
            frmCsd.Show();
        }

        private void allCartonLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            var shipments = activeChild as WeeklyShipments;
            if (shipments == null)
                return;
            var child = shipments;
            child.btnGenerateCartonBarcods_Click(null, new EventArgs());
        }

        private void allPolybagLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            var shipments = activeChild as WeeklyShipments;
            if (shipments == null) return;
            var child = shipments;
            child.btnGeneratePolybagBarcods_Click(null, new EventArgs());
        }

        private void allBatchLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            if (!(activeChild is WeeklyShipments)) return;
            var child = (WeeklyShipments)activeChild;
            child.btnGenerateAllBatchLabels_Click(null, new EventArgs());
        }

        private void startResumeFillingCartonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            var shipments = activeChild as WeeklyShipments;
            if (shipments == null) return;
            var child = shipments;
            child.btnFillCarton_Click(null, new EventArgs());
        }

        private void cartonAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            if (!(activeChild is WeeklyShipments))
                return;
            var child = (WeeklyShipments)activeChild;
            child.btnClearCartonArea_Click(null, new EventArgs());
        }

        private void allCartonItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            var shipments = activeChild as WeeklyShipments;
            if (shipments == null) return;
            var child = shipments;
            child.btnClearAllCartonItems_Click(null, new EventArgs());
        }

        private void allFilledCartonItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var activeChild = ActiveMdiChild;

            var shipments = activeChild as WeeklyShipments;
            if (shipments == null) return;
            var child = shipments;
            child.btnClearFilledCartons_Click(null, new EventArgs());
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddUser>())
            {
                frm.BringToFront();
                return;
            }

            var addUsr = new AddUser {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            addUsr.Show();
        }

        private void viewUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewUsers>())
            {
                frm.BringToFront();
                return;
            }

            var viewUsers = new ViewUsers {Type = 3, StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            viewUsers.Show();
        }

        private void modifyUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewUsers>())
            {
                frm.BringToFront();
                return;
            }

            var viewUsers = new ViewUsers {Type = 1, Text = "Modify User", StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            viewUsers.Show();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewUsers>())
            {
                frm.BringToFront();
                return;
            }

            var viewUsers = new ViewUsers
            {
                Type = 2, Text = "Delete User",
                StartPosition = FormStartPosition.CenterScreen,
                MdiParent = this
            };
            viewUsers.Show();
        }

        private void newFactoryInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmNewInvoice = new AddInvoice
            {
                StartPosition = FormStartPosition.CenterScreen,
                MdiParent = this, TypeOfInvoice = 1,
                Text = "New Factory Invoice"
            };
            frmNewInvoice.Show();
        }

        private void viewFactoryInvoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmViewInvoice = new ViewInvoice
            {
                StartPosition = FormStartPosition.CenterScreen,
                MdiParent = this, TypeOfInvoice = 1,
                Text = "View Factory Invoices"
            };
            frmViewInvoice.Show();
        }

        private void newIndimanInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmViewInvoice = new ViewInvoice
            {
                StartPosition = FormStartPosition.CenterScreen,
                MdiParent = this, TypeOfInvoice = 2,
                Text = "New Indiman Invoice"
            };
            frmViewInvoice.Show();
        }

        private void viewIndimanInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmViewInvoice = new ViewInvoice
            {
                StartPosition = FormStartPosition.CenterScreen,
                MdiParent = this, TypeOfInvoice = 0,
                Text = "View Indiman Invoices"
            };
            frmViewInvoice.Show();
        }

        private void addBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddBank>())
            {
                frm.BringToFront();
                return;
            }

            var frmAddBank = new AddBank {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmAddBank.Show();
        }

        private void viewBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewBank>())
            {
                frm.BringToFront();
                return;
            }

            var frmViewBank = new ViewBank {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmViewBank.Show();
        }

        private void addPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddPort>())
            {
                frm.BringToFront();
                return;
            }

            var frmAddPort = new AddPort {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmAddPort.Show();
        }

        private void viewPortsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewPorts>())
            {
                frm.BringToFront();
                return;
            }

            var frmviewPorts = new ViewPorts {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmviewPorts.Show();
        }

        private void addShipmentModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddShipmentMode>())
            {
                frm.BringToFront();
                return;
            }

            var frmAddShipmentMode = new AddShipmentMode {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmAddShipmentMode.Show();
        }

        private void viewShipmentModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewShipmentMode>())
            {
                frm.BringToFront();
                return;
            }

            var frmViewShipmentModes = new ViewShipmentMode {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmViewShipmentModes.Show();
        }

        private void addShippingAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<AddShippingAddress>())
            {
                frm.BringToFront();
                return;
            }

            var frmAddShippingAddress = new AddShippingAddress {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmAddShippingAddress.Show();
        }

        private void viewShippingAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var frm in MdiChildren.OfType<ViewShippingAddress>())
            {
                frm.BringToFront();
                return;
            }

            var frmViewShippingAddress = new ViewShippingAddress {StartPosition = FormStartPosition.CenterScreen, MdiParent = this};
            frmViewShippingAddress.Show();
        }

        #endregion                                                                              

 
    }
}
