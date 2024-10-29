using PdfiumViewer;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace PDFTool.BLLs
{
    public class ConvertToImage
    {
        public void GenerateImage(string filefullname, string fileType, int dpi)
        {
            try
            {
                string imagename = Path.GetFileNameWithoutExtension(filefullname);
                string imageSavePath = Path.GetDirectoryName(filefullname);

                //source: https://stackoverflow.com/questions/54258482/how-to-convert-a-pdf-to-a-bitmap-image-using-pdfiumviewer
                // Load PDF Document from file
                var doc = PdfDocument.Load(filefullname);
                // Loop through pages
                for (int page = 0; page < doc.PageCount; page++)
                {
                    // source: https://github.com/pvginkel/PdfiumViewer/issues/161
                    using (var img = doc.Render(page, dpi, dpi, PdfRenderFlags.CorrectFromDpi))
                    {
                        // Save rendered image to disc
                        string savename = $"{imageSavePath}\\{imagename}_{page + 1}{fileType}";

                        if (Utils.CheckFileExists(savename))
                        {
                            if (MessageBox.Show(string.Format(Utils.Messages.FileExists, $"{imagename}_{page + 1}{fileType}") + " " + Utils.Messages.ContinueQuestion, Utils.Status.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                                return;
                            }
                        }

                        img.Save(savename);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
