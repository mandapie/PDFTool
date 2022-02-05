using PdfiumViewer;
using System.Drawing.Imaging;
using System.IO;

namespace PDFTool.BLLs
{
    public class ConvertToImage
    {
        public bool GenerateImage(string filefullname, string imageSavePath, string fileType, int dpi)
        {
            try
            {
                string imagename = Path.GetFileNameWithoutExtension(filefullname);

                //source: https://stackoverflow.com/questions/54258482/how-to-convert-a-pdf-to-a-bitmap-image-using-pdfiumviewer
                // Load PDF Document from file
                using (var doc = PdfDocument.Load(filefullname))
                {
                    // Loop through pages
                    for (int page = 0; page < doc.PageCount; page++)
                    {
                        // source: https://github.com/pvginkel/PdfiumViewer/issues/161
                        using (var img = doc.Render(page, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
                        {
                            // Save rendered image to disc
                            string savename = $"{imageSavePath}\\{imagename}_{page + 1}{fileType}";
                            img.Save(savename);
                        }
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
