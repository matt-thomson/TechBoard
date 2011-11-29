using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Moq;
using NUnit.Framework;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF.Test
{
    [TestFixture, RequiresSTA]
    public class SoundBlockViewTest
    {
        #region Constants
        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string TITLE_NEW = "New Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.8;
        #endregion

        #region Private properties
        // Object under test.
        SoundBlockView mSoundBlockView;

        // Mock objects.
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private SoundBlock mSoundBlock;
        private Window mWindow;
        private Button mButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create a sound block for this view.
            mSoundBlock = new SoundBlock();
            mSoundBlock.Title = TITLE;
            mSoundBlock.FileName = FILE_NAME;
            mSoundBlock.Volume = VOLUME;

            // Create mock objects.
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);
            SoundBlockView.MediaController = mMockMediaController.Object;

            // Create the sound block view, and set the data context.
            mSoundBlockView = new SoundBlockView();
            mSoundBlockView.DataContext = mSoundBlock;
        
            // Get a reference to the button.
            mButton = mSoundBlockView.Content as Button;

            // Create a window, and add the view to it.
            mWindow = new Window();
            mWindow.Content = mSoundBlockView;

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
            // Clicking the button will play the soundBlock.
            mMockMediaController.Setup(c => c.Play(FILE_NAME, VOLUME));

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);
        }
        #endregion
    }
}
