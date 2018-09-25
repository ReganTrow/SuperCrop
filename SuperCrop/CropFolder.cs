using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SuperCrop
{
    class CropFolder
    {
        public List<AspectRatio> AspectRatios { get; set; }
        public string[] Filenames { get; set; }
        public int FileCount { get; set; }
        public int JPGCount { get; set; }
        public string FolderDescriptionDialog { get; set; }
        public bool IsValidCropFolder { get; set; }
        public string Path { get; set; }

        private static string NO_CROPPING_FOLDER_MESSAGE = "Please specify a cropping folder that exists.\n";
        private static string FILE_COUNT_MESSAGE = "There are {0} files in the specified folder, {1} of which are .jpg files.\n";
        private static string NO_JPG_MESSAGE = "As there are no .jpg files in the specified cropping folder, no cropping operations will be possible.\n";
        private static string ASPECT_RATIO_INTRODUCTION_MESSAGE = "The following aspect ratios (W X H) are present in images in the specified cropping folder:\n";
        private static string ASPECT_RATIO_LISTING_MESSAGE = "    - {0} x {1}\n";
        private static string TOO_MANY_ASPECT_RATIOS_MESSAGE = "Sorry, Super Crop only allows mass cropping on folders in which " +
                            "all the images have the same aspect ratios.\n";

        private static string JPG_FILE_EXTENSION = ".jpg";

        public CropFolder(string path)
        {
            if (Directory.Exists(path) == false)
            {
                AspectRatios = new List<AspectRatio>();
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

        // Constructor for initialising from FolderDescriptionTextBlock
        public CropFolder(string path, string folderDescriptionDialog)
        {
            Path = path;
            FolderDescriptionDialog = folderDescriptionDialog;
            if(Directory.Exists(Path)==false)
            {
                AspectRatios = new List<AspectRatio>();
                FileCount = 0;
                JPGCount = 0;
                IsValidCropFolder = false;
            }
            else
            {
                string[] folderDescriptionLines = FolderDescriptionDialog.Split(new[] { '\r', '\n' });
                
            }
        }

        // Initialises the JPGCount property - must only be called if the path is known to exist.
        private void InitialiseJPGCount()
        {
            int jpgCounter = 0;
            foreach (string filename in Filenames)
            {
                if (filename.EndsWith(JPG_FILE_EXTENSION))
                    jpgCounter++;
            }
            JPGCount = jpgCounter;
        }

        // Initialises the AspectRatios property - must only be called if the path is known to exist.
        private void InitialiseAspectRatios()
        {
            AspectRatios = new List<AspectRatio>();
            foreach (string filename in Filenames)
            {
                if (filename.EndsWith(JPG_FILE_EXTENSION))
                {
                    BitmapDecoder imageDecoder = BitmapDecoder.Create(
                        new Uri(filename, UriKind.Absolute),
                        BitmapCreateOptions.DelayCreation,
                        BitmapCacheOption.None);
                    BitmapFrame imageFrame = imageDecoder.Frames[0];
                    // The below lowers the aspect ratio using the greatest common factor of the image hieght and width.
                    int greatestCommonFactor = GreatestCommonFactor(imageFrame.PixelHeight, imageFrame.PixelWidth);
                    AspectRatio aspectRatio = new AspectRatio(
                        imageFrame.PixelHeight / greatestCommonFactor,
                        imageFrame.PixelWidth / greatestCommonFactor);
                    if (AspectRatios.Contains(aspectRatio) == false) 
                    {
                        AspectRatios.Add(aspectRatio);
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
            FolderDescriptionDialog = "\n";
            if (Directory.Exists(Path) == false)
                FolderDescriptionDialog += NO_CROPPING_FOLDER_MESSAGE;
            else
            {
                FolderDescriptionDialog += String.Format(FILE_COUNT_MESSAGE, FileCount, JPGCount);
                if (JPGCount == 0)
                {
                    FolderDescriptionDialog += NO_JPG_MESSAGE;
                }
                else
                {
                    FolderDescriptionDialog += ASPECT_RATIO_INTRODUCTION_MESSAGE;
                    foreach (var aspectRatio in AspectRatios)
                    {
                        FolderDescriptionDialog += String.Format(ASPECT_RATIO_LISTING_MESSAGE, aspectRatio.Width, aspectRatio.Height);
                    }
                    if (AspectRatios.Count > 1)
                    {
                        FolderDescriptionDialog += TOO_MANY_ASPECT_RATIOS_MESSAGE;
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
