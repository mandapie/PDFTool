using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Linq;
using System.Windows;

namespace PDFTool.BLLs
{
    public class ConvertToPDF
    {
        //source: https://stackoverflow.com/questions/36052918/c-sharp-how-to-convert-an-image-to-a-pdf-using-a-free-library
        public void GeneratePDF(string[] filenamesArr, int width = 300)
        {
            try
            {
                //use the first file in the array as the pdf a name
                string filefullname = Path.ChangeExtension(filenamesArr.First(), ".pdf");

                //check that the filename is not in use
                if (Utils.CheckFileExists(filefullname))
                {
                    if (MessageBox.Show(string.Format(Utils.Messages.FileExists, Path.GetFileName(filefullname)) + " " + Utils.Messages.ContinueQuestion, Utils.Status.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                //create pdf
                PdfDocument document = new PdfDocument();
                foreach (string filename in filenamesArr)
                {
                    PdfPage page = document.AddPage(); // Create an empty page or load existing

                    XImage img = XImage.FromFile(filename);
                    // Calculate new height to keep image ratio
                    var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                    // Change PDF Page size to match image
                    page.Width = width;
                    page.Height = height;

                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(img, 0, 0, width, height);

                }

                document.Save(filefullname);
            }
            catch
            {
                throw;
            }
        }
    }
}
