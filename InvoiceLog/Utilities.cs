using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;

namespace InvoiceLog
{
    public class Utilities
    {
        static public List<Vendor> AllVendors;
        static public List<string> AllVendorNames;
        static public string AttachmentPath;
        static public string SamplePath;

        static public void LoadVendors()
        {
            using (InvoiceDC dc = GetDC())
            {
                if (dc != null)
                {
                    var vendors =
                        from vendor in dc.Vendors
                        orderby vendor.VendorName
                        select vendor;
                    AllVendors = new List<Vendor>();
                    AllVendorNames = new List<string>();
                    foreach (var vendor in vendors)
                    {
                        AllVendors.Add(vendor);
                        AllVendorNames.Add(vendor.VendorName);
                    }
                }
            }
        }

        /// <summary>
        /// Return the matching Vendor object, or null if no such vendor.
        /// </summary>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        static public Vendor FindVendor(string vendorName)
        {
            return AllVendors.Find(ven => ven.VendorName == vendorName);
        }

        static public InvoiceDC GetDC()
        {
            ConnectionStringSettings con = ConfigurationManager.ConnectionStrings["MainCon"];
            if (con == null)
            {
                MessageBox.Show("Unable to find connection string [MainCon]");
                return null;
            }
            return new InvoiceDC(con.ConnectionString);
        }

        public static string MakeAttachmentFileName(string vendorName, string invoiceNumber)
        {
            string folder = AttachmentVendorFolder(vendorName);
            string fullPath = Path.Combine(folder, "Inv" + MakeLegalForPath(invoiceNumber) + ".pdf");
            return fullPath;
        }

        public static string AttachmentVendorFolder(string vendorName)
        {
            return Path.Combine(AttachmentPath, MakeLegalForPath(vendorName));
        }

        public static void EnsureAttachmentVendorFolderExists(string vendorName)
        {
            string vendorFolder = AttachmentVendorFolder(vendorName);
            if (!Directory.Exists(vendorFolder))
                Directory.CreateDirectory(vendorFolder);
        }

        public static string MakeLegalForPath(string input)
        {
            return input.Replace("'", "").Replace(" ", "").Replace(".", "").Replace("&", "").Replace(",", "");
        }
    }
}
