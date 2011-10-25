using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using SoundBoard.Model;

namespace SoundBoard.WPF.Test
{
    [TestFixture, RequiresSTA]
    public class SoundButtonTest
    {
        #region Constants
        // Properties of the sound associated with the button.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        #endregion

        #region Private properties
        // The object under test.
        private SoundButton SoundButton { get; set; }

        // The sound associated with the button.
        private Sound Sound { get; set; }

        // List of received click events.
        private List<Sound> ReceivedClickedSounds { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void Init()
        {
            // Create a sound.
            Sound = new Sound(TITLE, FILE_NAME);

            // Create the object under test, and register for events on it.
            SoundButton = new SoundButton(Sound);
            SoundButton.OnSoundButtonClick += HandleSoundButtonClick;

            // Create a list of received click events.
            ReceivedClickedSounds = new List<Sound>();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestClick()
        {
            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, SoundButton);
            SoundButton.RaiseEvent(args);

            // Check that the event was raised.
            Assert.AreEqual(1, ReceivedClickedSounds.Count);
            Assert.AreEqual(Sound, ReceivedClickedSounds[0]);
        }
        #endregion

        #region Event handlers
        private void HandleSoundButtonClick(Sound xiSound)
        {
            ReceivedClickedSounds.Add(xiSound);
        }
        #endregion
    }
}
