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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TechBoard.App
{
    /// <summary>
    /// Interaction logic for EditBoardWindow.xaml
    /// </summary>
    public partial class EditBoardWindow : Window
    {
        #region Private members
        private IBoardController mBoardController;
        #endregion

        #region Constructors
        public EditBoardWindow(IBoardController xiBoardController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;

            // Set the data context for this window.
            BlocksList.DataContext = mBoardController;
        }
        #endregion

        #region Event handlers
        private void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void HandleAddButtonClick(object sender, RoutedEventArgs e)
        {
            // Show the add block window.
            AddBlockWindow window = new AddBlockWindow(mBoardController);
            Nullable<bool> result = window.ShowDialog();

            // If the user clicked 'OK', then add a block of the requested type.
            Type blockType = window.BlockTypesList.SelectedItem as Type;

            if ((result == true) && (blockType != null))
            {
                UserControl block = Activator.CreateInstance(blockType) as UserControl;
                mBoardController.Add(block);
                BlocksList.SelectedItem = block;
            }
        }

        private void HandleRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            UserControl block = BlocksList.SelectedItem as UserControl;
            mBoardController.Remove(block);
        }

        private void HandleMoveUpButtonClick(object sender, RoutedEventArgs e)
        {
            UserControl block = BlocksList.SelectedItem as UserControl;
            mBoardController.MoveUp(block);
        }

        private void HandleMoveDownButtonClick(object sender, RoutedEventArgs e)
        {
            UserControl block = BlocksList.SelectedItem as UserControl;
            mBoardController.MoveDown(block);
        }
        #endregion
    }
}
