using System.Windows;
using System.Windows.Controls;

namespace SoundBoard
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

            mController = FileDialogController.StaticInstance;
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