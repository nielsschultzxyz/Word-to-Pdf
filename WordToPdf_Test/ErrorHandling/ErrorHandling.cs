using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordToPdf_Test.ErrorHandling
{
    class ErrorHandling
    {
        public static string ErrorLogPath = @"..\..\errorLogFile.txt";
        public static void writeErrorLog(string msg)
        {
            using (StreamWriter writer = new StreamWriter(ErrorLogPath))
            {
                if (!File.Exists(ErrorLogPath))
                {
                    File.Create(ErrorLogPath);
                }

                writer.WriteLine($"ErrorLog ({DateTime.Now}):\nError Message:\n{msg}");
            }
        }
    }
}
