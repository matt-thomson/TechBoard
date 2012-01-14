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
using TechBoard.Plugins.Sounds;

namespace TechBoard.Plugins.Sounds.Test
{
    [TestFixture, RequiresSTA]
    public class SoundBlockTest
    {
        #region Constants
        // Properties of a sound.
        private const string TITLE = "New Sound";
        private const string TITLE_NEW = "Sound Title";
        private const string FILE_NAME = "";
        private const string FILE_NAME_NEW = "sound.mp3";
        private const double VOLUME = 0.5;
        private const double VOLUME_NEW = 0.8;
        #endregion

        #region Private properties
        // Object under test.
        SoundBlock mSoundBlock;

        // Mock objects.
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private Window mWindow;
        private Button mButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);

            // Create a sound block.
            mSoundBlock = new SoundBlock(mMockMediaController.Object);

            // Set the data context for the sound block.
            mSoundBlock.DataContext = mSoundBlock;
        
            // Get a reference to the button.
            mButton = mSoundBlock.Content as Button;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mSoundBlock;

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
        public void TestFileName()
        {
            Assert.AreEqual(FILE_NAME, mSoundBlock.FileName);

            // Now change the file name.
            mSoundBlock.FileName = FILE_NAME_NEW;
            Assert.AreEqual(mSoundBlock.FileName, FILE_NAME_NEW);
        }

        [Test]
        public void TestTitle()
        {
            Assert.AreEqual(TITLE, mSoundBlock.Title);

            // Now change the title.
            mSoundBlock.Title = TITLE_NEW;
            Assert.AreEqual(mSoundBlock.Title, TITLE_NEW);
        }

        [Test]
        public void TestVolume()
        {
            Assert.AreEqual(VOLUME, mSoundBlock.Volume);

            // Now change the volume.
            mSoundBlock.Volume = VOLUME_NEW;
            Assert.AreEqual(mSoundBlock.Volume, VOLUME_NEW);
        }

        [Test]
        public void TestButtonText()
        {
            // Check the button text.
            Assert.AreEqual(TITLE, mButton.Content);

            // Change the sound title, and check that the button text is updated.
            mSoundBlock.Title = TITLE_NEW;
            Assert.AreEqual(TITLE_NEW, mButton.Content);
        }
        
        [Test]
        public void TestClickButton()
        {       
            // Clicking the button will play the sound.
            mMockMediaController.Setup(c => c.Play(mSoundBlock));

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);
        }

        [Test]
        public void TestClickButtonNoFileName()
        {
            // Clear the file name on the block.
            mSoundBlock.FileName = string.Empty;

            // Click on the button.  Nothing will happen.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);
        }

        [Test]
        public void TestUnloadPlaying()
        {
            // Set the current sound to be this sound block.  It will be stopped when we unload.
            mMockMediaController.Setup(c => c.CurrentSoundBlock).Returns(mSoundBlock);
            mMockMediaController.Setup(c => c.Stop());

            // Unload the sound block.
            RoutedEventArgs args = new RoutedEventArgs(UserControl.UnloadedEvent, mSoundBlock);
            mSoundBlock.RaiseEvent(args);
        }

        [Test]
        public void TestUnloadNotPlaying()
        {
            // No sound is playing.
            mMockMediaController.Setup(c => c.CurrentSoundBlock).Returns<SoundBlock>(null);

            // Unload the sound block.
            RoutedEventArgs args = new RoutedEventArgs(UserControl.UnloadedEvent, mSoundBlock);
            mSoundBlock.RaiseEvent(args);
        }
        #endregion
    }
}
