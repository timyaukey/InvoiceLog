using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WillowLib.WinHelper;

namespace InvoiceLog
{
    public partial class Vendor : IEntity
    {
        public string Validate()
        {
            if (string.IsNullOrEmpty(VendorName))
                return "Vendor name is required.";
            return null;
        }

        public bool HasData
        {
            get { return VendorId != 0; }
        }
    }
}
