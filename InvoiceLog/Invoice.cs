using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WillowLib.WinHelper;

namespace InvoiceLog
{
    public partial class Invoice : IEntity
    {
        public string Validate()
        {
            if (string.IsNullOrEmpty(VendorName))
                return "Vendor name is required.";
            if (string.IsNullOrEmpty(InvoiceNumber))
                return "Invoice number is required.";
            if (!DueDate.HasValue)
                return "Due date is required.";
            if (!Amount.HasValue)
                return "Amount is required.";
            if (IsCredit != 0 && Amount > 0)
                return "Credit amounts must be negative.";
            if (IsCredit == 0 && Amount < 0)
                return "Invoice amounts must be positive.";
            return null;
        }

        public bool HasData
        {
            get { return InvoiceId != 0; }
        }
    }
}
