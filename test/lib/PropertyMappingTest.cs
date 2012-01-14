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
using NUnit.Framework;
using TechBoard.Plugins.Test;

namespace TechBoard.Test
{
    [TestFixture]
    public class PropertyMappingTest : DependencyObject
    {
        #region Dependency properties
        private static DependencyProperty DepProperty = DependencyProperty.Register("MyProperty",
                                                                            typeof(string),
                                                                            typeof(PropertyMappingTest));
        #endregion

        #region Constants
        private const string TEXT_1 = "Some text";
        private const string TEXT_2 = "Some other text";
        #endregion

        #region Public properties
        [TestBlockProperty]
        public string MyProperty
        {
            get { return (string)GetValue(DepProperty); }
            set { SetValue(DepProperty, value); }
        }
        #endregion

        #region Private members
        // Object under test
        private PropertyMapping mMapping;
        private PropertyInfo mPropInfo;
        #endregion

        [SetUp]
        public void TestInit()
        {
            // Set up the mapping.
            mPropInfo = this.GetType().GetProperty("MyProperty");
            mMapping = new PropertyMapping(mPropInfo, this);
        }

        [Test]
        public void TestProperty()
        {
            Assert.AreEqual(mPropInfo, mMapping.Property);
        }

        [Test]
        public void TestTarget()
        {
            Assert.AreEqual(this, mMapping.Target);
        }

        [Test]
        public void TestAttribute()
        {
            Assert.IsInstanceOf<TestBlockPropertyAttribute>(mMapping.Attribute);
        }

        [Test]
        public void TestPropertyValue()
        {
            // Set the value of the property directly.
            MyProperty = TEXT_1;

            // Check that PropertyValue on the mapping is updated.
            Assert.AreEqual(TEXT_1, mMapping.PropertyValue);

            // Now set PropertyValue.
            mMapping.PropertyValue = TEXT_2;

            // Check the value of the property directly.
            Assert.AreEqual(TEXT_2, MyProperty);
        }
    }
}
