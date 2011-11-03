using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SoundBoardWindow : Window
    {
        #region Constructor
        public SoundBoardWindow()
        {
            InitializeComponent();
            SoundsList.DataContext = BoardHandler.Instance;
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
            MediaElement.Stop();
            BoardHandler.New();
        }

        private void HandleMenuOptionOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                MediaElement.Stop();
                BoardHandler.Load(openDialog.FileName);
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
                BoardHandler.Save(saveDialog.FileName);
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

        #region Button event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Sound sound = button.DataContext as Sound;

            MediaElement.Stop();
            MediaElement.Source = new Uri(sound.FileName);
            MediaElement.Volume = sound.Volume;
            MediaElement.Play();
        }
        #endregion
    }
}