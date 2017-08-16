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
using BpOmniaBridge.CommandUtility;

namespace BpOmniaBridge
{
    public partial class BpOmniaForm : Form
    {
        public BPS.BPDevice app;
        private IPatient patient;
        private ISpiro spiro;
        private ITest currentTest;
        private string subjectID;
        private string visitID;
        private Archive archive;
        // List of results: Diasgnosis/PEF/FEV1/FVC/PEFPOST/FEV1POST/FVCPOST/testID
        private List<string> results = new List<string> { };
        private bool pdfCreated = false;


        public BpOmniaForm()
        {
            InitializeComponent();
            IntPtr myHandle = this.Handle;
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
                app.eOnNewTest += new BPS.BPDevice.DeviceEventHandler(app_eOnNewTest);
                StatusBar("After performing the tests in OMNIA, press Save Tests button");
                
            }
            else
                closeApp();
        }

        private void BpOmniaForm_Load(object sender, EventArgs e)
        {

        }

        

        #region Method
        public void closeApp()
        {
            this.Close();
            Utility.Utility.Log("Bridge => Closed");
        }

        private void app_eOnNewTest()
        {
            Utility.Utility.Log("BP => new_test");
            currentTest = app.CurrentTest;
            patient = currentTest.Patient;
            CreateSelectSubjectAndVistCard();
        }

        private void CreateSelectSubjectAndVistCard()
        {
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            var id = patient.InternalId.ToString();
            var name = patient.Name.First;
            var middlename = patient.Name.Middle;
            var lastname = patient.Name.Last;
            var dob = patient.DOB.ToString("yyyyMMdd");
            
            // handle different in gender lists
            var bpGender = patient.Gender.ToString();
            if (bpGender == "Unknown") { bpGender = "Other"; };
            var gender = bpGender;

            // handle different in ethnicity lists
            var bpEthnicity = patient.Ethnicity.ToString();
            // ASK: how to match the ethnicity list in BP with OMNIA's
            var ethnicity = "Caucasian";
            var height = patient.Height.ToString();
            var weight = patient.Weight.ToString();
            string[] prmValues = {id, name, middlename, lastname, dob, gender, ethnicity, height, weight };

            //check if user is preset in DB
            archive = new Archive(prmNames, prmValues);
            subjectID = archive.CreateSubject();
            bool done = false;
            if (subjectID != "NAK")
            {
                archive.SetRecordID(subjectID);
                visitID = archive.TodayVisitCard();

                if (visitID != "NAK")
                {
                    //Select visit card
                    string[] key = new string[] { "RecordID" };
                    string[] value = new string[] { visitID };
                    done = new CommandList.CommandList().SelectVisit(key, value);
                }
                
            }

            if (done)
            {
                PopulateSubjectAndVisitCard(prmValues);
            }
            else
            {
                Utility.Utility.Log("error: Visit card not created/found");
                MessageBox.Show("Something went wrong during the Subject/Visit selectiong or creating. Please try to run the test again from BP.", "OMNIA Archive Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                closeApp();
            }
        }

        public void PopulateSubjectAndVisitCard(string[] array)
        {
            firstname.Text = array[1];
            middlename.Text = array[2];
            lastname.Text = array[3];
            var dobDateTime = DateTime.ParseExact(array[4], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            dob.Text = dobDateTime.ToString("dd-MM-yyyy");
            gender.Text = array[5];
            ethnicity.Text = array[6];
            height.Text = array[7];
            weight.Text = array[8];
        }

        public void StatusBar(string status)
        {
            statusBar.Text = status;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Invoke(new Action(delegate () { StatusBar("Exporting data...please wait"); }));
            archive.ExportTests(visitID);
            Invoke(new Action(delegate () { StatusBar("Elaborating data...please wait"); }));
            results = archive.ReadExportDataFile();
            Invoke(new Action(delegate () { StatusBar("Generating PDF file...please wait"); }));
            pdfCreated = archive.GeneratePDF(patient.Name.FullName.ToString(), patient.DOB.ToString("dd-MM-yyyy"), results.ElementAt(7));
            StatusBar("Saving data in BP...please wait");
            SaveCurrentTest(sender, e);
        }

        private void SaveCurrentTest(object sender, EventArgs e)
        {
            Utility.Utility.Log("action: Bridge => Saving Test");
            bool success;
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filename = DateTime.Today.ToString("dd-MM-yyy") + " - " + patient.Name.FullName + " (" + patient.DOB.ToString("dd-MM-yyyy") + ")" + ".pdf";
            string filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "pdf_files", filename);
            spiro = new Spiro();
            spiro.Patient = patient;
            spiro.User = currentTest.User;
            if (pdfCreated) { spiro.ImageFile = filePath; }
            if(results.Count < 6)
            {
                success = false;
                goto noResults;
            }

            if (results.ElementAt(1) != "")
                spiro.PEFR = results.ElementAt(1);
            if (results.ElementAt(4) != "")
                spiro.PEFRPost = results.ElementAt(4);
            if (results.ElementAt(2) != "")
                spiro.FEV1 = results.ElementAt(2);
            if (results.ElementAt(5) != "")
                spiro.FEV1Post = results.ElementAt(5);
            if (results.ElementAt(3) != "")
                spiro.FVC = results.ElementAt(3);
            if (results.ElementAt(6) != "")
                spiro.FVCPost = results.ElementAt(6);
            if (results.ElementAt(0) != "")
                spiro.Comment = results.ElementAt(0);
            
            spiro.DateTime = DateTime.Now;
            spiro.Device = "COSMED Spiro";

            success = spiro.SaveTest();
        noResults:;
            MessageBox.Show(success ? "Data saved successfully." : "Failed saving data.");

            if (success)
                Utility.Utility.Log("action: Bridge => Data successfully saved");
                app.OnTestComplete();
        }
        
        #endregion
    }
}
