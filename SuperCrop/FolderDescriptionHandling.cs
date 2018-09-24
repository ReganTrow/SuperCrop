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
        // Returns a string with the DescriptionText and a boolean representing whether the folder is crop-able.
        public static Tuple<string, Boolean> DescriptionText(string path)
        {
            if (Directory.Exists(path)==false)
            {
                return Tuple.Create("Please specify a cropping folder that exists.", false);
            }
            else
            {
                string descriptionString = "\n";
                descriptionString += jpgCountText(path);
                Tuple<string, Boolean> jpgAspectRatioTextTuple = jpgAspectRatioText(path);
                descriptionString += jpgAspectRatioTextTuple.Item1;
                return Tuple.Create(descriptionString,jpgAspectRatioTextTuple.Item2);
            }
        }

        private static string jpgCountText(string path)
        {
            Tuple <int,int> fileCountTuple = FolderCheck.GetFileBreakdown(path);
            return String.Format("There are {0} files in the specified folder, {1} of which are .jpg files.\n",
                fileCountTuple.Item1,
                fileCountTuple.Item2);
        }

        // Returns a string with the jpgAspectRatioText and a boolean representing whether the folder is crop-able based on the amount of .jpg files
        // and the aspect ratios.
        private static Tuple<string,Boolean> jpgAspectRatioText(string path)
        {
            if (FolderCheck.GetJPGCount(path)==0)
            {
                return Tuple.Create(
                    "As there are no .jpg files in the specified cropping folder, no cropping operations will be possible.\n",
                    false);
            }
            else
            {
                List<Tuple<int, int>> aspectRatioList = FolderCheck.GetAspectRatios(path);
                String outputString = "The following aspect ratios (H X W) are present in images in the specified cropping folder:\n";
                foreach (var aspectRatio in aspectRatioList)
                {
                    outputString += String.Format("    - {0} x {1}\n", aspectRatio.Item1, aspectRatio.Item2);
                }
                if (aspectRatioList.Count > 1)
                {
                    outputString += "Sorry, Super Crop only allows mass cropping on folders in which all the images have the same aspect ratios.\n";
                    return Tuple.Create(outputString, false);
                }
                else
                {
                    return Tuple.Create(outputString, true);
                }
                    

                
            }
        }
    }
}
