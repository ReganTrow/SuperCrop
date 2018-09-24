using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCrop
{
    class FolderCheck
    {
        // Checks the folder path and returns how many files it contains. Will return 0 if the folder does not exist.
        public static int GetFileCount(string path)
        {
            if (Directory.Exists(path) == false)
            {
                return 0;
            }
            else
            {
                string[] filenameArray = Directory.GetFiles(path);
                return filenameArray.Length;
            }
        }


        // Checks the folder path and returns how many files and jpg files it contains. Will return 0, 0 if the folder does not exist.
        public static Tuple<int,int> GetJPGFileCount(string path)
        {
            if (Directory.Exists(path)==false)
            {
                return Tuple.Create(0,0);
            }
            else
            {
                string[] filenameArray = Directory.GetFiles(path);
                int jpgCounter = 0;
                foreach (string filename in filenameArray)
                {
                    if (filename.EndsWith(".jpg"))
                    {
                        jpgCounter++;
                    }
                }
                return Tuple.Create(filenameArray.Length,jpgCounter);
            }
        }        
    }
}
