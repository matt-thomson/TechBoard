using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.App
{
    public class BlockPropertyEditorSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object xiItem,
                                                    DependencyObject xiContainer)
        {
            DataTemplate template = new DataTemplate();
            PropertyMapping mapping = xiItem as PropertyMapping;

            if (mapping != null)
            {
                // Now get the view attribute from the editor property attribute.
                object[] attrs = mapping.Attribute.GetType().GetCustomAttributes(
                                                    typeof(BlockPropertyEditorAttribute), false);
                BlockPropertyEditorAttribute viewAttr = attrs[0] as BlockPropertyEditorAttribute;
                
                // Set up the template from the view attribute.
                template.VisualTree = new FrameworkElementFactory(viewAttr.ViewType);
            }

            return template;
        }
    }
}
