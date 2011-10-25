using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    public class SoundButton : Button
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
            Sound = xiSound;

            Content = Sound.Title;
            FontSize = 24;
            Width = 494;

            Click += HandleClick;
        }
        #endregion 

        #region Event handlers
        private void HandleClick(object sender, RoutedEventArgs e)
        {
            OnSoundButtonClick(Sound);
        }
        #endregion
    }
}