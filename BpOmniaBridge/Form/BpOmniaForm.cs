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
using System.Xml.Linq;
using BPS;
using BpOmniaBridge.CommandList;
using BpOmniaBridge.Utility;

namespace BpOmniaBridge
{
    public partial class BpOmniaForm : Form
    {
       
        public BpOmniaForm()
        {
            InitializeComponent();
            Utility.Utility.CreateParamsFolder();
            Utility.Utility.CreateLogFile();
        }

        private void BpOmniaForm_Load(object sender, EventArgs e)
        {

        }

        private void statusBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Utility.Utility.Log("Close bridge");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CommandList.CommandList().Logout();
        }
    }
}
