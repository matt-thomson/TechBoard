using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for EditBoardWindow.xaml
    /// </summary>
    public partial class EditBoardWindow : Window
    {
        #region Private members
        private IBoardController mBoardController;
        #endregion

        #region Constructors
        public EditBoardWindow(IBoardController xiBoardController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;

            // Set the data context for this window.
            BlocksList.DataContext = mBoardController;
        }
        #endregion

        #region Event handlers
        private void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void HandleAddButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                string fileName = openDialog.FileName;
                string[] fileNameSplit = fileName.Split('\\');
                string title = fileNameSplit[fileNameSplit.Length - 1];

                SoundBlock sound = new SoundBlock(title, fileName);
                mBoardController.Add(sound);
            }
        }

        private void HandleRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            SoundBlock sound = BlocksList.SelectedItem as SoundBlock;
            mBoardController.Remove(sound);
        }
        #endregion
    }
}
