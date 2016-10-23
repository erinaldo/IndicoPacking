

using System.Configuration;
using System.Data.SqlClient;

namespace IndicoPacking.Common
{
    internal class ConnectionManager
    {
        internal static SqlConnection IndicoPackingConnection
        {
            get
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoPacking"].ConnectionString);
                return connection;
            }
        }

        internal static SqlConnection IndicoConnection
        {
            get
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["IndicoConnString"].ConnectionString);
                return connection;
            }
        }
    }
}
