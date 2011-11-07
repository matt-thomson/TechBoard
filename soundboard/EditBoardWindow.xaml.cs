using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using SoundBoard.Model;
using SoundBoard.Controller;

namespace SoundBoard
{
    /// <summary>
    /// Interaction logic for EditBoardWindow.xaml
    /// </summary>
    public partial class EditBoardWindow : Window
    {
        #region Private members
        private static EditBoardWindow mInstance;
        #endregion

        #region Constructors
        public EditBoardWindow()
        {
            InitializeComponent();
            SoundsList.DataContext = BoardHandler.Instance;
        }
        #endregion

        #region Static methods
        public static void Open()
        {
            if (mInstance == null)
            {
                mInstance = new EditBoardWindow();
            }

            mInstance.Show();
            mInstance.Activate();
        }
        #endregion

        #region Event handlers
        private void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            mInstance = null;
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

                Sound sound = new Sound(title, fileName);
                BoardHandler.Add(sound);
            }
        }

        private void HandleRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            Sound sound = SoundsList.SelectedItem as Sound;
            BoardHandler.Remove(sound);
        }
        #endregion
    }
}
