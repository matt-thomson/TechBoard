using NUnit.Framework;
using SoundBoard.Plugins.Test;

namespace SoundBoard.App.Test
{
    [TestFixture, RequiresSTA]
    public class BoardControllerTest
    {
        #region Constants
        // Location of test boards.
        private const string V03_BOARD = "..\\..\\..\\data\\BoardTest\\v03.board";
        private const string EXPECTED_BOARD = "..\\..\\..\\data\\BoardTest\\expected.board";
        private const string EMPTY_BOARD = "..\\..\\..\\data\\BoardTest\\empty.board";

        // Location to save board to.
        private const string SAVED_BOARD = "C:\\temp\\output.board";

        // Properties of a block.
        private const string TEXT_1 = "Some text 1";
        private const string TEXT_2 = "Some text 2";
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
        public void TestAddRemove()
        {            
            // Create a new board.
            mBoardController.New();

            // Create a block.
            TestBlock block = new TestBlock();

            // Add the block to the board.
            mBoardController.Add(block);
            Assert.AreEqual(1, mBoardController.CurrentBoard.Blocks.Count);

            // Now remove the block.
            mBoardController.Remove(block);
            Assert.AreEqual(0, mBoardController.CurrentBoard.Blocks.Count);
        }

        [Test]
        public void TestLoadBoardV03()
        {
            // Load the board from disk.
            mBoardController.Load(V03_BOARD);

            // Verify the board contents.
            Board board = mBoardController.CurrentBoard;
            Assert.AreEqual(2, board.Blocks.Count);

            TestBlock block = board.Blocks[0] as TestBlock;
            Assert.AreEqual(TEXT_1, block.MyProperty);

            block = board.Blocks[1] as TestBlock;
            Assert.AreEqual(TEXT_2, block.MyProperty);
        }

        [Test]
        public void TestLoadEmptyBoard()
        {
            // Load the board from disk.
            mBoardController.Load(EMPTY_BOARD);

            // Verify the board contents.
            Board board = mBoardController.CurrentBoard;
            Assert.AreEqual(0, board.Blocks.Count);
        }

        [Test]
        public void TestSaveBoard()
        {
            // Create two blocks, and add them to the board.
            TestBlock block = new TestBlock();
            block.MyProperty = TEXT_1;
            mBoardController.Add(block);

            block = new TestBlock();
            block.MyProperty = TEXT_2;
            mBoardController.Add(block);

            // Save the board to disk.
            mBoardController.Save(SAVED_BOARD);

            // Verify the file contents.
            FileAssert.AreEqual(EXPECTED_BOARD, SAVED_BOARD);
        }
        #endregion
    }
}
