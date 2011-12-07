using System.Reflection;
using System.Windows;
using NUnit.Framework;
using SoundBoard.Test;

namespace SoundBoard.Model.Test
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
