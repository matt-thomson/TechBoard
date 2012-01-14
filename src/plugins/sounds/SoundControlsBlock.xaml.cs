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
