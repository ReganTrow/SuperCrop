using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperCrop
{
    class FolderPathHandling
    {
        public static string ReturnFolderPath(string description)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = description;
            string folderSelection = "";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderSelection=folderBrowserDialog.SelectedPath;
            }
            return folderSelection;
        }
    }
}
