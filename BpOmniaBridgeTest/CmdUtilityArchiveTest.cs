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
        [TestMethod]
        public void ArchiveTest()
        {
            var testHelper = new TestHelper();
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
        public void DataElaborationTest()
        {
            var testHelper = new TestHelper();
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            string[] prmValues = { "001", "BP", "Omnia", "Bridge", "19800101", "Male", "Caucasian", "180", "80" };
            var archive = new Archive(prmNames, prmValues);
            List<string> result = new List<string> { };
            string filePath;

            // Only PRE test
            filePath = testHelper.CopyFileToTest("test_only_PRE", "tests", ".xml");
            result = archive.ReadExportDataFile();
            Assert.AreEqual("Diagnosis only PRE", result[0]);
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
        }   
    }
}
