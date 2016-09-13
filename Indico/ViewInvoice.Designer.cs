namespace IndicoPacking
{
    partial class ViewInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewInvoice));
            this.btnOK = new System.Windows.Forms.Button();
            this.gridInvoices = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoices.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(559, 471);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(196, 33);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gridInvoices
            // 
            this.gridInvoices.Location = new System.Drawing.Point(7, 30);
            // 
            // 
            // 
            this.gridInvoices.MasterTemplate.AllowAddNewRow = false;
            this.gridInvoices.MasterTemplate.AllowDeleteRow = false;
            this.gridInvoices.MasterTemplate.AllowEditRow = false;
            this.gridInvoices.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridInvoices.MasterTemplate.EnableFiltering = true;
            this.gridInvoices.MasterTemplate.EnableGrouping = false;
            this.gridInvoices.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridInvoices.Name = "gridInvoices";
            this.gridInvoices.Size = new System.Drawing.Size(1365, 430);
            this.gridInvoices.TabIndex = 2;
            this.gridInvoices.Text = "radGridView1";
            // 
            // ViewInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1377, 516);
            this.Controls.Add(this.gridInvoices);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ViewInvoice";
            this.Text = "View Invoices";
            this.Load += new System.EventHandler(this.ViewInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoices.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private Telerik.WinControls.UI.RadGridView gridInvoices;
    }
}