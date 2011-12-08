using System;
using Microsoft.Win32;

namespace SoundBoard
{
    public class FileDialogController : IFileDialogController
    {
        #region Public properties
        public static FileDialogController StaticInstance
        {
            get
            {
                if (mStaticInstance == null)
                {
                    mStaticInstance = new FileDialogController();
                }

                return mStaticInstance;
            }
        }
        #endregion

        #region Static properties
        private static FileDialogController mStaticInstance;
        #endregion

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
        #endregion
    }
}
