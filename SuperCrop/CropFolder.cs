using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperCrop
{
    class CropFolder
    {
        public List<Tuple<int,int>> AspectRatios { get; set; }
        public string[] Filenames { get; set; }
        public int FileCount { get; set; }
        public int JPGCount { get; set; }
        public string FolderDescriptionDialog { get; set; }
        public Boolean IsValidCropFolder { get; set; }
        public string Path { get; set; }

        public CropFolder(string path)
        {
            if (Directory.Exists(path) == false)
            {
                AspectRatios = new List<Tuple<int, int>>();
                Path = path;
                FileCount = 0;
                JPGCount = 0;
                InitialiseFolderDescriptionDialog();
                IsValidCropFolder = false;
            }
            else
            {
                Path = path;
                Filenames = Directory.GetFiles(path);
                FileCount = Filenames.Length;
                InitialiseJPGCount();
                InitialiseAspectRatios();
                InitialiseIsValidCropFolder();
                InitialiseFolderDescriptionDialog();
            }
        }

        // Initialises the JPGCount property - must only be called if the path is known to exist.
        private void InitialiseJPGCount()
        {
            int jpgCounter = 0;
            foreach (string filename in Filenames)
            {
                if (filename.EndsWith(".jpg"))
                    jpgCounter++;
            }
            JPGCount = jpgCounter;
        }

        // Initialises the AspectRatios property - must only be called if the path is known to exist.
        private void InitialiseAspectRatios()
        {
            AspectRatios = new List<Tuple<int, int>>();
            foreach (string filename in Filenames)
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
                        imageFrame.PixelHeight / greatestCommonFactor,
                        imageFrame.PixelWidth / greatestCommonFactor);
                    if (AspectRatios.Contains(imageDimensions) == false)
                    {
                        AspectRatios.Add(imageDimensions);
                    }
                }
            }
        }

        private void InitialiseIsValidCropFolder()
        {
            IsValidCropFolder = (JPGCount > 0 && AspectRatios.Count == 1 && Directory.Exists(Path)) ? true : false;             
        }

        private void InitialiseFolderDescriptionDialog()
        {
            if (Directory.Exists(Path) == false)
                FolderDescriptionDialog = "\nPlease specify a cropping folder that exists.\n";
            else
            {
                FolderDescriptionDialog = String.Format("There are {0} files in the specified folder, {1} of which are .jpg files.\n",
                    FileCount,
                    JPGCount);
                if (JPGCount == 0)
                {
                    FolderDescriptionDialog+="As there are no .jpg files in the specified cropping folder, no cropping operations will be possible.\n";
                }
                else
                {
                    FolderDescriptionDialog += "The following aspect ratios (H X W) are present in images in the specified cropping folder:\n";
                    foreach (var aspectRatio in AspectRatios)
                    {
                        FolderDescriptionDialog += String.Format("    - {0} x {1}\n", aspectRatio.Item1, aspectRatio.Item2);
                    }
                    if (AspectRatios.Count > 1)
                    {
                        FolderDescriptionDialog += "Sorry, Super Crop only allows mass cropping on folders in which " +
                            "all the images have the same aspect ratios.\n";
                    }
                }
            }
        }

        // Returns the greatest common factor of the two inputs
        private static int GreatestCommonFactor(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
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
