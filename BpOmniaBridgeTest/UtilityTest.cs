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
            // test that in the Public Doc folder, the BpOmniaBridge folder is created
            // test if inside the BpOmniaBridge temp_files and pdf_files folders are created
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
            // test if the Log file is created in the BpOmniaBridge folder
            // test if the Bridge => started and the correct date is written in the log
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
