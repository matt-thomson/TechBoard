/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

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
