using System;
using Telerik.WinControls.UI;

namespace IndicoPacking.Common
{
    public class IndimanPriceColumn : GridViewDecimalColumn
    {
        public RadGridView ParentGrid { get; set; }

        public IndimanPriceColumn(string fieldName)
            : base(fieldName)
        { }

        public override Type GetCellType(GridViewRowInfo row)
        {
            if (row is GridViewTableHeaderRowInfo)
            {
                return typeof(IndimanPriceHeaderCellElement);
            }

            return base.GetCellType(row);
        }
    }
}
