using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using BPS;
using BpOmniaBridge.CommandList;
using BpOmniaBridge.Utility;
using System.Runtime.InteropServices;

namespace BpOmniaBridge
{
    public partial class BpOmniaForm : Form
    {
        public BPS.BPDevice app;
        private IPatient patient;
        private ISpiro spiro;
        private ITest currentTest;
       
        public BpOmniaForm()
        {
            InitializeComponent();
            Utility.Utility.CreateUtilityFolders();
            Utility.Utility.CreateLogFile();
            bool omnia = Utility.Utility.RunOmnia();
            if (omnia)
                try
                {
                    Type type = Type.GetTypeFromProgID("BPS.BPDevice");
                    app = (BPS.BPDevice)Activator.CreateInstance(type);
                }
                catch (COMException)
                {
                    try
                    {
                        Type type = Type.GetTypeFromProgID("BPS.BPDevice");
                        app = new BPS.BPDevice();// (BPDevice.Device.BPDevice)Activator.CreateInstance(type);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + Environment.NewLine + ex.InnerException.Message + " BPDeviceStart");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace, "Bp Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (app != null)
                    {
                        Utility.Utility.Log("BP instance created");
                        app.eOnNewTest += new BPS.BPDevice.DeviceEventHandler(app_eOnNewTest);
                    }
                    
            else
                closeApp();


        }

        private void BpOmniaForm_Load(object sender, EventArgs e)
        {

        }

        private void statusBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            closeApp();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] names = new string[] { "Firstname", "Lastname" };
            string[] values = new string[] { "Ema", "Maglio" };
            new CommandList.CommandList().Logout();
        }

        #region Method
        public void closeApp()
        {
            this.Close();
            app.OnTestComplete();
            Utility.Utility.Log("Close bridge");
        }

        private void app_eOnNewTest()
        {
            currentTest = app.CurrentTest;
            Utility.Utility.Log(currentTest.ToString());
            //Invoke(new Action(delegate () { PopulateUser(); }));
            Invoke(new Action(delegate () { PopulatePatient(); }));
        }

        private void PopulatePatient()
        {
            Utility.Utility.Log("PopulatePatient");
            patient = currentTest.Patient;
            string[] prmNames = { "Firstname", "LastName", "DayOfBirth", "Gender", "EthnicGroup" };
            var name = patient.Name.First;
            var lastname = patient.Name.Last;
        }

        #endregion
    }
}
