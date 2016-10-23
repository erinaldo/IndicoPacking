using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndicoPacking.Common
{
    public class IndicoPackingForm:Form
    {
        protected SqlConnection IndicoPackingConnection => ConnectionManager.IndicoPackingConnection;

        protected SqlConnection IndicoConnection => ConnectionManager.IndicoConnection;
    }
}
