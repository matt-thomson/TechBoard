using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace SoundBoard.Model
{
    public class Board
    {
        #region Public properties
        public ObservableCollection<SoundBlock> Blocks { get; private set; }
        #endregion

        #region Constructors
        public Board() 
        {
            Blocks = new ObservableCollection<SoundBlock>();
        }
        #endregion

        #region Static methods
        public static Board Load(string xiFileName)
        {
            // Create a new board.
            Board soundBoard = new Board();

            // Load and parse the file.
            XDocument doc = XDocument.Load(xiFileName);

            // TODO New file format
            // Extract the blocks from the file.
            foreach (XElement blockElement in doc.Descendants("Board"))
            {               
                // TODO Mapping of block types to plugins
                XElement soundsElement = blockElement.Element("Sounds");

                if (soundsElement != null)
                {
                    foreach (XElement soundElement in soundsElement.Descendants("Sound"))
                    {
                        SoundBlock block = SoundBlock.FromXElement(soundElement);
                        soundBoard.Blocks.Add(block);
                    }
                }
            }
            
            return soundBoard;
        }
        #endregion

        #region Public methods
        public void Save(String xiFileName)
        {
            // Create an XML document for this soundboard.
            XElement doc = new XElement("Board");

            // TODO New file format
            XElement soundsElement = new XElement("Sounds");
            doc.Add(soundsElement);

            // Format the list of blocks as an XML element.
            foreach (SoundBlock block in Blocks)
            {
                soundsElement.Add(block.ToXElement());
            }

            // Save to file.
            doc.Save(xiFileName);
        }
        #endregion
    }
}