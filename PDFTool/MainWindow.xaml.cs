using Microsoft.Win32;
using PDFTool.BLLs;
using System;
using System.Windows;

namespace PDFTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".pdf";
                //dialog.Filter = "Image files (*.png;*.jpg)|Pdf Files (*.pdf)";

                if (dialog.ShowDialog() == true)
                {
                    //string filepath = dialog.FileName;
                    //string filename = filepath.Split('\\').Last();
                    filenameTextbox.Text = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, "Error");
            }
        }

        private void ShrinkPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string filefullname = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".pdf" };
                if (!Utils.ValidateFile(filefullname, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), "Warning");
                    return;
                }

                // shrink
                CompressPDF bll = new CompressPDF();
                if (bll.ShrinkPDF(filefullname))
                {
                    MessageBox.Show(Utils.Messages.SuccessConvert, "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, "Error");
            }
        }

        private void ImageToPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string imagefullname = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".jpg", ".png" };
                if (!Utils.ValidateFile(imagefullname, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), "Warning");
                    return;
                }

                // convert
                ConvertToPDF bll = new ConvertToPDF();
                if (bll.GeneratePDF(imagefullname))
                {
                    MessageBox.Show(Utils.Messages.SuccessConvert, "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, "Error");
            }
        }

        private void PDFToImage(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string filefullname = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".pdf" };
                if (!Utils.ValidateFile(filefullname, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), "Warning");
                    return;
                }

                // get selected radio button
                string fileType = ".bmp"; //set default value
                if ((bool)pngRadioBtn.IsChecked)
                {
                    fileType = ".png";
                }
                else if ((bool)jpgRadioBtn.IsChecked)
                {
                    fileType = ".jpg";
                }

                int dpi = 200; //set default value
                if ((bool)dpi300RadioBtn.IsChecked)
                {
                    dpi = 300;
                }
                else if ((bool)dpi600RadioBtn.IsChecked)
                {
                    dpi = 600;
                }

                // convert
                ConvertToImage bll = new ConvertToImage();
                if (bll.GenerateImage(filefullname, fileType, dpi))
                {
                    MessageBox.Show(Utils.Messages.SuccessConvert, "Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, "Error");
            }
        }
    }
}
