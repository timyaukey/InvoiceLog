using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WillowLib.WinHelper
{
    public abstract class GridFormHelper<TForm, TEntity>
        where TForm : Form
        where TEntity : IEntity
    {
        protected TForm mForm;
        private DataGridView mGrid;
        private bool mRowDirty;
        private bool mRowHasData;

        public event EventHandler<EventArgs> RowHasDataChanged;

        public GridFormHelper(TForm form, DataGridView grid)
        {
            mForm = form;
            mForm.FormClosing += Form_FormClosing;

            mGrid = grid;
            mGrid.RowEnter += Grid_RowEnter;
            mGrid.RowValidating += Grid_RowValidating;
            mGrid.RowValidated += Grid_RowValidated;
            mGrid.DataError += Grid_DataError;
            mGrid.CellValueChanged += Grid_CellValueChanged;
        }

        public bool RowDirty
        {
            get { return mRowDirty; }
            set { mRowDirty = value; }
        }

        public bool RowHasData
        {
            get { return mRowHasData; }
            private set
            {
                if (value != mRowHasData)
                {
                    mRowHasData = value;
                    if (RowHasDataChanged != null)
                        RowHasDataChanged(this, new EventArgs());
                }
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CurrentRowIsValid())
            {
                if (MessageBox.Show("Do you want to close the window without saving?",
                    "Discard Changes", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                if (!mGrid.EndEdit())
                {
                    e.Cancel = true;
                    return;
                }
                SaveWork();
            }
            DisposeDataSource();
        }

        public void SaveWork()
        {
            try
            {
                TEntity entity = (TEntity)mGrid.CurrentRow.DataBoundItem;
                if (entity != null)
                {
                    SaveSideEffects(entity);
                }
                CommitDataSource();
                mRowDirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected abstract void DisposeDataSource();

        protected abstract void CommitDataSource();

        protected virtual void SaveSideEffects(TEntity entity)
        {
        }

        private void Grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = mGrid.Rows[e.RowIndex];
                TEntity entity = (TEntity)row.DataBoundItem;
                if (entity == null)
                    RowHasData = false;
                else
                    RowHasData = entity.HasData;
                RowEnter(entity);
            }
        }

        protected virtual void RowEnter(TEntity entity)
        {
        }

        private void Grid_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (mRowDirty)
            {
                DataGridViewRow row = mGrid.Rows[e.RowIndex];
                TEntity entity = (TEntity)row.DataBoundItem;
                string errorMessage = entity.Validate();
                if (errorMessage != null)
                {
                    ShowValidationError(errorMessage);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void Grid_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (mRowDirty)
            {
                DataGridViewRow row = mGrid.Rows[e.RowIndex];
                TEntity entity = (TEntity)row.DataBoundItem;
                SetDefaultValues(entity);
                SaveWork();
            }
        }

        protected virtual void SetDefaultValues(TEntity entity)
        {
        }

        private void Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridViewColumn errorCol = mGrid.Columns[e.ColumnIndex];
            MessageBox.Show("Error in " + errorCol.HeaderText + ": " + e.Exception.Message);
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            RowDirty = true;
            RowHasData = true;
        }

        public void ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error");
        }

        public bool CurrentRowIsValid()
        {
            if (mRowDirty)
            {
                TEntity entity = (TEntity)mGrid.CurrentRow.DataBoundItem;
                string errorMessage = entity.Validate();
                if (errorMessage != null)
                {
                    ShowValidationError(errorMessage);
                    return false;
                }
            }
            return true;
        }
    
    }
}
