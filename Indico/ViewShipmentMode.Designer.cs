namespace IndicoPacking
{
    partial class ViewShipmentMode
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnOK = new System.Windows.Forms.Button();
            this.gridShipmentModes = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentModes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentModes.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(128, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridShipmentModes
            // 
            this.gridShipmentModes.Location = new System.Drawing.Point(12, 12);
            // 
            // 
            // 
            this.gridShipmentModes.MasterTemplate.AllowDragToGroup = false;
            this.gridShipmentModes.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridShipmentModes.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.gridShipmentModes.Name = "gridShipmentModes";
            this.gridShipmentModes.ReadOnly = true;
            this.gridShipmentModes.Size = new System.Drawing.Size(340, 363);
            this.gridShipmentModes.TabIndex = 2;
            this.gridShipmentModes.Text = "radGridView1";
            // 
            // ViewShipmentMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 428);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gridShipmentModes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewShipmentMode";
            this.Text = "View Shipment Mode";
            this.Load += new System.EventHandler(this.ViewShipmentMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentModes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridShipmentModes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private Telerik.WinControls.UI.RadGridView gridShipmentModes;
    }
}