using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFTool.BLLs
{
    public class CompressPDF
    {
        public bool ShrinkPDF(string filefullname)
        {
            try
            {
                string shrunkfilename = Path.GetFileNameWithoutExtension(filefullname);
                string shrunkfileSavePath = Path.GetDirectoryName(filefullname);
                string shrunkfilefullname = $"{shrunkfileSavePath}//{shrunkfilename}_compressed.pdf";

                PdfDocument document = PdfReader.Open(filefullname);
                document.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
                document.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;
                document.Options.NoCompression = false;
                document.Options.CompressContentStreams = true; // Defaults to false in debug build, so we set it to true.

                document.Save(shrunkfilefullname);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
