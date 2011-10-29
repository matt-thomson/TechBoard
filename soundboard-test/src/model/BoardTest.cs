using NUnit.Framework;

namespace SoundBoard.Model.Test
{
    public class BoardTest
    {
        #region Constants
        // Location of test boards.
        private const string EXPECTED_BOARD = "..\\..\\data\\expected.board";
        private const string EMPTY_BOARD = "..\\..\\data\\empty.board";

        // Location to save board to.
        private const string SAVED_BOARD = "C:\\temp\\output.board";

        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        #endregion

        #region Private properties
        // The object under test.
        private Board Board { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void Init()
        {
            // Set up the object under test.
            Board = new Board();

            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);

            // Add it to the board.
            Board.Sounds.Add(sound);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestBoardContents()
        {
            Assert.AreEqual(1, Board.Sounds.Count);

            Sound sound = Board.Sounds[0];

            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
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
        public void TestLoadBoard()
        {
            // Load the board from disk.
            Board = Board.Load(EXPECTED_BOARD);

            // Verify the board contents.
            TestBoardContents();
        }

        [Test]
        public void TestLoadEmptyBoard()
        {
            // Load the board from disk.
            Board = Board.Load(EMPTY_BOARD);

            // Verify the board contents.
            Assert.AreEqual(0, Board.Sounds.Count);
        }
        #endregion
    }
}