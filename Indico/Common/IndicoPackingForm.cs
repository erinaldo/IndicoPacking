using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace IndicoPacking.Common
{
    public class IndicoPackingForm:Form
    {
        protected SqlConnection IndicoPackingConnection => ConnectionManager.IndicoPackingConnection;

        protected SqlConnection IndicoConnection => ConnectionManager.IndicoConnection;

        protected  string InstalledFolder { get; private set; }

        public IndicoPackingForm()
        {
            try
            {
                InstalledFolder = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("bin"));

            }
            catch (Exception)
            {
             //ignored   
            }
        }
    }
}
