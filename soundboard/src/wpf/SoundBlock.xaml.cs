using System.Windows.Controls;
using System.Windows;
using SoundBoard.Model;
using SoundBoard.Controller;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundBlock.xaml
    /// </summary>
    public partial class SoundBlock : ListView
    {
        #region Private properties
        IMediaController mMediaController;
        #endregion

        public SoundBlock(IMediaController xiMediaController)
        {
            // Initialize.
            InitializeComponent();
            mMediaController = xiMediaController;

            // Set the data context for this block.
            DataContext = BoardController.Instance;
        }

        #region Button event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Sound sound = button.DataContext as Sound;
            mMediaController.Play(sound.FileName, sound.Volume);
        }
        #endregion
    }
}
