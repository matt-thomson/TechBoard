using System.Windows;
using System.Windows.Controls;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    public class EditorPropertyTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object xiItem,
                                                    DependencyObject xiContainer)
        {
            DataTemplate template = new DataTemplate();
            PropertyMapping mapping = xiItem as PropertyMapping;

            if (mapping != null)
            {
                // Get the editor property attribute from the property.
                object[] attrs = mapping.Property.GetCustomAttributes(typeof(EditorPropertyAttribute), false);
                EditorPropertyAttribute attr = attrs[0] as EditorPropertyAttribute;

                // Now get the view attribute from the editor property attribute.
                attrs = attr.GetType().GetCustomAttributes(typeof(EditorPropertyViewAttribute), false);
                EditorPropertyViewAttribute viewAttr = attrs[0] as EditorPropertyViewAttribute;
                
                // Set up the template from the view attribute.
                template.VisualTree = new FrameworkElementFactory(viewAttr.ViewType);
            }

            return template;
        }
    }
}
