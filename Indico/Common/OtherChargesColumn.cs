using System;
using Telerik.WinControls.UI;

namespace IndicoPacking.Common
{
    public class OtherChargesColumn : GridViewDecimalColumn
    {
        public RadGridView ParentGrid { get; set; }

        public OtherChargesColumn(string fieldName)
            : base(fieldName)
        { }

        public override Type GetCellType(GridViewRowInfo row)
        {
            if (row is GridViewTableHeaderRowInfo)
            {
                return typeof(OtherChargesHeaderCellElement);
            }

            return base.GetCellType(row);
        }
    }
}
