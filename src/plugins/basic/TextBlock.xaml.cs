using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.Plugins.Basic
{
    /// <summary>
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    [Block("{6A2910EE-BDD9-453A-87F4-7994AF4223AD}")]
    public partial class TextBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty TextProperty = DependencyProperty.Register("Text",
                                                                                     typeof(string),
                                                                                     typeof(TextBlock));
        #endregion

        #region Public properties
        [TextBlockProperty]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Constructors
        public TextBlock()
        {
            // Initialize.
            InitializeComponent();

            // Set up the default properties.
            Text = "New Text";
        }
        #endregion
    }
}
