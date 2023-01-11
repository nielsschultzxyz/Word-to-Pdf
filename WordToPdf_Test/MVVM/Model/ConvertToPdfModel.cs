using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordToPdf_Test.Core;
using WordToPdf_Test.ErrorHandling;
using Aspose.Words;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Security.Policy;
using System.ComponentModel;

namespace WordToPdf_Test.MVVM.Model
{
    internal class ConvertToPdfModel : ObservableObject
    {
        /* TextBox update Text Eigenschaft -> FilePath sobald eine Datei geladen wird */
        private string fileName = "*Drag and Drop your wordfile";
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (FileName != value)
                {
                    fileName = value;
                    OnPopertyChanged(nameof(FileName));
                }
            }
        }
        
        /* RelayCommands */
        public RelayCommand ConvertToPdfCommand { get; set; }
        public RelayCommand LoadFileCommand { get; set; }

        /* Ctor ConvertModel*/
        public ConvertToPdfModel()
        {
            LoadFileCommand = new RelayCommand((o => {
                FileName = LoadFile();
                MessageBox.Show($"Filename: {FileName}");
            }));

            ConvertToPdfCommand = new RelayCommand((o => {
                if(!String.IsNullOrEmpty(FileName))
                {
                    Task.Run(() => {
                        ConvertToPdfFile(FileName);
                        FileName = "*Drag and Drop your wordfile";
                    });
                }
            }));
        }

        /* Methoden */
        public static void ConvertToPdfFile(string filename)
        {
            try
            {
                var doc = new Document(filename);
                var pdfName = $"{filename.Split('.')[0]}.pdf";

                doc.Save(pdfName);

                if (File.Exists(pdfName))
                {
                    MessageBox.Show("The File has been converted to pdf!");
                }
                else
                    MessageBox.Show("Something went wrong!");
            }
            catch (Exception ex)
            { 
                ErrorHandling.ErrorHandling.writeErrorLog(ex.Message);
            }
        }

        public string LoadFile()
        {
            string FilePath = "";
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                FilePath = ofd.FileName;

                if (checkIfDocTyp(FilePath))
                    MessageBox.Show("Can Convert Doc!");
                else
                    MessageBox.Show("Convert not possible!");
            }
            return FilePath;
        }

        private bool checkIfDocTyp(string doc)
        {
            bool check = false;
            string[] typ = doc.Split('.');

            foreach (string DateiTyp in typ)
            {
                if (DateiTyp == "doc" || DateiTyp == "docx")
                    check = true;
            }
            return check;
        }

    }
}
