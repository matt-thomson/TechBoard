using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SoundBoard.Model
{
    public class Board
    {
        #region Public properties
        public List<Sound> Sounds { get; private set; }
        #endregion

        #region Constructors
        public Board() 
        {
            Sounds = new List<Sound>();
        }
        #endregion

        #region Static methods
        public static Board Load(string xiFileName)
        {
            // Create a new board.
            Board soundBoard = new Board();

            // Load and parse the file.
            XDocument doc = XDocument.Load(xiFileName);

            // Extract the sounds from the file.
            foreach (XElement soundElement in doc.Descendants("Sound"))
            {               
                Sound sound = new Sound(soundElement.Element("Title").Value,
                                        soundElement.Element("FileName").Value);
                soundBoard.Sounds.Add(sound);
            }
            
            return soundBoard;
        }
        #endregion

        #region Public methods
        public void Save(String xiFileName)
        {
            // Create an XML document for this soundboard.
            XElement doc = new XElement("Board");
                
            // Format the list of sounds as an XML element.
            XElement soundList = new XElement("Sounds");

            foreach (Sound sound in Sounds)
            {
                soundList.Add(sound.ToXElement());
            }

            // Put the list of sounds into the document.
            doc.Add(soundList);

            // Save to file.
            doc.Save(xiFileName);
        }
        #endregion
    }
}
