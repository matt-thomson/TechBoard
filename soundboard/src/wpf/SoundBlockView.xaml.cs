using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundBlockView.xaml
    /// </summary>
    public partial class SoundBlockView : ListView
    {
        #region Private properties
        public static IMediaController MediaController;
        #endregion

        public SoundBlockView()
        {
            // Initialize.
            InitializeComponent();
        }

        #region Button event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Sound sound = button.DataContext as Sound;
            MediaController.Play(sound.FileName, sound.Volume);
        }
        #endregion
    }
}
