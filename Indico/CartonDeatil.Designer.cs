namespace IndicoPacking
{
    partial class CartonDeatils
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartonDeatils));
            this.grdCartonDeatils = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblQtyFilled = new System.Windows.Forms.Label();
            this.lblQtyFiledCount = new System.Windows.Forms.Label();
            this.lblNoItemsMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdCartonDeatils)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCartonDeatils
            // 
            this.grdCartonDeatils.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCartonDeatils.Location = new System.Drawing.Point(4, 41);
            this.grdCartonDeatils.Name = "grdCartonDeatils";
            this.grdCartonDeatils.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdCartonDeatils.Size = new System.Drawing.Size(938, 319);
            this.grdCartonDeatils.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(791, 366);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(151, 31);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblQtyFilled
            // 
            this.lblQtyFilled.AutoSize = true;
            this.lblQtyFilled.Location = new System.Drawing.Point(9, 18);
            this.lblQtyFilled.Name = "lblQtyFilled";
            this.lblQtyFilled.Size = new System.Drawing.Size(82, 13);
            this.lblQtyFilled.TabIndex = 2;
            this.lblQtyFilled.Text = "Quantity Filled : ";
            // 
            // lblQtyFiledCount
            // 
            this.lblQtyFiledCount.AutoSize = true;
            this.lblQtyFiledCount.Location = new System.Drawing.Point(99, 18);
            this.lblQtyFiledCount.Name = "lblQtyFiledCount";
            this.lblQtyFiledCount.Size = new System.Drawing.Size(0, 13);
            this.lblQtyFiledCount.TabIndex = 3;
            // 
            // lblNoItemsMessage
            // 
            this.lblNoItemsMessage.AutoSize = true;
            this.lblNoItemsMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoItemsMessage.Location = new System.Drawing.Point(288, 148);
            this.lblNoItemsMessage.Name = "lblNoItemsMessage";
            this.lblNoItemsMessage.Size = new System.Drawing.Size(427, 25);
            this.lblNoItemsMessage.TabIndex = 4;
            this.lblNoItemsMessage.Text = "There are no items added to this carton";
            // 
            // CartonDeatils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 409);
            this.Controls.Add(this.lblNoItemsMessage);
            this.Controls.Add(this.lblQtyFiledCount);
            this.Controls.Add(this.lblQtyFilled);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdCartonDeatils);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CartonDeatils";
            this.Text = "Carton Details";
            this.Load += new System.EventHandler(this.CartonDeatils_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCartonDeatils)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdCartonDeatils;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblQtyFilled;
        private System.Windows.Forms.Label lblQtyFiledCount;
        private System.Windows.Forms.Label lblNoItemsMessage;
    }
}