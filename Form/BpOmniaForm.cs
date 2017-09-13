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
using System.Runtime.InteropServices;
using System.Configuration;
using System.Reflection;

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
        // List of results: Diasgnosis/PEFR/FEV1/FVC/PEFRPOST/FEV1POST/FVCPOST/testID
        private List<string> results = new List<string> { };
        private bool pdfCreated = false;
        private Command currentCommand;
        private List<string> States = new List<string> { "Login", "Subject", "GetVisitCardList", "VisitCard", "NewVisitCard", "SelectVisitCard", "ShowResultAndWait", "ExportData", "ReadDataAndGeneratePDF", "SaveInBP"};
        private int errorCode = 0;
        private int currentStateIndex;
        private Dictionary<string, List<string>> currentResult;
        private string[] patientDetails;

        public BpOmniaForm()
        {
            InitializeComponent();
            //IntPtr myHandle = this.Handle; NOT SURE ABOUT THIS
            Utility.Initialize();
            currentStateIndex = 0;

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
                Utility.Log("Error => " + ex.Message);
                MessageBox.Show(ex.Message + ex.StackTrace, "Bp Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (app != null)
            {
                WatchForFileDotOut();
                app.eOnNewTest += new BPS.BPDevice.DeviceEventHandler(app_eOnNewTest);
                StatusBar("After performing the tests in OMNIA, press Save Tests button");
                Login();
            }
        }



        #region Method
        // Folder watcher to create the reading event 
        private void WatchForFileDotOut()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var folderPath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = folderPath;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.out*";
            watcher.Changed += new FileSystemEventHandler(Read);
            watcher.EnableRaisingEvents = true;
        }

        // read out file and invoke correct method
        private void Read(object source, FileSystemEventArgs e)
        {
            currentResult = currentCommand.Receive();
            if (currentResult["values"][0] != "NACK" && currentStateIndex != 8)
            {
                Utility.Log("Trigger => " + States[currentStateIndex]);
                string nextState = States[currentStateIndex+1];
                var type = this.GetType();
                MethodInfo method = this.GetType().GetMethod(nextState);
                method.Invoke(this, null);
            }
            else
            {
                errorCode = currentStateIndex;
                closeApp();
            }
        }

        public void closeApp()
        {
            if(errorCode != 0)
            {   
                Utility.Log(String.Format("Error code {0}: {1}", errorCode, Utility.ErrorList(errorCode)));
                MessageBox.Show(String.Format("Error code {0}: {1}", errorCode, Utility.ErrorList(errorCode), "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            else
            {
                Utility.Log("Bridge => Closed");
            }
            this.Close();
        }

        private void app_eOnNewTest()
        {
            Utility.Log("BP => new_test");
            currentTest = app.CurrentTest;
            patient = currentTest.Patient;
        }

        public void Login()
        {
            currentCommand = new CommandList().Login("ocp", "bp");
        }

        public void Subject()
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
            var ethnicity = Utility.MatchEthnicity(patient.Ethnicity.ToString());
            var height = patient.Height.ToString();
            var weight = patient.Weight.ToString();
            string[] prmValues = { id, name, middlename, lastname, dob, gender, ethnicity, height, weight };

            patientDetails = prmValues;

            //check if user is preset in DB
            archive = new Archive(prmNames, prmValues);
            currentCommand = archive.CreateSubject();
        }

        public void GetVisitCardList()
        {
            subjectID = currentResult["values"][0];
            archive.SetRecordID(subjectID);
            currentCommand = archive.GetVisitCardList();
        }

        public void VisitCard()
        {
            archive.SetVisitList(currentResult);
            visitID = archive.TodayVisitCard(DateTime.Today.ToString("yyyyMMdd"));

            if (visitID == "not found")
            {
                currentCommand = archive.NewVisitCard();
            }
            else
            {
                //Select visit card
                string[] key = new string[] { "RecordID" };
                string[] value = new string[] { visitID };
                currentCommand = new CommandList().SelectVisit(key, value);
                currentStateIndex += 1; // need to skip the Select Visit card method
            }
        }

        public void SelectVisitCard()
        {
            visitID = currentResult["values"][0];
            string[] key = new string[] { "RecordID" };
            string[] value = new string[] { visitID };
            currentCommand = new CommandList().SelectVisit(key, value);
        }

        public void ShowResultAndWait()
        {
            PopulateSubjectAndVisitCard(patientDetails);
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
            ExportData();
        }

        public void ExportData()
        {
            StatusBar("Exporting data...please wait");
            currentCommand = archive.ExportTests(visitID);
        }

        public void ReadDataAndGeneratePDF()
        {
            StatusBar("Elaborating data and generating PDF file...please wait");
            results = archive.ReadExportDataFile();
            currentCommand = archive.GeneratePDF(patient.Name.FullName.ToString(), patient.DOB.ToString("dd-MM-yyyy"), results.ElementAt(7));
        }

        private void SaveInBP()
        {
            StatusBar("Saving data in BP...please wait");
            Utility.Log("action: Bridge => Saving Test");
            pdfCreated = (currentResult["values"][0] == "ACK");

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
            MessageBox.Show(success ? "Data saved successfully." : "Failed saving data, try to click Save Tests again");

            if (success)
                Utility.Log("action: Bridge => Data successfully saved");
                app.OnTestComplete();
                // delete PDF file if requested
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["SavePdfFiles"]) && pdfCreated)
                {
                    File.Delete(filePath);
                }
                closeApp();
        }

        #endregion

    }
}
