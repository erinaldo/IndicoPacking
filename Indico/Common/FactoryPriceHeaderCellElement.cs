using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace IndicoPacking.Common
{
    public class FactoryPriceHeaderCellElement : GridHeaderCellElement
    {
        private RadTextBoxElement txtPrice = null;

        public RadGridView ParentGrid { get; set; }

        public FactoryPriceHeaderCellElement(GridViewColumn col, GridRowElement row)
            : base(col, row)
        {
            FactoryPriceColumn column = (FactoryPriceColumn)col;
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
            txtPrice.KeyPress += txtPrice_KeyPress;
            mathStack.Children.Add(txtPrice);

            RadButtonElement btnApply = new RadButtonElement();
            btnApply.Text = "Apply";
            btnApply.StretchHorizontally = false;
            mathStack.Children.Add(btnApply);
            btnApply.Click += btnApply_Click;

            this.Children.Add(mathStack);
        }

        void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            // checks to make sure only 1 decimal is allowed
            if (e.KeyChar == 46)
            {
                if ((sender as RadTextBoxElement).Text.IndexOf(e.KeyChar) != -1)
                    e.Handled = true;
            }
        }

        void btnApply_Click(object sender, EventArgs e)
        {
          //  RadGridViewElement grid = (RadGridViewElement) this.Parent.Parent.Parent.Parent.Parent.Parent.Parent;
            IEnumerator grdRowEnumerator =  this.ParentGrid.ChildRows.GetEnumerator();
            while(grdRowEnumerator.MoveNext())
            {
                GridViewRowInfo rowInfo = (GridViewRowInfo)grdRowEnumerator.Current;
                if (rowInfo.IsVisible && this.txtPrice.Text != string.Empty)
                {
                    rowInfo.Cells["Factory Price"].Value = this.txtPrice.Text;
                }
            }

            txtPrice.Text = string.Empty;
        }

        public override bool IsCompatible(GridViewColumn data, object context)
        {
            return data is FactoryPriceColumn && context is GridHeaderCellElement; ;
        }

        protected override Type ThemeEffectiveType
        {
            get { return typeof(GridHeaderCellElement); }
        }
    }
}
