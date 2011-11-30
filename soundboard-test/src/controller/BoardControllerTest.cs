using NUnit.Framework;
using SoundBoard.WPF;

namespace SoundBoard.Controller.Test
{
    [TestFixture, RequiresSTA]
    public class BoardControllerTest
    {
        #region Constants
        private const string EXPECTED_BOARD = "..\\..\\data\\BoardControllerTest\\expected.board";
        private const string SAVED_BOARD = "C:\\temp\\output.board";
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "soundBlock.mp3";
        #endregion

        #region Private properties
        // Object under test.
        private BoardController mBoardController = new BoardController();
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create a new board.
            mBoardController.New();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestNew()
        {
            // Create a new board.
            mBoardController.New();

            // Check the new board.
            Assert.AreEqual(0, mBoardController.CurrentBoard.Blocks.Count);
        }

        [Test]
        public void TestLoadSave()
        {
            // Load a board.
            mBoardController.Load(EXPECTED_BOARD);

            // Check the loaded board.
            Assert.AreEqual(1, mBoardController.CurrentBoard.Blocks.Count);

            // Now save the board.
            mBoardController.Save(SAVED_BOARD);

            // Verify the file contents.
            FileAssert.AreEqual(EXPECTED_BOARD, SAVED_BOARD);
        }

        [Test]
        // TODO move to SoundBlock tests
        public void TestAddRemove()
        {            
            // Create a new board.
            mBoardController.New();

            // Create a sound block.
            SoundBlock soundBlock = new SoundBlock();

            // Add the sound block to the board.
            mBoardController.Add(soundBlock);
            Assert.AreEqual(1, mBoardController.CurrentBoard.Blocks.Count);

            // Now remove the soundBlock.
            mBoardController.Remove(soundBlock);
            Assert.AreEqual(0, mBoardController.CurrentBoard.Blocks.Count);
        }
        #endregion
    }
}
