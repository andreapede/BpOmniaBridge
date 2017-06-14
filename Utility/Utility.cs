using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace BpOmniaBridge.Utility
{
    public class Utility
    {
        //folder where the xml files will be exchanged between OMNIA and BP
        public static void CreateUtilityFolders()
        {
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var tempFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            var pdfFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files");
            Directory.CreateDirectory(tempFilePath);
            Directory.CreateDirectory(pdfFilePath);
        }

        //Create utility folder for BpOmniaBridge + log file
        public static void CreateLogFile()
        {
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullpath);
            Log("Bridge => Started");
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
                w.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
                w.Write(" -- action: {0}", message);
            }
        }

        public static bool RunOmnia()
        {
            //TODO: get the omniaPath from a config file ConfigurationManager.AppSettings["key"]
            //ASK: it will always be into Standalone?
            string promFileX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            var fullPath = Path.Combine(promFileX86, "COSMED", "Omnia", "Standalone", "CosmedLab.exe");
            if (!File.Exists(fullPath))
            {
                Log("error => incorrect OmniaPath");
                MessageBox.Show("Omnia is not installed in the default folder!", "Omnia not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            var pname = Process.GetProcessesByName("CosmedLab");
            //ASK: is that a way to know when OMNIA is running?
            if (pname.Length == 0)
                Process.Start(fullPath);
            return new CommandList.CommandList().Login("ocp", "bp");
        }

    }
}
