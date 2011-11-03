using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

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
