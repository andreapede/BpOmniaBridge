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

        public string TempFileFolder()
        {
            var cmnDocPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            return Path.Combine(cmnDocPath, "BpOmniaBridge", "temp_files");
        }

        public string CopyFileToTest(string fileFrom, string fileTo = "same", string ext = ".out")
        {
            if (fileTo == "same")
                fileTo = fileFrom;
            var currentFolder = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "");
            string fileToMove = Path.Combine(currentFolder, "toTest", fileFrom + ext);
            var destFileName = Path.Combine(TempFileFolder(), fileTo + ext);

            File.Copy(fileToMove, destFileName, true);

            return destFileName;
        }

        public void DeleteTempFileIN(string fileName, string ext = ".in")
        {
            var filePath = Path.Combine(TempFileFolder(), fileName + ext);
            File.Delete(filePath);
        }

    }
}
