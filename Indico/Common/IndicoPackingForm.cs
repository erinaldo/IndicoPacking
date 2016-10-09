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
        protected SqlConnection IndicoPackingConnection
        {
            get
            {
                var connection =new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString);
                return connection;
            }
        }
    }
}
