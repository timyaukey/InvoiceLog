using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WillowLib.WinHelper;

namespace InvoiceLog
{
    public partial class VendorForm : Form
    {
        private InvoiceDC mDC;
        private VendorGridHelper mHelper;

        // NOTE: VendorId must have PrimaryKey=True and Auto-Sync=OnInsert in DataContext.

        public VendorForm()
        {
            InitializeComponent();
            mHelper = new VendorGridHelper(this, this.grdVendors);
        }

        private void VendorForm_Load(object sender, EventArgs e)
        {
            mDC = Utilities.GetDC();

            GridBuilder builder;
            builder = new GridBuilder(grdVendors);
            builder.AddIntegerColumn("VendorId", "ID", 3, true);
            builder.AddTextBoxColumn("VendorName", "Vendor Name", 16, false);
            builder.AddCheckBoxColumn("ScanInvoices", "Scan Invoices", 5, false);
            builder.AddTextBoxColumn("CategoryName", "Category", 12, false);
            builder.AddTextBoxColumn("InvNumFormat", "Invoice# Format", 10, false);
            builder.AddTextBoxColumn("Terms", "Terms", 6, false);
            builder.AddTextBoxColumn("Memo", "Memo", 20, false);
            
            var vendors = from vendor in mDC.Vendors
                          orderby vendor.VendorName
                          select vendor;
            grdVendors.DataSource = vendors;
            
        }

        private class VendorGridHelper : GridFormHelper<VendorForm, Vendor>
        {
            public VendorGridHelper(VendorForm form, DataGridView grid)
                : base(form, grid)
            {
            }

            protected override void DisposeDataSource()
            {
                mForm.mDC.Dispose();
            }

            protected override void CommitDataSource()
            {
                mForm.mDC.SubmitChanges();
            }
        }
    }
}
