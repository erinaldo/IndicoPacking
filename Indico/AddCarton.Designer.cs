namespace IndicoPacking
{
    partial class AddCarton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCarton));
            this.lblCartonName = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnSaveCarton = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtCartonname = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblCartonName
            // 
            this.lblCartonName.AutoSize = true;
            this.lblCartonName.Location = new System.Drawing.Point(26, 27);
            this.lblCartonName.Name = "lblCartonName";
            this.lblCartonName.Size = new System.Drawing.Size(35, 13);
            this.lblCartonName.TabIndex = 0;
            this.lblCartonName.Text = "Name";
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(26, 72);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(46, 13);
            this.lblQty.TabIndex = 1;
            this.lblQty.Text = "Quantity";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(106, 72);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(171, 20);
            this.txtQty.TabIndex = 3;
            // 
            // btnSaveCarton
            // 
            this.btnSaveCarton.Location = new System.Drawing.Point(63, 163);
            this.btnSaveCarton.Name = "btnSaveCarton";
            this.btnSaveCarton.Size = new System.Drawing.Size(100, 30);
            this.btnSaveCarton.TabIndex = 4;
            this.btnSaveCarton.Text = "Save";
            this.btnSaveCarton.UseVisualStyleBackColor = true;
            this.btnSaveCarton.Click += new System.EventHandler(this.btnSaveCarton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(185, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtCartonname
            // 
            this.txtCartonname.Location = new System.Drawing.Point(106, 27);
            this.txtCartonname.Name = "txtCartonname";
            this.txtCartonname.Size = new System.Drawing.Size(171, 20);
            this.txtCartonname.TabIndex = 7;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 122);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 8;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(106, 114);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(171, 20);
            this.txtDescription.TabIndex = 9;
            // 
            // AddCarton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 222);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtCartonname);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveCarton);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.lblQty);
            this.Controls.Add(this.lblCartonName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddCarton";
            this.Text = "Add  Carton";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCartonName;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnSaveCarton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtCartonname;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
    }
}