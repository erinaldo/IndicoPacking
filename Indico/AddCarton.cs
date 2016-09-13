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
    public partial class AddCarton : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion    

        #region Properties

        public Form ParentMDIForm { get; set; }

        #endregion

        public AddCarton()
        {
            InitializeComponent();

            context = new IndicoPackingEntities();

            this.txtCartonname.Focus();
        }           

        private void btnSaveCarton_Click(object sender, EventArgs e)
        {
            // Validate carton name and quantity 
            if (string.IsNullOrWhiteSpace(txtCartonname.Text) && string.IsNullOrWhiteSpace(txtQty.Text))
            {
                MessageBox.Show("Name and quantity are required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (string.IsNullOrWhiteSpace(txtCartonname.Text))
            {
                MessageBox.Show("Name is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else if (string.IsNullOrWhiteSpace(txtQty.Text))
            {
                MessageBox.Show("Quantity is required", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Check the newly added carton exists in system or not
            string newCartonName = txtCartonname.Text;
            string newDescription = txtDescription.Text;
            int newQty;

            var ctn = (from c in context.Cartons
                          where c.Name == newCartonName
                          select c).FirstOrDefault();

            if (ctn != null && ctn.Name == newCartonName)
            {
                MessageBox.Show("The added carton name already exists in the system", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                if (int.TryParse(txtQty.Text, out newQty))
                {
                    newQty = int.Parse(txtQty.Text);
                    Carton carton = new Carton();
                    {
                        carton.Name = newCartonName;
                        carton.Qty = newQty;
                        carton.Description = newDescription;
                    }

                    context.Cartons.Add(carton);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
