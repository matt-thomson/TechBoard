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

namespace TechBoard
{
    /// <summary>
    /// Interaction logic for FileBlockPropertyEditor.xaml
    /// </summary>
    public partial class FileBlockPropertyEditor : UserControl
    {
        #region Private properties
        private IFileDialogController mController;
        #endregion

        #region Constructors
        public FileBlockPropertyEditor()
        {
            InitializeComponent();
            mController = new FileDialogController();
        }

        public FileBlockPropertyEditor(IFileDialogController xiController)
        {
            InitializeComponent();
            mController = xiController;
        }
        #endregion

        private void HandleFileButtonClick(object sender, RoutedEventArgs e)
        {
            // Find the filter from the property.
            PropertyMapping mapping = DataContext as PropertyMapping;
            FileBlockPropertyAttribute attr = mapping.Attribute as FileBlockPropertyAttribute;

            string filename = mController.OpenFile(attr.Filter);

            if (filename != null)
            {
                Field.Text = filename;
            }
        }
    }
}