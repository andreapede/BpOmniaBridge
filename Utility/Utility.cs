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

namespace BpOmniaBridge
{
    public class Utility
    {
        public static void Initialize()
        {
            CreateUtilityFolders();
            if(Convert.ToBoolean(ConfigurationManager.AppSettings["LogEnabled"]))
            {
                CreateLogFile();
            }
        }

        //folder where the xml files will be exchanged between OMNIA and BP
        public static void CreateUtilityFolders()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var tempFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            var pdfFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files");
            Directory.CreateDirectory(tempFilePath);
            Directory.CreateDirectory(pdfFilePath);
        }

        //Create utility folder for BpOmniaBridge + log file
        public static void CreateLogFile()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullpath);
            Log("Bridge => Started", true);
        }

        //log file, using the clean flag to run the cleaning only when requested (bridge started only)
        public static void Log(string message, bool clean = false)
        {
            //create the file if doesn't exist
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullPath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullPath);
            var filePath = Path.Combine(fullPath, "log.txt");
            
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["LogEnabled"]))
            {
                //clean if necessary (rows more than 1000)
                if (clean)
                {
                    if (File.ReadLines(filePath).Count() > 1000)
                    {
                        var lines = File.ReadAllLines(filePath).Skip(800);
                        File.WriteAllLines(filePath, lines);
                        // empty line will be created but it won't create any issue
                    }
                }
      
                //write log
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    //write log
                    writer.Write("\r\n Log Entry: ");
                    writer.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                    writer.Write(" -- action: {0}", message);
                }
            }
            
        }

        public static bool IsOmniaRunning()
        {
            Mutex mutex;
            bool rv = Mutex.TryOpenExisting("{67F958EF-778C-4327-9285-43280B6891A7}", out mutex);
            mutex?.Close();

            return rv;
        }

        public static bool RunOmnia()
        {
            if (IsOmniaRunning())
            {
                return true;
            }

            //string promFileX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            //var fullPath = Path.Combine(promFileX86, "COSMED", "Omnia", "Standalone", "CosmedLab.exe");
            var omniaPath = ConfigurationManager.AppSettings["OmniaPath"];
            if (!File.Exists(omniaPath))
            {
                Log("error => incorrect OmniaPath");
                MessageBox.Show("Please run Omnia before starting the Spirometry test from Best Practice", "Omnia not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            Process.Start(omniaPath);
            return true;
        }

        // used in the UnitTest to test the flags
        public static void RefreshConfig()
        {
            ConfigurationManager.RefreshSection("appSettings");
        }


        // mathcing ethinicity, basically aboriginal don't have specific etnicity so they will be in Other group
        // Australian in Caucasian
        // any other nation will be Caucasian with message box to redifine that into Omnia if needed
        public static string MatchEthnicity(string value, bool test = false)
        {
            Dictionary<string, string> ethnicity = new Dictionary<string, string>();
            ethnicity.Add("Aboroginal", "Other");
            ethnicity.Add("TSI", "Other");
            ethnicity.Add("ATSI", "Other");
            ethnicity.Add("9999", "Other");
            ethnicity.Add("Other", "Other");
            ethnicity.Add("Australian", "Caucasian");
            
            try
            {
                return ethnicity[value];
            }
            catch (KeyNotFoundException)
            {
                if (!test) {
                                MessageBox.Show("The ethnicity has been set to Caucasian, please change it in OMNIA if needed", 
                                "Matching the ethnicity",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning );
                            }
                return "Caucasian";
            }
        }

        public static string ErrorList(int index)
        {
            Dictionary<int, string> errors = new Dictionary<int, string>();
            errors.Add(0, "Something went wrong with the login");
            errors.Add(1, "Something went wrong during the creation of the Subject");
            errors.Add(2, "Something went wrong when retriving visti card list from Omnia");
            errors.Add(3, "Something went wrong during TodayVisitCard elaboration");
            errors.Add(4, "Something went wrong in creating a visit card");
            errors.Add(5, "Something went wrong in selecting a visit card");
            errors.Add(6, "Something went wrong in populating the user interface");
            errors.Add(7, "Something went wrong in exporting the tests");
            errors.Add(8, "Something went wrong in elaborating the results");

            return errors[index];
        }


    }
}
