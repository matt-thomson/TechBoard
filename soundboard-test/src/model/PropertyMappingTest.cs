using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Reflection;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class PropertyMappingTest
    {
        #region Public properties
        public string MyProperty { get; set; }
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
    }
}
