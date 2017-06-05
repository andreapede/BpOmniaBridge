using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using System.Windows.Forms;

namespace BpOmniaBridge.CommandUtility
{
    public class WriteCommand
    {
        private static string cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        private static string folderPath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
        private XDocument command;
        private string fileName;

        public WriteCommand(string name)
        {
            command = new XDocument(
                new XElement("OmniaXB")
                );
            fileName = name;
        }

        public void BasicCommand(string system, string cmd)
        {
            command.Element("OmniaXB").Add(new XElement(system));
            command.Element("OmniaXB").Element(system).Add(new XElement(cmd));
        }

        public void AddCommands(string system, string cmd, string[] paramsNames, string[] paramsValues)
        {
            BasicCommand(system, cmd);

            int index = 0;
            foreach (string prm in paramsNames)
            {
                command.Element("OmniaXB").Element(system).Element(cmd).Add(new XElement(prm, paramsValues[index]));
                index += 1;
            }
        }

        public void Save()
        {
            //TODO: make this smarter!!! XDocument creates the first line with xml details which are not required 
            var filePath = Path.Combine(folderPath, fileName);
            command.Save(filePath);
            string[] lines = File.ReadAllLines(filePath);
            lines = lines.Skip(1).ToArray();
            File.WriteAllLines(filePath, lines);
        }

    }

    public class Command
    {   
        public Command(string name, string sys, string cmmd)
        {
            FileName = name;
            System = sys;
            Cmd = cmmd;
            ParNames = new string[] { };
            ParValues = new string[] { };
        }

        public void AddParams(string[] names, string[] values)
        {
            ParNames = names;
            ParValues = values;
        }

        //Properties
        private string FileName { get; set; }
        private string System { get; set; }
        private string Cmd { get; set; }
        private string[] ParNames { get; set; }
        private string[] ParValues { get; set; }

        public void Send()
        {
            var command = new WriteCommand(FileName + ".in");
            command.AddCommands(System, Cmd, ParNames, ParValues);
            command.Save();
            Utility.Utility.Log("CMD => " + FileName);
        }
    }

    public class ReadCommands
    {
        private static string cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
        private static string folderPath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");

        //Initializer name of the file and type of result
        public ReadCommands(string name, string system, string cmd)
        {
            FileName = name;
            CmdSystem = system;
            Cmd = cmd;
            CheckFileExists();
        }

        //Properties
        private string FileName;
        private string CmdSystem;
        private string Cmd;
        public bool FileExist;
        public List<string> ResultValues;
        public List<string> ResultKeys;

        #region Methods
        public void Read()
        {
            ResultValues = new List<string> { };
            ResultKeys = new List<string> { };
            if (FileExist)
            {
                var filePath = Path.Combine(folderPath, FileName + ".out");
                //get all the elemnts under the command key
                IEnumerable<XElement> elements = XDocument.Load(filePath).Elements("OmniaXB").Elements(CmdSystem).Elements(Cmd);
                foreach( XElement element in elements)
                {
                    ResultKeys.Add(element.Name.ToString());
                    ResultValues.Add(element.Value);
                }
                DeleteFile();
                Utility.Utility.Log("Read => done");
            }
            else
            { ResultValues.Add("NACK"); }
                
        }
                
        private void CheckFileExists()
        {
            Utility.Utility.Log("Read => start");
            // Set timeout to 30 seconds
            // ASK: is this ok?
            var timeout = DateTime.Now.Add(TimeSpan.FromSeconds(30));
            var fullPath = Path.Combine(folderPath, FileName + ".out");
            while (!File.Exists(fullPath))
            {
                if (DateTime.Now > timeout)
                {
                    Utility.Utility.Log("timeout => " + FileName + ".out has not been created within 20 secs");
                    FileExist = false;
                    MessageBox.Show("Omnia is not answering. Check Bridge log file", "Communication Issue", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DeleteFile(".in");
                    break;
                }
                Thread.Sleep(200);
            }
            FileExist = File.Exists(fullPath);
            Read();
        }

        private void DeleteFile(string in_out = ".out")
        {
            var filePath = Path.Combine(folderPath, FileName + in_out);
            File.Delete(filePath);
        }
    }
    #endregion

    #region Archive Methods

    public class Archive
    {
        //Initializer
        public Archive(string[] keys, string[] values)
        {
            keysArray = keys;
            valuesArrau = values;

        }

        //Properties
        private string[] keysArray;
        private string[] valuesArrau;

        public string TodayVisitCard()
        {
            List<object> result = new CommandList.CommandList().GetSubjectVisitList(keysArray, valuesArrau);
            List<object> visitListKeys = new List<object> { };
            List<object> visitListValues = new List<object> { };

            if (visitList.First() != "NACK")
            {
                if (Int32.Parse(visitList.First()) > 0)
                {
                    bool found = false;
                    //existing visit card - check if the one of today exist already
                    visitList.RemoveAt(0);
                    foreach (string visit in visitList)
                    {
                        if (visit == DateTime.Today.ToString("yyyyMMdd"))
                        {
                            found = true;
                            break;
                        }
                    }


                }
            }

        }
    }
    #endregion
}
