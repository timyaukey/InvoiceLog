using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WillowLib.WinHelper;

namespace InvoiceLog
{
    public class GridVendorSearchEditCell : DataGridViewTextBoxEditingControl
    {
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if ((int)e.KeyChar == 19)
            {
                string vendorName = this.Text;
                Vendor vendor = Utilities.AllVendors.Find(x => x.VendorName.StartsWith(vendorName,
                    StringComparison.InvariantCultureIgnoreCase));
                if (vendor != null)
                {
                    this.Text = vendor.VendorName;
                }
                else
                {
                    MessageBox.Show("No vendor matching that name.");
                }
            }
        }
    }
}
