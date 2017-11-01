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
        private FileSystemEventHandler Event;

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

            BpOmniaForm form = new BpOmniaForm { };
            form.SetTestEnv(BP.CurrentTest);

            Assert.IsTrue(testHelper.CheckFile("login"));
            testHelper.DeleteTempFileIN("login");

            // simulate Omnia reply
            testHelper.CopyFileToTest("login_ack", "login");
            // simulate FileSystemWatcher
            Event += new FileSystemEventHandler(form.Read);

        }
    }
}
