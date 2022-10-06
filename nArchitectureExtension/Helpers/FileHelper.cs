using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nArchitectureExtension.Helpers
{
    public static class FileHelper
    {
        public static void FileCreate(string directoryPath, string filePath, string text)
        {
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            if (!File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(text);
                    }
                }
            }
        }
    }
}
