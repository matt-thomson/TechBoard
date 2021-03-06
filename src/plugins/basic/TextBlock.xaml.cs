﻿/*
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

namespace TechBoard.Plugins.Basic
{
    /// <summary>
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    [Block("{6A2910EE-BDD9-453A-87F4-7994AF4223AD}")]
    public partial class TextBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty TextProperty = DependencyProperty.Register("Text",
                                                                                     typeof(string),
                                                                                     typeof(TextBlock));
        #endregion

        #region Public properties
        [TextBlockProperty]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Constructors
        public TextBlock()
        {
            // Initialize.
            InitializeComponent();

            // Set up the default properties.
            Text = "New Text";
        }
        #endregion
    }
}
