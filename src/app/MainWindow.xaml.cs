using System;
using System.Windows;
using Microsoft.Win32;

namespace SoundBoard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        private const string FILTER = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";
        #endregion

        #region Private properties
        private IBoardController mBoardController;
        private IFileDialogController mFileDialogController;
        private EditBoardWindow mEditBoardWindow;
        #endregion

        #region Constructor
        public MainWindow(IBoardController xiBoardController,
                          IFileDialogController xiFileDialogController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;
            mFileDialogController = xiFileDialogController;

            // Set the data context.
            DataContext = mBoardController;

            // Create the edit board window.
            mEditBoardWindow = new EditBoardWindow(mBoardController);
        }
        #endregion

        #region Window event handlers
        private void HandleWindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Menu event handlers
        private void HandleMenuOptionNew(object sender, RoutedEventArgs e)
        {
            mBoardController.New();
        }

        private void HandleMenuOptionOpen(object sender, RoutedEventArgs e)
        {
            string filename = mFileDialogController.OpenFile(FILTER);

            if (filename != null)
            {
                mBoardController.Load(filename);
            }
        }

        private void HandleMenuOptionSave(object sender, RoutedEventArgs e)
        {
            string filename = mFileDialogController.SaveFile(FILTER);

            if (filename != null)
            {
                mBoardController.Save(filename);
            }
        }

        private void HandleMenuOptionExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleMenuOptionEditor(object sender, RoutedEventArgs e)
        {
            mEditBoardWindow.Show();
            mEditBoardWindow.Activate();
        }
        #endregion
    }
}