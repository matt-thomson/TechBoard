using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using NUnit.Framework;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class SoundBlockTest
    {
        #region Constants
        // Location of test blocks.
        private const string V01_BLOCK = "..\\..\\data\\SoundBlockTest\\v01.block";
        private const string V02_BLOCK = "..\\..\\data\\SoundBlockTest\\v02.block";
        private const string EXPECTED_BLOCK = "..\\..\\data\\SoundBlockTest\\expected.block";
        
        // Location to save board to.
        private const string SAVED_BLOCK = "C:\\temp\\output.block";

        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string TITLE_NEW = "New Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.5;
        private const double VOLUME_NEW = 0.8;
        #endregion

        #region Private properties
        // The object under test.
        private SoundBlock SoundBlock { get; set; }        
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the object under test.
            SoundBlock = new SoundBlock();
            SoundBlock.Title = TITLE;
            SoundBlock.FileName = FILE_NAME;
        }
        #endregion

        #region Test cases
        [Test]
        public void TestTitle()
        {
            Assert.AreEqual(TITLE, SoundBlock.Title);

            // Now change the title.
            SoundBlock.Title = TITLE_NEW;
            Assert.AreEqual(SoundBlock.Title, TITLE_NEW);
        }

        [Test]
        public void TestFileName()
        {
            Assert.AreEqual(FILE_NAME, SoundBlock.FileName);
        }

        [Test]
        public void TestVolume()
        {
            // Check the default volume.
            Assert.AreEqual(VOLUME, SoundBlock.Volume);

            // Now change the volume.
            SoundBlock.Volume = VOLUME_NEW;
            Assert.AreEqual(SoundBlock.Volume, VOLUME_NEW);
        }
        #endregion
    }
}
