using BpOmniaBridge;
using BPS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class BpOmniaFormTest
    {
        TestHelper testHelper = new TestHelper();
        BpOmniaForm form = new BpOmniaForm();
        IPatient patient;
        IName name;
        public IPatient Patient { get => patient; set => patient = value; }
        public IName Name { get => name; set => name = value; }

        [TestMethod]
        public void StateMachineTest()
        {
            //SetNewPatient
            form.SetPatient(testHelper.GetNewPatient());
            
            //Login
            form.Login();
            Assert.AreEqual(0, form.currentStateIndex);
            // Omnia
            testHelper.CopyFileToTest("login_ack", "login");
            testHelper.DeleteTempFileIN("login");
        }
    }
}
