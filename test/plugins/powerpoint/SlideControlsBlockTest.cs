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

using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using TechBoard.Plugins.PowerPoint;

namespace TechBoard.Plugins.PowerPoint.Test
{
    [TestFixture, RequiresSTA]
    public class SlideControlsBlockTest
    {
        #region Private properties
        // Object under test.
        SlideControlsBlock mSlideControlsBlock;

        // Mock objects.
        private Mock<IPowerPointController> mMockPowerPointController;

        // Other objects.
        private Window mWindow;
        private Button mPreviousButton;
        private Button mNextButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockPowerPointController = new Mock<IPowerPointController>(MockBehavior.Strict);

            // Create a slide controls block.
            mSlideControlsBlock = new SlideControlsBlock(mMockPowerPointController.Object);

            // Set the data context for the slide controls block.
            mSlideControlsBlock.DataContext = mSlideControlsBlock;
        
            // Get reference to the buttons.
            Grid grid = mSlideControlsBlock.Content as Grid;
            mPreviousButton = grid.Children[0] as Button;
            mNextButton = grid.Children[1] as Button;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mSlideControlsBlock;

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
        public void TestClickPreviousButton()
        {       
            // Clicking the button will switch to the previous slide.
            mMockPowerPointController.Setup(c => c.PreviousSlide());

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mPreviousButton);
            mPreviousButton.RaiseEvent(args);
        }

        [Test]
        public void TestClickNextButton()
        {
            // Clicking the button will switch to the next slide.
            mMockPowerPointController.Setup(c => c.NextSlide());

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mNextButton);
            mNextButton.RaiseEvent(args);
        }
        #endregion
    }
}
