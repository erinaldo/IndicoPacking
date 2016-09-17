using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;

namespace IndicoPacking
{
    public static class Utility
    {
        public static byte[] GetBytes(this string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

       /* public static void ProtectConnectionString()
        {
            ToggleConnectionStringProtection(System.Windows.Forms.Application.ExecutablePath, true);
        }

        public static void UnprotectConnectionString()
        {
            ToggleConnectionStringProtection(System.Windows.Forms.Application.ExecutablePath, false);
        }

        private static void ToggleConnectionStringProtection(string pathName, bool protect)
        {
            // Define the Dpapi provider name.
            string strProvider = "DataProtectionConfigurationProvider";
            // string strProvider = "RSAProtectedConfigurationProvider";

            System.Configuration.Configuration oConfiguration = null;
            System.Configuration.ConnectionStringsSection oSection = null;

            try
            {
                // Open the configuration file and retrieve 
                // the connectionStrings section.

                // For Web!
                // oConfiguration = System.Web.Configuration.
                //                  WebConfigurationManager.OpenWebConfiguration("~");

                // For Windows!
                // Takes the executable file name without the config extension.
                oConfiguration = System.Configuration.ConfigurationManager.
                                                OpenExeConfiguration(pathName);

                if (oConfiguration != null)
                {
                    bool blnChanged = false;

                    oSection = oConfiguration.GetSection("connectionStrings") as
                System.Configuration.ConnectionStringsSection;

                    if (oSection != null)
                    {
                        if ((!(oSection.ElementInformation.IsLocked)) &&
                (!(oSection.SectionInformation.IsLocked)))
                        {
                            if (protect)
                            {
                                if (!(oSection.SectionInformation.IsProtected))
                                {
                                    blnChanged = true;

                                    // Encrypt the section.
                                    oSection.SectionInformation.ProtectSection
                                (strProvider);
                                }
                            }
                            else
                            {
                                if (oSection.SectionInformation.IsProtected)
                                {
                                    blnChanged = true;

                                    // Remove encryption.
                                    oSection.SectionInformation.UnprotectSection();
                                }
                            }
                        }

                        if (blnChanged)
                        {
                            // Indicates whether the associated configuration section 
                            // will be saved even if it has not been modified.
                            oSection.SectionInformation.ForceSave = true;

                            // Save the current configuration.
                            oConfiguration.Save();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        } */

        /// <summary>
        /// get number of weeks of a year (default is current year)
        /// </summary>
        /// <param name="year">year to check</param>
        /// <returns>number of weeks</returns>
        /// <author>rusith</author>
        public static int getWeekCountOfAYear(int year=-1)
        {
            year = year == -1 ? DateTime.Now.Year : year;
            var dfi = DateTimeFormatInfo.CurrentInfo;
            var date1 = new DateTime(year, 12, 31);
            var cal = dfi.Calendar;
            return cal.GetWeekOfYear(date1, dfi.CalendarWeekRule,
                                                dfi.FirstDayOfWeek);
        }
    }

}
