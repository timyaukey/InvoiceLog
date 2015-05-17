using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
//using System.Linq;
using System.Text;

using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

using Saraff.UI;

namespace InvoiceLog
{
    public class ImagesToPDF
    {
        private ScanResult _ScanResult;
        private PdfDocument _PdfDoc;
        private PageSize _PageSize = PageSize.Letter;
        private XGraphics _Gfx;
        private double _XScale;
        private double _YScale;
        private double _XMarginInches = 0.25d;
        private double _YMarginInches = 0.25d;
        private double _PageWidth;
        private double _PageHeight;
        private double _PageAspectRatio;        // Height over width

        public ImagesToPDF(ScanResult scanResult)
        {
            _ScanResult = scanResult;
            _PdfDoc = new PdfDocument();
        }

        public PdfDocument Create()
        {
            _PdfDoc.Info.Title = "PDF Document";
            int pageNumber = 0;
            foreach (Image image in _ScanResult.Images)
            {
                PdfPage page = _PdfDoc.AddPage();
                _Gfx = XGraphics.FromPdfPage(page, XGraphicsUnit.Point);
                // Convert the image to JPG format, which makes the PDF much smaller.
                // Each page has to have a different file name, or else it uses the same one repeatedly.
                // I tried using a MemoryStream, but it threw a generic GDI+ exception.
                pageNumber++;
                string tempFileName = Path.Combine(Path.GetTempPath(), "itoptemp" + pageNumber.ToString() + ".jpg");
                using (FileStream jpgStream = new FileStream(tempFileName, FileMode.Create))
                {
                    image.Save(jpgStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                using (XImage xImage = XImage.FromFile(tempFileName))
                {
                    double aspectRatio = (double)xImage.PixelHeight / (double)xImage.PixelWidth;
                    PageSize pageSize = PageSize.Letter;
                    if (aspectRatio > (12.5d / 8.5d))
                        pageSize = PageSize.Legal;
                    SetPageMetrics(pageSize);
                    double drawWidthInches = _PageWidth - 2 * _XMarginInches;
                    double scaledHeight = drawWidthInches * aspectRatio;
                    _Gfx.DrawImage(xImage,
                        new XRect(InchesToInternalX(0.0d), InchesToInternalY(0.0d),
                            InchesToInternalX(drawWidthInches), InchesToInternalY(scaledHeight)));
                }
            }
            return _PdfDoc;
        }

        private void SetPageMetrics(PageSize pageSize)
        {
            _PageSize = pageSize;

            // Figure out the scale based on the selected page size, the conversion of inches
            // to points, and the assumed print margins of 0.25 inches. We have to consider
            // page margins because the page size describes the entire physical page, but Acrobat
            // Reader (and most ways of printing PDF's) will scale the document down in size if
            // necessary to make it fit inside the printer margins. In other words a document
            // declared to be 8.5" by 11" will actually be shrunk slightly when printed on that
            // size of paper because of the printer margins. And therefore the measurements taken
            // from a physical ruler on sheet of paper are slightly shorter than the number of inches
            // you need to use to print something at that position.
            _XScale = 72.0d;
            _YScale = 72.0d;
            // To properly support a page size it must be mentioned
            // here, but the default is letter size which is hopefully
            // reasonably close.
            switch (pageSize)
            {
                case PageSize.Legal:
                    _PageWidth = 8.5d;
                    _PageHeight = 14.0d;
                    break;
                // Default to letter
                default:
                    _PageWidth = 8.5d;
                    _PageHeight = 11.0d;
                    break;
            }
            _PageAspectRatio = _PageHeight / _PageWidth;
            _XScale = _XScale * (_PageWidth / (_PageWidth - 2.0d * _XMarginInches));
            _YScale = _YScale * (_PageHeight / (_PageHeight - 2.0d * _YMarginInches));
        }

        /// <summary>
        /// Convert an "X" measurement in inches from the print margin to the coordinate system
        /// used internally, which is points (1/72 of an inch) measured from the print margin.
        /// </summary>
        /// <param name="inches"></param>
        /// <returns></returns>
        private double InchesToInternalX(double inches)
        {
            return inches * _XScale;
        }

        /// <summary>
        /// Like InchesToInternalX(), but in the "Y" dimension.
        /// </summary>
        /// <param name="inches"></param>
        /// <returns></returns>
        private double InchesToInternalY(double inches)
        {
            return inches * _YScale;
        }
    }
}
