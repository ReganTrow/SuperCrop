using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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

        // Checks the folder path and returns how many .jpg files it contains. Will return 0 if the folder does not exist.
        public static int GetJPGCount(string path)
        {
            if (Directory.Exists(path) == false)
            {
                return 0;
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
                return jpgCounter;
            }
        }


        // Checks the folder path and returns how many files and jpg files it contains. Will return 0, 0 if the folder does not exist.
        public static Tuple<int,int> GetFileBreakdown(string path)
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

        

        // Checks the folder path and returns a list with tuples of unique aspect ratios of images in the folder. 
        // Will return an empty list if the folder does not exist.
        public static List<Tuple<int,int>> GetAspectRatios(string path)
        {
            List<Tuple<int, int>> aspectRatioList = new List<Tuple<int, int>>();
            if (Directory.Exists(path) == false)
            {
                return aspectRatioList;
            }
            else
            {
                string[] filenameArray = Directory.GetFiles(path);
                foreach (string filename in filenameArray)
                {
                    if (filename.EndsWith(".jpg"))
                    {
                        BitmapDecoder imageDecoder = BitmapDecoder.Create(
                            new Uri(filename, UriKind.Absolute),
                            BitmapCreateOptions.DelayCreation,
                            BitmapCacheOption.None);
                            BitmapFrame imageFrame = imageDecoder.Frames[0];
                        // The below lowers the aspect ratio using the greatest common factor of the image hieght and width.
                        int greatestCommonFactor = GreatestCommonFactor(imageFrame.PixelHeight, imageFrame.PixelWidth);
                        Tuple<int, int> imageDimensions = Tuple.Create(
                            imageFrame.PixelHeight/greatestCommonFactor,
                            imageFrame.PixelWidth/greatestCommonFactor);
                        if (aspectRatioList.Contains(imageDimensions)==false)
                        {
                            aspectRatioList.Add(imageDimensions);
                        }
                    }
                }
                return aspectRatioList;
            }
        }

        // Returns the greatest common factor of the two inputs
        private static int GreatestCommonFactor(int a, int b)
        {
            while (a!=0 && b!=0)
            {
                if (a>b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                return b;
            else
                return a;
        }


    }
}
