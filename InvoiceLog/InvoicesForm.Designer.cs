namespace InvoiceLog
{
    partial class InvoicesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grdInvoices = new System.Windows.Forms.DataGridView();
            this.btnShowInvoices = new System.Windows.Forms.Button();
            this.cboQueryMethod = new System.Windows.Forms.ComboBox();
            this.chkShowExportedInvoices = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblVendorSearchHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // grdInvoices
            // 
            this.grdInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInvoices.Location = new System.Drawing.Point(12, 12);
            this.grdInvoices.Name = "grdInvoices";
            this.grdInvoices.Size = new System.Drawing.Size(1135, 489);
            this.grdInvoices.TabIndex = 0;
            this.grdInvoices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdInvoices_CellContentClick);
            // 
            // btnShowInvoices
            // 
            this.btnShowInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShowInvoices.Location = new System.Drawing.Point(196, 510);
            this.btnShowInvoices.Name = "btnShowInvoices";
            this.btnShowInvoices.Size = new System.Drawing.Size(105, 23);
            this.btnShowInvoices.TabIndex = 2;
            this.btnShowInvoices.Text = "Show Invoices";
            this.btnShowInvoices.UseVisualStyleBackColor = true;
            this.btnShowInvoices.Click += new System.EventHandler(this.btnShowInvoices_Click);
            // 
            // cboQueryMethod
            // 
            this.cboQueryMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboQueryMethod.FormattingEnabled = true;
            this.cboQueryMethod.Location = new System.Drawing.Point(12, 512);
            this.cboQueryMethod.Name = "cboQueryMethod";
            this.cboQueryMethod.Size = new System.Drawing.Size(178, 21);
            this.cboQueryMethod.TabIndex = 1;
            // 
            // chkShowExportedInvoices
            // 
            this.chkShowExportedInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkShowExportedInvoices.AutoSize = true;
            this.chkShowExportedInvoices.Location = new System.Drawing.Point(307, 514);
            this.chkShowExportedInvoices.Name = "chkShowExportedInvoices";
            this.chkShowExportedInvoices.Size = new System.Drawing.Size(106, 17);
            this.chkShowExportedInvoices.TabIndex = 3;
            this.chkShowExportedInvoices.Text = "Include Exported";
            this.chkShowExportedInvoices.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1028, 510);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(119, 23);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblVendorSearchHint
            // 
            this.lblVendorSearchHint.AutoSize = true;
            this.lblVendorSearchHint.ForeColor = System.Drawing.Color.Red;
            this.lblVendorSearchHint.Location = new System.Drawing.Point(442, 514);
            this.lblVendorSearchHint.Name = "lblVendorSearchHint";
            this.lblVendorSearchHint.Size = new System.Drawing.Size(163, 13);
            this.lblVendorSearchHint.TabIndex = 6;
            this.lblVendorSearchHint.Text = "Use ^S To Search Vendor Name";
            // 
            // InvoicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 545);
            this.Controls.Add(this.lblVendorSearchHint);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.chkShowExportedInvoices);
            this.Controls.Add(this.cboQueryMethod);
            this.Controls.Add(this.btnShowInvoices);
            this.Controls.Add(this.grdInvoices);
            this.Name = "InvoicesForm";
            this.Text = "Invoices";
            this.Load += new System.EventHandler(this.InvoicesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdInvoices;
        private System.Windows.Forms.Button btnShowInvoices;
        private System.Windows.Forms.ComboBox cboQueryMethod;
        private System.Windows.Forms.CheckBox chkShowExportedInvoices;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblVendorSearchHint;
    }
}

