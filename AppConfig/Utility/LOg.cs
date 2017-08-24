using System;
using System.IO;

namespace AppConfig
{
    class Log
    {
        private string filePath;
        public Log(string path)
        {
            filePath = path;
        }

        public void WriteLog(string log, bool starts = false)
        {
            using (StreamWriter w = File.AppendText(filePath))
            {
                if (starts)
                {
                    w.Write("\r\nAuto configuration: ");
                    w.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                }
                w.Write("\r\n" + log);
            }
        }
    }
}
