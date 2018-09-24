using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperCrop
{
    class FolderDescriptionHandling
    {
        public static string DescriptionText(string path)
        {
            if (Directory.Exists(path)==false)
            {
                return "Please specify a cropping folder that exists.";
            }
            else
            {
                string descriptionString = "";
                descriptionString += jpgCountText(path);
                descriptionString += jpgAspectRatioText(path);
                return descriptionString;
            }
        }

        private static string jpgCountText(string path)
        {
            Tuple <int,int> fileCountTuple = FolderCheck.GetJPGFileCount(path);
            return String.Format("There are {0} files in the specified folder, {1} of which are .jpg files. \n",
                fileCountTuple.Item1,
                fileCountTuple.Item2);
        }

        private static string jpgAspectRatioText(string path)
        {
            return "";
        }
    }
}
