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

            // @@@ Ugly hack to avoid exception when disconnecting - can we do better?
            if ((context != null) &&
                (!context.GetType().FullName.Equals("MS.Internal.NamedObject")))
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
