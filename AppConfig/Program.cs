using System;
using System.IO;
using System.Threading;
using System.Xml.Linq;

namespace AppConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BpOmniaBridge is setting up Omnia:");
            
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var acsFile = Path.Combine(cmnDocPath, "Cosmed", "CosmedLab", "acs.xml");
            var bridgeTempFile = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            XDocument doc = XDocument.Load(acsFile);
            XElement bridgeControl = doc.Element("ExtendedConfiguration").Element("Bridge").Element("Control");
            bridgeControl.Attribute("ID").Value = "file";
            Console.WriteLine("... ID -> file ...");
            Thread.Sleep(500);
            bridgeControl.Attribute("Value").Value = "xml";
            Console.WriteLine("... Value -> xml ...");
            Thread.Sleep(500);
            bridgeControl.Attribute("Item2").Value = "utf-8";
            Console.WriteLine("... Item2 -> utf-8 ...");
            Thread.Sleep(500);
            bridgeControl.Attribute("Item3").Value = bridgeTempFile;
            Console.WriteLine("... Item3 -> " + bridgeTempFile + " ...");
            Thread.Sleep(500);
            bridgeControl.Attribute("Item4").Value = "OmniaXB";
            Console.WriteLine("... Item4 -> OmniaXB ...");
            Thread.Sleep(500);

            doc.Save(acsFile);
        
            Console.WriteLine("Omnia settings updated - please create User as per documentation");
            Console.WriteLine(" ");
            Console.WriteLine("....please wait....");
            Console.WriteLine(" ");
            Thread.Sleep(2000);
        }
    }
}