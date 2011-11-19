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
                foreach (XElement soundsElement in blockElement.Descendants("Sounds"))
                {
                    SoundBlock block = SoundBlock.FromXElement(soundsElement);
                    soundBoard.Blocks.Add(block);
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

            // Format the list of blocks as an XML element.
            // TODO New file format                
            foreach (SoundBlock block in Blocks)
            {
                doc.Add(block.ToXElement());
            }

            // Save to file.
            doc.Save(xiFileName);
        }
        #endregion
    }
}