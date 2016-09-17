namespace IndicoPacking
{
    partial class ChangeBoxSize
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeBoxSize));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.cmbCartons = new System.Windows.Forms.ComboBox();
            this.lblBoxSize = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(48, 114);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(55, 23);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(227, 16);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "You are going to change the box size";
            // 
            // cmbCartons
            // 
            this.cmbCartons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCartons.FormattingEnabled = true;
            this.cmbCartons.Location = new System.Drawing.Point(120, 66);
            this.cmbCartons.Name = "cmbCartons";
            this.cmbCartons.Size = new System.Drawing.Size(160, 21);
            this.cmbCartons.TabIndex = 3;
            // 
            // lblBoxSize
            // 
            this.lblBoxSize.AutoSize = true;
            this.lblBoxSize.Location = new System.Drawing.Point(57, 70);
            this.lblBoxSize.Name = "lblBoxSize";
            this.lblBoxSize.Size = new System.Drawing.Size(48, 13);
            this.lblBoxSize.TabIndex = 4;
            this.lblBoxSize.Text = "Box Size";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(201, 114);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangeBoxSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 159);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblBoxSize);
            this.Controls.Add(this.cmbCartons);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ChangeBoxSize";
            this.ShowInTaskbar = false;
            this.Text = "Change Box Size";
            this.Load += new System.EventHandler(this.frmChangeBoxSize_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ComboBox cmbCartons;
        private System.Windows.Forms.Label lblBoxSize;
        private System.Windows.Forms.Button btnCancel;
    }
}