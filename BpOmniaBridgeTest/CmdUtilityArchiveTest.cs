using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;
using System.Xml.Linq;

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

            // simulate OMNIA's reply
            testHelper.CopyFileToTest("select_create_subj");
            // create subject
            var subjectID = archive.CreateSubject();

            // delete *.in file
            testHelper.DeleteTempFileIN("select_create_subj");

            // test read correct ID from out file
            Assert.AreEqual("12000000-3000-4000-5000-600000000001", subjectID);

            archive.SetRecordID(subjectID);
            // simulate OMNIA's reply
            testHelper.CopyFileToTest("list_visit_card");

            // simulate visit card already present
            var visitID = archive.TodayVisitCard("20131029");
            // test read correct ID from out file
            Assert.AreNotEqual("ID_00000000-0000-0000-0000-000000000002", visitID);
            Assert.AreEqual("00000000-0000-0000-0000-000000000002", visitID);

            // delete *.in file
            testHelper.DeleteTempFileIN("list_visit_card");

            // simulate OMNIA's reply
            testHelper.CopyFileToTest("list_visit_card");
            testHelper.CopyFileToTest("create_visit_card");
            // new visit card for today
            visitID = archive.TodayVisitCard(DateTime.Today.ToString("yyyyMMdd"));
            // test read correct ID from out file
            Assert.AreEqual("00000000-0000-0000-0000-000000000001", visitID);

            // delete *.in file
            testHelper.DeleteTempFileIN("create_visit_card");
            testHelper.DeleteTempFileIN("list_visit_card");
        }

        [TestMethod]
        public void DataElaborationTest()
        {

        }
    }
}
