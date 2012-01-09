using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace TechBoard
{
    public class PropertyMapping : DependencyObject
    {
        #region Dependency properties
        private static readonly DependencyProperty PropertyValueProperty =
            DependencyProperty.Register("PropertyValue", typeof(object), typeof(PropertyMapping));
        #endregion

        #region Public properties
        public object PropertyValue
        {
            get { return GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }
        public PropertyInfo Property { get; private set; }
        public object Target { get; private set; }
        public BlockPropertyAttribute Attribute { get; private set; }
        #endregion

        #region Constructors
        public PropertyMapping(PropertyInfo xiProperty,
                               object xiTarget)
        {
            Property = xiProperty;
            Target = xiTarget;

            // Set up the binding to the dependency property.
            Binding binding = new Binding
            {
                Source = Target,
                Path = new PropertyPath(Property.Name, null),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                Mode = BindingMode.TwoWay
            };

            BindingOperations.SetBinding(this,
                                         PropertyValueProperty,
                                         binding);

            // Set up the block property attribute.           
            object[] attrs = Property.GetCustomAttributes(typeof(BlockPropertyAttribute), false);

            if (attrs.Length > 0)
            {
                Attribute = attrs[0] as BlockPropertyAttribute;
            }
        }
        #endregion
    }
}
