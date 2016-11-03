namespace IndicoPacking
{
    partial class ViewShippingAddress
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnOK = new System.Windows.Forms.Button();
            this.gridShippingAddress = new Telerik.WinControls.UI.RadGridView();
            this.SynchronizeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridShippingAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShippingAddress.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(1064, 447);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(119, 34);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridShippingAddress
            // 
            this.gridShippingAddress.Location = new System.Drawing.Point(12, 22);
            // 
            // 
            // 
            this.gridShippingAddress.MasterTemplate.AllowDragToGroup = false;
            this.gridShippingAddress.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridShippingAddress.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridShippingAddress.Name = "gridShippingAddress";
            this.gridShippingAddress.ReadOnly = true;
            this.gridShippingAddress.Size = new System.Drawing.Size(1171, 411);
            this.gridShippingAddress.TabIndex = 2;
            this.gridShippingAddress.Text = "radGridView1";
            // 
            // SynchronizeButton
            // 
            this.SynchronizeButton.Location = new System.Drawing.Point(943, 447);
            this.SynchronizeButton.Name = "SynchronizeButton";
            this.SynchronizeButton.Size = new System.Drawing.Size(113, 33);
            this.SynchronizeButton.TabIndex = 3;
            this.SynchronizeButton.Text = "Synchronize ";
            this.SynchronizeButton.UseVisualStyleBackColor = true;
            this.SynchronizeButton.Click += new System.EventHandler(this.OnSynchronizeButtonClick);
            // 
            // ViewShippingAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 493);
            this.ControlBox = false;
            this.Controls.Add(this.SynchronizeButton);
            this.Controls.Add(this.gridShippingAddress);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewShippingAddress";
            this.Text = "View Shipping Address";
            this.Load += new System.EventHandler(this.ViewShippingAddress_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShippingAddress.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShippingAddress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private Telerik.WinControls.UI.RadGridView gridShippingAddress;
        private System.Windows.Forms.Button SynchronizeButton;
    }
}