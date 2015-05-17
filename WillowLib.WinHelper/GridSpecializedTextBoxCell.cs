using System;
using System.Windows.Forms;

namespace WillowLib.WinHelper
{
    public class GridSpecializedTextBoxCell<TEdit> : DataGridViewTextBoxCell
        where TEdit : DataGridViewTextBoxEditingControl
        
    {
        public override Type EditType
        {
            get
            {
                return typeof(TEdit);
            }
        }
    }
}
