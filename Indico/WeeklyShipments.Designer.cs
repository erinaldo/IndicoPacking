namespace IndicoPacking
{
    partial class WeeklyShipments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeeklyShipments));
            this.btnAddData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlWeekEndDate = new System.Windows.Forms.ComboBox();
            this.grdShipmentDetails = new System.Windows.Forms.DataGridView();
            this.pnlmain = new System.Windows.Forms.Panel();
            this.btnSynchronize = new System.Windows.Forms.Button();
            this.btnLoadLocal = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grpGetData = new System.Windows.Forms.GroupBox();
            this.btnGenerateCartonBarcods = new System.Windows.Forms.Button();
            this.btnGeneratePolybagBarcods = new System.Windows.Forms.Button();
            this.lblDespatchAddresses = new System.Windows.Forms.Label();
            this.lblOrderDeatilItems = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClearAllCartonItems = new System.Windows.Forms.Button();
            this.lblCurrentCount = new System.Windows.Forms.Label();
            this.lblCurrentCountValue = new System.Windows.Forms.Label();
            this.btnClearCartonArea = new System.Windows.Forms.Button();
            this.lblSelectedRows = new System.Windows.Forms.Label();
            this.lblSelectedRowsvalue = new System.Windows.Forms.Label();
            this.btnFillCarton = new System.Windows.Forms.Button();
            this.btnClearFilledCartons = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picVLImage = new System.Windows.Forms.PictureBox();
            this.picPatternImage = new System.Windows.Forms.PictureBox();
            this.clearFilledCartonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claerAllCartonItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCartonAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatePolybagLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genarateCartonLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGenerateAllBatchLabels = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.grdOrderDetailItem = new Telerik.WinControls.UI.RadGridView();
            this.btnFillingFirstScanningPolybags = new System.Windows.Forms.Button();
            this.lblSearchByPO = new System.Windows.Forms.Label();
            this.txtPONumber = new System.Windows.Forms.TextBox();
            this.GeneratePackingListButton = new System.Windows.Forms.Button();
            this.lblPAtternImage = new System.Windows.Forms.Label();
            this.CartonColorsPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ddlCarton = new System.Windows.Forms.ComboBox();
            this.btnAddcarton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipmentDetails)).BeginInit();
            this.grpGetData.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVLImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPatternImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderDetailItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderDetailItem.MasterTemplate)).BeginInit();
            this.CartonColorsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddData
            // 
            this.btnAddData.Location = new System.Drawing.Point(547, 19);
            this.btnAddData.Name = "btnAddData";
            this.btnAddData.Size = new System.Drawing.Size(99, 48);
            this.btnAddData.TabIndex = 0;
            this.btnAddData.Text = "Add Data";
            this.btnAddData.UseVisualStyleBackColor = true;
            this.btnAddData.Click += new System.EventHandler(this.btnAddData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Shipment Week";
            // 
            // ddlWeekEndDate
            // 
            this.ddlWeekEndDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlWeekEndDate.FormattingEnabled = true;
            this.ddlWeekEndDate.Location = new System.Drawing.Point(136, 30);
            this.ddlWeekEndDate.Name = "ddlWeekEndDate";
            this.ddlWeekEndDate.Size = new System.Drawing.Size(170, 21);
            this.ddlWeekEndDate.TabIndex = 2;
            this.ddlWeekEndDate.SelectedIndexChanged += new System.EventHandler(this.ddlWeekEndDate_SelectedIndexChanged);
            // 
            // grdShipmentDetails
            // 
            this.grdShipmentDetails.AllowUserToAddRows = false;
            this.grdShipmentDetails.AllowUserToDeleteRows = false;
            this.grdShipmentDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdShipmentDetails.Location = new System.Drawing.Point(12, 101);
            this.grdShipmentDetails.MultiSelect = false;
            this.grdShipmentDetails.Name = "grdShipmentDetails";
            this.grdShipmentDetails.ReadOnly = true;
            this.grdShipmentDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdShipmentDetails.Size = new System.Drawing.Size(822, 262);
            this.grdShipmentDetails.TabIndex = 3;
            this.grdShipmentDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVshipmentdetail_CellClick);
            // 
            // pnlmain
            // 
            this.pnlmain.AllowDrop = true;
            this.pnlmain.AutoScroll = true;
            this.pnlmain.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlmain.Location = new System.Drawing.Point(1048, 66);
            this.pnlmain.Name = "pnlmain";
            this.pnlmain.Size = new System.Drawing.Size(309, 666);
            this.pnlmain.TabIndex = 5;
            // 
            // btnSynchronize
            // 
            this.btnSynchronize.Location = new System.Drawing.Point(314, 14);
            this.btnSynchronize.Name = "btnSynchronize";
            this.btnSynchronize.Size = new System.Drawing.Size(99, 30);
            this.btnSynchronize.TabIndex = 7;
            this.btnSynchronize.Text = "Synchronize DBs";
            this.btnSynchronize.UseVisualStyleBackColor = true;
            this.btnSynchronize.Click += new System.EventHandler(this.btnSynchronize_Click);
            // 
            // btnLoadLocal
            // 
            this.btnLoadLocal.Location = new System.Drawing.Point(419, 15);
            this.btnLoadLocal.Name = "btnLoadLocal";
            this.btnLoadLocal.Size = new System.Drawing.Size(100, 31);
            this.btnLoadLocal.TabIndex = 8;
            this.btnLoadLocal.Text = "Load From Local";
            this.btnLoadLocal.UseVisualStyleBackColor = true;
            this.btnLoadLocal.Click += new System.EventHandler(this.btnLoadLocal_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // grpGetData
            // 
            this.grpGetData.Controls.Add(this.btnSynchronize);
            this.grpGetData.Controls.Add(this.btnLoadLocal);
            this.grpGetData.Location = new System.Drawing.Point(16, 14);
            this.grpGetData.Name = "grpGetData";
            this.grpGetData.Size = new System.Drawing.Size(525, 52);
            this.grpGetData.TabIndex = 10;
            this.grpGetData.TabStop = false;
            // 
            // btnGenerateCartonBarcods
            // 
            this.btnGenerateCartonBarcods.Location = new System.Drawing.Point(17, 55);
            this.btnGenerateCartonBarcods.Name = "btnGenerateCartonBarcods";
            this.btnGenerateCartonBarcods.Size = new System.Drawing.Size(153, 30);
            this.btnGenerateCartonBarcods.TabIndex = 11;
            this.btnGenerateCartonBarcods.Text = "Generate All Carton Labels";
            this.btnGenerateCartonBarcods.UseVisualStyleBackColor = true;
            this.btnGenerateCartonBarcods.Click += new System.EventHandler(this.btnGenerateCartonBarcods_Click);
            // 
            // btnGeneratePolybagBarcods
            // 
            this.btnGeneratePolybagBarcods.Location = new System.Drawing.Point(17, 19);
            this.btnGeneratePolybagBarcods.Name = "btnGeneratePolybagBarcods";
            this.btnGeneratePolybagBarcods.Size = new System.Drawing.Size(153, 30);
            this.btnGeneratePolybagBarcods.TabIndex = 12;
            this.btnGeneratePolybagBarcods.Text = "Generate All Polybag Labels";
            this.btnGeneratePolybagBarcods.UseVisualStyleBackColor = true;
            this.btnGeneratePolybagBarcods.Click += new System.EventHandler(this.btnGeneratePolybagBarcods_Click);
            // 
            // lblDespatchAddresses
            // 
            this.lblDespatchAddresses.AutoSize = true;
            this.lblDespatchAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchAddresses.Location = new System.Drawing.Point(13, 78);
            this.lblDespatchAddresses.Name = "lblDespatchAddresses";
            this.lblDespatchAddresses.Size = new System.Drawing.Size(153, 16);
            this.lblDespatchAddresses.TabIndex = 13;
            this.lblDespatchAddresses.Text = "Despatch Addresses";
            // 
            // lblOrderDeatilItems
            // 
            this.lblOrderDeatilItems.AutoSize = true;
            this.lblOrderDeatilItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDeatilItems.Location = new System.Drawing.Point(12, 382);
            this.lblOrderDeatilItems.Name = "lblOrderDeatilItems";
            this.lblOrderDeatilItems.Size = new System.Drawing.Size(133, 16);
            this.lblOrderDeatilItems.TabIndex = 14;
            this.lblOrderDeatilItems.Text = "Order Deatil Items";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1390, 946);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(432, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClearAllCartonItems
            // 
            this.btnClearAllCartonItems.Location = new System.Drawing.Point(17, 64);
            this.btnClearAllCartonItems.Name = "btnClearAllCartonItems";
            this.btnClearAllCartonItems.Size = new System.Drawing.Size(155, 30);
            this.btnClearAllCartonItems.TabIndex = 16;
            this.btnClearAllCartonItems.Text = "Cllear All Carton Items";
            this.btnClearAllCartonItems.UseVisualStyleBackColor = true;
            this.btnClearAllCartonItems.Click += new System.EventHandler(this.btnClearAllCartonItems_Click);
            // 
            // lblCurrentCount
            // 
            this.lblCurrentCount.AutoSize = true;
            this.lblCurrentCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentCount.Location = new System.Drawing.Point(231, 384);
            this.lblCurrentCount.Name = "lblCurrentCount";
            this.lblCurrentCount.Size = new System.Drawing.Size(75, 13);
            this.lblCurrentCount.TabIndex = 17;
            this.lblCurrentCount.Text = "Current Count:";
            // 
            // lblCurrentCountValue
            // 
            this.lblCurrentCountValue.AutoSize = true;
            this.lblCurrentCountValue.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentCountValue.Location = new System.Drawing.Point(309, 384);
            this.lblCurrentCountValue.Name = "lblCurrentCountValue";
            this.lblCurrentCountValue.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentCountValue.TabIndex = 18;
            this.lblCurrentCountValue.Text = "0";
            // 
            // btnClearCartonArea
            // 
            this.btnClearCartonArea.Location = new System.Drawing.Point(17, 25);
            this.btnClearCartonArea.Name = "btnClearCartonArea";
            this.btnClearCartonArea.Size = new System.Drawing.Size(153, 30);
            this.btnClearCartonArea.TabIndex = 19;
            this.btnClearCartonArea.Text = "Clear Carton Area";
            this.btnClearCartonArea.UseVisualStyleBackColor = true;
            this.btnClearCartonArea.Click += new System.EventHandler(this.btnClearCartonArea_Click);
            // 
            // lblSelectedRows
            // 
            this.lblSelectedRows.AutoSize = true;
            this.lblSelectedRows.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectedRows.Location = new System.Drawing.Point(382, 384);
            this.lblSelectedRows.Name = "lblSelectedRows";
            this.lblSelectedRows.Size = new System.Drawing.Size(82, 13);
            this.lblSelectedRows.TabIndex = 20;
            this.lblSelectedRows.Text = "Selected Rows:";
            // 
            // lblSelectedRowsvalue
            // 
            this.lblSelectedRowsvalue.AutoSize = true;
            this.lblSelectedRowsvalue.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectedRowsvalue.Location = new System.Drawing.Point(468, 384);
            this.lblSelectedRowsvalue.Name = "lblSelectedRowsvalue";
            this.lblSelectedRowsvalue.Size = new System.Drawing.Size(13, 13);
            this.lblSelectedRowsvalue.TabIndex = 21;
            this.lblSelectedRowsvalue.Text = "0";
            // 
            // btnFillCarton
            // 
            this.btnFillCarton.Location = new System.Drawing.Point(862, 311);
            this.btnFillCarton.Name = "btnFillCarton";
            this.btnFillCarton.Size = new System.Drawing.Size(154, 30);
            this.btnFillCarton.TabIndex = 22;
            this.btnFillCarton.Text = "Start/Resume Filling Carton";
            this.btnFillCarton.UseVisualStyleBackColor = true;
            this.btnFillCarton.Click += new System.EventHandler(this.btnFillCarton_Click);
            // 
            // btnClearFilledCartons
            // 
            this.btnClearFilledCartons.Location = new System.Drawing.Point(17, 100);
            this.btnClearFilledCartons.Name = "btnClearFilledCartons";
            this.btnClearFilledCartons.Size = new System.Drawing.Size(154, 29);
            this.btnClearFilledCartons.TabIndex = 23;
            this.btnClearFilledCartons.Text = "Clear All Filled Cartons";
            this.btnClearFilledCartons.UseVisualStyleBackColor = true;
            this.btnClearFilledCartons.Click += new System.EventHandler(this.btnClearFilledCartons_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearFilledCartons);
            this.groupBox1.Controls.Add(this.btnClearAllCartonItems);
            this.groupBox1.Controls.Add(this.btnClearCartonArea);
            this.groupBox1.Location = new System.Drawing.Point(845, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 140);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clear";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGeneratePolybagBarcods);
            this.groupBox2.Controls.Add(this.btnGenerateCartonBarcods);
            this.groupBox2.Location = new System.Drawing.Point(845, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 89);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Labels";
            // 
            // picVLImage
            // 
            this.picVLImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picVLImage.Location = new System.Drawing.Point(845, 409);
            this.picVLImage.Name = "picVLImage";
            this.picVLImage.Size = new System.Drawing.Size(197, 150);
            this.picVLImage.TabIndex = 26;
            this.picVLImage.TabStop = false;
            // 
            // picPatternImage
            // 
            this.picPatternImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPatternImage.Location = new System.Drawing.Point(845, 582);
            this.picPatternImage.Name = "picPatternImage";
            this.picPatternImage.Size = new System.Drawing.Size(197, 150);
            this.picPatternImage.TabIndex = 27;
            this.picPatternImage.TabStop = false;
            // 
            // clearFilledCartonsToolStripMenuItem
            // 
            this.clearFilledCartonsToolStripMenuItem.Name = "clearFilledCartonsToolStripMenuItem";
            this.clearFilledCartonsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.clearFilledCartonsToolStripMenuItem.Text = "Clear Filled Cartons";
            // 
            // claerAllCartonItemsToolStripMenuItem
            // 
            this.claerAllCartonItemsToolStripMenuItem.Name = "claerAllCartonItemsToolStripMenuItem";
            this.claerAllCartonItemsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.claerAllCartonItemsToolStripMenuItem.Text = "Claer All carton Items";
            // 
            // clearCartonAreaToolStripMenuItem
            // 
            this.clearCartonAreaToolStripMenuItem.Name = "clearCartonAreaToolStripMenuItem";
            this.clearCartonAreaToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.clearCartonAreaToolStripMenuItem.Text = "Clear Carton Area";
            // 
            // generatePolybagLabelsToolStripMenuItem
            // 
            this.generatePolybagLabelsToolStripMenuItem.Name = "generatePolybagLabelsToolStripMenuItem";
            this.generatePolybagLabelsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.generatePolybagLabelsToolStripMenuItem.Text = "Generate Polybag labels";
            // 
            // genarateCartonLabelsToolStripMenuItem
            // 
            this.genarateCartonLabelsToolStripMenuItem.Name = "genarateCartonLabelsToolStripMenuItem";
            this.genarateCartonLabelsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.genarateCartonLabelsToolStripMenuItem.Text = "Genarate Carton Labels";
            // 
            // btnGenerateAllBatchLabels
            // 
            this.btnGenerateAllBatchLabels.Location = new System.Drawing.Point(862, 22);
            this.btnGenerateAllBatchLabels.Name = "btnGenerateAllBatchLabels";
            this.btnGenerateAllBatchLabels.Size = new System.Drawing.Size(142, 29);
            this.btnGenerateAllBatchLabels.TabIndex = 29;
            this.btnGenerateAllBatchLabels.Text = "Generate All Batch Labels";
            this.btnGenerateAllBatchLabels.UseVisualStyleBackColor = true;
            this.btnGenerateAllBatchLabels.Click += new System.EventHandler(this.btnGenerateAllBatchLabels_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(848, 392);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Visual Layout Image";
            // 
            // grdOrderDetailItem
            // 
            this.grdOrderDetailItem.AutoScroll = true;
            this.grdOrderDetailItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdOrderDetailItem.Location = new System.Drawing.Point(12, 404);
            // 
            // 
            // 
            this.grdOrderDetailItem.MasterTemplate.AllowDragToGroup = false;
            this.grdOrderDetailItem.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.grdOrderDetailItem.MasterTemplate.ClipboardCutMode = Telerik.WinControls.UI.GridViewClipboardCutMode.Disable;
            this.grdOrderDetailItem.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.grdOrderDetailItem.MasterTemplate.EnableFiltering = true;
            this.grdOrderDetailItem.MasterTemplate.MultiSelect = true;
            this.grdOrderDetailItem.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdOrderDetailItem.Name = "grdOrderDetailItem";
            this.grdOrderDetailItem.ReadOnly = true;
            // 
            // 
            // 
            this.grdOrderDetailItem.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.grdOrderDetailItem.Size = new System.Drawing.Size(825, 325);
            this.grdOrderDetailItem.TabIndex = 33;
            this.grdOrderDetailItem.Text = "radGridView1";
            // 
            // btnFillingFirstScanningPolybags
            // 
            this.btnFillingFirstScanningPolybags.Location = new System.Drawing.Point(862, 349);
            this.btnFillingFirstScanningPolybags.Name = "btnFillingFirstScanningPolybags";
            this.btnFillingFirstScanningPolybags.Size = new System.Drawing.Size(155, 40);
            this.btnFillingFirstScanningPolybags.TabIndex = 34;
            this.btnFillingFirstScanningPolybags.Text = "Filling First Scanning Polybags";
            this.btnFillingFirstScanningPolybags.UseVisualStyleBackColor = true;
            this.btnFillingFirstScanningPolybags.Click += new System.EventHandler(this.btnFillingFirstScanningPolybags_Click);
            // 
            // lblSearchByPO
            // 
            this.lblSearchByPO.AutoSize = true;
            this.lblSearchByPO.Location = new System.Drawing.Point(637, 74);
            this.lblSearchByPO.Name = "lblSearchByPO";
            this.lblSearchByPO.Size = new System.Drawing.Size(83, 13);
            this.lblSearchByPO.TabIndex = 51;
            this.lblSearchByPO.Text = "Search by PO #";
            // 
            // txtPONumber
            // 
            this.txtPONumber.Location = new System.Drawing.Point(730, 71);
            this.txtPONumber.Name = "txtPONumber";
            this.txtPONumber.Size = new System.Drawing.Size(104, 20);
            this.txtPONumber.TabIndex = 52;
            // 
            // GeneratePackingListButton
            // 
            this.GeneratePackingListButton.Location = new System.Drawing.Point(652, 19);
            this.GeneratePackingListButton.Name = "GeneratePackingListButton";
            this.GeneratePackingListButton.Size = new System.Drawing.Size(126, 46);
            this.GeneratePackingListButton.TabIndex = 53;
            this.GeneratePackingListButton.Text = "Generate Packing List";
            this.GeneratePackingListButton.UseVisualStyleBackColor = true;
            this.GeneratePackingListButton.Click += new System.EventHandler(this.OnGeneratePackingListButtonClick);
            // 
            // lblPAtternImage
            // 
            this.lblPAtternImage.AutoSize = true;
            this.lblPAtternImage.Location = new System.Drawing.Point(844, 566);
            this.lblPAtternImage.Name = "lblPAtternImage";
            this.lblPAtternImage.Size = new System.Drawing.Size(76, 13);
            this.lblPAtternImage.TabIndex = 55;
            this.lblPAtternImage.Text = "Pattern  Image";
            // 
            // CartonColorsPanel
            // 
            this.CartonColorsPanel.Controls.Add(this.label5);
            this.CartonColorsPanel.Controls.Add(this.label6);
            this.CartonColorsPanel.Controls.Add(this.label4);
            this.CartonColorsPanel.Controls.Add(this.label9);
            this.CartonColorsPanel.Controls.Add(this.label10);
            this.CartonColorsPanel.Controls.Add(this.label11);
            this.CartonColorsPanel.Location = new System.Drawing.Point(1044, 39);
            this.CartonColorsPanel.Name = "CartonColorsPanel";
            this.CartonColorsPanel.Size = new System.Drawing.Size(313, 21);
            this.CartonColorsPanel.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Not Yet Filled";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(201, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 15);
            this.label6.TabIndex = 55;
            this.label6.Text = "  ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Partially Filled";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(201)))), ((int)(((byte)(171)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(101, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 15);
            this.label9.TabIndex = 53;
            this.label9.Text = "  ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 52;
            this.label10.Text = "Carton Filled";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(210)))), ((int)(((byte)(202)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Location = new System.Drawing.Point(11, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 15);
            this.label11.TabIndex = 51;
            this.label11.Text = "  ";
            // 
            // ddlCarton
            // 
            this.ddlCarton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCarton.FormattingEnabled = true;
            this.ddlCarton.Location = new System.Drawing.Point(1046, 5);
            this.ddlCarton.Name = "ddlCarton";
            this.ddlCarton.Size = new System.Drawing.Size(185, 21);
            this.ddlCarton.TabIndex = 56;
            this.ddlCarton.SelectedIndexChanged += new System.EventHandler(this.ddlCarton_SelectedIndexChanged);
            // 
            // btnAddcarton
            // 
            this.btnAddcarton.Location = new System.Drawing.Point(1245, 3);
            this.btnAddcarton.Name = "btnAddcarton";
            this.btnAddcarton.Size = new System.Drawing.Size(97, 30);
            this.btnAddcarton.TabIndex = 57;
            this.btnAddcarton.Text = "Add Carton";
            this.btnAddcarton.UseVisualStyleBackColor = true;
            this.btnAddcarton.Click += new System.EventHandler(this.btnAddcarton_Click);
            // 
            // WeeklyShipments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 739);
            this.Controls.Add(this.CartonColorsPanel);
            this.Controls.Add(this.ddlCarton);
            this.Controls.Add(this.btnAddcarton);
            this.Controls.Add(this.lblPAtternImage);
            this.Controls.Add(this.GeneratePackingListButton);
            this.Controls.Add(this.txtPONumber);
            this.Controls.Add(this.lblSearchByPO);
            this.Controls.Add(this.btnFillingFirstScanningPolybags);
            this.Controls.Add(this.grdOrderDetailItem);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerateAllBatchLabels);
            this.Controls.Add(this.btnAddData);
            this.Controls.Add(this.picPatternImage);
            this.Controls.Add(this.picVLImage);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFillCarton);
            this.Controls.Add(this.lblSelectedRowsvalue);
            this.Controls.Add(this.lblSelectedRows);
            this.Controls.Add(this.lblCurrentCountValue);
            this.Controls.Add(this.lblCurrentCount);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblOrderDeatilItems);
            this.Controls.Add(this.lblDespatchAddresses);
            this.Controls.Add(this.pnlmain);
            this.Controls.Add(this.grdShipmentDetails);
            this.Controls.Add(this.ddlWeekEndDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpGetData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WeeklyShipments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipments";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdShipmentDetails)).EndInit();
            this.grpGetData.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVLImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPatternImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderDetailItem.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderDetailItem)).EndInit();
            this.CartonColorsPanel.ResumeLayout(false);
            this.CartonColorsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlWeekEndDate;
        private System.Windows.Forms.DataGridView grdShipmentDetails;
        private System.Windows.Forms.Panel pnlmain;
        private System.Windows.Forms.Button btnSynchronize;
        private System.Windows.Forms.Button btnLoadLocal;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox grpGetData;
        private System.Windows.Forms.Button btnGenerateCartonBarcods;
        private System.Windows.Forms.Button btnGeneratePolybagBarcods;
        private System.Windows.Forms.Label lblDespatchAddresses;
        private System.Windows.Forms.Label lblOrderDeatilItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClearAllCartonItems;
        private System.Windows.Forms.Label lblCurrentCount;
        private System.Windows.Forms.Label lblCurrentCountValue;
        private System.Windows.Forms.Button btnClearCartonArea;
        private System.Windows.Forms.Label lblSelectedRows;
        private System.Windows.Forms.Label lblSelectedRowsvalue;
        private System.Windows.Forms.Button btnFillCarton;
        private System.Windows.Forms.Button btnClearFilledCartons;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picVLImage;
        private System.Windows.Forms.PictureBox picPatternImage;
        private System.Windows.Forms.ToolStripMenuItem clearFilledCartonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claerAllCartonItemsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCartonAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatePolybagLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genarateCartonLabelsToolStripMenuItem;
        private System.Windows.Forms.Button btnGenerateAllBatchLabels;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadGridView grdOrderDetailItem;
        private System.Windows.Forms.Button btnFillingFirstScanningPolybags;
        private System.Windows.Forms.Label lblSearchByPO;
        private System.Windows.Forms.TextBox txtPONumber;
        private System.Windows.Forms.Button GeneratePackingListButton;
        private System.Windows.Forms.Label lblPAtternImage;
        private System.Windows.Forms.Panel CartonColorsPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ddlCarton;
        private System.Windows.Forms.Button btnAddcarton;
    }
}

