﻿using System.Collections.Generic;
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
            SoundBlock = new SoundBlock(TITLE, FILE_NAME);
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

        [Test]
        public void TestToXElement()    
        {
            // Save the XElement representation of the soundBlock block to disk.
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
            Assert.AreEqual(TITLE, block.Title);
            Assert.AreEqual(FILE_NAME, block.FileName);
            Assert.AreEqual(VOLUME, block.Volume);
        }

        [Test]
        public void TestFromXElementV02()
        {
            // Load the V0.2 format sound block from disk.
            XElement element = XElement.Load(V02_BLOCK);
            SoundBlock block = SoundBlock.FromXElement(element);

            // Check the sound block attributes.            
            Assert.AreEqual(TITLE, block.Title);
            Assert.AreEqual(FILE_NAME, block.FileName);
            Assert.AreEqual(VOLUME_NEW, block.Volume);
        }
        #endregion
    }
}
