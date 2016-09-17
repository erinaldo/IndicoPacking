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
    public partial class AddPort : Form
    {
        #region Properties

        public int PortId { get; set; }

        #endregion

        #region Constructors

        public AddPort()
        {
            InitializeComponent();
        }

        private void AddPort_Load(object sender, EventArgs e)
        {
            IndicoPackingEntities context = new IndicoPackingEntities();

            Port port = context.Ports.Where(p => p.ID == PortId).FirstOrDefault();

            if (port != null)
            {
                this.txtPortName.Text = port.Name.ToString();
                this.txtDescription.Text = port.Description.ToString();
            }
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                IndicoPackingEntities context = new IndicoPackingEntities();

                Port port = null;

                if (PortId == 0)
                {
                    port = new Port();
                    port.Name = this.txtPortName.Text;
                    port.Description = this.txtDescription.Text;

                    context.Ports.Add(port);
                }
                else
                {
                    port = context.Ports.Where(p => p.ID == PortId).FirstOrDefault();

                    port.Name = this.txtPortName.Text;
                    port.Description = this.txtDescription.Text; 
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
            bool isPortNameValid = true, isDescriptionValid = true;

            rfvPortName.Clear();
            if (string.IsNullOrWhiteSpace(txtPortName.Text))
            {
                rfvPortName.SetError(txtPortName, "Port Name is required");
                isPortNameValid = false;
            }

            rfvDescription.Clear();
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                rfvDescription.SetError(txtDescription, "Description is required");
                isDescriptionValid = false;
            }

            if (isPortNameValid && isDescriptionValid)
            {
                isAllFormFieldEntriesValid = true;
            }

            return isAllFormFieldEntriesValid;
        }

        #endregion       
    }
}
