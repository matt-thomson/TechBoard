using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SoundBoardWindow : Window
    {
        #region Private properties
        private IMediaController mMediaController;
        #endregion

        #region Constructor
        public SoundBoardWindow(MediaController xiMediaController)
        {
            // Initialize.
            InitializeComponent();
            mMediaController = xiMediaController;

            // Create and add the sound block.
            SoundBlock block = new SoundBlock(mMediaController);
            mDockPanel.Children.Add(block);
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
            mMediaController.Stop();
            BoardController.New();
        }

        private void HandleMenuOptionOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                mMediaController.Stop();
                BoardController.Load(openDialog.FileName);
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
                BoardController.Save(saveDialog.FileName);
            }
        }

        private void HandleMenuOptionExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleMenuOptionEditor(object sender, RoutedEventArgs e)
        {
            EditBoardWindow.Open();
        }
        #endregion
    }
}