namespace InvoiceLog
{
    partial class InvoiceMoreForm
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
            this.btnScanInvoice = new System.Windows.Forms.Button();
            this.btnViewInvoice = new System.Windows.Forms.Button();
            this.btnViewSample = new System.Windows.Forms.Button();
            this.lblVendorNameLabel = new System.Windows.Forms.Label();
            this.lblInvoiceNumberLabel = new System.Windows.Forms.Label();
            this.lblInvoiceNumberText = new System.Windows.Forms.Label();
            this.lblVendorNameText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnScanInvoice
            // 
            this.btnScanInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScanInvoice.Location = new System.Drawing.Point(266, 59);
            this.btnScanInvoice.Name = "btnScanInvoice";
            this.btnScanInvoice.Size = new System.Drawing.Size(109, 23);
            this.btnScanInvoice.TabIndex = 0;
            this.btnScanInvoice.Text = "Scan Invoice";
            this.btnScanInvoice.UseVisualStyleBackColor = true;
            this.btnScanInvoice.Click += new System.EventHandler(this.btnScanInvoice_Click);
            // 
            // btnViewInvoice
            // 
            this.btnViewInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewInvoice.Location = new System.Drawing.Point(266, 88);
            this.btnViewInvoice.Name = "btnViewInvoice";
            this.btnViewInvoice.Size = new System.Drawing.Size(109, 23);
            this.btnViewInvoice.TabIndex = 1;
            this.btnViewInvoice.Text = "View Invoice";
            this.btnViewInvoice.UseVisualStyleBackColor = true;
            this.btnViewInvoice.Click += new System.EventHandler(this.btnViewInvoice_Click);
            // 
            // btnViewSample
            // 
            this.btnViewSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewSample.Location = new System.Drawing.Point(266, 117);
            this.btnViewSample.Name = "btnViewSample";
            this.btnViewSample.Size = new System.Drawing.Size(109, 23);
            this.btnViewSample.TabIndex = 2;
            this.btnViewSample.Text = "View Hints";
            this.btnViewSample.UseVisualStyleBackColor = true;
            this.btnViewSample.Click += new System.EventHandler(this.btnViewSample_Click);
            // 
            // lblVendorNameLabel
            // 
            this.lblVendorNameLabel.Location = new System.Drawing.Point(12, 9);
            this.lblVendorNameLabel.Name = "lblVendorNameLabel";
            this.lblVendorNameLabel.Size = new System.Drawing.Size(80, 21);
            this.lblVendorNameLabel.TabIndex = 3;
            this.lblVendorNameLabel.Text = "Vendor Name:";
            // 
            // lblInvoiceNumberLabel
            // 
            this.lblInvoiceNumberLabel.Location = new System.Drawing.Point(12, 30);
            this.lblInvoiceNumberLabel.Name = "lblInvoiceNumberLabel";
            this.lblInvoiceNumberLabel.Size = new System.Drawing.Size(95, 23);
            this.lblInvoiceNumberLabel.TabIndex = 4;
            this.lblInvoiceNumberLabel.Text = "Invoice Number:";
            // 
            // lblInvoiceNumberText
            // 
            this.lblInvoiceNumberText.AutoSize = true;
            this.lblInvoiceNumberText.Location = new System.Drawing.Point(113, 30);
            this.lblInvoiceNumberText.Name = "lblInvoiceNumberText";
            this.lblInvoiceNumberText.Size = new System.Drawing.Size(48, 13);
            this.lblInvoiceNumberText.TabIndex = 5;
            this.lblInvoiceNumberText.Text = "(number)";
            // 
            // lblVendorNameText
            // 
            this.lblVendorNameText.AutoSize = true;
            this.lblVendorNameText.Location = new System.Drawing.Point(113, 9);
            this.lblVendorNameText.Name = "lblVendorNameText";
            this.lblVendorNameText.Size = new System.Drawing.Size(39, 13);
            this.lblVendorNameText.TabIndex = 6;
            this.lblVendorNameText.Text = "(name)";
            // 
            // InvoiceMoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 151);
            this.Controls.Add(this.lblVendorNameText);
            this.Controls.Add(this.lblInvoiceNumberText);
            this.Controls.Add(this.lblInvoiceNumberLabel);
            this.Controls.Add(this.lblVendorNameLabel);
            this.Controls.Add(this.btnViewSample);
            this.Controls.Add(this.btnViewInvoice);
            this.Controls.Add(this.btnScanInvoice);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InvoiceMoreForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scanning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScanInvoice;
        private System.Windows.Forms.Button btnViewInvoice;
        private System.Windows.Forms.Button btnViewSample;
        private System.Windows.Forms.Label lblVendorNameLabel;
        private System.Windows.Forms.Label lblInvoiceNumberLabel;
        private System.Windows.Forms.Label lblInvoiceNumberText;
        private System.Windows.Forms.Label lblVendorNameText;
    }
}