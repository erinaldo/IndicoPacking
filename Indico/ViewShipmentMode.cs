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
    public partial class ViewShipmentMode : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Constructors

        public ViewShipmentMode()
        {
            InitializeComponent();
        }

        private void ViewShipmentMode_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            this.LoadGridShipmentModes();
            this.AddCustomColumn();

            this.gridShipmentModes.CommandCellClick += gridShipmentModes_CommandCellClick;
        }

        #endregion

        #region Events

        void gridShipmentModes_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;

            GridViewRowInfo clickedRow = this.gridShipmentModes.Rows[cell.RowIndex];
            int shipmentModeId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (this.gridShipmentModes.Columns["editColumn"] != null && cell.ColumnIndex == this.gridShipmentModes.Columns["editColumn"].Index && cell.RowIndex >= 0)
            {
                // Handle Edit                
                AddShipmentMode addShipmentMode = new AddShipmentMode();
                addShipmentMode.StartPosition = FormStartPosition.CenterScreen;
                addShipmentMode.ShipmentModeId = shipmentModeId;
                addShipmentMode.Text = "Edit Shipment Mode";
                addShipmentMode.ShowDialog();
            }
            else if (this.gridShipmentModes.Columns["deleteColumn"] != null && cell.ColumnIndex == this.gridShipmentModes.Columns["deleteColumn"].Index && cell.RowIndex >= 0)
            {
                // Remove from Modes table
                context.ShipmentModes.Remove((from u in context.ShipmentModes
                                      where u.ID == shipmentModeId
                                      select u).FirstOrDefault());

                context.SaveChanges();
            }

            this.gridShipmentModes.DataSource = null;
            this.gridShipmentModes.Columns.Clear();
            this.gridShipmentModes.Rows.Clear();

            this.LoadGridShipmentModes();
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
            this.gridShipmentModes.MasterTemplate.Columns.Add(editColumn);

            GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
            deleteColumn.Name = "deleteColumn";
            deleteColumn.UseDefaultText = true;
            deleteColumn.DefaultText = "Delete";
            deleteColumn.FieldName = "delete";
            deleteColumn.HeaderText = "";
            this.gridShipmentModes.MasterTemplate.Columns.Add(deleteColumn);
        }

        private void LoadGridShipmentModes()
        {
            context = new IndicoPackingEntities();

            this.gridShipmentModes.DataSource = context.ShipmentModes.ToList();

            this.gridShipmentModes.Columns["ID"].IsVisible = false;
            this.gridShipmentModes.Columns["IndicoShipmentModeId"].IsVisible = false;
            this.gridShipmentModes.Columns["Invoices"].IsVisible = false;

            this.gridShipmentModes.Columns["Name"].Width = 100;
            this.gridShipmentModes.Columns["Description"].Width = 100;
        }

        #endregion                      
    }
}
