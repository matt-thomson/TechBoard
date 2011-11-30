using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    /// <summary>
    /// Interaction logic for TextBlockPropertyEditor.xaml
    /// </summary>
    public partial class TextBlockPropertyEditor : UserControl
    {
        public TextBlockPropertyEditor()
        {
            InitializeComponent();
        }

        private void HandleDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PropertyMapping context = DataContext as PropertyMapping;

            // Only set up the binding once.
            if ((context != null) && (e.OldValue == null))
            {
                Binding binding = new Binding
                {
                    Source = context.Target,
                    Path = new PropertyPath(context.Property.Name, null),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                Field.SetBinding(TextBox.TextProperty, binding);
            }
        }
    }
}
