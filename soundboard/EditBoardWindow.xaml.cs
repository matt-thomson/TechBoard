using System.ComponentModel;
using System.Windows;
using SoundBoard.Model;

namespace SoundBoard
{
    /// <summary>
    /// Interaction logic for EditBoardWindow.xaml
    /// </summary>
    public partial class EditBoardWindow : Window
    {
        #region Private members
        private static EditBoardWindow mInstance;
        #endregion

        #region Constructors
        public EditBoardWindow()
        {
            InitializeComponent();
            SoundsList.DataContext = BoardHandler.Instance;
        }
        #endregion

        #region Static methods
        public static void Open()
        {
            if (mInstance == null)
            {
                mInstance = new EditBoardWindow();
            }

            mInstance.Show();
            mInstance.Activate();
        }
        #endregion

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            mInstance = null;
        }
    }
}
