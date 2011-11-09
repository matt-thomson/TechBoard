using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using SoundBoard.Model;

namespace SoundBoard.Controller.Test
{
    [TestFixture]
    public class BoardControllerTest
    {
        #region Constants
        private const string EXPECTED_BOARD = "..\\..\\data\\expected.board";
        private const string SAVED_BOARD = "C:\\temp\\output.board";
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        #endregion

        #region Private properties
        // Received property changed events.
        private List<string> ReceivedPropertyChangedEvents { get; set; }
        #endregion

        #region Initialization
        [TestFixtureSetUp]
        public void Init()
        {
            // Check the initial board.
            Assert.AreEqual(0, BoardController.Instance.CurrentBoard.Sounds.Count);

            // Register to be notified of property changes.
            BoardController.Instance.PropertyChanged += HandlePropertyChanged;
        }

        [SetUp]
        public void TestInit()
        {
            // Set up the list of property changed events.
            ReceivedPropertyChangedEvents = new List<string>();            
        }
        #endregion

        #region Test cases
        [Test]
        public void TestNew()
        {
            // Check that no property changes have been received so far.
            Assert.AreEqual(0, ReceivedPropertyChangedEvents.Count);

            // Create a new board.
            BoardController.New();

            // This triggers a property changed event.
            Assert.AreEqual(1, ReceivedPropertyChangedEvents.Count);
            Assert.AreEqual("CurrentBoard", ReceivedPropertyChangedEvents[0]);

            // Check the new board.
            Assert.AreEqual(0, BoardController.Instance.CurrentBoard.Sounds.Count);
        }

        [Test]
        public void TestLoadSave()
        {
            // Check that no property changes have been received so far.
            Assert.AreEqual(0, ReceivedPropertyChangedEvents.Count);

            // Load a board.
            BoardController.Load(EXPECTED_BOARD);

            // This triggers a property changed event.
            Assert.AreEqual(1, ReceivedPropertyChangedEvents.Count);
            Assert.AreEqual("CurrentBoard", ReceivedPropertyChangedEvents[0]);

            // Check the loaded board.
            Assert.AreEqual(1, BoardController.Instance.CurrentBoard.Sounds.Count);

            // Now save the board.
            BoardController.Save(SAVED_BOARD);

            // Verify the file contents.
            FileAssert.AreEqual(EXPECTED_BOARD, SAVED_BOARD);

            // There are no more property changed events.
            Assert.AreEqual(1, ReceivedPropertyChangedEvents.Count);
        }

        [Test]
        public void TestAddRemove()
        {
            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);

            // Add the sound to the board.
            BoardController.Add(sound);
            Assert.AreEqual(1, BoardController.Instance.CurrentBoard.Sounds.Count);

            // Now remove the sound.
            BoardController.Remove(sound);
            Assert.AreEqual(0, BoardController.Instance.CurrentBoard.Sounds.Count);

            // Check that no property changes were received during the test.
            Assert.AreEqual(0, ReceivedPropertyChangedEvents.Count);
        }
        #endregion

        #region Event handlers
        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Assert.AreEqual(BoardController.Instance, sender as BoardController);
            ReceivedPropertyChangedEvents.Add(e.PropertyName);
        }
        #endregion
    }
}
