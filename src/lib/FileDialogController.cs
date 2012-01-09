using System;
using Microsoft.Win32;

namespace TechBoard
{
    public class FileDialogController : IFileDialogController
    {
        #region Public methods
        public string OpenFile(string xiFilter)
        {
            string filename = null;

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = xiFilter;

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                filename = openDialog.FileName;
            }

            return filename;
        }

        public string SaveFile(string xiFilter)
        {
            string filename = null;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = xiFilter;
            saveDialog.FileName = "untitled";

            Nullable<bool> result = saveDialog.ShowDialog();

            if (result == true)
            {
                filename = saveDialog.FileName;
            }

            return filename;
        }
        #endregion
    }
}
