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
    public partial class ChangeBoxSize : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Properties

        public int CartonId { get; set; }

        #endregion

        #region Constructors

        public ChangeBoxSize()
        {
            InitializeComponent();

            context = new IndicoPackingEntities();
        }

        #endregion

        #region Events

        private void frmChangeBoxSize_Load(object sender, EventArgs e)
        { 
            this.cmbCartons.DataSource = context.Cartons.ToList();
            this.cmbCartons.DisplayMember = "Name";
            this.cmbCartons.ValueMember = "ID";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CartonId = int.Parse(this.cmbCartons.SelectedValue.ToString());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
