using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace IndicoPacking.Common
{
    class IndimanPriceHeaderCellElement : GridHeaderCellElement
    {
        private RadTextBoxElement txtPrice = null;

        public RadGridView ParentGrid { get; set; }

        public IndimanPriceHeaderCellElement(GridViewColumn col, GridRowElement row)
            : base(col, row)
        {
            IndimanPriceColumn column = (IndimanPriceColumn)col;
            this.ParentGrid = column.ParentGrid;
            this.TextAlignment = ContentAlignment.TopCenter;
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();

            StackLayoutElement mathStack = new StackLayoutElement();
            mathStack.Margin = new Padding(2, 25, 20, 2);
            mathStack.StretchHorizontally = true;

            txtPrice = new RadTextBoxElement();
            txtPrice.StretchHorizontally = true;
            mathStack.Children.Add(txtPrice);

            RadButtonElement btnApply = new RadButtonElement();
            btnApply.Text = "Apply";
            btnApply.StretchHorizontally = false;
            mathStack.Children.Add(btnApply);
            btnApply.Click += btnApply_Click;

            this.Children.Add(mathStack);
        }

        void btnApply_Click(object sender, EventArgs e)
        {
          //RadGridViewElement grid = (RadGridViewElement) this.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
            IEnumerator grdRowEnumerator =  this.ParentGrid.ChildRows.GetEnumerator();
            while(grdRowEnumerator.MoveNext())
            {
                GridViewRowInfo rowInfo = (GridViewRowInfo)grdRowEnumerator.Current;
                if (rowInfo.IsVisible && this.txtPrice.Text != string.Empty)
                {
                    rowInfo.Cells["Indiman Price"].Value = this.txtPrice.Text;
                }
            }

            txtPrice.Text = string.Empty;
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return data is IndimanPriceColumn && context is GridHeaderCellElement; ;
        }

        protected override Type ThemeEffectiveType
        {
            get { return typeof(GridHeaderCellElement); }
        }
    }
}
