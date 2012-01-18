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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TechBoard.Plugins.PowerPoint
{
    /// <summary>
    /// Interaction logic for SlideControlsBlock.xaml
    /// </summary>
    [Block("{EE41819D-AE47-4CEA-A31C-DC96CA9D1E7D}")]
    public partial class SlideControlsBlock : UserControl
    {
        #region Private properties
        private IPowerPointController mPowerPointController;
        #endregion

        #region Constructors
        public SlideControlsBlock()
        {
            mPowerPointController = PowerPointController.StaticInstance;
            Init();
        }

        public SlideControlsBlock(IPowerPointController xiPowerPointController)
        {
            mPowerPointController = xiPowerPointController;
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
        private void HandlePreviousButtonClick(object sender, RoutedEventArgs e)
        {
            mPowerPointController.PreviousSlide();
        }

        private void HandleNextButtonClick(object sender, RoutedEventArgs e)
        {
            mPowerPointController.NextSlide();
        }

        private void HandleTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int index = Convert.ToInt32(SlideNumber.Text);
                    mPowerPointController.GoToSlide(index);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }

                SlideNumber.Text = string.Empty;
            }
        }
        #endregion
    }
}
