﻿using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace SoundBoard.Model
{
    public class PropertyMapping : DependencyObject
    {
        public object PropertyValue
        {
            get { return GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }

        public static readonly DependencyProperty PropertyValueProperty =
            DependencyProperty.Register("PropertyValue", typeof(object), typeof(PropertyMapping));

        public PropertyInfo Property { get; private set; }
        public object Target { get; private set; }

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
        }
    }
}
