/* Этот файл является частью примеров использования библиотеки Saraff.Twain.NET
 * © SARAFF SOFTWARE (Кирножицкий Андрей), 2011.
 * Saraff.Twain.NET - свободная программа: вы можете перераспространять ее и/или
 * изменять ее на условиях Меньшей Стандартной общественной лицензии GNU в том виде,
 * в каком она была опубликована Фондом свободного программного обеспечения;
 * либо версии 3 лицензии, либо (по вашему выбору) любой более поздней
 * версии.
 * Saraff.Twain.NET распространяется в надежде, что она будет полезной,
 * но БЕЗО ВСЯКИХ ГАРАНТИЙ; даже без неявной гарантии ТОВАРНОГО ВИДА
 * или ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Подробнее см. в Меньшей Стандартной
 * общественной лицензии GNU.
 * Вы должны были получить копию Меньшей Стандартной общественной лицензии GNU
 * вместе с этой программой. Если это не так, см.
 * <http://www.gnu.org/licenses/>.)
 * 
 * This file is part of samples of Saraff.Twain.NET.
 * © SARAFF SOFTWARE (Kirnazhytski Andrei), 2011.
 * Saraff.Twain.NET is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * Saraff.Twain.NET is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * You should have received a copy of the GNU Lesser General Public License
 * along with Saraff.Twain.NET. If not, see <http://www.gnu.org/licenses/>.
 * 
 * PLEASE SEND EMAIL TO:  twain@saraff.ru.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Saraff.Twain;

namespace Saraff.UI
{

    public partial class MultiPage : Form
    {
        private bool _isEnable = false;
        private const string _ExceptionCaption = "Exception";
        // This is disposed when the form is disposed.
        private ResultDisposer _ResultDisposer = null;
        /// <summary>
        /// If not null after form closes, then scan was completed
        /// and "Save" button clicked. Contains the Image objects
        /// created by the scan and checked in the list. If null
        /// then scan was not completed. If Result!=null then the
        /// caller must call Result.Dispose();
        /// </summary>
        public ScanResult Result { get; private set; }

        public MultiPage()
        {
            InitializeComponent();
            _ResultDisposer = new ResultDisposer();
            this.components.Add(_ResultDisposer);
            /*
            try {
                this._twain.OpenDSM();
            } catch(Exception ex) {
                MessageBox.Show(string.Format("{0}\n\n{1}",ex.Message,ex.StackTrace),
                    _ExceptionCaption,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }*/
        }

        private void MultiPage_Load(object sender, EventArgs e)
        {
            try
            {
                this._twain.OpenDSM();
                lblCurrentSource.Text = _twain.SelectedSourceName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}", ex.Message, ex.StackTrace),
                    _ExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MultiPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this._twain != null)
                {
                    this._twain.CloseDSM();
                }
                // TO DO: dispose of images if canceled.
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}", ex.Message, ex.StackTrace),
                    _ExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectSource_Click(object sender, EventArgs e)
        {
            try
            {
                this._twain.CloseDataSource();
                if (this._twain.SelectSource())
                    lblCurrentSource.Text = _twain.SelectedSourceName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _ExceptionCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                this._twain.Acquire();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _ExceptionCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _twain_AcquireCompleted(object sender, EventArgs e)
        {
            try
            {
                ClearResult();
                lstPages.Items.Clear();
                for (int imageIndex = 0; imageIndex < _twain.ImageCount; imageIndex++)
                {
                    Image image = _twain.GetImage(imageIndex);
                    PageListItem item = new PageListItem(imageIndex + 1, image);
                    _ResultDisposer.Result.Images.Add(image);
                    lstPages.Items.Add(item);
                    lstPages.SetItemChecked(imageIndex, true);
                }
                if (lstPages.Items.Count > 0)
                {
                    lstPages.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _ExceptionCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ResultDisposer.Result == null)
            {
                MessageBox.Show("There is nothing to save.");
                return;
            }
            // Construct a Result object with all the selected pages.
            // All unselected pages are disposed immediately.
            Result = new ScanResult();
            for (int imageIndex = 0; imageIndex < lstPages.Items.Count; imageIndex++)
            {
                Image image = _ResultDisposer.Result.Images[imageIndex];
                if (lstPages.GetItemChecked(imageIndex))
                {
                    Result.Images.Add(image);
                }
                else
                {
                    image.Dispose();
                }
            }
            _ResultDisposer.Result = null;
            Close();
        }

        private void lstPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageListItem item = (PageListItem)lstPages.SelectedItem;
            if (item != null)
            {
                this.pictureBox1.Image = item.Image;
            }
        }

        private void _twain_TwainStateChanged(object sender, Twain32.TwainStateEventArgs e)
        {
            try
            {
                if ((e.TwainState & Twain32.TwainStateFlag.DSEnabled) == 0 && this._isEnable)
                {
                    this._isEnable = false;
                    // <<< scaning finished (or closed)
                }
                this._isEnable = (e.TwainState & Twain32.TwainStateFlag.DSEnabled) != 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _ExceptionCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearResult()
        {
            if (_ResultDisposer.Result != null)
                _ResultDisposer.Result.Dispose();
            _ResultDisposer.Result = new ScanResult();
        }

        private class PageListItem
        {
            private readonly int _PageNumber;
            public readonly Image Image;

            public PageListItem(int pageNumber, Image image)
            {
                _PageNumber = pageNumber;
                Image = image;
            }

            public override string ToString()
            {
                return "Page " + _PageNumber.ToString();
            }
        }

        /// <summary>
        /// One of these is added to this.components in the form constructor,
        /// so the ScanResult it contains will be disposed when the form
        /// is disposed. The "Save" button sets .Result=null to prevent the
        /// ScanResult from being disposed when the form is closed, so the
        /// caller can use the images. In this case the caller is responsible
        /// for calling Dispose() of the ScanResult passed back from the form.
        /// </summary>
        private class ResultDisposer : System.ComponentModel.Component
        {
            public ScanResult Result;
            private bool _Disposed;

            public ResultDisposer()
            {
                Result = null;
                _Disposed = false;
            }

            protected override void Dispose(bool disposing)
            {
                if (_Disposed)
                    return;
                if (disposing && (Result!=null))
                    Result.Dispose();
                base.Dispose(disposing);
                _Disposed = true;
            }

            ~ResultDisposer()
            {
                Dispose(false);
            }
        }
    }
}
