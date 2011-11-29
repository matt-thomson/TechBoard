using NUnit.Framework;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class BoardTest
    {
        #region Constants
        // Location of test boards.
        private const string V03_BOARD = "..\\..\\data\\BoardTest\\v03.board";
        private const string EXPECTED_BOARD = "..\\..\\data\\BoardTest\\expected.board";
        private const string EMPTY_BOARD = "..\\..\\data\\BoardTest\\empty.board";

        // Location to save board to.
        private const string SAVED_BOARD = "C:\\temp\\output.board";

        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME_DEFAULT = 0.5;
        private const double VOLUME = 0.8;
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

            // Create a sound block.
            SoundBlock block = new SoundBlock();
            block.Title = TITLE;
            block.FileName = FILE_NAME;

            // Add it to the board.
            Board.Blocks.Add(block);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestBoardContents()
        {
            Assert.AreEqual(1, Board.Blocks.Count);

            SoundBlock soundBlock = Board.Blocks[0];

            Assert.AreEqual(TITLE, soundBlock.Title);
            Assert.AreEqual(FILE_NAME, soundBlock.FileName);
            Assert.AreEqual(VOLUME_DEFAULT, soundBlock.Volume);
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