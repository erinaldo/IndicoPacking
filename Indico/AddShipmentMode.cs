using IndicoPacking.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndicoPacking
{
    public partial class AddShipmentMode : Form
    {
        #region Properties

        public int ShipmentModeId { get; set; }

        #endregion

        #region Constructors

        public AddShipmentMode()
        {
            InitializeComponent();
        }

        private void AddShipmentMode_Load(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            ShipmentMode shipmentMode = context.ShipmentModes.Where(s => s.ID == ShipmentModeId).FirstOrDefault();

            if(shipmentMode != null)
            {
                this.txtModeName.Text = shipmentMode.Name.ToString();
                this.txtDescription.Text = shipmentMode.Description.ToString();
            }
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                IndicoPackingEntities context = new IndicoPackingEntities();

                ShipmentMode shipmentMode = null;

                if (ShipmentModeId == 0)
                {
                    shipmentMode = new ShipmentMode();
                    shipmentMode.Name = this.txtModeName.Text;
                    shipmentMode.Description = this.txtDescription.Text;

                    context.ShipmentModes.Add(shipmentMode);
                }
                else
                {
                    shipmentMode = context.ShipmentModes.Where(s => s.ID == ShipmentModeId).FirstOrDefault();

                    shipmentMode.Name = this.txtModeName.Text;
                    shipmentMode.Description = this.txtDescription.Text;
                }

                context.SaveChanges();

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion     
   
        #region Methods

        private bool ValidateForm()
        {
            bool isAllFormFieldEntriesValid = false;
            bool isNameValid = true, isDescriptionValid = true;

            rfvModeName.Clear();
            if (string.IsNullOrWhiteSpace(txtModeName.Text))
            {
                rfvModeName.SetError(txtModeName, "Mode Name is required");
                isNameValid = false;
            }

            rfvDescription.Clear();
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                rfvDescription.SetError(txtDescription, "Description is required");
                isDescriptionValid = false;
            }

            if (isNameValid && isDescriptionValid)
            {
                isAllFormFieldEntriesValid = true;
            }

            return isAllFormFieldEntriesValid;
        }

        #endregion               
    }
}
