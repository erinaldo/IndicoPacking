using System;
using System.Configuration;

namespace IndicoPacking.DAL.Common
{
    class IndicoPackingConfiguration
    {
        private  static IndicoPackingConfiguration _configuration;
        private static string _connectionString;

        public static IndicoPackingConfiguration AppSettings => _configuration ?? (_configuration = new IndicoPackingConfiguration());

        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(_connectionString))
                    return _connectionString;

                string fromConfigurationFile;
                try { fromConfigurationFile = ConfigurationManager.ConnectionStrings["IndicoPacking"].ConnectionString; }
                catch (Exception) { fromConfigurationFile = null; }
                _connectionString = string.IsNullOrEmpty(fromConfigurationFile) ?
                    @"Data Source=MUFASA-PC\SQLEXPRESS;initial catalog=Indico2.0;Integrated Security=True"
                    : fromConfigurationFile;
                return _connectionString;
            }
        }
    }
}
