namespace IndicoPacking
{
    partial class ViewBank
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
            this.gridBanks = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridBanks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBanks.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBanks
            // 
            this.gridBanks.Location = new System.Drawing.Point(12, 23);
            // 
            // 
            // 
            this.gridBanks.MasterTemplate.AllowDragToGroup = false;
            this.gridBanks.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridBanks.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridBanks.Name = "gridBanks";
            this.gridBanks.ReadOnly = true;
            this.gridBanks.Size = new System.Drawing.Size(1000, 259);
            this.gridBanks.TabIndex = 0;
            this.gridBanks.Text = "radGridView1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(470, 302);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 31);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ViewBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 345);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gridBanks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewBank";
            this.Text = "View Bank";
            this.Load += new System.EventHandler(this.ViewBank_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridBanks.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBanks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridBanks;
        private System.Windows.Forms.Button btnOK;
    }
}