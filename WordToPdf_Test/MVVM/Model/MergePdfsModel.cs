using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordToPdf_Test.Core;
using WordToPdf_Test.ErrorHandling;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Collections.ObjectModel;

namespace WordToPdf_Test.MVVM.Model
{
    class MergePdfsModel : ObservableObject
    {
        public string FileName { get; set; }

        private string filePath = "*Drag and Drop your wordfile";
        public string FilePath
        {
            get { return filePath; }
            set
            {
                if (FilePath != value)
                {
                    filePath = value;
                    OnPopertyChanged(nameof(FilePath));
                }
            }
        }

        public MergePdfsModel(string filePath, string filename)
        {
            FilePath = filePath;
            FileName= filename;
        }

        // Load a Pdf into the app (FileDialog)
        public static string LoadPdfFile()
        {
            string path = "";
            OpenFileDialog ofd = new OpenFileDialog();

            if(ofd.ShowDialog() == true)
            {
                path = ofd.FileName;

                if (File.Exists(path) && !String.IsNullOrEmpty(path) && checkIfFileIsPdf(path))
                {
                    MessageBox.Show($"Pdf: {path} loaded!");
                }
                else
                    MessageBox.Show("File has to been Pdf!");
            }
            return path;
        }

        /*
         * Mergepdf takes every filepath that is current in the ListView and merge them into a new file (mergedpdf)
         * The file will be saved in the same dir
        */
        public static void MergePdfs(string targetPath, string[] pdfFiles)
        {
            try
            {
                PdfDocument document = new PdfDocument();

                foreach (string pdf in pdfFiles)
                {
                    PdfDocument inputPdfDocument = PdfReader.Open(pdf, PdfDocumentOpenMode.Import);
                    document.Version = inputPdfDocument.Version;

                    foreach (PdfPage page in inputPdfDocument.Pages)
                    {
                        document.AddPage(page);
                    }
                }
                document.Save(targetPath);
                MessageBox.Show($"File has been merged and saved");
            }
            catch(Exception ex)
            {
                ErrorHandling.ErrorHandling.writeErrorLog(ex.Message);
            }
        }

        // returns the filepaht without the pdf path, so the new filename can be set (mergedpdf)
        public static string SaveFilePath(string filename)
        {
            string[] pathSplitArr = filename.Split("\\");
            var newpath = "";

            for (int i = 0; i < pathSplitArr.Length; i++)
            {
                newpath += pathSplitArr[i];
            }
            newpath += "\\";
            MessageBox.Show(newpath);

            return filename.Split('.')[0];
        }

        // checks if the selected/choosen file is a pdf
        static bool checkIfFileIsPdf(string filepath)
        {
            var check = false;
            var filetyp = "";

            if (File.Exists(filepath))
            {
                filetyp = filepath.Split(".")[1];

                if(filetyp == "pdf")
                    check = true;
            }
            return check;
        }
    }
}
