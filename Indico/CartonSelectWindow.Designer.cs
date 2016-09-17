namespace IndicoPacking
{
    partial class CartonSelectWindow
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
            this.CartonsList = new Telerik.WinControls.UI.RadCheckedListBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.CartonsList)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CartonsList
            // 
            this.CartonsList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CartonsList.GroupItemSize = new System.Drawing.Size(200, 200);
            this.CartonsList.ItemSize = new System.Drawing.Size(80, 60);
            this.CartonsList.Location = new System.Drawing.Point(0, 0);
            this.CartonsList.Name = "CartonsList";
            this.CartonsList.Size = new System.Drawing.Size(285, 468);
            this.CartonsList.TabIndex = 0;
            this.CartonsList.Text = "radCheckedListBox1";
            this.CartonsList.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(this.OnCartonListVisualItemFormatting);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(25, 3);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(83, 33);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OnOkButtonClick);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(114, 3);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(81, 33);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(201, 3);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(81, 33);
            this.SelectAllButton.TabIndex = 3;
            this.SelectAllButton.Text = "Select All";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.OnSelectAllButtonClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.SelectAllButton);
            this.flowLayoutPanel1.Controls.Add(this.CancelButton);
            this.flowLayoutPanel1.Controls.Add(this.OkButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 475);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(285, 44);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // CartonSelectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 519);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.CartonsList);
            this.MinimumSize = new System.Drawing.Size(301, 0);
            this.Name = "CartonSelectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CartonSelectWindow";
            ((System.ComponentModel.ISupportInitialize)(this.CartonsList)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadCheckedListBox CartonsList;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}