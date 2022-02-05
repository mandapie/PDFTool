using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace PDFTool.BLLs
{
    public class ConvertToPDF
    {
        //source: https://stackoverflow.com/questions/36052918/c-sharp-how-to-convert-an-image-to-a-pdf-using-a-free-library
        public bool GeneratePDF(string imagefullname, string filefullname, int width = 300)
        {
            try
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage(); // Create an empty page or load existing

                XImage img = XImage.FromFile(imagefullname);
                // Calculate new height to keep image ratio
                var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                // Change PDF Page size to match image
                page.Width = width;
                page.Height = height;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                gfx.DrawImage(img, 0, 0, width, height);

                document.Save(filefullname);
                //Process.Start(filename); // open file

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
