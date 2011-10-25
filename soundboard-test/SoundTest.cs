using NUnit.Framework;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class SoundTest
    {
        #region Constants
        // Properties of the sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        #endregion

        #region Private properties
        // The object under test.
        private Sound Sound { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void Init()
        {
            // Set up the object under test.
            Sound = new Sound(TITLE, FILE_NAME);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestTitle()
        {
            Assert.AreEqual(TITLE, Sound.Title);
        }

        [Test]
        public void TestFileName()
        {
            Assert.AreEqual(FILE_NAME, Sound.FileName);
        }
        #endregion
    }
}
