using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PDFTool.BLLs
{
    public class CompressPDF
    {
        public void ShrinkPDF(string filefullname)
        {
            try
            {
                string shrunkfilename = Path.GetFileNameWithoutExtension(filefullname);
                string shrunkfileSavePath = Path.GetDirectoryName(filefullname);
                string shrunkfilefullname = $"{shrunkfileSavePath}//{shrunkfilename}_compressed.pdf";

                if (Utils.CheckFileExists(shrunkfilefullname))
                {
                    if (MessageBox.Show(string.Format(Utils.Messages.FileExists, $"{shrunkfilename}_compressed.pdf") + " " + Utils.Messages.ContinueQuestion, Utils.Status.Warning, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                PdfDocument document = PdfReader.Open(filefullname);
                document.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
                document.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;
                document.Options.NoCompression = false;
                document.Options.CompressContentStreams = true; // Defaults to false in debug build, so we set it to true.

                document.Save(shrunkfilefullname);
            }
            catch
            {
                throw;
            }
        }
    }
}
