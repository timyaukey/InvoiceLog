using System;
using System.Collections.Generic;
using System.Drawing;

namespace Saraff.UI
{
    public class ScanResult : IDisposable
    {
        public readonly List<Image> Images;
        private bool _Disposed;

        public ScanResult()
        {
            Images = new List<Image>();
            _Disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_Disposed)
                return;

            if (disposing)
            {
                foreach (var image in Images)
                {
                    image.Dispose();
                }
                Images.Clear();
            }

            _Disposed = true;
        }

        /// <summary>
        /// Not needed in this case because this class does not directly
        /// use any unmanaged resources, but included for adherence to
        /// the design pattern.
        /// </summary>
        ~ScanResult()
        {
            Dispose(false);
        }
    }
}
