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
    public partial class ViewBank : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        #endregion

        #region Constructors

        public ViewBank()
        {
            InitializeComponent();
        }

        private void ViewBank_Load(object sender, EventArgs e)
        {
            context = new IndicoPackingEntities();

            this.LoadGridBanks();
            this.AddCustomColumn();

            this.gridBanks.CommandCellClick += gridBanks_CommandCellClick;
        }        

        #endregion

        #region Events

        void gridBanks_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement cell = (GridCommandCellElement)sender;

            GridViewRowInfo clickedRow = this.gridBanks.Rows[cell.RowIndex];
            int bankId = int.Parse(clickedRow.Cells["ID"].Value.ToString());

            if (this.gridBanks.Columns["editColumn"] != null && cell.ColumnIndex == this.gridBanks.Columns["editColumn"].Index && cell.RowIndex >= 0)
            {
                // Handle Edit                
                AddBank addbank = new AddBank();
                addbank.StartPosition = FormStartPosition.CenterScreen;
                addbank.BankId = bankId;
                addbank.Text = "Edit Bank";
                addbank.ShowDialog();
            }
            else if (this.gridBanks.Columns["deleteColumn"] != null && cell.ColumnIndex == this.gridBanks.Columns["deleteColumn"].Index && cell.RowIndex >= 0)
            {
                // Remove from Bank table
                context.Banks.Remove((from u in context.Banks
                                         where u.ID == bankId
                                         select u).FirstOrDefault());

                context.SaveChanges();
            }

            this.gridBanks.DataSource = null;
            this.gridBanks.Columns.Clear();
            this.gridBanks.Rows.Clear();

            this.LoadGridBanks();            
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
            this.gridBanks.MasterTemplate.Columns.Add(editColumn);
           
            GridViewCommandColumn deleteColumn = new GridViewCommandColumn();
            deleteColumn.Name = "deleteColumn";
            deleteColumn.UseDefaultText = true;
            deleteColumn.DefaultText = "Delete";
            deleteColumn.FieldName = "delete";
            deleteColumn.HeaderText = "";
            this.gridBanks.MasterTemplate.Columns.Add(deleteColumn);            
        }

        private void LoadGridBanks()
        {
            context = new IndicoPackingEntities();

            this.gridBanks.DataSource = context.Banks.ToList();

            this.gridBanks.Columns["ID"].IsVisible = false;
            this.gridBanks.Columns["Country1"].IsVisible = false;
            this.gridBanks.Columns["Invoices"].IsVisible = false;

            this.gridBanks.Columns["Name"].Width = 150;
        }

        #endregion       
    }
}
