using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class FormTest
    {

        TestHelper testHelper = new TestHelper();
        [TestMethod]
        public void FullSuccessWorkFlowTest()
        {
            Hashtable patient = new Hashtable
            {
                { "id", 1 },
                { "name", "Firstname" },
                { "lastname", "Lastname" },
                { "dob", DateTime.Parse("01/01/1980") },
                { "gender", "Male" },
                { "height", 180 },
                { "weight", 80 },
                { "ethnicity", "Australian" }
            };

            Hashtable user = new Hashtable
            {
                { "id", 1 },
                { "name", "User" }
            };


            SimulateBP BP = new SimulateBP(patient, user);

            BpOmniaForm form = new BpOmniaForm(true);
            form.SetTestEnv(BP.CurrentTest);

            Assert.IsTrue(testHelper.CheckFile("login"));
            Assert.AreEqual("Login", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("login");

            // simulate Omnia reply
            testHelper.CopyFileToTest("login_ack", "login");
            // simulate FileSystemWatcher
            form.Read();
            
            Assert.IsTrue(testHelper.CheckFile("select_create_subj"));
            Assert.AreEqual("Subject", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("select_create_subj");

            // simulate Omnia reply
            testHelper.CopyFileToTest("select_create_subj");
            // simulate FileSystemWatcher
            form.Read();

            Assert.IsTrue(testHelper.CheckFile("list_visit_card"));
            Assert.AreEqual("GetVisitCardList", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("list_visit_card");

            // simulate Omnia reply
            testHelper.CopyFileToTest("list_visit_card");
            // simulate FileSystemWatcher
            form.Read();

            Assert.IsTrue(testHelper.CheckFile("create_visit_card"));
            Assert.AreEqual("VisitCard", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("create_visit_card");

            // simulate Omnia reply
            testHelper.CopyFileToTest("create_visit_card");
            // simulate FileSystemWatcher
            form.Read();

            Assert.IsTrue(testHelper.CheckFile("select_visit_card"));
            Assert.AreEqual("SelectVisitCard", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("select_visit_card");

            // simulate Omnia reply
            testHelper.CopyFileToTest("select_visit_card");
            // simulate FileSystemWatcher
            form.Read();

            Assert.AreEqual("ShowResultAndWait", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("select_visit_card");

            form.ExportData();

            // simulate Omnia reply
            testHelper.CopyFileToTest("export_data");
            testHelper.CopyFileToTest("test_only_PRE", "tests", ".xml");
            // simulate FileSystemWatcher
            form.Read();

            Assert.IsTrue(testHelper.CheckFile("export_data"));
            Assert.AreEqual("ExportData", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("export_data");

            // simulate Omnia reply
            testHelper.CopyFileToTest("export_data");
            testHelper.CopyFileToTest("test_only_PRE", "tests", ".xml");
            // simulate FileSystemWatcher
            form.Read();

            Assert.IsTrue(testHelper.CheckFile("export_report"));
            Assert.AreEqual("ReadDataAndGeneratePDF", form.States[form.currentStateIndex]);
            testHelper.DeleteTempFileIN("export_report");

            Assert.AreEqual("Diagnosis only PRE with FVC/FEV1% withing the normal range", form.results[0]);
            Assert.AreEqual("442", form.results[1]);
            Assert.AreEqual("3.60", form.results[2]);
            Assert.AreEqual("4.66", form.results[3]);
            Assert.AreEqual("", form.results[4]);
            Assert.AreEqual("", form.results[5]);
            Assert.AreEqual("", form.results[6]);
            Assert.AreEqual("24e48dba-f3ae-4650-8f8a-7c98502a1678", form.results[7]);
            Assert.AreEqual("PRE", form.results[8]);
        }
    }
}
