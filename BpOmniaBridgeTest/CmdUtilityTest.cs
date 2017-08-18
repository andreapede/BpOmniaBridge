using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BpOmniaBridge;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BpOmniaBridgeTest
{
    [TestClass]
    public class CmdUtilityTest
    {
        [TestMethod]
        public void WriteCommandNoParamsTest()
        {   
            var command = new WriteCommand("no_params.in");
            command.AddCommands("TestSystem", "TestCmd", new string[] {}, new string[] { });
            command.Save();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "no_params.in");

            Assert.AreEqual(true, File.Exists(filePath));

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
            var command = new WriteCommand("with_params.in");
            Guid my_guid = new Guid("11A4801F-7977-4D3E-8D1E-6CA0BE52E604");

            command.AddCommands("TestSystem", "TestCmd", new string[] { "Param0", "Param1", "Param2" }, new string[] { "Value0", "Value1", my_guid.ToString() }, true, 2);
            command.Save();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "with_params.in");

            Assert.AreEqual(true, File.Exists(filePath));

            var elements = XDocument.Load(filePath).Element("OmniaXB").Element("TestSystem").Element("TestCmd").Elements();
            int num_params = 0;

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
    }
}
