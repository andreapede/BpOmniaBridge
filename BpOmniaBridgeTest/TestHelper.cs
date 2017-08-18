using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BpOmniaBridgeTest
{
    class TestHelper
    {
        public string CopyFileToTest(string fileName)
        {
            var currentFolder = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            string fileToMove = Path.Combine(currentFolder, "toTest", fileName + ".out");
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var destFileName = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", fileName + ".out");

            File.Copy(fileToMove, destFileName, true);

            return destFileName;
        }

        public void DeleteTempFileIN(string fileName)
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var filePath = Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files", fileName + ".in");
            File.Delete(filePath);
        }
    }
}
