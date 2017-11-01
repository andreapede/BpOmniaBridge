using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BPS;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Reflection;

namespace BpOmniaBridge
{
    public partial class BpOmniaForm : Form
    {
        public BPDevice app;
        private dynamic patient;
        private ISpiro spiro;
        private dynamic currentTest;
        public string subjectID;
        public string visitID;
        public Archive archive;
        // List of results: Diasgnosis/PEFR/FEV1/FVC/PEFRPOST/FEV1POST/FVCPOST/testID
        public List<string> results = new List<string> { };
        public bool pdfCreated = false;
        public Command currentCommand;
        public List<string> States = new List<string> { "Login", "Subject", "GetVisitCardList", "VisitCard", "SelectVisitCard", "ShowResultAndWait", "ExportData", "ReadDataAndGeneratePDF", "SaveInBP"};
        public int errorCode = -1;
        public int currentStateIndex;
        public Dictionary<string, List<string>> currentResult;
        public string[] patientDetails;
        FileSystemWatcher watcher;
        bool test = false;

        public BpOmniaForm()
        {
            InitializeComponent();
            //IntPtr myHandle = this.Handle; NOT SURE ABOUT THIS
            Utility.Initialize();
            currentStateIndex = 0;

            if (!test)
            {
                try
                {
                    Type type = Type.GetTypeFromProgID("BPS.BPDevice");
                    app = (BPDevice)Activator.CreateInstance(type);
                }
                catch (COMException)
                {
                    try
                    {
                        Type type = Type.GetTypeFromProgID("BPS.BPDevice");
                        app = new BPDevice();// (BPDevice.Device.BPDevice)Activator.CreateInstance(type);
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
            }

            if (app != null)
            {
                WatchForFileDotOut();
                app.eOnNewTest += new BPDevice.DeviceEventHandler(app_eOnNewTest);
                StatusBar("After performing the tests in OMNIA, press Save Tests button");
            }
        }



        #region Method
        // Folder watcher to create the reading event 
        private void WatchForFileDotOut()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var folderPath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");

            watcher = new FileSystemWatcher();
            watcher.Path = folderPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.Filter = "*.out*";
            watcher.Created += new FileSystemEventHandler(Read);
            watcher.EnableRaisingEvents = true;
        }

        // read out file and invoke correct method
        public void Read(object source, FileSystemEventArgs e)
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var folderPath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");

            currentResult = currentCommand.Receive();
            if (currentResult["values"][0] != "NAK" && currentStateIndex != 8)
            {
                currentStateIndex += 1;
                Utility.Log("Trigger => " + States[currentStateIndex]);
                var nextState = States[currentStateIndex];
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
            if(errorCode != -1)
            {   
                Utility.Log(String.Format("Error code {0}: {1}", errorCode, Utility.ErrorList(errorCode)));
                MessageBox.Show(String.Format("Error code {0}: {1}", errorCode, Utility.ErrorList(errorCode)), "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            PatientPreliminaryCheck();
        }

        private void PatientPreliminaryCheck()
        {
            if(patient.DOB.ToString("yyyyMMdd") == "00010101" || patient.Gender.ToString() == "0")
            {
                MessageBox.Show("Date of birth and gender can't be blank, please update the details and try again",
                    "DOB and Gender blank issue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                closeApp();
                return;
            }
            Login();
        }

        public void Login()
        {
            currentCommand = new CommandList().Login("ocp", "bp");
        }

        public void Subject()
        {
            string[] prmNames = { "ID", "FirstName", "MiddleName", "LastName", "DayOfBirth", "Gender", "EthnicGroup", "Height", "Weight" };
            try
            {
                string id = patient.InternalId.ToString();
                string name = patient.Name.First;
                string middlename = patient.Name.Middle;
                string lastname = patient.Name.Last;
                string dob = patient.DOB.ToString("yyyyMMdd");

                // handle different in gender lists
                string bpGender = patient.Gender.ToString();
                if (bpGender == "Unknown") { bpGender = "Other"; };
                string gender = bpGender;
                string ethnicity = Utility.MatchEthnicity(patient.Ethnicity.ToString());
                string height = patient.Height.ToString();
                string weight = patient.Weight.ToString();
                string[] prmValues = { id, name, middlename, lastname, dob, gender, ethnicity, height, weight };
                patientDetails = prmValues;

                //check if user is preset in DB
                archive = new Archive(prmNames, prmValues);
                currentCommand = archive.CreateSubject();
            }
            catch(Exception ex)
            {
                errorCode = 1;
                closeApp();
            }
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
            this.TopMost = true;
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
            if(results[2] == "no_tests")
            {
                MessageBox.Show("An error occured in exporting the tests xml file.\n" +
                    "Please make sure to perform at least 1 test before pressing the Save Tests button", "Exporting test error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                currentStateIndex = 6;
                ShowResultAndWait();
            }
            else
            {
                List<string> recordIDs = new List<string> { };
                recordIDs = results.GetRange(7, results.Count - 8);
                currentCommand = archive.GeneratePDF(patient.Name.FullName.ToString(), patient.DOB.ToString("dd-MM-yyyy"), recordIDs);
            }
        }
        
        public void SaveInBP()
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

        #region TestMethods
        public void SetTestEnv(dynamic monkTest)
        {
            test = true;
            app = new BPDevice();
            currentTest =  monkTest;
            patient = currentTest.Patient;
            PatientPreliminaryCheck();
        }

        public ISpiro GetResults()
        {
            return spiro;
        }
        #endregion
    }
}
