using System.Windows;
using System.Windows.Controls;
using System;

namespace TechBoard.Plugins.Basic
{
    /// <summary>
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    [Block("{3A59C428-B96E-4E33-AA89-E18A54B8709A}")]
    public partial class GUIDGeneratorBlock : UserControl
    {
        #region Constructors
        public GUIDGeneratorBlock()
        {
            // Initialize.
            InitializeComponent();

            // Generate an initial GUID.
            GenerateGUID();
        }
        #endregion

        #region Private methods
        private void GenerateGUID()
        {
            GUID.Text = Guid.NewGuid().ToString().ToUpper();
        }
        #endregion

        #region Button event handlers
        private void HandleGenerateButtonClick(object sender, RoutedEventArgs e)
        {
            GenerateGUID();
        }

        private void HandleCopyButtonClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(GUID.Text);
        }
        #endregion
    }
}
