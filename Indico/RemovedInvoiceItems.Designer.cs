namespace IndicoPacking
{
    partial class RemovedInvoiceItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemovedInvoiceItems));
            this.gridRemovedItems = new Telerik.WinControls.UI.RadGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblItems = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridRemovedItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRemovedItems.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridRemovedItems
            // 
            this.gridRemovedItems.Location = new System.Drawing.Point(12, 27);
            // 
            // 
            // 
            this.gridRemovedItems.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridRemovedItems.MasterTemplate.EnableFiltering = true;
            this.gridRemovedItems.MasterTemplate.EnableGrouping = false;
            this.gridRemovedItems.MasterTemplate.MultiSelect = true;
            this.gridRemovedItems.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridRemovedItems.Name = "gridRemovedItems";
            this.gridRemovedItems.ReadOnly = true;
            this.gridRemovedItems.Size = new System.Drawing.Size(1141, 351);
            this.gridRemovedItems.TabIndex = 0;
            this.gridRemovedItems.Text = "radGridView1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(816, 395);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(151, 35);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(1002, 395);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(12, 406);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(57, 16);
            this.lblItems.TabIndex = 3;
            this.lblItems.Text = "Items : ";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCount.Location = new System.Drawing.Point(67, 406);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(0, 16);
            this.lblItemCount.TabIndex = 4;
            // 
            // RemovedInvoiceItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1165, 444);
            this.ControlBox = false;
            this.Controls.Add(this.lblItemCount);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gridRemovedItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RemovedInvoiceItems";
            this.Text = "Removed Items";
            this.Load += new System.EventHandler(this.RemovedInvoiceItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridRemovedItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRemovedItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridRemovedItems;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Label lblItemCount;
    }
}