using System;
using System.Windows.Forms;

namespace WillowLib.WinHelper
{
    public class GridSpecializedTextBoxColumn<TEdit> : DataGridViewTextBoxColumn
        where TEdit : DataGridViewTextBoxEditingControl
    {
        public GridSpecializedTextBoxColumn()
        {
            CellTemplate = new GridSpecializedTextBoxCell<TEdit>();
        }
    }
}
