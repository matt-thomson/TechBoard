using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using SoundBoard.Controller;

namespace SoundBoard.WPF.Test
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
            // Set some properties on the sound block.
            mSoundBlock.FileName = FILE_NAME_NEW;
            mSoundBlock.Volume = VOLUME_NEW;

            // Clicking the button will play the sound.
            mMockMediaController.Setup(c => c.Play(FILE_NAME_NEW, VOLUME_NEW));

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);
        }
        #endregion
    }
}
