using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking;
using IndicoPacking.Model;

namespace IndicoPacking
{
    public partial class CartonSizeDetails : Form
    {
        #region Fields

        IndicoPackingEntities context = new IndicoPackingEntities();

        #endregion

        #region Properties

        /// <summary>
        /// This type to identify the operation.
        /// 1 - Edit
        /// 2 - Delete
        /// 3 - View
        /// </summary>
        public int Type { get; set; }

        public ComboBox MainFormCartonComboBox { get; set; }

        public Form ParentMDIForm { get; set; }

        #endregion

        #region Constructors

        public CartonSizeDetails()
        {
            InitializeComponent();
            this.gridCartonSizeDetails.CellClick += gridCartonSizeDetails_CellClick;
            this.gridCartonSizeDetails.Columns.Clear();
        }

        private void frmCartonSizeDetails_Load(object sender, EventArgs e)
        {
            var carton = (from c in context.Cartons
                          select new { c.ID, c.Name, c.Qty, c.Description }).ToList();

            this.gridCartonSizeDetails.Columns.Clear();
            this.gridCartonSizeDetails.DataSource = carton;

            if (this.Type != 3)
            {
                this.AddCustomColumn();
            }
        }

        #endregion

        #region Events

        void gridCartonSizeDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.gridCartonSizeDetails.Columns["btnEditDelete"] != null && e.ColumnIndex == this.gridCartonSizeDetails.Columns["btnEditDelete"].Index && e.RowIndex >= 0)
            {
                IndicoPackingEntities context = new IndicoPackingEntities();
                DataGridViewRow clickedRow = this.gridCartonSizeDetails.Rows[e.RowIndex];
                int cartonId = int.Parse(clickedRow.Cells["ID"].Value.ToString());
                if (this.Type == 1) // Handle Edit
                {
                    ModifyCarton frmModifyCarton = new ModifyCarton();
                    frmModifyCarton.StartPosition = FormStartPosition.CenterScreen;
                    frmModifyCarton.CartonId = cartonId;
                    frmModifyCarton.ParentMDIForm = this.ParentMDIForm;
                    frmModifyCarton.ShowDialog();                  
                }
                else // Handle Delete 
                {
                    if (MessageBox.Show("Are you sure, you want to delete this carton?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Check this carton has been used in the system or not. 
                        // If used then show messages to the user that this can't be deleted. Otherwise delete
                        if ((from sdc in context.ShipmentDetailCartons
                                 where sdc.Carton == cartonId
                                 select sdc).ToList().Count > 0)
                        {
                            MessageBox.Show("This carton is used in the system therefore can't be deleted.");
                        }
                        else
                        {
                            context.Cartons.Remove((from c in context.Cartons
                                                    where c.ID == cartonId
                                                    select c).FirstOrDefault());
                            context.SaveChanges();

                            // Now if open any WeeklyShipments doalog need to repopulate the carton drop down
                            foreach (Form child in ParentMDIForm.MdiChildren)
                            {
                                if (child != null && child is IndicoPacking.WeeklyShipments)
                                {
                                    IndicoPacking.WeeklyShipments weeklyShipments = (IndicoPacking.WeeklyShipments)child;
                                    weeklyShipments.SetCartonDatasource = true;
                                }
                            }
                        }
                    }
                }

                var carton = (from c in context.Cartons
                              select new { c.ID, c.Name, c.Qty, c.Description }).ToList();

                this.gridCartonSizeDetails.DataSource = null;
                this.gridCartonSizeDetails.Columns.Clear();
                this.gridCartonSizeDetails.DataSource = carton;

                // Add custom column
                this.AddCustomColumn();
            }
        }      

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Private Methods

        private void AddCustomColumn()
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "btnEditDelete";
            btnColumn.Text = (this.Type == 1) ? "Edit" : "Delete";
            btnColumn.UseColumnTextForButtonValue = true;
            this.gridCartonSizeDetails.Columns.Add(btnColumn);
            this.gridCartonSizeDetails.Columns["btnEditDelete"].HeaderText = "";
        }       

        #endregion
    }
}
