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
        #region Private properties
        private Board Board { get; set; }
        #endregion

        #region Constructor
        public SoundBoardWindow()
        {
            InitializeComponent();
            Board = new Board();
        }
        #endregion

        #region Menu event handlers
        private void MenuOptionNew_Click(object sender, RoutedEventArgs e)
        {
            Board = new Board();
            ClearGrid();
        }

        private void MenuOptionOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            openDialog.Filter = "SoundBoards (*.board)|*.board|All files (*.*)|*.*";

            Nullable<bool> result = openDialog.ShowDialog();

            if (result == true)
            {
                Board = Board.Load(openDialog.FileName);
            }

            ClearGrid();

            foreach (Sound sound in Board.Sounds)
            {
                AddSoundToGrid(sound);
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
                AddSoundToGrid(sound);
            }
        }
        #endregion

        #region Button event handlers
        private void HandleSoundButtonClick(Sound xiSound)
        {
            MediaElement.Stop();
            MediaElement.Source = new Uri(xiSound.FileName);
            MediaElement.Play();
        }
        #endregion

        #region Private methods
        private void AddSoundToGrid(Sound xiSound)
        {
            SoundButton soundButton = new SoundButton(xiSound);
            soundButton.OnSoundButtonClick += HandleSoundButtonClick;

            // Add a row to the grid.
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(50);
            mGrid.RowDefinitions.Add(row);

            // Put a button in the new row.
            Grid.SetColumn(soundButton, 0);
            Grid.SetRow(soundButton, mGrid.RowDefinitions.Count - 1);
            mGrid.Children.Add(soundButton);
        }

        private void ClearGrid()
        {
            MediaElement.Stop();
            mGrid.Children.Clear();
            mGrid.RowDefinitions.Clear();
        }
        #endregion
    }
}