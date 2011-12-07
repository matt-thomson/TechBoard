using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for FileBlockPropertyEditor.xaml
    /// </summary>
    public partial class FileBlockPropertyEditor : UserControl
    {
        #region Static properties
        private static IFileDialogController mStaticController;
        #endregion

        #region Private properties
        private IFileDialogController mController;
        #endregion

        #region Constructors
        public FileBlockPropertyEditor()
        {
            InitializeComponent();

            if (mStaticController == null)
            {
                mStaticController = new FileDialogController();
            }

            mController = mStaticController;
        }

        public FileBlockPropertyEditor(IFileDialogController xiController)
        {
            InitializeComponent();
            mController = xiController;
        }
        #endregion

        private void HandleFileButtonClick(object sender, RoutedEventArgs e)
        {
            // TODO need to be able to pick filter
            string filename = mController.OpenFile("Sounds (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*");

            if (filename != null)
            {
                Field.Text = filename;
            }
        }
    }
}
