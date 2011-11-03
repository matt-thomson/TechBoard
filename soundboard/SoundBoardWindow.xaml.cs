using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using SoundBoard.Model;
using System.Collections.ObjectModel;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SoundBoardWindow : Window
    {
        #region Private properties
        private Board Board { get; set; }
        #endregion

        #region Constructor
        public SoundBoardWindow()
        {
            InitializeComponent();
            Board = new Board();
            SoundsList.DataContext = Board;
        }
        #endregion

        #region Menu event handlers
        private void MenuOptionNew_Click(object sender, RoutedEventArgs e)
        {
            MediaElement.Stop();
            Board = new Board();
            SoundsList.DataContext = Board;
        }

        private void MenuOptionOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                MediaElement.Stop();
                Board = Board.Load(openDialog.FileName);
                SoundsList.DataContext = Board;
            }
        }

        private void MenuOptionSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.FileName = "untitled";
            saveDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = saveDialog.ShowDialog();

            if (result == true)
            {
                Board.Save(saveDialog.FileName);
            }
        }

        private void MenuOptionExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuOptionAddSound_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                string filename = openDialog.FileName;
                string title = filename.Split('\\').Last();

                Sound sound = new Sound(title, filename);
                Board.Sounds.Add(sound);
            }
        }
        #endregion

        #region Button event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Sound sound = button.DataContext as Sound;

            MediaElement.Stop();
            MediaElement.Source = new Uri(sound.FileName);
            MediaElement.Play();
        }
        #endregion
    }
}