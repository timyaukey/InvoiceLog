using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PdfSharp.Pdf;

namespace InvoiceLog
{
    public partial class InvoiceMoreForm : Form
    {
        private string mVendorName;
        private string mInvoiceNumber;

        public PdfDocument NewPDF;

        public InvoiceMoreForm()
        {
            InitializeComponent();
        }

        public void ShowDialog(string vendorName, string invoiceNumber)
        {
            mVendorName = vendorName;
            mInvoiceNumber = invoiceNumber;
            lblVendorNameText.Text = mVendorName;
            lblInvoiceNumberText.Text = mInvoiceNumber;
            NewPDF = null;
            this.ShowDialog();
        }

        private void btnScanInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mVendorName))
            {
                MessageBox.Show("Vendor name is required to scan an invoice.");
                return;
            }
            if (string.IsNullOrEmpty(mInvoiceNumber))
            {
                MessageBox.Show("Invoice number is required to scan an invoice.");
                return;
            }
            
            Vendor vendor = Utilities.FindVendor(mVendorName);
            if (vendor == null)
            {
                if (MessageBox.Show("Vendor name not recognized. Are you sure you " +
                    "want to scan under this name?", "Confirm", MessageBoxButtons.OKCancel) !=
                    DialogResult.OK)
                    return;
            }
            else
            {
                if (!vendor.ScanInvoices)
                {
                    MessageBox.Show("Invoices do not need to be scanned for this vendor.");
                    return;
                }
            }

            string attachmentFile = Utilities.MakeAttachmentFileName(mVendorName, mInvoiceNumber);
            if (File.Exists(attachmentFile))
            {
                DialogResult result = MessageBox.Show("Invoice already has an attachment. " +
                    "Do you want to replace it?", "Attachment Exists", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                    return;
            }

            using (Saraff.UI.MultiPage frm = new Saraff.UI.MultiPage())
            {
                frm.ShowDialog();
                if (frm.Result != null && frm.Result.Images.Count > 0)
                {
                    using (frm.Result)
                    {
                        ImagesToPDF itop = new ImagesToPDF(frm.Result);
                        try
                        {
                            NewPDF = itop.Create();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Problem creating PDF: " + ex.Message);
                        }
                    }
                    MessageBox.Show("Scan completed.");
                }
                else
                {
                    MessageBox.Show("Scan canceled.");
                }
            }
        }

        private void btnViewInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mVendorName))
            {
                MessageBox.Show("Vendor name is required to view an invoice.");
                return;
            }
            if (string.IsNullOrEmpty(mInvoiceNumber))
            {
                MessageBox.Show("Invoice number is required to view an invoice.");
                return;
            }
            string attachmentFile = Utilities.MakeAttachmentFileName(mVendorName, mInvoiceNumber);
            if (!File.Exists(attachmentFile))
            {
                MessageBox.Show("Either there is no scanned invoice or it hasn't been saved yet.");
                return;
            }
            System.Diagnostics.Process.Start(attachmentFile);
        }

        private void btnViewSample_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mVendorName))
            {
                MessageBox.Show("Vendor name is required for hints.");
                return;
            }

            string vendorPath = Path.Combine(Utilities.SamplePath, Utilities.MakeLegalForPath(mVendorName));
            if (!Directory.Exists(vendorPath))
            {
                MessageBox.Show("This vendor has no invoice hints (sample folder not found).");
                return;
            }
            string sampleFile = Path.Combine(vendorPath, "Invoice.jpg");
            if (!File.Exists(sampleFile))
            {
                MessageBox.Show("This vendor has no invoice hints (sample file not found).");
                return;
            }
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(sampleFile);
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);
        }
    }
}
