using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace InvoiceLog
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Invoice Log 1.1.1";
            string exeToRoot = ".";
            if (Environment.CommandLine.Contains("/visualstudio"))
            {
                exeToRoot = "..\\..\\..";
            }
            string exePath = Path.GetDirectoryName(Application.ExecutablePath);
            Utilities.SamplePath = PathFromConfigSetting(exePath, exeToRoot, "SamplePath");
            Utilities.AttachmentPath = PathFromConfigSetting(exePath, exeToRoot, "AttachmentPath");
            Utilities.LoadVendors();
            if (!Directory.Exists(Utilities.AttachmentPath))
            {
                MessageBox.Show("Attachment folder " + Utilities.AttachmentPath + " does not exist.");
                this.Close();
            }
        }

        private string PathFromConfigSetting(string exePath, string exeToRoot, string settingName)
        {
            string settingValue = ConfigurationManager.AppSettings[settingName];
            if (settingValue.Substring(1, 1) == ":")
                return settingValue;
            if (settingValue.StartsWith(@"\\"))
                return settingValue;
            string rootPath = Path.Combine(exePath, exeToRoot);
            string resultPath = Path.Combine(rootPath, settingValue);
            return resultPath;
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            InvoicesForm frm = new InvoicesForm();
            frm.ShowDialog();
        }

        private void btnVendors_Click(object sender, EventArgs e)
        {
            VendorForm frm = new VendorForm();
            frm.ShowDialog();
            Utilities.LoadVendors();
        }
    }
}
