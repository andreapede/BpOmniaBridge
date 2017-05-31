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
using BPS;

namespace BpOmniaBridge
{
    public partial class BpOmniaForm : Form
    {
        public BpOmniaForm()
        {
            InitializeComponent();
            Create_log_file();

        }

        private static void Create_log_file()
        {
            var cmnDocPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            var fullpath = Path.Combine(cmnDocPath, "BpOmniaBridge");
            Directory.CreateDirectory(fullpath);
            var filepath = Path.Combine(fullpath, "log.txt");
            using (StreamWriter w = File.AppendText(filepath))
            {
                Log("Bridge started", w);
            }
        }

        public static void Log(string message, TextWriter w)
        {
            w.Write("\r\n Log Entry: ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
            w.WriteLine(" action: {0}", message);
            w.WriteLine("-------------------------------");
        }

        private void BpOmniaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
