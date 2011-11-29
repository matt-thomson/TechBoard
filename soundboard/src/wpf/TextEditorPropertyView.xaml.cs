using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for TextEditorPropertyView.xaml
    /// </summary>
    public partial class TextEditorPropertyView : UserControl
    {
        public TextEditorPropertyView()
        {
            InitializeComponent();
        }

        private void HandleDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            dynamic context = DataContext;

            // Only set up the binding once.
            if ((context != null) && (e.OldValue == null))
            {
                Binding binding = new Binding
                {
                    Source = context.Target,
                    Path = new PropertyPath(context.Property),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                Field.SetBinding(TextBox.TextProperty, binding);
            }
        }
    }
}
