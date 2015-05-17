using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvoiceLog
{
    public class GridBuilder
    {
        private DataGridView mGrid;
        private int mCharWidth;

        public GridBuilder(DataGridView grid)
        {
            mGrid = grid;
            Size charSize = TextRenderer.MeasureText("a", mGrid.DefaultCellStyle.Font);
            mCharWidth = charSize.Width;
            mGrid.AutoGenerateColumns = false;
        }

        public DataGridViewColumn AddColumn(DataGridViewColumn col, string propertyName, string columnTitle,
            int widthInChars, bool readOnly)
        {
            col.DataPropertyName = propertyName;
            col.HeaderText = columnTitle;
            col.Width = widthInChars * mCharWidth;
            col.ReadOnly = readOnly;
            mGrid.Columns.Add(col);
            return col;
        }

        public DataGridViewTextBoxColumn AddTextBoxColumn(string propertyName, string columnTitle,
            int widthInChars, bool readOnly)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            AddColumn(col, propertyName, columnTitle, widthInChars, readOnly);
            return col;
        }

        public DataGridViewTextBoxColumn AddCurrencyColumn(string propertyName, string columnTitle,
            int widthInChars, bool readOnly)
        {
            DataGridViewTextBoxColumn col = AddTextBoxColumn(propertyName, columnTitle, widthInChars, readOnly);
            col.DefaultCellStyle.Format = "c";
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return col;
        }

        public DataGridViewTextBoxColumn AddIntegerColumn(string propertyName, string columnTitle,
            int widthInChars, bool readOnly)
        {
            DataGridViewTextBoxColumn col = AddTextBoxColumn(propertyName, columnTitle, widthInChars, readOnly);
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            return col;
        }

        public DataGridViewCheckBoxColumn AddCheckBoxColumn(string propertyName, string columnTitle,
            int widthInChars, bool readOnly)
        {
            DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn();
            col.TrueValue = 1;
            col.FalseValue = 0;
            AddColumn(col, propertyName, columnTitle, widthInChars, readOnly);
            return col;
        }

        public DataGridViewComboBoxColumn AddComboBoxColumn(string propertyName,
            string columnTitle, int widthInChars, bool readOnly,
            object dataSource, string displayMember, string valueMember)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            AddColumn(col, propertyName, columnTitle, widthInChars, readOnly);
            col.AutoComplete = true;
            col.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            col.DisplayMember = displayMember;
            col.ValueMember = valueMember;
            col.DataSource = dataSource;
            return col;
        }

        public DataGridViewComboBoxColumn AddComboBoxColumn(string propertyName,
            string columnTitle, int widthInChars, bool readOnly,
            IEnumerable<string> items)
        {
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            AddColumn(col, propertyName, columnTitle, widthInChars, readOnly);
            col.AutoComplete = true;
            col.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            foreach (string item in items)
            {
                col.Items.Add(item);
            }
            return col;
        }

        public DataGridViewButtonColumn AddButtonColumn(string columnTitle, string buttonText, int widthInChars)
        {
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = columnTitle;
            col.Width = widthInChars * mCharWidth;
            col.Text = buttonText;
            col.UseColumnTextForButtonValue = true;
            mGrid.Columns.Add(col);
            return col;
        }
    }
}
