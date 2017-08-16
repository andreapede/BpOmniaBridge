using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class UtilityTest
    {
        [TestMethod]
        public void CreateUtilityFoldersTest()
        {
            Utility.CreateUtilityFolders();
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var tempFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
            var pdfFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files");

            Assert.AreEqual(true, Directory.Exists(tempFilePath), "Temp file folder not created");
            Assert.AreEqual(true, Directory.Exists(pdfFilePath), "PDF file folder not created");
        }

        [TestMethod]
        public void CreateLogFileTest()
        {
            Utility.CreateLogFile();
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filepath = Path.Combine(cmnDocPath, "BpOmniaBridge", "log.txt");

            Assert.AreEqual(true, File.Exists(filepath), "Log file not created");
            var lines = File.ReadAllLines(filepath);
            string lastline = lines.GetValue(lines.Length-1).ToString();
            Assert.AreEqual(true, lastline.Contains(DateTime.Today.ToLongDateString()), "Log not working");
            Assert.AreEqual(true, lastline.Contains("Bridge => Started"), "Log not working");
        }
    }
}
