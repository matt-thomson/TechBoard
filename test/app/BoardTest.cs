using NUnit.Framework;
using SoundBoard.Plugins.Test;

namespace SoundBoard.App.Test
{
    [TestFixture, RequiresSTA]
    public class BoardTest
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
        // The object under test.
        private Board Board { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the object under test.
            Board = new Board();

            // Create two blocks, and add them to the board.
            TestBlock block = new TestBlock();
            block.MyProperty = TEXT_1;
            Board.Blocks.Add(block);

            block = new TestBlock();
            block.MyProperty = TEXT_2;
            Board.Blocks.Add(block);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestBoardContents()
        {
            Assert.AreEqual(2, Board.Blocks.Count);

            TestBlock block = Board.Blocks[0] as TestBlock;
            Assert.AreEqual(TEXT_1, block.MyProperty);

            block = Board.Blocks[1] as TestBlock;
            Assert.AreEqual(TEXT_2, block.MyProperty);
        }

        [Test]
        public void TestSave()
        {
            // Save the board to disk.
            Board.Save(SAVED_BOARD);

            // Verify the file contents.
            FileAssert.AreEqual(EXPECTED_BOARD, SAVED_BOARD);
        }

        [Test]
        public void TestLoadBoardV03()
        {
            // Load the board from disk.
            Board = Board.Load(V03_BOARD);

            // Verify the board contents.
            TestBoardContents();
        }

        [Test]
        public void TestLoadEmptyBoard()
        {
            // Load the board from disk.
            Board = Board.Load(EMPTY_BOARD);

            // Verify the board contents.
            Assert.AreEqual(0, Board.Blocks.Count);
        }
        #endregion
    }
}