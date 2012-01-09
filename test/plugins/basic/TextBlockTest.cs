using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using TechBoard.Plugins.Basic;

namespace TechBoard.Plugins.Basic.Test
{
    [TestFixture, RequiresSTA]
    public class TextBlockTest
    {
        #region Constants
        // Properties of a text block.
        private const string TEXT = "New Text";
        private const string TEXT_NEW = "Some Other Text";
        #endregion

        #region Private properties
        // Object under test.
        TextBlock mTextBlock;

        // Other objects.
        private Window mWindow;
        private Label mLabel;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create a text block.
            mTextBlock = new TextBlock();

            // Set the data context for the text block.
            mTextBlock.DataContext = mTextBlock;
        
            // Get a reference to the label.
            mLabel = mTextBlock.Content as Label;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mTextBlock;

            // Show the window to activate the bindings.
            mWindow.Show();
        }
        #endregion

        #region Clean up
        [TearDown]
        public void TestEnd()
        {
            mWindow.Hide();
        }
        #endregion

        #region Test cases        
        [Test]
        public void TestText()
        {
            // Check the text.
            Assert.AreEqual(TEXT, mTextBlock.Text);
            Assert.AreEqual(TEXT, mLabel.Content);

            // Change the text, and check that the label is updated.
            mTextBlock.Text = TEXT_NEW;
            Assert.AreEqual(TEXT_NEW, mLabel.Content);
        }
        #endregion
    }
}
