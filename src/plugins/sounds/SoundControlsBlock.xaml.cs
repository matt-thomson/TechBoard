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
        #region Dependency properties
        private static DependencyProperty FadeDurationProperty = DependencyProperty.Register("FadeDuration",
                                                                                typeof(double),
                                                                                typeof(SoundControlsBlock));        
        #endregion

        #region Private properties
        private IMediaController mMediaController;
        #endregion

        #region Public properties
        [RangeBlockProperty(0, 5000, 250)]
        public double FadeDuration
        {
            get { return (double)GetValue(FadeDurationProperty); }
            set { SetValue(FadeDurationProperty, value); }
        }
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

            // Set up the default properties.
            FadeDuration = 3000;
        }
        #endregion

        #region Event handlers
        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {
            mMediaController.Stop();
        }

        private void HandleFadeButtonClick(object sender, RoutedEventArgs e)
        {
            mMediaController.Fade(FadeDuration);
        }
        #endregion
    }
}
