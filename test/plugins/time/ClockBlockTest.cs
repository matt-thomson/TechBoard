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
