
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;

namespace IndicoPacking.Tools
{
    public class IndicoPackingLog
    {
        private static IndicoPackingLog _log;
        private FileInfo _logFile;

        private IndicoPackingLog()
        {
            var filePath = Directory.GetCurrentDirectory()+@"\Data\log.txt";
            var directoryPath = Directory.GetCurrentDirectory()+@"\Data\";
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            _logFile= new FileInfo(filePath);
        }

        public static IndicoPackingLog GetObject()
        {
            if(_log==null)
                _log= new IndicoPackingLog();
            return _log;
        }

        public void Log(string str, params object[] parameters)
        {
            if(string.IsNullOrWhiteSpace(str))
                return;
            var body = string.Format(str, parameters);
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine(string.Format(Environment.NewLine+Environment.NewLine+"{0} ----", DateTime.Now.ToString(CultureInfo.CurrentCulture)));
            contentBuilder.Append("\t"+body.Replace(Environment.NewLine, Environment.NewLine + "\t"));
            var stream=_logFile.AppendText();
            stream.Write(contentBuilder.ToString());
            stream.Close();
        }

        public void Log(Exception e, string str, params object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;
            var body = string.Format(str, parameters);
            var contentBuilder = new StringBuilder();
            contentBuilder.AppendLine(string.Format(Environment.NewLine + Environment.NewLine + "{0} ----", DateTime.Now.ToString(CultureInfo.CurrentCulture)));
            contentBuilder.AppendLine(body.Replace(Environment.NewLine, Environment.NewLine + "\t"));
            contentBuilder.AppendLine("Exception --");
            contentBuilder.AppendLine("\t" + e.Message);
            contentBuilder.Append("\t"+e.StackTrace.Replace(Environment.NewLine, Environment.NewLine + "\t"));
            var stream = _logFile.AppendText();
            stream.Write(contentBuilder.ToString());
            stream.Close();
        }
    }
}
