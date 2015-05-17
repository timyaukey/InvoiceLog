using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Saraff.UI;
using PdfSharp.Pdf;
using WillowLib.WinHelper;

namespace InvoiceLog
{
    public partial class InvoicesForm : Form
    {
        private InvoiceDC mDC;
        private PdfDocument mNewPDF;
        private InvoiceGridHelper mHelper;
        private int mColIdxVendorName;
        private int mColIdxInvoiceNumber;
        private int mColIdxMoreButton;
        // Original data for current row when it was loaded.
        private string mOrigRowVendor;
        private string mOrigRowInvNum;

        // NOTE: InvoiceId must have PrimaryKey=True and Auto-Sync=OnInsert in DataContext.

        public InvoicesForm()
        {
            InitializeComponent();
            mHelper = new InvoiceGridHelper(this, this.grdInvoices);
        }

        private void InvoicesForm_Load(object sender, EventArgs e)
        {
            mDC = Utilities.GetDC();
            GridBuilder builder;
            builder = new GridBuilder(grdInvoices);
            builder.AddIntegerColumn("InvoiceId", "ID", 4, true);
            mColIdxVendorName = builder.AddColumn(new GridSpecializedTextBoxColumn<GridVendorSearchEditCell>(), "VendorName", "Vendor Name", 20, false).DisplayIndex;
            builder.AddTextBoxColumn("PONumber", "PO Number", 7, false);
            mColIdxInvoiceNumber = builder.AddTextBoxColumn("InvoiceNumber", "Invoice Number", 7, false).DisplayIndex;
            builder.AddColumn(new GridDateColumn(), "InvoiceDate", "Invoice Date", 6, false);
            builder.AddTextBoxColumn("Terms", "Terms", 5, false);
            builder.AddColumn(new GridDateColumn(), "DueDate", "Due Date", 6, false);
            builder.AddCurrencyColumn("Amount", "Amount", 6, false);
            builder.AddCheckBoxColumn("IsCredit", "Is Credit", 5, false);
            builder.AddCheckBoxColumn("Exported", "Exported", 5, false);
            mColIdxMoreButton = builder.AddButtonColumn("Scanning", "More...", 5).DisplayIndex;
            builder.AddColumn(new GridDateColumn(), "CreateDate", "Create Date", 6, false);

            cboQueryMethod.Items.Add(new QueryInvoicesOption(QueryInvoicesByCreateDate, "By create date"));
            cboQueryMethod.Items.Add(new QueryInvoicesOption(QueryInvoicesByDueDate, "By due date"));
            cboQueryMethod.Items.Add(new QueryInvoicesOption(QueryInvoicesByVendorInvNum, "By vendor & invoice number"));
            cboQueryMethod.SelectedIndex = 0;
            QueryInvoicesOption option = (QueryInvoicesOption)cboQueryMethod.SelectedItem;
            ShowInvoices(option.Query());
        }

        private void ShowValidationError(string message)
        {
            mHelper.ShowValidationError(message);
        }

        private void btnShowInvoices_Click(object sender, EventArgs e)
        {
            if (mHelper.CurrentRowIsValid())
            {
                mHelper.SaveWork();
                QueryInvoicesOption option = (QueryInvoicesOption)cboQueryMethod.SelectedItem;
                var query = option.Query();
                ShowInvoices(query);
            }
        }

        private IQueryable<Invoice> QueryInvoicesByCreateDate()
        {
            var invoices = AddExportFilter(mDC.Invoices).
                OrderBy(inv => inv.CreateDate);
            return invoices;
        }

        private IQueryable<Invoice> QueryInvoicesByVendorInvNum()
        {
            var invoices = AddExportFilter(mDC.Invoices).
                OrderBy(inv => inv.VendorName).ThenBy(inv=>inv.InvoiceNumber);
            return invoices;
        }

        private IQueryable<Invoice> QueryInvoicesByDueDate()
        {
            var invoices = AddExportFilter(mDC.Invoices).
                OrderBy(inv => inv.DueDate);
            return invoices;
        }

        private IQueryable<Invoice> AddExportFilter(IQueryable<Invoice> query)
        {
            if (chkShowExportedInvoices.Checked)
                return query;
            else
                return query.Where(inv => (inv.Exported == 0));
        }

        private void ShowInvoices(IQueryable<Invoice> invoices)
        {
            grdInvoices.DataSource = invoices;
            mHelper.RowDirty = false;
        }

        private delegate IQueryable<Invoice> QueryInvoicesDelegate();

        private class QueryInvoicesOption
        {
            public readonly QueryInvoicesDelegate Query;
            private readonly string mLabel;

            public QueryInvoicesOption(QueryInvoicesDelegate showInvoices, string label)
            {
                Query = showInvoices;
                mLabel = label;
            }

            public override string ToString()
            {
                return mLabel;
            }
        }

        private void grdInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == mColIdxMoreButton)
            {
                string vendorName = (string)grdInvoices.CurrentRow.Cells[mColIdxVendorName].Value;
                string invoiceNumber = (string)grdInvoices.CurrentRow.Cells[mColIdxInvoiceNumber].Value;
                using (var moreForm = new InvoiceMoreForm())
                {
                    moreForm.ShowDialog(vendorName, invoiceNumber);
                    if (moreForm.NewPDF != null)
                    {
                        mHelper.RowDirty = true;
                        mNewPDF = moreForm.NewPDF;
                    }
                }
            }
        }

        private void MoveAttachmentIfNeeded(Invoice inv)
        {
            if (mOrigRowVendor != null && mOrigRowInvNum != null)
            {
                if (mOrigRowVendor != inv.VendorName ||
                    mOrigRowInvNum != inv.InvoiceNumber)
                {
                    string oldAttachmentFile = Utilities.MakeAttachmentFileName(mOrigRowVendor, mOrigRowInvNum);
                    if (File.Exists(oldAttachmentFile))
                    {
                        Utilities.EnsureAttachmentVendorFolderExists(inv.VendorName);
                        string newAttachmentFile = Utilities.MakeAttachmentFileName(inv.VendorName, inv.InvoiceNumber);
                        File.Move(oldAttachmentFile, newAttachmentFile);
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            IQueryable<Invoice> invoices = (IQueryable<Invoice>)grdInvoices.DataSource;
            StringBuilder output = new StringBuilder();
            foreach (var invoice in invoices)
            {
                StringBuilder line = new StringBuilder();
                string invoiceDate = invoice.InvoiceDate.HasValue ? invoice.InvoiceDate.Value.ToShortDateString() : "";
                string dueDate = invoice.DueDate.Value.ToShortDateString();
                decimal amount = invoice.Amount.HasValue ? invoice.Amount.Value : (decimal)0.0;
                if (invoice.IsCredit > 0)
                    amount = -amount;
                line.Append(invoice.VendorName + "\t");
                line.Append(invoice.PONumber + "\t");
                line.Append(invoiceDate + "\t");
                line.Append(invoice.InvoiceNumber + "\t");
                line.Append(invoice.Terms + "\t");
                line.Append(dueDate + "\t");
                line.Append(amount.ToString() + "\t");
                line.Append(((invoice.IsCredit != 0) ? "Credit" : "Invoice") + "\t");
                line.Append("E:Stock");
                if (output.Length > 0)
                    output.AppendLine("");
                output.Append(line.ToString());
            }
            try
            {
                Clipboard.Clear();
                Clipboard.SetText(output.ToString());
                MessageBox.Show("Invoices exported to clipboard.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting invoices to clipboard: " + ex.Message);
            }
        }

        private class InvoiceGridHelper : GridFormHelper<InvoicesForm, Invoice>
        {
            public InvoiceGridHelper(InvoicesForm form, DataGridView grid)
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

            protected override void SaveSideEffects(Invoice entity)
            {
                mForm.MoveAttachmentIfNeeded(entity);
                if (mForm.mNewPDF != null)
                {
                    using (mForm.mNewPDF)
                    {
                        Utilities.EnsureAttachmentVendorFolderExists(entity.VendorName);
                        mForm.mNewPDF.Save(Utilities.MakeAttachmentFileName(entity.VendorName, entity.InvoiceNumber));
                        mForm.mNewPDF.Close();
                    }
                    mForm.mNewPDF = null;
                }
            }

            protected override void RowEnter(Invoice entity)
            {
                if (entity != null)
                {
                    mForm.mOrigRowVendor = entity.VendorName;
                    mForm.mOrigRowInvNum = entity.InvoiceNumber;
                }
                else
                {
                    mForm.mOrigRowVendor = null;
                    mForm.mOrigRowInvNum = null;
                }
            }

            protected override void SetDefaultValues(Invoice entity)
            {
                if (!entity.CreateDate.HasValue)
                {
                    entity.CreateDate = DateTime.Today;
                }
            }
        }
    }
}
