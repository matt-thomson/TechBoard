using System.Reflection;
using System.Windows;
using NUnit.Framework;
using TechBoard.Plugins.Test;

namespace TechBoard.App.Test
{
    [TestFixture]
    public class BlockPropertyEditorSelectorTest
    {
        #region Public properties
        [TestBlockProperty]
        public string MyProperty { get; set; }
        #endregion

        #region Private properties
        // Object under test.
        private BlockPropertyEditorSelector mSelector;
        private PropertyMapping mMapping;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create a new selector.
            mSelector = new BlockPropertyEditorSelector();

            // Create a mapping to the property.            
            PropertyInfo property = GetType().GetProperty("MyProperty");
            mMapping = new PropertyMapping(property, this);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestSelect()
        {
            DataTemplate template = mSelector.SelectTemplate(mMapping, null);
            Assert.IsInstanceOf<FrameworkElementFactory>(template.VisualTree);

            FrameworkElementFactory factory = template.VisualTree as FrameworkElementFactory;
            Assert.AreEqual(typeof(TestBlockPropertyEditor), factory.Type);
        }
        #endregion
    }
}
