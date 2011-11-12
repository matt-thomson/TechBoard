using System.ComponentModel;
using Moq;
using NUnit.Framework;
using SoundBoard.Controller;
using SoundBoard.Model;
using System.Windows;

namespace SoundBoard.WPF.Test
{
    [TestFixture, RequiresSTA]
    public class SoundBlockTest
    {
        #region Constants
        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.8;
        #endregion

        #region Private properties
        // Object under test.
        SoundBlock mSoundBlock;

        // Mock objects.
        private Mock<IBoardController> mMockBoardController;
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private Window mWindow;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockBoardController = new Mock<IBoardController>(MockBehavior.Strict);
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);

            // Create the sound block.
            mSoundBlock = new SoundBlock(mMockBoardController.Object,
                                         mMockMediaController.Object);

            // Create a window, and add the sound block to it.
            mWindow = new Window();
            mWindow.Content = mSoundBlock;

            // Show the window to activate the bindings.
            mWindow.Show();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestDataContext()
        {
            Assert.AreEqual(mSoundBlock.DataContext, mMockBoardController.Object);
        }

        [Test]
        public void TestLoadBoard()
        {
            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);
            sound.Volume = VOLUME;

            // Create a board, and add the sound to it.
            Board board = new Board();
            board.Sounds.Add(sound);

            // The BoardController will return this board.
            mMockBoardController.Setup(c => c.CurrentBoard).Returns(board);

            // Fire the BoardController's PropertyChanged event.
            mMockBoardController.Raise(c => c.PropertyChanged += null,
                                       mMockBoardController.Object, 
                                       new PropertyChangedEventArgs("CurrentBoard"));

            // Check the board is loaded.
            Assert.AreEqual(1, mSoundBlock.Items.Count);
        }
        #endregion
    }
}
