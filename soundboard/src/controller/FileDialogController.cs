using System;
using Microsoft.Win32;

namespace SoundBoard.Controller
{
    public class FileDialogController : IFileDialogController
    {
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
    }
}
