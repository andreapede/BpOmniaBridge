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
        
        //folder where the xml files will be exchanged between OMNIA and BP
        public void CreateUtilityFolders()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var tempFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            var pdfFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files");
            Directory.CreateDirectory(tempFilePath);
            Directory.CreateDirectory(pdfFilePath);
        }

        //Create utility folder for BpOmniaBridge + log file
        public void CreateLogFile()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullpath);
        }
    }
}
