using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using IndicoPacking.Model;

namespace IndicoPacking
{
    public partial class CartonSelectWindow : Form
    {
        public List<int> SelectedCartons { get; set; }

        public CartonSelectWindow(IEnumerable<ShipmentDetailCarton> cartons)
        {
            InitializeComponent();
            SelectedCartons=new List<int>();
            InitializeCartonList(cartons.ToList());
        }

        private void InitializeCartonList(List<ShipmentDetailCarton> cartons)
        {
            if(cartons==null || cartons.Count<1)
                return;
            CartonsList.DataSource = cartons;
        }

        private void OnCartonListVisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {

            var installedFolder = (new FileInfo(Application.ExecutablePath).Directory?.FullName)+"\\";
            var item = e.VisualItem;
            var carton = item.Data.Value as ShipmentDetailCarton;
            if (carton == null)
                return;
            var detailCount = carton.OrderDeatilItems.Count;
            var filledCount = carton.OrderDeatilItems.Count(o => o.IsPolybagScanned);
            item.Image = Image.FromFile(installedFolder + @"images\" + (detailCount >= filledCount ? "closedbox.png" : "openbox.png")).GetThumbnailImage(60,50,null,IntPtr.Zero);
            item.Text = string.Format("<html>" +
                   "<span style=\"font-size:15pt;font-family:Segoe UI;\">  <b>" + carton.Number + "</b></span>" +
                   "<br/><span style=\"font-size:10.5pt;\">  Added : " + detailCount +
                   "<br>  Filled : " + filledCount + " </span>");

            if (item.Children.Count <= 0)
                return;
            var checkBoxItem = item.Children[0] as ListViewItemCheckbox;
            if (checkBoxItem != null) checkBoxItem.Margin = new Padding(2);
        }

        private void OnOkButtonClick(object sender, System.EventArgs e)
        {
            var checkedItems = CartonsList.CheckedItems;
            foreach (var shipmentDetailCarton in checkedItems.Select(checkedItem => checkedItem.Value).OfType<ShipmentDetailCarton>())
                SelectedCartons.Add(shipmentDetailCarton.ID);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OnSelectAllButtonClick(object sender, System.EventArgs e)
        {
            CartonsList.CheckAllItems();
        }
    }
}
