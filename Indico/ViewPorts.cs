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
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    public partial class ViewPorts : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Constructors

        public ViewPorts()
        {
            InitializeComponent();
        }

        private void ViewPorts_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            this.LoadGridPorts();
            this.AddCustomColumn();

            this.gridPorts.CommandCellClick += gridPorts_CommandCellClick;
        }       

        #endregion

        #region Events

        void gridPorts_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;

            GridViewRowInfo clickedRow = this.gridPorts.Rows[cell.RowIndex];
            int portId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (this.gridPorts.Columns["editColumn"] != null && cell.ColumnIndex == this.gridPorts.Columns["editColumn"].Index && cell.RowIndex >= 0)
            {
                // Handle Edit                
                AddPort addport = new AddPort();
                addport.StartPosition = FormStartPosition.CenterScreen;
                addport.PortId = portId;
                addport.Text = "Edit Port";
                addport.ShowDialog();
            }
            else if (this.gridPorts.Columns["deleteColumn"] != null && cell.ColumnIndex == this.gridPorts.Columns["deleteColumn"].Index && cell.RowIndex >= 0)
            {
                // Remove from Port table
                context.Ports.Remove((from u in context.Ports
                                      where u.ID == portId
                                      select u).FirstOrDefault());

                context.SaveChanges();
            }

            this.gridPorts.DataSource = null;
            this.gridPorts.Columns.Clear();
            this.gridPorts.Rows.Clear();

            this.LoadGridPorts();
            this.AddCustomColumn();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Methods

        private void AddCustomColumn()
        {
            GridViewCommandColumn editColumn = new GridViewCommandColumn();
            editColumn.Name = "editColumn";
            editColumn.UseDefaultText = true;
            editColumn.DefaultText = "Edit";
            editColumn.FieldName = "edit";
            editColumn.HeaderText = "";
            this.gridPorts.MasterTemplate.Columns.Add(editColumn);

            GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
            deleteColumn.Name = "deleteColumn";
            deleteColumn.UseDefaultText = true;
            deleteColumn.DefaultText = "Delete";
            deleteColumn.FieldName = "delete";
            deleteColumn.HeaderText = "";
            this.gridPorts.MasterTemplate.Columns.Add(deleteColumn);
        }

        private void LoadGridPorts()
        {
            context = new IndicoPackingEntities();

            this.gridPorts.DataSource = context.Ports.ToList();

            this.gridPorts.Columns["ID"].IsVisible = false;
            this.gridPorts.Columns["IndicoPortId"].IsVisible = false;
            this.gridPorts.Columns["Invoices"].IsVisible = false;
            this.gridPorts.Columns["DistributorClientAddresses"].IsVisible = false;

            this.gridPorts.Columns["Name"].Width = 100;
            this.gridPorts.Columns["Description"].Width = 100;
        }

        #endregion               
    }
}
