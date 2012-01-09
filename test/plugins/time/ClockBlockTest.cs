using System;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;

namespace TechBoard.Plugins.Time.Test
{
    [TestFixture, RequiresSTA]
    public class ClockBlockTest
    {
        #region Constants
        private DateTime INITIAL_TIME = new DateTime(1986, 2, 19, 21, 58, 30);
        private string INITIAL_TIME_STRING = "21:58:30";
        private DateTime LATER_TIME = new DateTime(2010, 5, 31, 19, 01, 15);
        private string LATER_TIME_STRING = "19:01:15";
        #endregion

        #region Private properties
        // Object under test.
        ClockBlock mClockBlock;

        // Mock objects.
        private Mock<ITimerController> mMockTimerController;

        // Other objects.
        private Window mWindow;
        private Label mLabel;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockTimerController = new Mock<ITimerController>(MockBehavior.Strict);

            // Set up the current time on the mock object.
            SetTime(INITIAL_TIME);

            // Create a clock block.
            mClockBlock = new ClockBlock(mMockTimerController.Object);

            // Set the data context for the clock block.
            mClockBlock.DataContext = mClockBlock;

            // Get a reference to the label.
            mLabel = mClockBlock.Content as Label;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mClockBlock;

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
        public void TestInitialTime()
        {
            Assert.AreEqual(INITIAL_TIME_STRING, mLabel.Content);
        }

        [Test]
        public void TestTimerPop()
        {
            // Fire the timer.
            SetTime(LATER_TIME);

            // Check the label is updated.
            Assert.AreEqual(LATER_TIME_STRING, mLabel.Content);
        }
        #endregion

        #region Private methods
        private void SetTime(DateTime xiDateTime)
        {
            mMockTimerController.Setup(c => c.Now).Returns(xiDateTime);
            mMockTimerController.Raise(c => c.TimerPop += null);
        }
        #endregion
    }
}
