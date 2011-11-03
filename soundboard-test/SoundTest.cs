using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class SoundTest
    {
        #region Constants
        // Properties of the sound.
        private const string TITLE = "Sound Title";
        private const string TITLE_NEW = "New Sound Title";
        private const string FILE_NAME = "sound.mp3";
        #endregion

        #region Private properties
        // The object under test.
        private Sound Sound { get; set; }

        // Received property changed events.
        private List<string> ReceivedPropertyChangedEvents { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void Init()
        {
            // Set up the object under test.
            Sound = new Sound(TITLE, FILE_NAME);

            // Set up the list of received events, and register to receive events into it.
            ReceivedPropertyChangedEvents = new List<string>();
            Sound.PropertyChanged += HandlePropertyChanged;
        }
        #endregion

        #region Test cases
        [Test]
        public void TestTitle()
        {
            Assert.AreEqual(TITLE, Sound.Title);

            // Check that no property changes have been received so far.
            Assert.AreEqual(0, ReceivedPropertyChangedEvents.Count);

            // Now change the title.
            Sound.Title = TITLE_NEW;
            Assert.AreEqual(Sound.Title, TITLE_NEW);

            // This triggers a property change event.
            Assert.AreEqual(1, ReceivedPropertyChangedEvents.Count);
            Assert.AreEqual("Title", ReceivedPropertyChangedEvents[0]);
        }

        [Test]
        public void TestFileName()
        {
            Assert.AreEqual(FILE_NAME, Sound.FileName);
        }
        #endregion

        #region Event handlers
        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Assert.AreEqual(Sound, sender as Sound);
            ReceivedPropertyChangedEvents.Add(e.PropertyName);
        }
        #endregion
    }
}