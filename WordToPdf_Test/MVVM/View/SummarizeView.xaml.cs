using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using WordToPdf_Test.MVVM.ViewModel;

namespace WordToPdf_Test.MVVM.View
{
    /// <summary>
    /// Interaktionslogik für SummarizeView.xaml
    /// </summary>
    public partial class SummarizeView : UserControl
    {
        public SummarizeView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (files != null && files.Length > 0)
                {
                    ((TextBox)sender).Text = files[0];
                }
            }
        }

        private void TextBox_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
