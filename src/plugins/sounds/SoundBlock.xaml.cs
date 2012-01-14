/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

using System.Windows;
using System.Windows.Controls;

namespace TechBoard.Plugins.Sounds
{
    /// <summary>
    /// Interaction logic for SoundBlock.xaml
    /// </summary>
    [Block("{13EBEAD3-3B03-4897-AFB9-3238632A3735}")]
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
        private IMediaController mMediaController;
        #endregion

        #region Public properties
        [FileBlockProperty("Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*")]
        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        [TextBlockProperty]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        [RangeBlockProperty(0.0, 1.0, 0.1)]
        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }
        #endregion

        #region Constructors
        public SoundBlock()
        {
            mMediaController = MediaController.StaticInstance;
            Init();
        }

        public SoundBlock(IMediaController xiMediaController)
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
            FileName = "";
            Title = "New Sound";
            Volume = 0.5;
        }
        #endregion

        #region Event handlers
        private void HandleSoundButtonClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                mMediaController.Play(this);
            }
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            // If this sound is currently playing, then stop it.
            if (mMediaController.CurrentSoundBlock == this)
            {
                mMediaController.Stop();
            }
        }
        #endregion
    }
}
