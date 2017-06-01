using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BpOmniaBridge.CommandUtility
{
    public class WriteCommand
    {
        private static string cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
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
            Utility.Utility.Log("CMD => "+FileName);
        }
    }
}
