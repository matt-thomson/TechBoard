using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundBlock.xaml
    /// </summary>
    public partial class SoundBlock : ListView
    {
        #region Private properties
        IBoardController mBoardController;
        IMediaController mMediaController;
        #endregion

        public SoundBlock(IBoardController xiBoardController,
                          IMediaController xiMediaController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;
            mMediaController = xiMediaController;

            // Set the data context for this block.
            DataContext = mBoardController;
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
