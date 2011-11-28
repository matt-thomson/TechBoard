using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundBlockView.xaml
    /// </summary>
    public partial class SoundBlockView : UserControl
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
            SoundBlock soundBlock = DataContext as SoundBlock;
            MediaController.Play(soundBlock.FileName, soundBlock.Volume);
        }
        #endregion
    }
}
