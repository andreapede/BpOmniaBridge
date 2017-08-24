using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml.Linq;
using Utility.ModifyRegistry;

namespace AppConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Bridge auto configuration";
            string cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var logFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "autoConfigLog.txt");
            Log log = new Log(logFilePath);

            try
            {
                Console.WriteLine("BpOmniaBridge is setting up Omnia ACS file:");
                log.WriteLog("BpOmniaBridge is setting up Omnia ACS file:", true);

                var acsFile = Path.Combine(cmnDocPath, "Cosmed", "CosmedLab", "acs.xml");
                var bridgeTempFile = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
                XDocument doc = XDocument.Load(acsFile);
                XElement bridgeControl = doc.Element("ExtendedConfiguration").Element("Bridge").Element("Control");
                bridgeControl.Attribute("ID").Value = "file";
                Console.WriteLine("  ID -> file  ");
                log.WriteLog("ID -> file");
                Thread.Sleep(500);
                bridgeControl.Attribute("Value").Value = "xml";
                Console.WriteLine("  Value -> xml  ");
                log.WriteLog("Value -> xml");
                Thread.Sleep(500);
                bridgeControl.Attribute("Item2").Value = "utf-8";
                Console.WriteLine("  Item2 -> utf-8  ");
                log.WriteLog("Item2 -> utf-8");
                Thread.Sleep(500);
                bridgeControl.Attribute("Item3").Value = bridgeTempFile;
                Console.WriteLine("  Item3 -> " + bridgeTempFile + "  ");
                log.WriteLog("Item3 -> " + bridgeTempFile);
                Thread.Sleep(500);
                bridgeControl.Attribute("Item4").Value = "OmniaXB";
                Console.WriteLine("  Item4 -> OmniaXB  ");
                log.WriteLog("Item4 -> OmniaXB");
                Thread.Sleep(500);

                doc.Save(acsFile);

                Console.WriteLine("Omnia settings updated - please create User as per documentation");
                Console.WriteLine(" ");
                Console.WriteLine("....please wait....");
                Console.WriteLine(" ");
                Thread.Sleep(1000);

                Console.WriteLine("BpOmniaBridge is setting up Best Practice register:");
                log.WriteLog("BpOmniaBridge is setting up Best Practice register:");
                string progFolder;
                try
                {
                    progFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                }
                catch
                {
                    progFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                }
                string bridgePath = Path.Combine(progFolder, "Cosmed", "BpOmniaBridge", "BpOmniaBridge.exe");

                if (!File.Exists(bridgePath))
                {
                    throw new Exception("Bridge software not found. Please restart the installation leaving the default installation folder.");
                }

                ModifyRegistry bpRegestry = new ModifyRegistry();
                string subKey1 = "SOFTWARE\\Best Practice Software\\Best Practice\\Device";
                string subKey2 = "SOFTWARE\\Wow6432Node\\Best Practice Software\\Best Practice\\Device";

                try
                {
                    bpRegestry.SubKey = subKey1;
                    bpRegestry.Write("SpiroPath", bridgePath);
                }
                catch
                {
                    bpRegestry.SubKey = subKey2;
                    bpRegestry.Write("SpiroPath", bridgePath);
                }

                Console.WriteLine("  SpiroPath -> " + bridgePath + "  ");
                log.WriteLog("SpiroPath -> " + bridgePath);
                Thread.Sleep(500);
                
                bpRegestry.Write("SpiroClass", "WindowsForms10.Window.8.app.0.13965fa_r11_ad1");
                Console.WriteLine("  SpiroClass -> WindowsForms10.Window.8.app.0.13965fa_r11_ad1 ");
                log.WriteLog("SpiroClass -> WindowsForms10.Window.8.app.0.13965fa_r11_ad1");
                Thread.Sleep(500);
                var fileVersion = FileVersionInfo.GetVersionInfo(bridgePath).FileVersion;
                bpRegestry.Write("SpiroName", "BP/Omnia Bridge v" + fileVersion);
                Console.WriteLine("  SprioName -> BP/Omnia Bridge v" + fileVersion +" ");
                log.WriteLog("SprioName -> BP/Omnia Bridge v" + fileVersion);
                Thread.Sleep(500);

                Console.WriteLine("");
                Console.WriteLine("Configuration's process successfully done!");
                log.WriteLog("Configuration's process successfully done!");
                Console.WriteLine("");
                Console.WriteLine("...Click any key to go close this...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
               
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("..........AN ERROR OCCURED.........");
                log.WriteLog("..........AN ERROR OCCURED.........");
                Console.WriteLine("Here more details:");
                log.WriteLog("Here more details:");
                Console.WriteLine(ex.Message);
                log.WriteLog(ex.Message);
                Console.WriteLine("");
                Console.WriteLine("");
                if (ex.Message.Contains("denied"))
                {
                    Console.WriteLine("Please try to run the installation again as Administrator");
                    Console.WriteLine("");
                    Console.WriteLine("");
                }
                Console.WriteLine("...Click enter to close this...");
                Console.ReadLine();
            }


        }

    }
}