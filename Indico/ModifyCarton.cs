using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IndicoPacking.Model;

namespace IndicoPacking
{
    public partial class ModifyCarton : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        Carton carton = null;

        #endregion

        #region Properties

        public int CartonId { get; set; }

        public Form ParentMDIForm { get; set; }

        #endregion

        public ModifyCarton()
        {
            InitializeComponent();

            context = new IndicoPackingEntities();
        }        

        private void frmModifyCarton_Load(object sender, EventArgs e)
        {
            // Get the carton that need to be edited
            carton = context.Cartons.Where(c => c.ID == CartonId).FirstOrDefault();
            this.txtNewname.Text = carton.Name;
            this.txtQty.Text = carton.Qty.ToString();
            if(carton.Description != null)
                this.txtDescription.Text = carton.Description.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate carton name and quantity 
            if (string.IsNullOrWhiteSpace(this.txtNewname.Text) && string.IsNullOrWhiteSpace(txtQty.Text))
            {
                MessageBox.Show("Name and quantity are required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (string.IsNullOrWhiteSpace(this.txtNewname.Text))
            {
                MessageBox.Show("Name is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (string.IsNullOrWhiteSpace(txtQty.Text))
            {
                MessageBox.Show("Quantity is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int newQty;

            if (int.TryParse(txtQty.Text, out newQty))
            {
                newQty = int.Parse(txtQty.Text);
                {
                    carton.Name = txtNewname.Text;
                    carton.Qty = newQty;
                    carton.Description = txtDescription.Text;
                }

                context.SaveChanges();
               
                this.Close();

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
            else
            {
                MessageBox.Show("Quantity must be a numeric value", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }   
    }
}
