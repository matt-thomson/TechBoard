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
