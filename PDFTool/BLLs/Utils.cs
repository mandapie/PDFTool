using System.IO;
using System.Linq;

namespace PDFTool.BLLs
{
    public static class Utils
    {
        public static class Messages
        {
            public const string Success = "Done!";
            public const string SuccessConvert = "File successfully converted.";

            public const string IncorrectFile = "Please select the correct file type.\n\n";
            public const string AcceptedFileList = "Acccepted file types: {0}";
            public const string FileExists = "File \"{0}\" exists.";
            public const string ContinueQuestion = "Do you want to continue?";

            public const string Error = "Something went wrong.\n\n";
        }

        public static class Status
        {
            public const string Success = "Success";
            public const string Error = "Error";
            public const string Warning = "Warning";
        }

        public static bool ValidateFile(string filename, string[] extentionTypes)
        {
            try
            {
                return extentionTypes.Contains(Path.GetExtension(filename).ToLower());
            }
            catch
            {
                throw;
            }
        }

        public static bool CheckFileExists(string filename)
        {
            try
            {
                return File.Exists(filename);
            }
            catch
            {
                throw;
            }
        }
    }
}
