using System.Windows;
using System.Windows.Controls;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for SoundButton.xaml
    /// </summary>
    public partial class SoundButton : Button
    {
        #region Private properties
        private Sound Sound { get; set; }
        #endregion

        #region Events
        public event SoundDelegate OnSoundButtonClick;
        #endregion

        #region Constructors
        public SoundButton(Sound xiSound)
        {
            InitializeComponent();
            Sound = xiSound;
            Content = Sound.Title;
            Click += HandleClick;
        }
        #endregion 

        #region Event handlers
        private void HandleClick(object sender, RoutedEventArgs e)
        {
            if (OnSoundButtonClick != null)
            {
                OnSoundButtonClick(Sound);
            }
        }
        #endregion
    }
}