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

namespace TechBoard.Plugins.Time
{
    /// <summary>
    /// Interaction logic for ClockBlock.xaml
    /// </summary>
    [Block("{315E02FB-E665-4516-B6E8-871A540B747C}")]
    public partial class ClockBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty TimeProperty = DependencyProperty.Register("Time",
                                                                                     typeof(string),
                                                                                     typeof(ClockBlock));
        #endregion

        #region Private members
        private ITimerController mTimerController;

        private string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        #endregion

        #region Constructors
        public ClockBlock()
        {
            mTimerController = TimerController.StaticInstance;
            Init();
        }

        public ClockBlock(ITimerController xiTimerController)
        {
            mTimerController = xiTimerController;
            Init();
        }
        #endregion

        #region Initialization
        public void Init()
        {
            // Initialize.
            InitializeComponent();

            // Register for timer pops.
            mTimerController.TimerPop += HandleTimerPop;

            // Set the initial time.
            Time = mTimerController.Now.ToString("T");
        }
        #endregion

        #region Event handlers
        private void HandleTimerPop()
        {
            Time = mTimerController.Now.ToString("T");
        }
        #endregion
    }
}
