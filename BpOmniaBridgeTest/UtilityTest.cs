using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;
using System.Linq;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class UtilityTest
    {
        TestHelper helper = new TestHelper();

        [TestMethod]
        public void CreateUtilityFoldersTest()
        {
            // test that in the Public Doc folder, the BpOmniaBridge folder is created
            // test if inside the BpOmniaBridge temp_files and pdf_files folders are created
            Utility.CreateUtilityFolders();

            Assert.AreEqual(true, Directory.Exists(helper.TempFileFolder()), "Temp file folder not created");
            Assert.AreEqual(true, Directory.Exists(helper.PdfFileFolder()), "PDF file folder not created");
        }

        [TestMethod]
        public void CreateLogFileTest()
        {
            // test if the Log file is created in the BpOmniaBridge folder
            // test if the Bridge => Started and the correct date is written in the log
            Utility.CreateLogFile();
            var filepath = Path.Combine(helper.BpOmniaFolder(), "log.txt");

            Assert.AreEqual(true, File.Exists(filepath), "Log file not created");
            var lines = File.ReadAllLines(filepath);
            string lastline = lines.GetValue(lines.Length-1).ToString();
            Assert.AreEqual(true, lastline.Contains(DateTime.Today.ToLongDateString()), "Log not working");
            Assert.AreEqual(true, lastline.Contains("Bridge => Started"), "Log not working");
        }

        [TestMethod]
        public void CleanLogFile()
        {
            //test if log file is cleaned if rows are more than 1000
            Utility.CreateLogFile();
            var bpOmniaFolder = helper.BpOmniaFolder();
            var correctLog = Path.Combine(bpOmniaFolder, "log.txt");
            var tempLog = Path.Combine(bpOmniaFolder , "temp_log.txt");

            // in case the test has failed before
            if(File.Exists(tempLog))
                File.Delete(tempLog);

            File.Move(correctLog, tempLog);
            
            helper.CopyFileToTest("test_log", "log", ".txt", bpOmniaFolder);

            Utility.Log("Clean", true);
            Assert.AreEqual(477, File.ReadLines(correctLog).Count());

            File.Delete(correctLog);
            File.Move(tempLog, correctLog); 
        }

        [TestMethod]
        public void MatchEthnicityTest()
        {
            Assert.AreEqual("Other", Utility.MatchEthnicity("Other"));
            Assert.AreEqual("Other", Utility.MatchEthnicity("TSI"));
            Assert.AreEqual("Other", Utility.MatchEthnicity("ATSI"));
            Assert.AreEqual("Other", Utility.MatchEthnicity("9999"));
            Assert.AreEqual("Caucasian", Utility.MatchEthnicity("Australian"));
            Assert.AreEqual("Caucasian", Utility.MatchEthnicity("anything else", true));
        }

        /*
        This test is not running because it changes the LogEnabledFlag in the App.config file but
        there is not a way to reload the appSettings between tests (apparently).
        TODO: FIX ME!!!!!!!!
        [TestMethod]
        public void LogEnabledFlagTest()
        {
            //test if the log file is not touched if LogEnabled flag is false
            Utility.CreateLogFile();
            var bpOmniaFolder = helper.BpOmniaFolder();
            var correctLog = Path.Combine(bpOmniaFolder, "log.txt");
            var tempLog = Path.Combine(bpOmniaFolder, "temp_log.txt");

            // in case the test has failed before
            if (File.Exists(tempLog))
                File.Delete(tempLog);

            File.Move(correctLog, tempLog);

            helper.CopyFileToTest("test_log", "log", ".txt", bpOmniaFolder);

            //set flag to false
            helper.SetAppConfigFlag("LogEnabled", "false");

            Utility.Log("Clean", true);
            Assert.AreEqual(1275, File.ReadLines(correctLog).Count());
            
            File.Delete(correctLog);
            File.Move(tempLog, correctLog);

            //set flag to true
            helper.SetAppConfigFlag("LogEnabled", "true");
        }*/
    }
}
