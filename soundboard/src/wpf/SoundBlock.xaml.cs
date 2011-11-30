using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundBlock.xaml
    /// </summary>
    public partial class SoundBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty TitleProperty = DependencyProperty.Register("Title",
                                                                                      typeof(string),
                                                                                      typeof(SoundBlock));
        private static DependencyProperty VolumeProperty = DependencyProperty.Register("Volume",
                                                                                       typeof(double),
                                                                                       typeof(SoundBlock));
        private static DependencyProperty FileNameProperty = DependencyProperty.Register("FileName",
                                                                                         typeof(string),
                                                                                         typeof(SoundBlock));
        #endregion

        #region Private properties
        public static IMediaController MediaController;
        #endregion

        #region Public properties
        [FileNameEditorProperty]
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        [TextEditorProperty]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        [RangeEditorProperty]
        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }
        #endregion

        public SoundBlock()
        {
            // Initialize.
            InitializeComponent();

            // Set up the default properties.            
            FileName = "";
            Title = "New Sound";
            Volume = 0.5;
        }

        #region Button event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            MediaController.Play(FileName, Volume);
        }
        #endregion
    }
}
