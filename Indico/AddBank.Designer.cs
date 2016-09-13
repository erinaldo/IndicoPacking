namespace IndicoPacking
{
    partial class AddBank
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblBankName = new System.Windows.Forms.Label();
            this.lblAccNumber = new System.Windows.Forms.Label();
            this.lblBranch = new System.Windows.Forms.Label();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.lblPostCode = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtPostCode = new System.Windows.Forms.TextBox();
            this.cmbCountry = new System.Windows.Forms.ComboBox();
            this.rfvBankName = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvAccountNumber = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvBranch = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvCountry = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblSwiftCode = new System.Windows.Forms.Label();
            this.txtSwiftCode = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvAccountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvCountry)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(39, 438);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(93, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(194, 438);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 32);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(25, 33);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(63, 13);
            this.lblBankName.TabIndex = 2;
            this.lblBankName.Text = "Bank Name";
            // 
            // lblAccNumber
            // 
            this.lblAccNumber.AutoSize = true;
            this.lblAccNumber.Location = new System.Drawing.Point(25, 73);
            this.lblAccNumber.Name = "lblAccNumber";
            this.lblAccNumber.Size = new System.Drawing.Size(87, 13);
            this.lblAccNumber.TabIndex = 3;
            this.lblAccNumber.Text = "Account Number";
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(25, 113);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(41, 13);
            this.lblBranch.TabIndex = 4;
            this.lblBranch.Text = "Branch";
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(25, 153);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(44, 13);
            this.lblNumber.TabIndex = 5;
            this.lblNumber.Text = "Number";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(25, 193);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(45, 13);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(25, 233);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(24, 13);
            this.lblCity.TabIndex = 7;
            this.lblCity.Text = "City";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(25, 271);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(32, 13);
            this.lblState.TabIndex = 8;
            this.lblState.Text = "State";
            // 
            // lblPostCode
            // 
            this.lblPostCode.AutoSize = true;
            this.lblPostCode.Location = new System.Drawing.Point(25, 309);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(56, 13);
            this.lblPostCode.TabIndex = 9;
            this.lblPostCode.Text = "Post Code";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(25, 385);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(43, 13);
            this.lblCountry.TabIndex = 10;
            this.lblCountry.Text = "Country";
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(139, 30);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(161, 20);
            this.txtBankName.TabIndex = 11;
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(139, 70);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(161, 20);
            this.txtAccountNumber.TabIndex = 12;
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(139, 110);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(161, 20);
            this.txtBranch.TabIndex = 13;
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(139, 150);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(161, 20);
            this.txtNumber.TabIndex = 14;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(139, 190);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(161, 20);
            this.txtAddress.TabIndex = 15;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(139, 230);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(161, 20);
            this.txtCity.TabIndex = 16;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(139, 269);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(161, 20);
            this.txtState.TabIndex = 17;
            // 
            // txtPostCode
            // 
            this.txtPostCode.Location = new System.Drawing.Point(139, 308);
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(161, 20);
            this.txtPostCode.TabIndex = 18;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.FormattingEnabled = true;
            this.cmbCountry.Location = new System.Drawing.Point(139, 386);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Size = new System.Drawing.Size(161, 21);
            this.cmbCountry.TabIndex = 19;
            // 
            // rfvBankName
            // 
            this.rfvBankName.ContainerControl = this;
            // 
            // rfvAccountNumber
            // 
            this.rfvAccountNumber.ContainerControl = this;
            // 
            // rfvBranch
            // 
            this.rfvBranch.ContainerControl = this;
            // 
            // rfvCountry
            // 
            this.rfvCountry.ContainerControl = this;
            // 
            // lblSwiftCode
            // 
            this.lblSwiftCode.AutoSize = true;
            this.lblSwiftCode.Location = new System.Drawing.Point(25, 347);
            this.lblSwiftCode.Name = "lblSwiftCode";
            this.lblSwiftCode.Size = new System.Drawing.Size(58, 13);
            this.lblSwiftCode.TabIndex = 20;
            this.lblSwiftCode.Text = "Swift Code";
            // 
            // txtSwiftCode
            // 
            this.txtSwiftCode.Location = new System.Drawing.Point(139, 347);
            this.txtSwiftCode.Name = "txtSwiftCode";
            this.txtSwiftCode.Size = new System.Drawing.Size(161, 20);
            this.txtSwiftCode.TabIndex = 21;
            // 
            // AddBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 487);
            this.ControlBox = false;
            this.Controls.Add(this.txtSwiftCode);
            this.Controls.Add(this.lblSwiftCode);
            this.Controls.Add(this.cmbCountry);
            this.Controls.Add(this.txtPostCode);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.txtBankName);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblPostCode);
            this.Controls.Add(this.lblState);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.lblAccNumber);
            this.Controls.Add(this.lblBankName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddBank";
            this.Text = "Add Bank";
            this.Load += new System.EventHandler(this.AddBank_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rfvBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvAccountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvCountry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.Label lblAccNumber;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label lblPostCode;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtPostCode;
        private System.Windows.Forms.ComboBox cmbCountry;
        private System.Windows.Forms.ErrorProvider rfvBankName;
        private System.Windows.Forms.ErrorProvider rfvAccountNumber;
        private System.Windows.Forms.ErrorProvider rfvBranch;
        private System.Windows.Forms.ErrorProvider rfvCountry;
        private System.Windows.Forms.TextBox txtSwiftCode;
        private System.Windows.Forms.Label lblSwiftCode;
    }
}