using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperCrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CroppingFolderSelectionBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            CroppingFolderTextBox.Text = FolderPathHandling.ReturnFolderPath("Please select the folder full of images that need to be cropped.");
        }

        private void OutputFolderSelectionBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OutputFolderTextBox.Text = FolderPathHandling.ReturnFolderPath("Please select the folder that you wish to save the cropped images to.");
        }

        private void CroppingFolderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CropFolder cropFolder = new CropFolder(CroppingFolderTextBox.Text);
            FolderDescriptionTextBlock.Text = cropFolder.FolderDescriptionDialog;
            CropButton.IsEnabled = cropFolder.IsValidCropFolder;
            if (cropFolder.IsValidCropFolder)
            {
                Sliders sliders = new Sliders(cropFolder.AspectRatios[0]);
                UpdateSliderControlsToSliderObjects(sliders);
            }
        }

        private void UpdateSliderControlsToSliderObjects(Sliders sliders)
        {
            CropDimensionsOriginXSlider.Value = sliders.OriginX.Value;
            CropDimensionsOriginYSlider.Value = sliders.OriginY.Value;
            CropDimensionsDimensionsHeightSlider.Value = sliders.DimensionHeight.Value;
            CropDimensionsDimensionsWidthSlider.Value = sliders.DimensionWidth.Value;
            CropDimensionsOriginXSlider.Maximum = sliders.OriginX.Maximum;
            CropDimensionsOriginYSlider.Maximum = sliders.OriginY.Maximum;
            CropDimensionsDimensionsHeightSlider.Maximum = sliders.DimensionHeight.Maximum;
            CropDimensionsDimensionsWidthSlider.Maximum = sliders.DimensionWidth.Maximum;
            EnableSliders();
        } 

        private void EnableSliders()
        {
            CropDimensionsOriginXSlider.IsEnabled = true;
            CropDimensionsOriginYSlider.IsEnabled = true;
            CropDimensionsDimensionsHeightSlider.IsEnabled = true;
            CropDimensionsDimensionsWidthSlider.IsEnabled = true;
        }

        private void CropDimensionsOriginXSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            CropFolder cropFolder = new CropFolder(CroppingFolderTextBox.Text, FolderDescriptionTextBlock.Text);
        }

        private void CropDimensionsOriginYSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void CropDimensionsDimensionHeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void CropDimensionsDimensionWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
