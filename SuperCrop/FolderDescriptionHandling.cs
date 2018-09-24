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
                string descriptionString = "\n";
                descriptionString += jpgCountText(path);
                descriptionString += jpgAspectRatioText(path);
                return descriptionString;
            }
        }

        private static string jpgCountText(string path)
        {
            Tuple <int,int> fileCountTuple = FolderCheck.GetFileBreakdown(path);
            return String.Format("There are {0} files in the specified folder, {1} of which are .jpg files.\n",
                fileCountTuple.Item1,
                fileCountTuple.Item2);
        }

        private static string jpgAspectRatioText(string path)
        {
            if (FolderCheck.GetJPGCount(path)==0)
            {
                return "As there are no .jpg files in the specified cropping folder, no cropping operations will be possible.\n";
            }
            else
            {
                String outputString = "The following aspect ratios (H X W) are present in images in the specified cropping folder:\n";
                foreach (var aspectRatio in FolderCheck.GetAspectRatios(path))
                {
                    outputString += String.Format("    - {0} x {1}\n", aspectRatio.Item1, aspectRatio.Item2);
                }
                return outputString;
            }
        }
    }
}
