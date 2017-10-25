using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Threading;
using BPS;

namespace BpOmniaBridgeTest
{
    class TestHelper
    {

        public string TempFileFolder()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            return Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
        }

        public string BpOmniaFolder()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            return Path.Combine(cmnDocPath, "BpOmniaBridge");
        }

        public string PdfFileFolder()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            return Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files");
        }

        public string CopyFileToTest(string fileFrom, string fileTo = "same", string ext = ".out", string destFolder = "tempFileFolder")
        {
            if (fileTo == "same")
                fileTo = fileFrom;
            var currentFolder = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            string fileToMove = Path.Combine(currentFolder, "toTest", fileFrom + ext);
            if (destFolder == "tempFileFolder")
            {
                destFolder = TempFileFolder();
            }

            var destFileName = Path.Combine(destFolder, fileTo + ext); 

            File.Copy(fileToMove, destFileName, true);

            return destFileName;
        }

        public void DeleteTempFileIN(string fileName, string ext = ".in")
        {
            var filePath = Path.Combine(TempFileFolder(), fileName + ext);
            File.Delete(filePath);
        }

        public void SetAppConfigFlag(string key, string value)
        {
            var currentFolder = Directory.GetCurrentDirectory().Replace("\\BpOmniaBridgeTest\\bin\\Debug", "");
            string filePath = Path.Combine(currentFolder, "App.config");
            XDocument xml = XDocument.Load(filePath);
            IEnumerable<XElement> elements = xml.Elements("configuration").Elements("appSettings").Elements();
            foreach(XElement element in elements)
            {
                if(element.FirstAttribute.Value == key)
                {
                    element.SetAttributeValue("value", value);
                }
            }
            xml.Save(filePath);
            // refresh appSettings
            BpOmniaBridge.Utility.RefreshConfig();
        }
    }
}
