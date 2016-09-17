namespace IndicoPacking
{
    partial class ParentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParentForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newShipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCartonLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allPolybagLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allBatchLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startResumeFillingCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartonAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allCartonItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allFilledCartonItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cartonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCartonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCartonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFactoryInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFactoryInvoicesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIndimanInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewIndimanInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPortsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addShipmentModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewShipmentModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shipToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addShippingAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewShippingAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.shipmentsToolStripMenuItem,
            this.cartonsToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.invoicesToolStripMenuItem,
            this.portToolStripMenuItem,
            this.modeToolStripMenuItem,
            this.shipToToolStripMenuItem,
            this.bankToolStripMenuItem,
            this.windowsMenu,
            this.viewMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1255, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(37, 20);
            this.fileMenu.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // shipmentsToolStripMenuItem
            // 
            this.shipmentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newShipmentToolStripMenuItem,
            this.generateLabelsToolStripMenuItem,
            this.startResumeFillingCartonToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.shipmentsToolStripMenuItem.Name = "shipmentsToolStripMenuItem";
            this.shipmentsToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.shipmentsToolStripMenuItem.Text = "&Shipments";
            // 
            // newShipmentToolStripMenuItem
            // 
            this.newShipmentToolStripMenuItem.Name = "newShipmentToolStripMenuItem";
            this.newShipmentToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.newShipmentToolStripMenuItem.Text = "&New/Edit Shipment";
            this.newShipmentToolStripMenuItem.Click += new System.EventHandler(this.newShipmentToolStripMenuItem_Click);
            // 
            // generateLabelsToolStripMenuItem
            // 
            this.generateLabelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allCartonLabelsToolStripMenuItem,
            this.allPolybagLabelsToolStripMenuItem,
            this.allBatchLabelsToolStripMenuItem});
            this.generateLabelsToolStripMenuItem.Name = "generateLabelsToolStripMenuItem";
            this.generateLabelsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.generateLabelsToolStripMenuItem.Text = "&Generate Labels";
            // 
            // allCartonLabelsToolStripMenuItem
            // 
            this.allCartonLabelsToolStripMenuItem.Name = "allCartonLabelsToolStripMenuItem";
            this.allCartonLabelsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.allCartonLabelsToolStripMenuItem.Text = "All Carton Labels";
            this.allCartonLabelsToolStripMenuItem.Click += new System.EventHandler(this.allCartonLabelsToolStripMenuItem_Click);
            // 
            // allPolybagLabelsToolStripMenuItem
            // 
            this.allPolybagLabelsToolStripMenuItem.Name = "allPolybagLabelsToolStripMenuItem";
            this.allPolybagLabelsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.allPolybagLabelsToolStripMenuItem.Text = "All Polybag Labels";
            this.allPolybagLabelsToolStripMenuItem.Click += new System.EventHandler(this.allPolybagLabelsToolStripMenuItem_Click);
            // 
            // allBatchLabelsToolStripMenuItem
            // 
            this.allBatchLabelsToolStripMenuItem.Name = "allBatchLabelsToolStripMenuItem";
            this.allBatchLabelsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.allBatchLabelsToolStripMenuItem.Text = "All Batch Labels";
            this.allBatchLabelsToolStripMenuItem.Click += new System.EventHandler(this.allBatchLabelsToolStripMenuItem_Click);
            // 
            // startResumeFillingCartonToolStripMenuItem
            // 
            this.startResumeFillingCartonToolStripMenuItem.Name = "startResumeFillingCartonToolStripMenuItem";
            this.startResumeFillingCartonToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.startResumeFillingCartonToolStripMenuItem.Text = "Start/&Resume Filling Carton";
            this.startResumeFillingCartonToolStripMenuItem.Click += new System.EventHandler(this.startResumeFillingCartonToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cartonAreaToolStripMenuItem,
            this.allCartonItemsToolStripMenuItem,
            this.allFilledCartonItemsToolStripMenuItem});
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.clearToolStripMenuItem.Text = "&Clear";
            // 
            // cartonAreaToolStripMenuItem
            // 
            this.cartonAreaToolStripMenuItem.Name = "cartonAreaToolStripMenuItem";
            this.cartonAreaToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.cartonAreaToolStripMenuItem.Text = "&Carton Area";
            this.cartonAreaToolStripMenuItem.Click += new System.EventHandler(this.cartonAreaToolStripMenuItem_Click);
            // 
            // allCartonItemsToolStripMenuItem
            // 
            this.allCartonItemsToolStripMenuItem.Name = "allCartonItemsToolStripMenuItem";
            this.allCartonItemsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.allCartonItemsToolStripMenuItem.Text = "All Carton &Items";
            this.allCartonItemsToolStripMenuItem.Click += new System.EventHandler(this.allCartonItemsToolStripMenuItem_Click);
            // 
            // allFilledCartonItemsToolStripMenuItem
            // 
            this.allFilledCartonItemsToolStripMenuItem.Name = "allFilledCartonItemsToolStripMenuItem";
            this.allFilledCartonItemsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.allFilledCartonItemsToolStripMenuItem.Text = "All Filled Carton I&tems";
            this.allFilledCartonItemsToolStripMenuItem.Click += new System.EventHandler(this.allFilledCartonItemsToolStripMenuItem_Click);
            // 
            // cartonsToolStripMenuItem
            // 
            this.cartonsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCartonToolStripMenuItem,
            this.viewCartonsToolStripMenuItem,
            this.modifyCartonToolStripMenuItem,
            this.deleteCartonToolStripMenuItem});
            this.cartonsToolStripMenuItem.Name = "cartonsToolStripMenuItem";
            this.cartonsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.cartonsToolStripMenuItem.Text = "&Cartons";
            // 
            // addCartonToolStripMenuItem
            // 
            this.addCartonToolStripMenuItem.Name = "addCartonToolStripMenuItem";
            this.addCartonToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.addCartonToolStripMenuItem.Text = "Add C&arton";
            this.addCartonToolStripMenuItem.Click += new System.EventHandler(this.addCartonToolStripMenuItem_Click);
            // 
            // viewCartonsToolStripMenuItem
            // 
            this.viewCartonsToolStripMenuItem.Name = "viewCartonsToolStripMenuItem";
            this.viewCartonsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.viewCartonsToolStripMenuItem.Text = "&View Cartons";
            this.viewCartonsToolStripMenuItem.Click += new System.EventHandler(this.viewCartonsToolStripMenuItem_Click);
            // 
            // modifyCartonToolStripMenuItem
            // 
            this.modifyCartonToolStripMenuItem.Name = "modifyCartonToolStripMenuItem";
            this.modifyCartonToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.modifyCartonToolStripMenuItem.Text = "&Modify Carton";
            this.modifyCartonToolStripMenuItem.Click += new System.EventHandler(this.modifyCartonToolStripMenuItem_Click);
            // 
            // deleteCartonToolStripMenuItem
            // 
            this.deleteCartonToolStripMenuItem.Name = "deleteCartonToolStripMenuItem";
            this.deleteCartonToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.deleteCartonToolStripMenuItem.Text = "&Delete Carton";
            this.deleteCartonToolStripMenuItem.Click += new System.EventHandler(this.deleteCartonToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addUserToolStripMenuItem,
            this.viewUsersToolStripMenuItem,
            this.modifyUserToolStripMenuItem,
            this.deleteUserToolStripMenuItem});
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // addUserToolStripMenuItem
            // 
            this.addUserToolStripMenuItem.Name = "addUserToolStripMenuItem";
            this.addUserToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.addUserToolStripMenuItem.Text = "A&dd User";
            this.addUserToolStripMenuItem.Click += new System.EventHandler(this.addUserToolStripMenuItem_Click);
            // 
            // viewUsersToolStripMenuItem
            // 
            this.viewUsersToolStripMenuItem.Name = "viewUsersToolStripMenuItem";
            this.viewUsersToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.viewUsersToolStripMenuItem.Text = "View Us&ers";
            this.viewUsersToolStripMenuItem.Click += new System.EventHandler(this.viewUsersToolStripMenuItem_Click);
            // 
            // modifyUserToolStripMenuItem
            // 
            this.modifyUserToolStripMenuItem.Name = "modifyUserToolStripMenuItem";
            this.modifyUserToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.modifyUserToolStripMenuItem.Text = "Mo&dify User";
            this.modifyUserToolStripMenuItem.Click += new System.EventHandler(this.modifyUserToolStripMenuItem_Click);
            // 
            // deleteUserToolStripMenuItem
            // 
            this.deleteUserToolStripMenuItem.Name = "deleteUserToolStripMenuItem";
            this.deleteUserToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.deleteUserToolStripMenuItem.Text = "De&lete User";
            this.deleteUserToolStripMenuItem.Click += new System.EventHandler(this.deleteUserToolStripMenuItem_Click);
            // 
            // invoicesToolStripMenuItem
            // 
            this.invoicesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFactoryInvoiceToolStripMenuItem,
            this.viewFactoryInvoicesToolStripMenuItem,
            this.newIndimanInvoiceToolStripMenuItem,
            this.viewIndimanInvoiceToolStripMenuItem});
            this.invoicesToolStripMenuItem.Name = "invoicesToolStripMenuItem";
            this.invoicesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.invoicesToolStripMenuItem.Text = "&Invoices";
            // 
            // newFactoryInvoiceToolStripMenuItem
            // 
            this.newFactoryInvoiceToolStripMenuItem.Name = "newFactoryInvoiceToolStripMenuItem";
            this.newFactoryInvoiceToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.newFactoryInvoiceToolStripMenuItem.Text = "Ne&w Factory Invoice";
            this.newFactoryInvoiceToolStripMenuItem.Click += new System.EventHandler(this.newFactoryInvoiceToolStripMenuItem_Click);
            // 
            // viewFactoryInvoicesToolStripMenuItem
            // 
            this.viewFactoryInvoicesToolStripMenuItem.Name = "viewFactoryInvoicesToolStripMenuItem";
            this.viewFactoryInvoicesToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.viewFactoryInvoicesToolStripMenuItem.Text = "View Factory Invoi&ces";
            this.viewFactoryInvoicesToolStripMenuItem.Click += new System.EventHandler(this.viewFactoryInvoicesToolStripMenuItem_Click);
            // 
            // newIndimanInvoiceToolStripMenuItem
            // 
            this.newIndimanInvoiceToolStripMenuItem.Name = "newIndimanInvoiceToolStripMenuItem";
            this.newIndimanInvoiceToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.newIndimanInvoiceToolStripMenuItem.Text = "New Indiman Invoice";
            this.newIndimanInvoiceToolStripMenuItem.Click += new System.EventHandler(this.newIndimanInvoiceToolStripMenuItem_Click);
            // 
            // viewIndimanInvoiceToolStripMenuItem
            // 
            this.viewIndimanInvoiceToolStripMenuItem.Name = "viewIndimanInvoiceToolStripMenuItem";
            this.viewIndimanInvoiceToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.viewIndimanInvoiceToolStripMenuItem.Text = "View Indiman Invoice";
            this.viewIndimanInvoiceToolStripMenuItem.Click += new System.EventHandler(this.viewIndimanInvoiceToolStripMenuItem_Click);
            // 
            // portToolStripMenuItem
            // 
            this.portToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPortToolStripMenuItem,
            this.viewPortsToolStripMenuItem});
            this.portToolStripMenuItem.Name = "portToolStripMenuItem";
            this.portToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.portToolStripMenuItem.Text = "Destination Ports";
            // 
            // addPortToolStripMenuItem
            // 
            this.addPortToolStripMenuItem.Name = "addPortToolStripMenuItem";
            this.addPortToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.addPortToolStripMenuItem.Text = "Add Port";
            this.addPortToolStripMenuItem.Click += new System.EventHandler(this.addPortToolStripMenuItem_Click);
            // 
            // viewPortsToolStripMenuItem
            // 
            this.viewPortsToolStripMenuItem.Name = "viewPortsToolStripMenuItem";
            this.viewPortsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.viewPortsToolStripMenuItem.Text = "View Ports";
            this.viewPortsToolStripMenuItem.Click += new System.EventHandler(this.viewPortsToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShipmentModeToolStripMenuItem,
            this.viewShipmentModeToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
            this.modeToolStripMenuItem.Text = "Shipment Modes";
            // 
            // addShipmentModeToolStripMenuItem
            // 
            this.addShipmentModeToolStripMenuItem.Name = "addShipmentModeToolStripMenuItem";
            this.addShipmentModeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.addShipmentModeToolStripMenuItem.Text = "Add Shipment Mode";
            this.addShipmentModeToolStripMenuItem.Click += new System.EventHandler(this.addShipmentModeToolStripMenuItem_Click);
            // 
            // viewShipmentModeToolStripMenuItem
            // 
            this.viewShipmentModeToolStripMenuItem.Name = "viewShipmentModeToolStripMenuItem";
            this.viewShipmentModeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.viewShipmentModeToolStripMenuItem.Text = "View Shipment Mode";
            this.viewShipmentModeToolStripMenuItem.Click += new System.EventHandler(this.viewShipmentModeToolStripMenuItem_Click);
            // 
            // shipToToolStripMenuItem
            // 
            this.shipToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addShippingAddressToolStripMenuItem,
            this.viewShippingAddressToolStripMenuItem});
            this.shipToToolStripMenuItem.Name = "shipToToolStripMenuItem";
            this.shipToToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.shipToToolStripMenuItem.Text = "Shipping Addresses";
            // 
            // addShippingAddressToolStripMenuItem
            // 
            this.addShippingAddressToolStripMenuItem.Name = "addShippingAddressToolStripMenuItem";
            this.addShippingAddressToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.addShippingAddressToolStripMenuItem.Text = "Add Shipping Address";
            this.addShippingAddressToolStripMenuItem.Click += new System.EventHandler(this.addShippingAddressToolStripMenuItem_Click);
            // 
            // viewShippingAddressToolStripMenuItem
            // 
            this.viewShippingAddressToolStripMenuItem.Name = "viewShippingAddressToolStripMenuItem";
            this.viewShippingAddressToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.viewShippingAddressToolStripMenuItem.Text = "View Shipping Address";
            this.viewShippingAddressToolStripMenuItem.Click += new System.EventHandler(this.viewShippingAddressToolStripMenuItem_Click);
            // 
            // bankToolStripMenuItem
            // 
            this.bankToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addBankToolStripMenuItem,
            this.viewBankToolStripMenuItem});
            this.bankToolStripMenuItem.Name = "bankToolStripMenuItem";
            this.bankToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.bankToolStripMenuItem.Text = "Bank Details";
            // 
            // addBankToolStripMenuItem
            // 
            this.addBankToolStripMenuItem.Name = "addBankToolStripMenuItem";
            this.addBankToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.addBankToolStripMenuItem.Text = "Add Bank";
            this.addBankToolStripMenuItem.Click += new System.EventHandler(this.addBankToolStripMenuItem_Click);
            // 
            // viewBankToolStripMenuItem
            // 
            this.viewBankToolStripMenuItem.Name = "viewBankToolStripMenuItem";
            this.viewBankToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.viewBankToolStripMenuItem.Text = "View Bank";
            this.viewBankToolStripMenuItem.Click += new System.EventHandler(this.viewBankToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(68, 20);
            this.windowsMenu.Text = "&Windows";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileVerticalToolStripMenuItem.Text = "Tile &Vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Tile &Horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.closeAllToolStripMenuItem.Text = "C&lose All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.arrangeIconsToolStripMenuItem.Text = "&Arrange Icons";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(44, 20);
            this.viewMenu.Text = "&View";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.toolBarToolStripMenuItem.Text = "&Toolbar";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.statusBarToolStripMenuItem.Text = "&Status Bar";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.aboutToolStripMenuItem.Text = "&About ......";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 760);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1255, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // ParentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1255, 782);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ParentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indico Packing System";
            this.Load += new System.EventHandler(this.ParentForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem shipmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allCartonLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allPolybagLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allBatchLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startResumeFillingCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cartonAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allCartonItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allFilledCartonItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCartonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCartonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFactoryInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewFactoryInvoicesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newShipmentToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem newIndimanInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewIndimanInvoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shipToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewPortsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addShipmentModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewShipmentModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addShippingAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewShippingAddressToolStripMenuItem;
    }
}



