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
            command.AddCommands("TestSystem", "TestCmd", new string[] { "Param0", "Param1" }, new string[] { "Value0", "Value1" });
            command.Save();

            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", "with_params.in");

            Assert.AreEqual(true, File.Exists(filePath));

            var elements = XDocument.Load(filePath).Element("OmniaXB").Element("TestSystem").Element("TestCmd").Elements();
            int num_params = 0;

            foreach(XElement element in elements)
            {
                Assert.AreEqual("Param" + num_params, element.Name);
                Assert.AreEqual("Value" + num_params, element.Value);
                num_params += 1;
            }

            Assert.AreEqual(2, num_params);

            File.Delete(filePath);
        }
    }
}
