namespace IndicoPacking
{
    partial class AddInvoice
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddInvoice));
            this.lblWeek = new System.Windows.Forms.Label();
            this.lblETD = new System.Windows.Forms.Label();
            this.lblShipmentkey = new System.Windows.Forms.Label();
            this.cmbWeek = new System.Windows.Forms.ComboBox();
            this.dtETD = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblInvoiceNumber = new System.Windows.Forms.Label();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.lblAWDNumber = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.dtInvoiceDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtAWDNumber = new System.Windows.Forms.TextBox();
            this.lblShipTo = new System.Windows.Forms.Label();
            this.cmbShipTo = new System.Windows.Forms.ComboBox();
            this.lblBillTo = new System.Windows.Forms.Label();
            this.cmbBillTo = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.gridOrderDetail = new Telerik.WinControls.UI.RadGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbShipmentKey = new Telerik.WinControls.UI.RadMultiColumnComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblShipToAddress = new System.Windows.Forms.Label();
            this.lblBillToAddress = new System.Windows.Forms.Label();
            this.lblBank = new System.Windows.Forms.Label();
            this.cmbBank = new System.Windows.Forms.ComboBox();
            this.rfvInvoiceNumber = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvPort = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvMode = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvShipTo = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvBillTo = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvBank = new System.Windows.Forms.ErrorProvider(this.components);
            this.rbWithGrroupByQty = new System.Windows.Forms.RadioButton();
            this.rbWithoutGroupByQty = new System.Windows.Forms.RadioButton();
            this.lblItems = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.btnAddRemovedItems = new System.Windows.Forms.Button();
            this.btnApplyCostSheetPrice = new System.Windows.Forms.Button();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.lblShipmentKeyEdit = new System.Windows.Forms.Label();
            this.btnInvoiceDetail = new System.Windows.Forms.Button();
            this.btnCombinedInvoice = new System.Windows.Forms.Button();
            this.btnInvoiceSummary = new System.Windows.Forms.Button();
            this.btnSaveAndPrint = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtETD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderDetail.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey.EditorControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey.EditorControl.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvInvoiceNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvShipTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBillTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBank)).BeginInit();
            this.grpItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWeek
            // 
            this.lblWeek.AutoSize = true;
            this.lblWeek.Location = new System.Drawing.Point(17, 32);
            this.lblWeek.Name = "lblWeek";
            this.lblWeek.Size = new System.Drawing.Size(36, 13);
            this.lblWeek.TabIndex = 0;
            this.lblWeek.Text = "Week";
            // 
            // lblETD
            // 
            this.lblETD.AutoSize = true;
            this.lblETD.Location = new System.Drawing.Point(17, 70);
            this.lblETD.Name = "lblETD";
            this.lblETD.Size = new System.Drawing.Size(29, 13);
            this.lblETD.TabIndex = 1;
            this.lblETD.Text = "ETD";
            // 
            // lblShipmentkey
            // 
            this.lblShipmentkey.AutoSize = true;
            this.lblShipmentkey.Location = new System.Drawing.Point(17, 108);
            this.lblShipmentkey.Name = "lblShipmentkey";
            this.lblShipmentkey.Size = new System.Drawing.Size(71, 13);
            this.lblShipmentkey.TabIndex = 2;
            this.lblShipmentkey.Text = "Shipment key";
            // 
            // cmbWeek
            // 
            this.cmbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeek.FormattingEnabled = true;
            this.cmbWeek.Location = new System.Drawing.Point(100, 29);
            this.cmbWeek.Name = "cmbWeek";
            this.cmbWeek.Size = new System.Drawing.Size(164, 21);
            this.cmbWeek.TabIndex = 3;
            // 
            // dtETD
            // 
            this.dtETD.Location = new System.Drawing.Point(100, 67);
            this.dtETD.Name = "dtETD";
            this.dtETD.Size = new System.Drawing.Size(164, 20);
            this.dtETD.TabIndex = 4;
            this.dtETD.TabStop = false;
            this.dtETD.Text = "Monday, October 12, 2015";
            this.dtETD.Value = new System.DateTime(2015, 10, 12, 0, 0, 0, 0);
            // 
            // lblInvoiceNumber
            // 
            this.lblInvoiceNumber.AutoSize = true;
            this.lblInvoiceNumber.Location = new System.Drawing.Point(320, 33);
            this.lblInvoiceNumber.Name = "lblInvoiceNumber";
            this.lblInvoiceNumber.Size = new System.Drawing.Size(82, 13);
            this.lblInvoiceNumber.TabIndex = 5;
            this.lblInvoiceNumber.Text = "Invoice Number";
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(320, 70);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(68, 13);
            this.lblInvoiceDate.TabIndex = 6;
            this.lblInvoiceDate.Text = "Invoice Date";
            // 
            // lblAWDNumber
            // 
            this.lblAWDNumber.AutoSize = true;
            this.lblAWDNumber.Location = new System.Drawing.Point(320, 107);
            this.lblAWDNumber.Name = "lblAWDNumber";
            this.lblAWDNumber.Size = new System.Drawing.Size(73, 13);
            this.lblAWDNumber.TabIndex = 7;
            this.lblAWDNumber.Text = "AWD Number";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(636, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Status";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(420, 30);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(164, 20);
            this.txtInvoiceNumber.TabIndex = 10;
            // 
            // dtInvoiceDate
            // 
            this.dtInvoiceDate.Location = new System.Drawing.Point(420, 67);
            this.dtInvoiceDate.Name = "dtInvoiceDate";
            this.dtInvoiceDate.Size = new System.Drawing.Size(164, 20);
            this.dtInvoiceDate.TabIndex = 11;
            this.dtInvoiceDate.TabStop = false;
            this.dtInvoiceDate.Text = "Monday, October 12, 2015";
            this.dtInvoiceDate.Value = new System.DateTime(2015, 10, 12, 16, 4, 45, 790);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(706, 30);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(164, 21);
            this.cmbStatus.TabIndex = 12;
            // 
            // txtAWDNumber
            // 
            this.txtAWDNumber.Location = new System.Drawing.Point(420, 104);
            this.txtAWDNumber.Name = "txtAWDNumber";
            this.txtAWDNumber.Size = new System.Drawing.Size(164, 20);
            this.txtAWDNumber.TabIndex = 13;
            // 
            // lblShipTo
            // 
            this.lblShipTo.AutoSize = true;
            this.lblShipTo.Location = new System.Drawing.Point(910, 33);
            this.lblShipTo.Name = "lblShipTo";
            this.lblShipTo.Size = new System.Drawing.Size(40, 13);
            this.lblShipTo.TabIndex = 14;
            this.lblShipTo.Text = "Ship to";
            // 
            // cmbShipTo
            // 
            this.cmbShipTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShipTo.FormattingEnabled = true;
            this.cmbShipTo.Location = new System.Drawing.Point(989, 29);
            this.cmbShipTo.Name = "cmbShipTo";
            this.cmbShipTo.Size = new System.Drawing.Size(164, 21);
            this.cmbShipTo.TabIndex = 15;
            // 
            // lblBillTo
            // 
            this.lblBillTo.AutoSize = true;
            this.lblBillTo.Location = new System.Drawing.Point(910, 70);
            this.lblBillTo.Name = "lblBillTo";
            this.lblBillTo.Size = new System.Drawing.Size(32, 13);
            this.lblBillTo.TabIndex = 16;
            this.lblBillTo.Text = "Bill to";
            // 
            // cmbBillTo
            // 
            this.cmbBillTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBillTo.FormattingEnabled = true;
            this.cmbBillTo.Location = new System.Drawing.Point(989, 66);
            this.cmbBillTo.Name = "cmbBillTo";
            this.cmbBillTo.Size = new System.Drawing.Size(164, 21);
            this.cmbBillTo.TabIndex = 17;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(636, 107);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(34, 13);
            this.lblMode.TabIndex = 18;
            this.lblMode.Text = "Mode";
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(706, 104);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(164, 21);
            this.cmbMode.TabIndex = 19;
            // 
            // gridOrderDetail
            // 
            this.gridOrderDetail.Location = new System.Drawing.Point(12, 178);
            // 
            // 
            // 
            this.gridOrderDetail.MasterTemplate.AllowAddNewRow = false;
            this.gridOrderDetail.MasterTemplate.AllowDeleteRow = false;
            this.gridOrderDetail.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridOrderDetail.MasterTemplate.EnableFiltering = true;
            this.gridOrderDetail.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridOrderDetail.Name = "gridOrderDetail";
            this.gridOrderDetail.Size = new System.Drawing.Size(1571, 525);
            this.gridOrderDetail.TabIndex = 20;
            this.gridOrderDetail.Text = "radGridView1";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(770, 710);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(149, 33);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbShipmentKey
            // 
            this.cmbShipmentKey.AutoSize = true;
            this.cmbShipmentKey.AutoSizeDropDownToBestFit = true;
            this.cmbShipmentKey.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            // 
            // cmbShipmentKey.NestedRadGridView
            // 
            this.cmbShipmentKey.EditorControl.BackColor = System.Drawing.SystemColors.Window;
            this.cmbShipmentKey.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShipmentKey.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmbShipmentKey.EditorControl.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.cmbShipmentKey.EditorControl.MasterTemplate.AllowAddNewRow = false;
            this.cmbShipmentKey.EditorControl.MasterTemplate.AllowCellContextMenu = false;
            this.cmbShipmentKey.EditorControl.MasterTemplate.AllowColumnChooser = false;
            this.cmbShipmentKey.EditorControl.MasterTemplate.EnableGrouping = false;
            this.cmbShipmentKey.EditorControl.MasterTemplate.ShowFilteringRow = false;
            this.cmbShipmentKey.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.cmbShipmentKey.EditorControl.Name = "NestedRadGridView";
            this.cmbShipmentKey.EditorControl.ReadOnly = true;
            this.cmbShipmentKey.EditorControl.ShowGroupPanel = false;
            this.cmbShipmentKey.EditorControl.Size = new System.Drawing.Size(240, 150);
            this.cmbShipmentKey.EditorControl.TabIndex = 0;
            this.cmbShipmentKey.Location = new System.Drawing.Point(100, 106);
            this.cmbShipmentKey.Name = "cmbShipmentKey";
            this.cmbShipmentKey.Size = new System.Drawing.Size(164, 20);
            this.cmbShipmentKey.TabIndex = 23;
            this.cmbShipmentKey.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(636, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Port";
            // 
            // cmbPort
            // 
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(706, 67);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(165, 21);
            this.cmbPort.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 18);
            this.label2.TabIndex = 26;
            this.label2.Text = "Order Deatil Items";
            // 
            // lblShipToAddress
            // 
            this.lblShipToAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipToAddress.Location = new System.Drawing.Point(1159, 29);
            this.lblShipToAddress.Name = "lblShipToAddress";
            this.lblShipToAddress.Size = new System.Drawing.Size(235, 41);
            this.lblShipToAddress.TabIndex = 27;
            // 
            // lblBillToAddress
            // 
            this.lblBillToAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillToAddress.Location = new System.Drawing.Point(1159, 70);
            this.lblBillToAddress.Name = "lblBillToAddress";
            this.lblBillToAddress.Size = new System.Drawing.Size(235, 42);
            this.lblBillToAddress.TabIndex = 28;
            // 
            // lblBank
            // 
            this.lblBank.AutoSize = true;
            this.lblBank.Location = new System.Drawing.Point(910, 107);
            this.lblBank.Name = "lblBank";
            this.lblBank.Size = new System.Drawing.Size(75, 13);
            this.lblBank.TabIndex = 29;
            this.lblBank.Text = "Bank Account";
            // 
            // cmbBank
            // 
            this.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBank.FormattingEnabled = true;
            this.cmbBank.Location = new System.Drawing.Point(989, 103);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Size = new System.Drawing.Size(164, 21);
            this.cmbBank.TabIndex = 30;
            // 
            // rfvInvoiceNumber
            // 
            this.rfvInvoiceNumber.ContainerControl = this;
            // 
            // rfvPort
            // 
            this.rfvPort.ContainerControl = this;
            // 
            // rfvMode
            // 
            this.rfvMode.ContainerControl = this;
            // 
            // rfvShipTo
            // 
            this.rfvShipTo.ContainerControl = this;
            // 
            // rfvBillTo
            // 
            this.rfvBillTo.ContainerControl = this;
            // 
            // rfvBank
            // 
            this.rfvBank.ContainerControl = this;
            // 
            // rbWithGrroupByQty
            // 
            this.rbWithGrroupByQty.AutoSize = true;
            this.rbWithGrroupByQty.Location = new System.Drawing.Point(328, 14);
            this.rbWithGrroupByQty.Name = "rbWithGrroupByQty";
            this.rbWithGrroupByQty.Size = new System.Drawing.Size(128, 17);
            this.rbWithGrroupByQty.TabIndex = 31;
            this.rbWithGrroupByQty.TabStop = true;
            this.rbWithGrroupByQty.Text = "Grid group by quantity";
            this.rbWithGrroupByQty.UseVisualStyleBackColor = true;
            // 
            // rbWithoutGroupByQty
            // 
            this.rbWithoutGroupByQty.AutoSize = true;
            this.rbWithoutGroupByQty.Location = new System.Drawing.Point(68, 14);
            this.rbWithoutGroupByQty.Name = "rbWithoutGroupByQty";
            this.rbWithoutGroupByQty.Size = new System.Drawing.Size(165, 17);
            this.rbWithoutGroupByQty.TabIndex = 32;
            this.rbWithoutGroupByQty.TabStop = true;
            this.rbWithoutGroupByQty.Text = "Grid without group by quantity";
            this.rbWithoutGroupByQty.UseVisualStyleBackColor = true;
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(320, 149);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(111, 13);
            this.lblItems.TabIndex = 33;
            this.lblItems.Text = "Number of Items:  ";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCount.Location = new System.Drawing.Point(428, 149);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(0, 13);
            this.lblItemCount.TabIndex = 34;
            // 
            // btnAddRemovedItems
            // 
            this.btnAddRemovedItems.Location = new System.Drawing.Point(1181, 115);
            this.btnAddRemovedItems.Name = "btnAddRemovedItems";
            this.btnAddRemovedItems.Size = new System.Drawing.Size(183, 23);
            this.btnAddRemovedItems.TabIndex = 35;
            this.btnAddRemovedItems.Text = "Add Removed Items";
            this.btnAddRemovedItems.UseVisualStyleBackColor = true;
            this.btnAddRemovedItems.Click += new System.EventHandler(this.btnAddRemovedItems_Click);
            // 
            // btnApplyCostSheetPrice
            // 
            this.btnApplyCostSheetPrice.Location = new System.Drawing.Point(1181, 146);
            this.btnApplyCostSheetPrice.Name = "btnApplyCostSheetPrice";
            this.btnApplyCostSheetPrice.Size = new System.Drawing.Size(183, 23);
            this.btnApplyCostSheetPrice.TabIndex = 36;
            this.btnApplyCostSheetPrice.Text = "Apply Costsheet Price";
            this.btnApplyCostSheetPrice.UseVisualStyleBackColor = true;
            this.btnApplyCostSheetPrice.Click += new System.EventHandler(this.btnApplyCostSheetPrice_Click_1);
            // 
            // grpItems
            // 
            this.grpItems.Controls.Add(this.rbWithGrroupByQty);
            this.grpItems.Controls.Add(this.rbWithoutGroupByQty);
            this.grpItems.Location = new System.Drawing.Point(629, 136);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(524, 36);
            this.grpItems.TabIndex = 37;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Items";
            // 
            // lblShipmentKeyEdit
            // 
            this.lblShipmentKeyEdit.AutoSize = true;
            this.lblShipmentKeyEdit.Location = new System.Drawing.Point(97, 108);
            this.lblShipmentKeyEdit.Name = "lblShipmentKeyEdit";
            this.lblShipmentKeyEdit.Size = new System.Drawing.Size(0, 13);
            this.lblShipmentKeyEdit.TabIndex = 1;
            // 
            // btnInvoiceDetail
            // 
            this.btnInvoiceDetail.Location = new System.Drawing.Point(1400, 70);
            this.btnInvoiceDetail.Name = "btnInvoiceDetail";
            this.btnInvoiceDetail.Size = new System.Drawing.Size(183, 23);
            this.btnInvoiceDetail.TabIndex = 38;
            this.btnInvoiceDetail.Text = "Invoice Detail";
            this.btnInvoiceDetail.UseVisualStyleBackColor = true;
            this.btnInvoiceDetail.Click += new System.EventHandler(this.btnInvoiceDetail_Click);
            // 
            // btnCombinedInvoice
            // 
            this.btnCombinedInvoice.Location = new System.Drawing.Point(1400, 108);
            this.btnCombinedInvoice.Name = "btnCombinedInvoice";
            this.btnCombinedInvoice.Size = new System.Drawing.Size(183, 23);
            this.btnCombinedInvoice.TabIndex = 39;
            this.btnCombinedInvoice.Text = "Combined Invoice";
            this.btnCombinedInvoice.UseVisualStyleBackColor = true;
            this.btnCombinedInvoice.Click += new System.EventHandler(this.btnCombinedInvoice_Click);
            // 
            // btnInvoiceSummary
            // 
            this.btnInvoiceSummary.Location = new System.Drawing.Point(1400, 32);
            this.btnInvoiceSummary.Name = "btnInvoiceSummary";
            this.btnInvoiceSummary.Size = new System.Drawing.Size(183, 23);
            this.btnInvoiceSummary.TabIndex = 40;
            this.btnInvoiceSummary.Text = "Invoice Summary";
            this.btnInvoiceSummary.UseVisualStyleBackColor = true;
            this.btnInvoiceSummary.Click += new System.EventHandler(this.btnInvoiceSummary_Click);
            // 
            // btnSaveAndPrint
            // 
            this.btnSaveAndPrint.Location = new System.Drawing.Point(936, 710);
            this.btnSaveAndPrint.Name = "btnSaveAndPrint";
            this.btnSaveAndPrint.Size = new System.Drawing.Size(149, 33);
            this.btnSaveAndPrint.TabIndex = 41;
            this.btnSaveAndPrint.Text = "Save and print";
            this.btnSaveAndPrint.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(608, 710);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(149, 33);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AddInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1595, 755);
            this.Controls.Add(this.btnSaveAndPrint);
            this.Controls.Add(this.btnInvoiceSummary);
            this.Controls.Add(this.btnCombinedInvoice);
            this.Controls.Add(this.btnInvoiceDetail);
            this.Controls.Add(this.lblShipmentKeyEdit);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.btnApplyCostSheetPrice);
            this.Controls.Add(this.btnAddRemovedItems);
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.cmbBank);
            this.Controls.Add(this.lblBank);
            this.Controls.Add(this.lblBillToAddress);
            this.Controls.Add(this.lblShipToAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbShipmentKey);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gridOrderDetail);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.lblMode);
            this.Controls.Add(this.cmbBillTo);
            this.Controls.Add(this.lblBillTo);
            this.Controls.Add(this.cmbShipTo);
            this.Controls.Add(this.lblShipTo);
            this.Controls.Add(this.txtAWDNumber);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dtInvoiceDate);
            this.Controls.Add(this.txtInvoiceNumber);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblAWDNumber);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.lblInvoiceNumber);
            this.Controls.Add(this.dtETD);
            this.Controls.Add(this.cmbWeek);
            this.Controls.Add(this.lblShipmentkey);
            this.Controls.Add(this.lblETD);
            this.Controls.Add(this.lblWeek);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddInvoice";
            this.Text = "Add Invoice";
            this.Load += new System.EventHandler(this.AddInvoice_Load);
            this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dtETD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderDetail.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrderDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey.EditorControl.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey.EditorControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbShipmentKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvInvoiceNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvShipTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBillTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBank)).EndInit();
            this.grpItems.ResumeLayout(false);
            this.grpItems.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWeek;
        private System.Windows.Forms.Label lblETD;
        private System.Windows.Forms.Label lblShipmentkey;
        private System.Windows.Forms.ComboBox cmbWeek;
        private Telerik.WinControls.UI.RadDateTimePicker dtETD;
        private System.Windows.Forms.Label lblInvoiceNumber;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label lblAWDNumber;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private Telerik.WinControls.UI.RadDateTimePicker dtInvoiceDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtAWDNumber;
        private System.Windows.Forms.Label lblShipTo;
        private System.Windows.Forms.ComboBox cmbShipTo;
        private System.Windows.Forms.Label lblBillTo;
        private System.Windows.Forms.ComboBox cmbBillTo;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private Telerik.WinControls.UI.RadMultiColumnComboBox cmbShipmentKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblShipToAddress;
        private System.Windows.Forms.Label lblBillToAddress;
        private System.Windows.Forms.Label lblBank;
        private System.Windows.Forms.ComboBox cmbBank;
        private System.Windows.Forms.ErrorProvider rfvInvoiceNumber;
        private System.Windows.Forms.ErrorProvider rfvPort;
        private System.Windows.Forms.ErrorProvider rfvMode;
        private System.Windows.Forms.ErrorProvider rfvShipTo;
        private System.Windows.Forms.ErrorProvider rfvBillTo;
        private System.Windows.Forms.ErrorProvider rfvBank;
        private System.Windows.Forms.RadioButton rbWithoutGroupByQty;
        private System.Windows.Forms.RadioButton rbWithGrroupByQty;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Button btnAddRemovedItems;
        private System.Windows.Forms.Button btnApplyCostSheetPrice;
        private Telerik.WinControls.UI.RadGridView gridOrderDetail;
        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.Label lblShipmentKeyEdit;
        private System.Windows.Forms.Button btnInvoiceSummary;
        private System.Windows.Forms.Button btnCombinedInvoice;
        private System.Windows.Forms.Button btnInvoiceDetail;
        private System.Windows.Forms.Button btnSaveAndPrint;
    }
}