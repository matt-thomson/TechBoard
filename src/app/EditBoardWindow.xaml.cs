using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SoundBoard.Plugins.Sounds;

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
            SoundBlock sound = new SoundBlock();
            mBoardController.Add(sound);
            BlocksList.SelectedItem = sound;
        }

        private void HandleRemoveButtonClick(object sender, RoutedEventArgs e)
        {
            UserControl sound = BlocksList.SelectedItem as UserControl;
            mBoardController.Remove(sound);
        }
        #endregion
    }
}
