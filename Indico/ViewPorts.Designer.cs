namespace IndicoPacking
{
    partial class ViewPorts
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
            this.gridPorts = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPorts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPorts.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPorts
            // 
            this.gridPorts.Location = new System.Drawing.Point(12, 12);
            // 
            // 
            // 
            this.gridPorts.MasterTemplate.AllowDragToGroup = false;
            this.gridPorts.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridPorts.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridPorts.Name = "gridPorts";
            this.gridPorts.ReadOnly = true;
            this.gridPorts.Size = new System.Drawing.Size(340, 363);
            this.gridPorts.TabIndex = 0;
            this.gridPorts.Text = "radGridView1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(128, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 26);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ViewPorts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 428);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gridPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewPorts";
            this.Text = "View Ports";
            this.Load += new System.EventHandler(this.ViewPorts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPorts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPorts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridPorts;
        private System.Windows.Forms.Button btnOK;
    }
}