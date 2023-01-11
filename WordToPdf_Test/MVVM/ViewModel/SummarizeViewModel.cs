using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WordToPdf_Test.Core;
using WordToPdf_Test.MVVM.Model;

namespace WordToPdf_Test.MVVM.ViewModel
{
    class SummarizeViewModel : ObservableObject
    {
        // prop
        public MergePdfsModel PdfsModel { get; set; }
        
        private ObservableCollection<MergePdfsModel> _mergePdfsList { get; set; }
        public ObservableCollection<MergePdfsModel> MergePdfsList
        {
            get { return _mergePdfsList; }
            set 
            {
                _mergePdfsList = value;
                OnPopertyChanged();
            }
        }

        // RalayCommands
        public RelayCommand AddPdfCommand { get; set; }
        public RelayCommand MergePdfsCommand { get; set; }
        public RelayCommand LoadIntoListViewCommand { get; set; }
        public RelayCommand RemoveItemCommand { get; set; }

        // ctor
        public SummarizeViewModel()
        {
            PdfsModel = new MergePdfsModel("*Drag and Drop your Pdfs", "");
            MergePdfsList = new ObservableCollection<MergePdfsModel>();

            // Add a new MergePdfModel into the ListView
            AddPdfCommand = new RelayCommand(o => {
                var filepath = MergePdfsModel.LoadPdfFile();
                PdfsModel.FilePath = filepath;
                
                // set the file in terms to add it into the ListView -> prop change FileName (Filepath) the name should be shown
                // after setListViewText(Filepath) return the name
                MergePdfsList.Add(new MergePdfsModel(filepath, setListViewItemText(filepath)));
                PdfsModel.FilePath = $"{filepath}\n\nDrag and drop another pdf";
                
            });

            Task.Run(() => {
                MergePdfsCommand = new RelayCommand(o => {
                    string[] pdfs = new string[MergePdfsList.Count];
                    var counter = 0;

                    foreach(MergePdfsModel pdfModel in MergePdfsList)
                    {
                        pdfs[counter] = pdfModel.FilePath;
                        counter++;
                    }

                    MergePdfsModel.MergePdfs($"{MergePdfsModel.SaveFilePath(PdfsModel.FilePath)}MergePdfs.pdf", pdfs);

                    MergePdfsList.Clear();
                    PdfsModel.FilePath = "Drag and drop your pdfs";
                });
            });

            LoadIntoListViewCommand = new RelayCommand(o => {
                var filepath = PdfsModel.FilePath;

                MergePdfsList.Add(new MergePdfsModel(filepath, setListViewItemText(filepath)));
                PdfsModel.FilePath = $"{filepath}\nDrag and drop another pdf";
            });

            // TODO Button isnt working -> ListViewTheme Command binding? / ControllTemplate Button?
            // Write a extion for the button? is it possible for ListViewItemContainer? 
            RemoveItemCommand = new RelayCommand(o => {
                removeListViewItem(PdfsModel.FileName, MergePdfsList);
                MessageBox.Show("Passiert hier was");
            });
        }

        static string setListViewItemText(string filepath)
        {
            string[] splitFile = filepath.Split('\\');
            return splitFile[splitFile.Length - 1];
        }

        static void removeListViewItem(string filename, ObservableCollection<MergePdfsModel> listview)
        {
            if (listview != null)
            {
                foreach (MergePdfsModel pdf in listview)
                {
                    if (pdf.FileName == filename)
                        listview.Remove(pdf);
                }
            }
        }
    }
}
