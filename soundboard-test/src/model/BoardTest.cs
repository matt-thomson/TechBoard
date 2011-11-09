using NUnit.Framework;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class BoardTest
    {
        #region Constants
        // Location of test boards.
        private const string V01_BOARD = "..\\..\\data\\v01.board";
        private const string V02_BOARD = "..\\..\\data\\v02.board";
        private const string EXPECTED_BOARD = "..\\..\\data\\expected.board";
        private const string EMPTY_BOARD = "..\\..\\data\\empty.board";

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
            Assert.AreEqual(VOLUME_DEFAULT, sound.Volume);
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
        public void TestLoadBoardV01()
        {
            // Load the board from disk.
            Board = Board.Load(V01_BOARD);

            // Verify the board contents.
            TestBoardContents();
        }

        [Test]
        public void TestLoadBoardV02()
        {
            // Load the board from disk.
            Board = Board.Load(V02_BOARD);

            // Verify the board contents.
            Assert.AreEqual(1, Board.Sounds.Count);

            Sound sound = Board.Sounds[0];

            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
            Assert.AreEqual(VOLUME, sound.Volume);
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