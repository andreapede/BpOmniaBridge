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
        public ReadCommands(string name, string system, string cmd, bool type)
        {
            FileName = name;
            CmdSystem = system;
            Cmd = cmd;
            JustResult = type;
            CheckFileExists();
        }

        //Properties
        private string FileName;
        private string CmdSystem;
        private string Cmd;
        public bool FileExist;
        public bool JustResult;
        public string Result;

        #region Methods
        public void Read()
        {
            // call this when expect just Result with ACK or NACK
            if (JustResult && FileExist)
            {
                var filePath = Path.Combine(folderPath, FileName + ".out");
                var resElement = XDocument.Load(filePath).Elements("OmniaXB").Elements(CmdSystem).Elements(Cmd).Elements("Result").First();
                Result = resElement.Value;
                DeleteFile();
                Utility.Utility.Log(" Read => done");
            }
                
        }
                
        private void CheckFileExists()
        {
            Utility.Utility.Log(" Read => start");
            // Set timeout to 20 seconds
            // ASK: is this ok?
            var timeout = DateTime.Now.Add(TimeSpan.FromSeconds(20));
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
        #endregion
    }
}
