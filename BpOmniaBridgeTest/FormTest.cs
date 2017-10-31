using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using BPS;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
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
                { "gender", "male" },
                { "height", 180 },
                { "weight", 80 },
                { "ethnicity", "Unknown" }
            };

            Hashtable user = new Hashtable
            {
                { "id", 1 },
                { "name", "User" }
            };

            SimulateBP BP = new SimulateBP(patient, user);
            
            
            BpOmniaForm form = new BpOmniaForm { };
            form.SetTestEnv(BP.CurrentTest);

            testHelper.CopyFileToTest("login_ack", "login");

            Event += new FileSystemEventHandler(form.Read);

        }
    }
}
