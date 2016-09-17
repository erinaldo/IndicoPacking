namespace IndicoPacking
{
    partial class FillingCarton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FillingCarton));
            this.grdItems = new System.Windows.Forms.DataGridView();
            this.picVLImage = new System.Windows.Forms.PictureBox();
            this.lblCartonNumber = new System.Windows.Forms.Label();
            this.lblLastAddedItem = new System.Windows.Forms.Label();
            this.lblCarton = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemsInCarton = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblItemsFilled = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItemsyetToBeFilled = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtPolybag = new System.Windows.Forms.TextBox();
            this.lblStartScanPolybags = new System.Windows.Forms.Label();
            this.lblMessageSinhala = new System.Windows.Forms.Label();
            this.lblStartScanPolybagSinhala = new System.Windows.Forms.Label();
            this.lblErrorMsgSinhala = new System.Windows.Forms.Label();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVLImage)).BeginInit();
            this.SuspendLayout();
            // 
            // grdItems
            // 
            this.grdItems.AllowUserToAddRows = false;
            this.grdItems.AllowUserToDeleteRows = false;
            this.grdItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdItems.Location = new System.Drawing.Point(10, 40);
            this.grdItems.Name = "grdItems";
            this.grdItems.ReadOnly = true;
            this.grdItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdItems.Size = new System.Drawing.Size(950, 476);
            this.grdItems.TabIndex = 0;
            // 
            // picVLImage
            // 
            this.picVLImage.Location = new System.Drawing.Point(972, 40);
            this.picVLImage.Name = "picVLImage";
            this.picVLImage.Size = new System.Drawing.Size(280, 207);
            this.picVLImage.TabIndex = 1;
            this.picVLImage.TabStop = false;
            // 
            // lblCartonNumber
            // 
            this.lblCartonNumber.AutoSize = true;
            this.lblCartonNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCartonNumber.Location = new System.Drawing.Point(13, 14);
            this.lblCartonNumber.Name = "lblCartonNumber";
            this.lblCartonNumber.Size = new System.Drawing.Size(111, 16);
            this.lblCartonNumber.TabIndex = 3;
            this.lblCartonNumber.Text = "Carton Number";
            // 
            // lblLastAddedItem
            // 
            this.lblLastAddedItem.AutoSize = true;
            this.lblLastAddedItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastAddedItem.Location = new System.Drawing.Point(972, 14);
            this.lblLastAddedItem.Name = "lblLastAddedItem";
            this.lblLastAddedItem.Size = new System.Drawing.Size(120, 16);
            this.lblLastAddedItem.TabIndex = 4;
            this.lblLastAddedItem.Text = "Last Added Item";
            // 
            // lblCarton
            // 
            this.lblCarton.AutoSize = true;
            this.lblCarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCarton.Location = new System.Drawing.Point(128, 13);
            this.lblCarton.Name = "lblCarton";
            this.lblCarton.Size = new System.Drawing.Size(0, 20);
            this.lblCarton.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(966, 486);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(286, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "OK";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(253, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Items In Carton";
            // 
            // lblItemsInCarton
            // 
            this.lblItemsInCarton.AutoSize = true;
            this.lblItemsInCarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsInCarton.Location = new System.Drawing.Point(369, 14);
            this.lblItemsInCarton.Name = "lblItemsInCarton";
            this.lblItemsInCarton.Size = new System.Drawing.Size(0, 16);
            this.lblItemsInCarton.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(492, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Items Filled";
            // 
            // lblItemsFilled
            // 
            this.lblItemsFilled.AutoSize = true;
            this.lblItemsFilled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsFilled.Location = new System.Drawing.Point(586, 14);
            this.lblItemsFilled.Name = "lblItemsFilled";
            this.lblItemsFilled.Size = new System.Drawing.Size(0, 16);
            this.lblItemsFilled.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(709, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Items yet to Be Filled";
            // 
            // lblItemsyetToBeFilled
            // 
            this.lblItemsyetToBeFilled.AutoSize = true;
            this.lblItemsyetToBeFilled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsyetToBeFilled.Location = new System.Drawing.Point(868, 14);
            this.lblItemsyetToBeFilled.Name = "lblItemsyetToBeFilled";
            this.lblItemsyetToBeFilled.Size = new System.Drawing.Size(0, 16);
            this.lblItemsyetToBeFilled.TabIndex = 12;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(360, 196);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(452, 31);
            this.lblMessage.TabIndex = 13;
            this.lblMessage.Text = "Please scan the carton barcode...";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcode.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtBarcode.Location = new System.Drawing.Point(802, 211);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(53, 13);
            this.txtBarcode.TabIndex = 14;
            // 
            // txtPolybag
            // 
            this.txtPolybag.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPolybag.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPolybag.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.txtPolybag.Location = new System.Drawing.Point(246, 538);
            this.txtPolybag.Name = "txtPolybag";
            this.txtPolybag.Size = new System.Drawing.Size(100, 13);
            this.txtPolybag.TabIndex = 15;
            // 
            // lblStartScanPolybags
            // 
            this.lblStartScanPolybags.AutoSize = true;
            this.lblStartScanPolybags.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartScanPolybags.Location = new System.Drawing.Point(968, 295);
            this.lblStartScanPolybags.Name = "lblStartScanPolybags";
            this.lblStartScanPolybags.Size = new System.Drawing.Size(242, 24);
            this.lblStartScanPolybags.TabIndex = 16;
            this.lblStartScanPolybags.Text = "Start Scaning Polybags...";
            // 
            // lblMessageSinhala
            // 
            this.lblMessageSinhala.AutoSize = true;
            this.lblMessageSinhala.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageSinhala.Location = new System.Drawing.Point(360, 236);
            this.lblMessageSinhala.Name = "lblMessageSinhala";
            this.lblMessageSinhala.Size = new System.Drawing.Size(462, 31);
            this.lblMessageSinhala.TabIndex = 17;
            this.lblMessageSinhala.Text = "කරුණාකර පෙට්ටිය ස්කෑන් කරන්න...";
            // 
            // lblStartScanPolybagSinhala
            // 
            this.lblStartScanPolybagSinhala.AutoSize = true;
            this.lblStartScanPolybagSinhala.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartScanPolybagSinhala.Location = new System.Drawing.Point(968, 337);
            this.lblStartScanPolybagSinhala.Name = "lblStartScanPolybagSinhala";
            this.lblStartScanPolybagSinhala.Size = new System.Drawing.Size(291, 24);
            this.lblStartScanPolybagSinhala.TabIndex = 18;
            this.lblStartScanPolybagSinhala.Text = "පොලිබෑග් ස්කෑන් කිරීම අරඹන්න...";
            // 
            // lblErrorMsgSinhala
            // 
            this.lblErrorMsgSinhala.AutoSize = true;
            this.lblErrorMsgSinhala.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMsgSinhala.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMsgSinhala.Location = new System.Drawing.Point(21, 377);
            this.lblErrorMsgSinhala.Name = "lblErrorMsgSinhala";
            this.lblErrorMsgSinhala.Size = new System.Drawing.Size(0, 31);
            this.lblErrorMsgSinhala.TabIndex = 20;
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMsg.Location = new System.Drawing.Point(21, 337);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(0, 31);
            this.lblErrorMsg.TabIndex = 19;
            // 
            // FillingCarton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1264, 527);
            this.Controls.Add(this.lblErrorMsgSinhala);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblStartScanPolybagSinhala);
            this.Controls.Add(this.lblMessageSinhala);
            this.Controls.Add(this.lblStartScanPolybags);
            this.Controls.Add(this.txtPolybag);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblItemsyetToBeFilled);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblItemsFilled);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblItemsInCarton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblCarton);
            this.Controls.Add(this.lblLastAddedItem);
            this.Controls.Add(this.lblCartonNumber);
            this.Controls.Add(this.picVLImage);
            this.Controls.Add(this.grdItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FillingCarton";
            this.Text = "Filling Carton";
            this.Load += new System.EventHandler(this.frmFillingCarton_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picVLImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdItems;
        private System.Windows.Forms.PictureBox picVLImage;
        private System.Windows.Forms.Label lblCartonNumber;
        private System.Windows.Forms.Label lblLastAddedItem;
        private System.Windows.Forms.Label lblCarton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemsInCarton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblItemsFilled;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemsyetToBeFilled;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtPolybag;
        private System.Windows.Forms.Label lblStartScanPolybags;
        private System.Windows.Forms.Label lblMessageSinhala;
        private System.Windows.Forms.Label lblStartScanPolybagSinhala;
        private System.Windows.Forms.Label lblErrorMsgSinhala;
        private System.Windows.Forms.Label lblErrorMsg;
    }
}