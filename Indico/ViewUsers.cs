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
using IndicoPacking.Common;

namespace IndicoPacking
{
    public partial class ViewUsers : Form
    {
        #region Fields

        IndicoPackingEntities context = null;

        public int Type { get; set; }

        #endregion

        public ViewUsers()
        {
            InitializeComponent();

            this.grdUsers.CellClick += grdUsers_CellClick;

            context = new IndicoPackingEntities();
        }       

        private void frmViewUsers_Load(object sender, EventArgs e)
        {
            this.LoadGrid();

            if (this.Type != 3)
            {
                this.AddCustomColumn();
            }
        }

        void grdUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.grdUsers.Columns["btnEditDelete"] != null && e.ColumnIndex == this.grdUsers.Columns["btnEditDelete"].Index && e.RowIndex >= 0)
            {                
                DataGridViewRow clickedRow = this.grdUsers.Rows[e.RowIndex];
                int userId = int.Parse(clickedRow.Cells["ID"].Value.ToString());
                if (this.Type == 1) // Handle Edit
                {
                    AddUser addUser = new AddUser();
                    addUser.StartPosition = FormStartPosition.CenterScreen;
                    addUser.UserId = userId;                   
                    addUser.ShowDialog(); 
                }
                else // Handle Delete 
                {
                    if (MessageBox.Show("Are you sure, you want to delete this user?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {                                          
                            context.Users.Remove((from u in context.Users
                                                    where u.ID == userId
                                                    select u).FirstOrDefault());
                            context.SaveChanges();                   
                    }
                }

                var carton = (from c in context.Cartons
                              select new { c.ID, c.Name, c.Qty }).ToList();

                this.grdUsers.DataSource = null;
                this.grdUsers.Columns.Clear();
                this.LoadGrid();

                // Add custom column
                this.AddCustomColumn();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }        

        #region Private Methods

        private void AddCustomColumn()
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = "btnEditDelete";
            btnColumn.Text = (this.Type == 1) ? "Edit" : "Delete";
            btnColumn.UseColumnTextForButtonValue = true;
            this.grdUsers.Columns.Add(btnColumn);
            this.grdUsers.Columns["btnEditDelete"].HeaderText = "";          
        }

        private void LoadGrid()
        {
            if (LoginInfo.Role == UserType.AppAdmin)
                this.grdUsers.DataSource = context.UserDetailsViews.Where(u => u.Role != "Application Administrator").ToList();
            else if (LoginInfo.Role == UserType.JkAdmin)
                this.grdUsers.DataSource = context.UserDetailsViews.Where(u => u.Role == "JK Administrator" || u.Role == "Filling Operator").ToList();
            else
                this.grdUsers.DataSource = context.UserDetailsViews.Where(u => u.Role == "Indiman Administrator").ToList();
        }

        #endregion
    }
}
