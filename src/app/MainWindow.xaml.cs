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
using Microsoft.Win32;

namespace TechBoard.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        private const string FILTER = "TechBoards (*.board)|*.board|All files (*.*)|*.*";
        #endregion

        #region Private properties
        private IBoardController mBoardController;
        private IFileDialogController mFileDialogController;
        private EditBoardWindow mEditBoardWindow;
        private AboutWindow mAboutWindow;
        #endregion

        #region Constructor
        public MainWindow(IBoardController xiBoardController,
                          IFileDialogController xiFileDialogController)
        {
            // Initialize.
            InitializeComponent();
            mBoardController = xiBoardController;
            mFileDialogController = xiFileDialogController;

            // Set the data context.
            DataContext = mBoardController;

            // Create other windows.
            mEditBoardWindow = new EditBoardWindow(mBoardController);
            mAboutWindow = new AboutWindow();
        }
        #endregion

        #region Window event handlers
        private void HandleWindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Menu event handlers
        private void HandleMenuOptionNew(object sender, RoutedEventArgs e)
        {
            mBoardController.New();
        }

        private void HandleMenuOptionOpen(object sender, RoutedEventArgs e)
        {
            string filename = mFileDialogController.OpenFile(FILTER);

            if (filename != null)
            {
                mBoardController.Load(filename);
            }
        }

        private void HandleMenuOptionSave(object sender, RoutedEventArgs e)
        {
            string filename = mFileDialogController.SaveFile(FILTER);

            if (filename != null)
            {
                mBoardController.Save(filename);
            }
        }

        private void HandleMenuOptionExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleMenuOptionEditor(object sender, RoutedEventArgs e)
        {
            mEditBoardWindow.Show();
            mEditBoardWindow.Activate();
        }

        private void HandleMenuOptionAbout(object sender, RoutedEventArgs e)
        {
            mAboutWindow.ShowDialog();
        }
        #endregion
    }
}