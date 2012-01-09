using System.Windows;

namespace TechBoard.App
{
    /// <summary>
    /// Interaction logic for AddBlockWindow.xaml
    /// </summary>
    public partial class AddBlockWindow : Window
    {
        #region Private members
        private IBoardController BoardController;
        #endregion

        #region Constructors
        public AddBlockWindow(IBoardController xiBoardController)
        {
            // Initialize.
            InitializeComponent();
            BoardController = xiBoardController;

            // Set the data context for the list.
            BlockTypesList.DataContext = BoardController;
        }
        #endregion

        #region Button event handlers
        public void HandleDoubleClick(object sender, RoutedEventArgs e)
        {
            if (BlockTypesList.SelectedItem != null)
            {
                DialogResult = true;
                Close();
            }
        }

        public void HandleOKButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void HandleCancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        #endregion
    }
}
