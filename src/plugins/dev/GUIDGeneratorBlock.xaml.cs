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
using System;

namespace TechBoard.Plugins.Basic
{
    /// <summary>
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    [Block("{3A59C428-B96E-4E33-AA89-E18A54B8709A}")]
    public partial class GUIDGeneratorBlock : UserControl
    {
        #region Constructors
        public GUIDGeneratorBlock()
        {
            // Initialize.
            InitializeComponent();

            // Generate an initial GUID.
            GenerateGUID();
        }
        #endregion

        #region Private methods
        private void GenerateGUID()
        {
            GUID.Text = Guid.NewGuid().ToString().ToUpper();
        }
        #endregion

        #region Button event handlers
        private void HandleGenerateButtonClick(object sender, RoutedEventArgs e)
        {
            GenerateGUID();
        }

        private void HandleCopyButtonClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(GUID.Text);
        }
        #endregion
    }
}
