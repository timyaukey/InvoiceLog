using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WillowLib.WinHelper
{
    public class GridDateEditCell : DataGridViewTextBoxEditingControl
    {
        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            DateTime result;
            base.OnValidating(e);
            if (!string.IsNullOrEmpty(this.Text))
            {
                if (!DateTime.TryParse(this.Text, out result))
                {
                    MessageBox.Show("Invalid date");
                    e.Cancel = true;
                }
            }
        }
    }
}
