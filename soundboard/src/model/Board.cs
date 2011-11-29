using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;

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

            // Extract the blocks from the file.
            foreach (XElement boardElement in doc.Descendants("Board"))
            {               
                // TODO Mapping of block types to plugins
                XElement blocksElement = boardElement.Element("Blocks");

                if (blocksElement != null)
                {
                    foreach (XElement blockElement in blocksElement.Descendants("Block"))
                    {
                        SoundBlock block = new SoundBlock();
                        Type blockType = block.GetType();

                        foreach (XElement propertyElement in blocksElement.Descendants())
                        {
                            PropertyInfo propInfo = blockType.GetProperty(propertyElement.Name.ToString());

                            if (propInfo != null)
                            {
                                // Find the editor property attribute.  This contains the function to convert
                                // the saved string to the property value.
                                object[] attrs = propInfo.GetCustomAttributes(typeof(EditorPropertyAttribute), false);
                                EditorPropertyAttribute attr = attrs[0] as EditorPropertyAttribute;

                                propInfo.SetValue(block, attr.FromString(propertyElement.Value), null);
                            }
                        }

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

            XElement blocksElement = new XElement("Blocks");
            doc.Add(blocksElement);

            // Format the list of blocks as an XML element.
            foreach (SoundBlock block in Blocks)
            {
                // TODO block type as GUID
                XElement blockElement = new XElement("Block");
                var properties = from p in block.GetType().GetProperties()
                                 where p.IsDefined(typeof(EditorPropertyAttribute), false)
                                 select new XElement(p.Name, p.GetValue(block, null));
                blockElement.Add(properties);
                blocksElement.Add(blockElement);
            }

            // Save to file.
            doc.Save(xiFileName);
        }
        #endregion
    }
}