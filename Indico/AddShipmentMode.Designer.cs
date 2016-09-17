namespace IndicoPacking
{
    partial class AddShipmentMode
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtModeName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblModeName = new System.Windows.Forms.Label();
            this.rfvModeName = new System.Windows.Forms.ErrorProvider(this.components);
            this.rfvDescription = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.rfvModeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvDescription)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(64, 137);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(83, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(110, 85);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(194, 20);
            this.txtDescription.TabIndex = 9;
            // 
            // txtModeName
            // 
            this.txtModeName.Location = new System.Drawing.Point(110, 34);
            this.txtModeName.Name = "txtModeName";
            this.txtModeName.Size = new System.Drawing.Size(194, 20);
            this.txtModeName.TabIndex = 8;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(25, 85);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 7;
            this.lblDescription.Text = "Description";
            // 
            // lblModeName
            // 
            this.lblModeName.AutoSize = true;
            this.lblModeName.Location = new System.Drawing.Point(25, 34);
            this.lblModeName.Name = "lblModeName";
            this.lblModeName.Size = new System.Drawing.Size(65, 13);
            this.lblModeName.TabIndex = 6;
            this.lblModeName.Text = "Mode Name";
            // 
            // rfvModeName
            // 
            this.rfvModeName.ContainerControl = this;
            // 
            // rfvDescription
            // 
            this.rfvDescription.ContainerControl = this;
            // 
            // AddShipmentMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 194);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtModeName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblModeName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddShipmentMode";
            this.Text = "Add Shipment Mode";
            this.Load += new System.EventHandler(this.AddShipmentMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rfvModeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rfvDescription)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtModeName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblModeName;
        private System.Windows.Forms.ErrorProvider rfvModeName;
        private System.Windows.Forms.ErrorProvider rfvDescription;
    }
}