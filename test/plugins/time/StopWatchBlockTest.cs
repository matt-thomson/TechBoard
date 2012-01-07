using System;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;

namespace SoundBoard.Plugins.Time.Test
{
    [TestFixture, RequiresSTA]
    public class StopWatchBlockTest
    {
        #region Constants
        private DateTime TIME1 = new DateTime(1986, 2, 19, 21, 58, 30, 250);
        private string DURATION1_STRING = "00:00:00.00";
        private DateTime TIME2 = new DateTime(1986, 2, 19, 23, 01, 00, 750);
        private string DURATION2_STRING = "01:02:30.50";
        private DateTime TIME3 = new DateTime(1986, 2, 19, 23, 03, 00, 750);
        private string DURATION3_STRING = "01:02:30.50";
        private DateTime TIME4 = new DateTime(1986, 2, 19, 23, 03, 30, 800);
        private string DURATION4_STRING = "01:03:00.55";
        #endregion

        #region Private properties
        // Object under test.
        StopWatchBlock mStopWatchBlock;

        // Mock objects.
        private Mock<ITimerController> mMockTimerController;

        // Other objects.
        private Window mWindow;
        private Label mDurationLabel;
        private Button mStartStopButton;
        private Button mResetButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockTimerController = new Mock<ITimerController>(MockBehavior.Strict);

            // Create a stop watch block.
            mStopWatchBlock = new StopWatchBlock(mMockTimerController.Object);

            // Set the data context for the stop watch block.
            mStopWatchBlock.DataContext = mStopWatchBlock;

            // Get references to the controls.
            Grid grid = mStopWatchBlock.Content as Grid;
            mDurationLabel = grid.Children[0] as Label;
            mStartStopButton = grid.Children[1] as Button;
            mResetButton = grid.Children[2] as Button;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mStopWatchBlock;

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
        public void TestInitialState()
        {
            CheckBlock(DURATION1_STRING, false);
        }

        [Test]
        public void TestRunStopWatch()
        {
            // Expect to request the current time when we click the start button.
            SetTime(TIME1);

            // Click on the start button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mStartStopButton);
            mStartStopButton.RaiseEvent(args);

            // Check the state of the block.
            CheckBlock(DURATION1_STRING, true);

            // Pop the timer.
            SetTime(TIME2);

            // Check that the duration has been updated.
            CheckBlock(DURATION2_STRING, true);

            // Click on the stop button.
            args = new RoutedEventArgs(Button.ClickEvent, mStartStopButton);
            mStartStopButton.RaiseEvent(args);

            // Check that the block has been updated.
            CheckBlock(DURATION2_STRING, false);

            // Pop the timer again.
            SetTime(TIME3);

            // This does not affect the duration, as the stop watch is not running.
            CheckBlock(DURATION3_STRING, false);

            // Click on the start button.
            args = new RoutedEventArgs(Button.ClickEvent, mStartStopButton);
            mStartStopButton.RaiseEvent(args);

            // Check that the block has been updated.
            CheckBlock(DURATION3_STRING, true);

            // Pop the timer again.
            SetTime(TIME4);

            // The duration is added to the previous duration.
            CheckBlock(DURATION4_STRING, true);

            // Click on the stop button.
            args = new RoutedEventArgs(Button.ClickEvent, mStartStopButton);
            mStartStopButton.RaiseEvent(args);

            // Check that the block has been updated.
            CheckBlock(DURATION4_STRING, false);

            // Finally, click on the reset button.
            args = new RoutedEventArgs(Button.ClickEvent, mResetButton);
            mResetButton.RaiseEvent(args);

            // Check that the duration is reset.
            CheckBlock(DURATION1_STRING, false);
        }
        #endregion

        #region Private methods
        private void CheckBlock(string xiDuration, bool xiRunning)
        {
            Assert.AreEqual(xiDuration, mDurationLabel.Content);
            Assert.AreEqual(!xiRunning, mResetButton.IsEnabled);

            if (xiRunning)
            {
                Assert.AreEqual("Stop", mStartStopButton.Content);
            }
            else
            {
                Assert.AreEqual("Start", mStartStopButton.Content);
            }
        }

        private void SetTime(DateTime xiDateTime)
        {
            mMockTimerController.Setup(c => c.Now).Returns(xiDateTime);
            mMockTimerController.Raise(c => c.TimerPop += null);
        }
        #endregion
    }
}
