namespace IndicoPacking
{
    partial class CartonSizeDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CartonSizeDetails));
            this.gridCartonSizeDetails = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridCartonSizeDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCartonSizeDetails
            // 
            this.gridCartonSizeDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCartonSizeDetails.Location = new System.Drawing.Point(4, 8);
            this.gridCartonSizeDetails.Name = "gridCartonSizeDetails";
            this.gridCartonSizeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCartonSizeDetails.Size = new System.Drawing.Size(546, 195);
            this.gridCartonSizeDetails.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(209, 220);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(127, 30);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // CartonSizeDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 265);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gridCartonSizeDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CartonSizeDetails";
            this.Text = "Carton Size Details";
            this.Load += new System.EventHandler(this.frmCartonSizeDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridCartonSizeDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridCartonSizeDetails;
        private System.Windows.Forms.Button btnOk;
    }
}