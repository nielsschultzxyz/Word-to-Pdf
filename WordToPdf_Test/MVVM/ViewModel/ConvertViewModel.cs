using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordToPdf_Test.MVVM.Model;
using WordToPdf_Test.Core;

namespace WordToPdf_Test.MVVM.ViewModel
{
    class ConvertViewModel : ObservableObject
    {
        public ConvertToPdfModel ModelConvertToPdf { get; set; }

        public ConvertViewModel()
        {
            ModelConvertToPdf = new ConvertToPdfModel();    
        }
    }
}
