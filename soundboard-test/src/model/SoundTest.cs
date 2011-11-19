using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using NUnit.Framework;

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
        private const double VOLUME = 0.5;
        private const double VOLUME_NEW = 0.8;

        // Location of test sounds.
        private const string V01_SOUND = "..\\..\\data\\SoundTest\\v01.sound";
        private const string V02_SOUND = "..\\..\\data\\SoundTest\\v02.sound";
        private const string EXPECTED_SOUND = "..\\..\\data\\SoundTest\\expected.sound";

        // Location to save sound to.
        private const string SAVED_SOUND = "C:\\temp\\output.sound";
        #endregion

        #region Private properties
        // The object under test.
        private Sound Sound { get; set; }

        // Received property changed events.
        private List<string> ReceivedPropertyChangedEvents { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
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

        [Test]
        public void TestVolume()
        {
            // Check the default volume.
            Assert.AreEqual(VOLUME, Sound.Volume);

            // Check that no property changes have been received so far.
            Assert.AreEqual(0, ReceivedPropertyChangedEvents.Count);

            // Now change the volume.
            Sound.Volume = VOLUME_NEW;
            Assert.AreEqual(Sound.Volume, VOLUME_NEW);

            // This triggers a property change event.
            Assert.AreEqual(1, ReceivedPropertyChangedEvents.Count);
            Assert.AreEqual("Volume", ReceivedPropertyChangedEvents[0]);
        }

        [Test]
        public void TestToXElement()
        {
            // Save the XElement representation of the sound to disk.
            XElement element = Sound.ToXElement();
            element.Save(SAVED_SOUND);

            // Compare the output XElement with the expected one.
            FileAssert.AreEqual(EXPECTED_SOUND, SAVED_SOUND);
        }

        [Test]
        public void TestFromXElementV01()
        {
            // Load the V0.1 format sound from disk.
            XElement element = XElement.Load(V01_SOUND);
            Sound sound = Sound.FromXElement(element);

            // Check the sound attributes.
            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
            Assert.AreEqual(VOLUME, sound.Volume);
        }

        [Test]
        public void TestFromXElementV02()
        {
            // Load the V0.2 format sound from disk.
            XElement element = XElement.Load(V02_SOUND);
            Sound sound = Sound.FromXElement(element);

            // Check the sound attributes.
            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
            Assert.AreEqual(VOLUME_NEW, sound.Volume);
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