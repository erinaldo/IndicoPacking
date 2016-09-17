using System;
using Telerik.WinControls.UI;

namespace IndicoPacking.Common
{
    public class FactoryPriceColumn : GridViewDecimalColumn
    {
        public RadGridView ParentGrid { get; set; }

        public FactoryPriceColumn(string fieldName)
            : base(fieldName)
        { }

        public override Type GetCellType(GridViewRowInfo row)
        {
            if (row is GridViewTableHeaderRowInfo)
            {
                return typeof(FactoryPriceHeaderCellElement);
            }

            return base.GetCellType(row);
        }
    }
}
