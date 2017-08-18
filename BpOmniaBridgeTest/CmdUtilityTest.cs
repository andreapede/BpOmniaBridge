using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Threading;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class CmdUtilityTest
    {
        [TestMethod]
        public void WriteCommandNoParamsTest()
        {   
            // test basic command for WriteCommand
            var command = new WriteCommand("no_params.in");
            command.AddCommands("TestSystem", "TestCmd", new string[] {}, new string[] { });
            command.Save();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "no_params.in");

            Assert.AreEqual(true, File.Exists(filePath));
            
            // test if maine root is OmniaXB
            // test system and command elements
            var root = XDocument.Load(filePath).Element("OmniaXB");
            var system = XDocument.Load(filePath).Element("OmniaXB").Element("TestSystem");
            var cmd = XDocument.Load(filePath).Element("OmniaXB").Element("TestSystem").Element("TestCmd");
            Assert.AreEqual("OmniaXB", root.Name);
            Assert.AreEqual("TestSystem", system.Name);
            Assert.AreEqual("TestCmd", cmd.Name);

            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteCommandWithParamsTest()
        {
            // test WriteCommand with parameters with Guid as well
            var command = new WriteCommand("with_params.in");
            Guid my_guid = new Guid("11A4801F-7977-4D3E-8D1E-6CA0BE52E604");

            command.AddCommands("TestSystem", "TestCmd", new string[] { "Param0", "Param1", "Param2" }, new string[] { "Value0", "Value1", my_guid.ToString() }, true, 2);
            command.Save();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "with_params.in");

            Assert.AreEqual(true, File.Exists(filePath));

            var elements = XDocument.Load(filePath).Element("OmniaXB").Element("TestSystem").Element("TestCmd").Elements();
            int num_params = 0;

            // test each parameter has the correct name and value
            // Important to test the GUID otherwise you get NACK from OMNIA
            foreach(XElement element in elements)
            {
                if(num_params == 2)
                {
                    Assert.AreEqual("Param" + num_params, element.Name);
                    Assert.AreEqual(new Guid(element.Value), my_guid);
                }
                else
                {
                    Assert.AreEqual("Param" + num_params, element.Name);
                    Assert.AreEqual("Value" + num_params, element.Value);
                }
                               
                num_params += 1;
            }

            Assert.AreEqual(3, num_params);

            File.Delete(filePath);
        }

        [TestMethod]
        public void CommandTest()
        {
            // same test of WriteCommandWithParams but using the higer lever class Command
            Guid my_guid = new Guid("11A4801F-7977-4D3E-8D1E-6CA0BE52E604");
            string[] names = new string[] { "Param0", "Param1", "Param2" };
            string[] values = new string[] { "Value0", "Value1", my_guid.ToString() };


            var cmd = new Command("use_command", "system", "command", true, 2);
            cmd.AddParams(names, values);
            cmd.Send();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "use_command.in");

            Assert.AreEqual(true, File.Exists(filePath));

            var elements = XDocument.Load(filePath).Element("OmniaXB").Element("system").Element("command").Elements();
            int num_params = 0;

            foreach (XElement element in elements)
            {
                if (num_params == 2)
                {
                    Assert.AreEqual("Param" + num_params, element.Name);
                    Assert.AreEqual(new Guid(element.Value), my_guid);
                }
                else
                {
                    Assert.AreEqual("Param" + num_params, element.Name);
                    Assert.AreEqual("Value" + num_params, element.Value);
                }

                num_params += 1;
            }

            Assert.AreEqual(3, num_params);

            File.Delete(filePath);

            // test log file
            var logFilePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "log.txt");

            var lines = File.ReadAllLines(logFilePath);
            string lastline = lines.GetValue(lines.Length - 1).ToString();
            Assert.AreEqual(true, lastline.Contains(DateTime.Today.ToLongDateString()), "Log not working");
            Assert.AreEqual(true, lastline.Contains("CMD => use_command"), "Log not working");
        }

        [TestMethod]
        public void ReadCommandTest()
        {
            // read basic command with ACK
            var filePath = new TestHelper().CopyFileToTest("login_ack");

            var read = new ReadCommands("login_ack", "System", "Login");
            Assert.AreEqual("ACK", read.ResultValues[0]);
            // test file is deleted after been read
            Assert.AreEqual(false, File.Exists(filePath));

            // read basic command with NAK
            new TestHelper().CopyFileToTest("login_nak");

            read = new ReadCommands("login_nak", "System", "Login");
            Assert.AreEqual("NAK", read.ResultValues[0]);

            // read complex command with list of params
            new TestHelper().CopyFileToTest("list_visit_card");

            read = new ReadCommands("list_visit_card", "Archive", "GetSubjectVisitList");
            // test keys
            int index = 0;
            foreach(string key in read.ResultKeys)
            {   
                if(index == 0)
                {
                    Assert.AreEqual("NumVisits", key);
                }else
                {
                    Assert.AreEqual("ID_00000000-0000-0000-0000-00000000000" + index.ToString(), key);
                }
                index += 1;
            }
            // test values
            index = 0;
            int firstVisit = 20131028;
            foreach (string value in read.ResultValues)
            {
                if (index == 0)
                {
                    Assert.AreEqual("4", value);
                }
                else
                {
                    Assert.AreEqual(firstVisit.ToString() , value);
                    firstVisit += 1;
                }
                index += 1;
            }

            //test timeout
            // set time to 1 sec
            read = new ReadCommands("login_ack", "System", "Login", 1, true);
            Assert.AreEqual(false, read.FileExist);
            Assert.AreEqual("NAK", read.ResultValues[0]);
        }
    }
}
