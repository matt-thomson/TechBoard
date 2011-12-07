using System.Windows;
using System.Windows.Controls;
using SoundBoard.Controller;
using SoundBoard.Model;

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
            // Find the filter from the property.
            PropertyMapping mapping = DataContext as PropertyMapping;
            object[] attrs = mapping.Property.GetCustomAttributes(typeof(FileBlockPropertyAttribute), false);
            FileBlockPropertyAttribute attr = attrs[0] as FileBlockPropertyAttribute;

            string filename = mController.OpenFile(attr.Filter);

            if (filename != null)
            {
                Field.Text = filename;
            }
        }
    }
}