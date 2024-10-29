using Microsoft.Win32;
using PDFTool.BLLs;
using System;
using System.Windows;
using System.Windows.Controls;

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

        #region select file
        private void SelectFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".pdf";
                dialog.Multiselect = true;

                if (dialog.ShowDialog() == true)
                {
                    filenameTextbox.Text = string.Join(";", dialog.FileNames);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, Utils.Status.Error);
            }
        }

        //source: https://stackoverflow.com/questions/4281857/wpf-drag-and-drop-to-a-textbox
        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        //source: https://stackoverflow.com/questions/4281857/wpf-drag-and-drop-to-a-textbox
        //source: https://www.codeproject.com/Articles/42696/Textbox-Drag-Drop-in-WPF
        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            object text = e.Data.GetData(DataFormats.FileDrop);
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = string.Format("{0}", ((string[])text)[0]);
            }
        }

        //source: https://stackoverflow.com/questions/4281857/wpf-drag-and-drop-to-a-textbox
        private void TextBox_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        private void ShrinkPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string filenames = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".pdf" };
                if (!Utils.ValidateFiles(filenames, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), Utils.Status.Error);
                    return;
                }

                // shrink
                string[] filenamesArr = filenames.Split(';');
                CompressPDF bll = new CompressPDF();

                foreach (string filename in filenamesArr)
                {
                    bll.ShrinkPDF(filename);
                }

                MessageBox.Show(Utils.Messages.Success, Utils.Status.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, Utils.Status.Error);
            }
        }

        private void ImageToPDF(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string filenames = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".jpg", ".png" };
                if (!Utils.ValidateFiles(filenames, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), Utils.Status.Error);
                    return;
                }

                // convert
                string[] filenamesArr = filenames.Split(';');
                ConvertToPDF bll = new ConvertToPDF();
                bll.GeneratePDF(filenamesArr);

                MessageBox.Show(Utils.Messages.Success, Utils.Status.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, Utils.Status.Error);
            }
        }

        private void PDFToImage(object sender, RoutedEventArgs e)
        {
            try
            {
                // validation
                string filenames = filenameTextbox.Text;
                string[] extentionTypes = new string[] { ".pdf" };
                if (!Utils.ValidateFiles(filenames, extentionTypes))
                {
                    MessageBox.Show(Utils.Messages.IncorrectFile + string.Format(Utils.Messages.AcceptedFileList, string.Join(", ", extentionTypes)), Utils.Status.Error);
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
                string[] filenamesArr = filenames.Split(';');
                ConvertToImage bll = new ConvertToImage();

                foreach (string filename in filenamesArr)
                {
                    bll.GenerateImage(filenames, fileType, dpi);
                }

                MessageBox.Show(Utils.Messages.Success, Utils.Status.Success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Utils.Messages.Error + ex.Message, Utils.Status.Error);
            }
        }
    }
}
