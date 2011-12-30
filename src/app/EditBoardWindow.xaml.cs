using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.App
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
            if (result == true)
            {
                Type blockType = window.BlockTypesList.SelectedItem as Type;
                UserControl block = Activator.CreateInstance(blockType) as UserControl;
                mBoardController.Add(block);
                BlocksList.SelectedItem = block;
            }
        }

        private void HandleRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            UserControl sound = BlocksList.SelectedItem as UserControl;
            mBoardController.Remove(sound);
        }
        #endregion
    }
}
