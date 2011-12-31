using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.Plugins.Sounds
{
    /// <summary>
    /// Interaction logic for SoundControlsBlock.xaml
    /// </summary>
    [Block("{D02FE220-E91F-47C1-962C-B5D2AE652356}")]
    public partial class SoundControlsBlock : UserControl
    {        
        #region Private properties
        private IMediaController mMediaController;
        #endregion

        #region Constructors
        public SoundControlsBlock()
        {
            mMediaController = MediaController.StaticInstance;
            Init();
        }

        public SoundControlsBlock(IMediaController xiMediaController)
        {
            mMediaController = xiMediaController;
            Init();
        }
        #endregion

        #region Initialization
        private void Init()
        {
            // Initialize.
            InitializeComponent();
        }
        #endregion

        #region Event handlers
        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {
            mMediaController.Stop();
        }

        private void HandleFadeButtonClick(object sender, RoutedEventArgs e)
        {
            mMediaController.Fade();
        }
        #endregion
    }
}
