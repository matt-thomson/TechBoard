using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SoundBoard.Model.Test
{
    [TestFixture]
    public class SoundBlockTest
    {
        #region Constants
        // Location of test blocks.
        private const string V01_BLOCK = "..\\..\\data\\SoundBlockTest\\v01.block";
        private const string EXPECTED_BLOCK = "..\\..\\data\\SoundBlockTest\\expected.block";
        
        // Location to save board to.
        private const string SAVED_BLOCK = "C:\\temp\\output.block";

        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.5;
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

            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);

            // Add it to the block.
            SoundBlock.Sounds.Add(sound);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestBlockContents()
        {
            Assert.AreEqual(1, SoundBlock.Sounds.Count);

            Sound sound = SoundBlock.Sounds[0];

            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
            Assert.AreEqual(VOLUME, sound.Volume);
        }

        [Test]
        public void TestToXElement()
        {
            // Save the XElement representation of the sound block to disk.
            XElement element = SoundBlock.ToXElement();
            element.Save(SAVED_BLOCK);

            // Compare the output XElement with the expected one.
            FileAssert.AreEqual(EXPECTED_BLOCK, SAVED_BLOCK);
        }

        [Test]
        public void TestFromXElementV01()
        {
            // Load the V0.1 format sound block from disk.
            XElement element = XElement.Load(V01_BLOCK);
            SoundBlock block = SoundBlock.FromXElement(element);

            // Check the sound block attributes.
            Assert.AreEqual(1, block.Sounds.Count);

            Sound sound = block.Sounds[0];
            Assert.AreEqual(TITLE, sound.Title);
            Assert.AreEqual(FILE_NAME, sound.FileName);
            Assert.AreEqual(VOLUME, sound.Volume);
        }
        #endregion
    }
}
