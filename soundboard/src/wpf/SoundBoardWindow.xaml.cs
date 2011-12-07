using System;
using System.Windows;
using Microsoft.Win32;
using SoundBoard.Controller;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SoundBoardWindow : Window
    {
        #region Private properties
        private IBoardController mBoardController;
        private EditBoardWindow mEditBoardWindow;
        #endregion

        #region Constructor
        public SoundBoardWindow(IBoardController xiBoardController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;

            // Create a new board.
            mBoardController.New();

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
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                mBoardController.Load(openDialog.FileName);
            }
        }

        private void HandleMenuOptionSave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.FileName = "untitled";
            saveDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = saveDialog.ShowDialog();

            if (result == true)
            {
                mBoardController.Save(saveDialog.FileName);
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