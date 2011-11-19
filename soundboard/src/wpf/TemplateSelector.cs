using System.Windows;
using System.Windows.Controls;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    public class TemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item,
                                                    DependencyObject container)
        {
            // TODO split out by type
            return SoundBlock.DataTemplate;
        }
    }
}
