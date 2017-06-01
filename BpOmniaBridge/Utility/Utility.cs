using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpOmniaBridge.Utility
{
    public class Utility
    {
        //folder where the xml files will be exchanged between OMNIA and BP
        public static void CreateParamsFolder()
        {
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            Directory.CreateDirectory(fullpath);
        }

        //Create utility folder for BpOmniaBridge + log file
        public static void CreateLogFile()
        {
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullpath);
            Log("Bridge started");
        }

        //log file
        public static void Log(string message)
        {
            //create the file if doesn't exist
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullPath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullPath);
            var filePath = Path.Combine(fullPath, "log.txt");
            //write the log
            using (StreamWriter w = File.AppendText(filePath))
            {
                w.Write("\r\n Log Entry: ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                w.WriteLine(" action: {0}", message);
                w.WriteLine("-------------------------------");
            }
        }
    }
}
