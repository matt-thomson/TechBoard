using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using TechBoard.Plugins.Sounds;

namespace TechBoard.Plugins.Sounds.Test
{
    [TestFixture, RequiresSTA]
    public class SoundControlsBlockTest
    {
        #region Constants
        private double FADE_DURATION = 3000;
        private double FADE_DURATION_NEW = 1500;
        #endregion

        #region Private properties
        // Object under test.
        SoundControlsBlock mSoundControlsBlock;

        // Mock objects.
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private Window mWindow;
        private Button mStopButton;
        private Button mFadeButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);

            // Create a sound controls block.
            mSoundControlsBlock = new SoundControlsBlock(mMockMediaController.Object);

            // Set the data context for the sound controls block.
            mSoundControlsBlock.DataContext = mSoundControlsBlock;
        
            // Get reference to the buttons.
            Grid grid = mSoundControlsBlock.Content as Grid;
            mStopButton = grid.Children[0] as Button;
            mFadeButton = grid.Children[1] as Button;

            // Create a window, and add the block to it.
            mWindow = new Window();
            mWindow.Content = mSoundControlsBlock;

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
        public void TestClickStopButton()
        {       
            // Clicking the button will stop the sound.
            mMockMediaController.Setup(c => c.Stop());

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mStopButton);
            mStopButton.RaiseEvent(args);
        }

        [Test]
        public void TestClickFadeButton()
        {
            // Check the value of the fade duration.
            Assert.AreEqual(FADE_DURATION, mSoundControlsBlock.FadeDuration);

            // Set the fade duration to a different value.
            mSoundControlsBlock.FadeDuration = FADE_DURATION_NEW;

            // Clicking the button will fade the sound.
            mMockMediaController.Setup(c => c.Fade(FADE_DURATION_NEW));

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mFadeButton);
            mFadeButton.RaiseEvent(args);
        }
        #endregion
    }
}
