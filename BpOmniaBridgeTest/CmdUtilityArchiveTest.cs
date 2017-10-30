using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class CmdUtilityArchiveTest
    {
        TestHelper testHelper = new TestHelper();

        [TestMethod]
        public void ArchiveTest()
        {
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            string[] prmValues = { "001", "BP", "Omnia", "Bridge", "19800101", "Male", "Caucasian", "180", "80" };
            var archive = new Archive(prmNames, prmValues);

            Command currentCommand;

            // create subject
            currentCommand = archive.CreateSubject();
            // simulate OMNIA's reply
            testHelper.CopyFileToTest("select_create_subj");
            var subjectID = currentCommand.Receive()["values"][0];

            // delete *.in file
            testHelper.DeleteTempFileIN("select_create_subj");

            // test read correct ID from out file
            Assert.AreEqual("12000000-3000-4000-5000-600000000001", subjectID);

            archive.SetRecordID(subjectID);
            currentCommand = archive.GetVisitCardList();
            // simulate OMNIA's reply
            testHelper.CopyFileToTest("list_visit_card");
            archive.SetVisitList(currentCommand.Receive());

            // simulate visit card already present
            var visitID = archive.TodayVisitCard("20131029");
            // test read correct ID from out file
            Assert.AreNotEqual("ID_00000000-0000-0000-0000-000000000002", visitID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000002", visitID);

            // delete *.in file
            testHelper.DeleteTempFileIN("list_visit_card");

            currentCommand = archive.GetVisitCardList();
            // simulate OMNIA's reply
            testHelper.CopyFileToTest("list_visit_card");
            archive.SetVisitList(currentCommand.Receive());

            // new visit card for today
            visitID = archive.TodayVisitCard(DateTime.Today.ToString("yyyyMMdd"));
            // test read correct ID from out file
            Assert.AreEqual("not found", visitID);

            currentCommand = archive.NewVisitCard();
            // simulate OMNIA's reply
            testHelper.CopyFileToTest("create_visit_card");
            Assert.AreEqual("00000000-0000-0000-0000-000000000001", currentCommand.Receive()["values"][0]);

            // delete *.in file
            testHelper.DeleteTempFileIN("create_visit_card");
            testHelper.DeleteTempFileIN("list_visit_card");
        }

        [TestMethod]
        public void ReadExportDataFileTest()
        {
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            string[] prmValues = { "001", "BP", "Omnia", "Bridge", "19800101", "Male", "Caucasian", "180", "80" };
            var archive = new Archive(prmNames, prmValues);
            List<string> result = new List<string> { };
            string filePath;
            
            //test no error raise in case of no test.xml file preset
            filePath = testHelper.CopyFileToTest("no_tests", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual(" ", result[0]);

            // Only PRE test
            filePath = testHelper.CopyFileToTest("test_only_PRE", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual("Diagnosis only PRE with FVC/FEV1% withing the normal range", result[0]);
            Assert.AreEqual("442", result[1]);
            Assert.AreEqual("3.60", result[2]);
            Assert.AreEqual("4.66", result[3]);
            Assert.AreEqual("", result[4]);
            Assert.AreEqual("", result[5]);
            Assert.AreEqual("", result[6]);
            Assert.AreEqual("24e48dba-f3ae-4650-8f8a-7c98502a1678", result[7]);
            Assert.AreEqual("PRE", result[8]);

            // Only PRE + BD POST test
            filePath = testHelper.CopyFileToTest("test_post_BD", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual("Diagnosis PRE + POST BD", result[0]);
            Assert.AreEqual("377", result[1]);
            Assert.AreEqual("3.27", result[2]);
            Assert.AreEqual("4.06", result[3]);
            Assert.AreEqual("490", result[4]);
            Assert.AreEqual("4.32", result[5]);
            Assert.AreEqual("5.24", result[6]);
            Assert.AreEqual("f48de305-0224-4121-970d-9ea191576742", result[7]);
            Assert.AreEqual("BD", result[8]);

            // Only PRE + BC POST test
            filePath = testHelper.CopyFileToTest("test_post_BC", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual("Diagnosis PRE + POST BC", result[0]);
            Assert.AreEqual("455", result[1]);
            Assert.AreEqual("4.24", result[2]);
            Assert.AreEqual("5.00", result[3]);
            Assert.AreEqual("409", result[4]);
            Assert.AreEqual("3.67", result[5]);
            Assert.AreEqual("4.65", result[6]);
            Assert.AreEqual("5f117269-4e80-41bc-bd8a-6a3b044c654f", result[7]);
            Assert.AreEqual("BC", result[8]);

            // Only Multi tests type
            filePath = testHelper.CopyFileToTest("multi_test_type", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual("Multi test types", result[0]);
            Assert.AreEqual("878", result[1]);
            Assert.AreEqual("4.80", result[2]);
            Assert.AreEqual("6.07", result[3]);
            Assert.AreEqual("", result[4]);
            Assert.AreEqual("", result[5]);
            Assert.AreEqual("", result[6]);
            Assert.AreEqual("09986630-0d12-4928-a72b-79f8b1285d0a", result[7]);
            Assert.AreEqual("c9706196-d993-4b14-99d6-8c48f7ccd693", result[8]);
            Assert.AreEqual("49274dab-bf35-48ce-b2b2-a0b3304d7b35", result[9]);
            Assert.AreEqual("2d14048d-a077-401a-85ea-fd2af64b9a10", result[10]);
            Assert.AreEqual("PRE_SUMMARY", result[11]);
        }

        [TestMethod]
        public void GeneratePDFTest()
        {
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            string[] prmValues = { "001", "BP", "Omnia", "Bridge", "19800101", "Male", "Caucasian", "180", "80" };
            var archive = new Archive(prmNames, prmValues);
            List<string> recordIDs = new List<string> { "09986630-0d12-4928-a72b-79f8b1285d0a",
                "c9706196-d993-4b14-99d6-8c48f7ccd693", "49274dab-bf35-48ce-b2b2-a0b3304d7b35" };

            archive.GeneratePDF("PatientName", "01011980", recordIDs);

            var filePath = Path.Combine(testHelper.TempFileFolder(), "export_report.in");
            IEnumerable<XElement> elements = XDocument.Load(filePath).Elements("OmniaXB").Elements("Archive").Elements("ExportReport").Elements();
            List<string> keys = new List<string> { };
            List<string> values = new List<string> { };

            foreach(XElement element in elements)
            {
                keys.Add(element.Name.ToString());
                values.Add(element.Value);
            }

            Assert.AreEqual("Filename", keys[0]);
            Assert.AreEqual(Path.Combine(testHelper.PdfFileFolder(), DateTime.Today.ToString("dd-MM-yyy") + " - PatientName (01011980).pdf"), values[0]);
            Assert.AreEqual("RecordID", keys[1]);
            Assert.AreEqual("09986630-0d12-4928-a72b-79f8b1285d0a", values[1]);
            Assert.AreEqual("ID", keys[2]);
            Assert.AreEqual("c9706196-d993-4b14-99d6-8c48f7ccd693", values[2]);
            Assert.AreEqual("ID1", keys[3]);
            Assert.AreEqual("49274dab-bf35-48ce-b2b2-a0b3304d7b35", values[3]);

            testHelper.DeleteTempFileIN("export_report");
        }
    }
}
