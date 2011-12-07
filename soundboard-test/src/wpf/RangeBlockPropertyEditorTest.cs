using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using SoundBoard.Model;
using SoundBoard.WPF;

namespace SoundBoard.WPF.Test
{
    [TestFixture, RequiresSTA]
    public class RangeBlockPropertyEditorTest : DependencyObject
    {
        #region Dependency properties
        private static DependencyProperty DepProperty = DependencyProperty.Register("MyProperty",
                                                                                    typeof(double),
                                                                                    typeof(RangeBlockPropertyEditorTest));
        #endregion

        #region Constants
        private const double VALUE_1 = 0.3;
        private const double VALUE_2 = 0.8;
        #endregion

        #region Public properties
        // Property for the editor to bind to.
        [TextBlockProperty]
        public double MyProperty
        {
            get { return (double)GetValue(DepProperty); }
            set { SetValue(DepProperty, value); }
        }
        #endregion

        #region Private members
        // Object under test.
        private RangeBlockPropertyEditor mEditor;

        // Other objects.
        private Window mWindow;
        private Slider mSlider;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the property.
            MyProperty = VALUE_1;

            // Create the editor.
            mEditor = new RangeBlockPropertyEditor();

            // Set the data context for the editor.
            PropertyInfo property = GetType().GetProperty("MyProperty");
            PropertyMapping mapping = new PropertyMapping(property, this);
            mEditor.DataContext = mapping;

            // Get references to the controls in the editor.
            mSlider = mEditor.Content as Slider;

            // Create a window, and add the editor to it.
            mWindow = new Window();
            mWindow.Content = mEditor;

            // Show the window to activate the bindings.
            mWindow.Show();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestSlider()
        {
            // Check the initial value of the slider.
            Assert.AreEqual(VALUE_1, mSlider.Value);

            // Set the property, and check that the slider is updated.
            MyProperty = VALUE_2;
            Assert.AreEqual(VALUE_2, mSlider.Value);

            // Move the slider, and check that the property is updated.
            mSlider.Value = VALUE_2;
            Assert.AreEqual(VALUE_2, MyProperty);

        }
        #endregion

        #region Clean up
        [TearDown]
        public void TestEnd()
        {
            mWindow.Hide();
        }
        #endregion
    }
}
