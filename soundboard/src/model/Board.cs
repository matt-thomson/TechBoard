using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using System.Xml.Linq;
using SoundBoard.WPF;
using SoundBoard.Test;

namespace SoundBoard.Model
{
    public class Board
    {
        #region Static properties
        public static Dictionary<Guid, Type> BlockTypes { get; private set; }
        #endregion

        #region Public properties
        public ObservableCollection<UserControl> Blocks { get; private set; }
        #endregion

        #region Constructors
        public Board() 
        {
            Blocks = new ObservableCollection<UserControl>();
            BlockTypes = new Dictionary<Guid, Type>();

            // TODO populate dynamically
            BlockTypes.Add(new Guid("{13EBEAD3-3B03-4897-AFB9-3238632A3735}"), typeof(SoundBlock));
            BlockTypes.Add(new Guid("{018C517C-973E-4954-BAA1-9D0A3ADA375F}"), typeof(TestBlock));
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
                XElement blocksElement = boardElement.Element("Blocks");

                if (blocksElement != null)
                {
                    foreach (XElement blockElement in blocksElement.Descendants("Block"))
                    {
                        // Find the relevant type by looking up the GUID.
                        Guid guid = new Guid(blockElement.Attribute("Guid").Value);
                        Type blockType = BlockTypes[guid];
                        UserControl block = Activator.CreateInstance(blockType) as UserControl;

                        foreach (XElement propertyElement in blocksElement.Descendants())
                        {
                            PropertyInfo propInfo = blockType.GetProperty(propertyElement.Name.ToString());

                            if (propInfo != null)
                            {
                                // Find the editor property attribute.  This contains the function to convert
                                // the saved string to the property value.
                                object[] attrs = propInfo.GetCustomAttributes(typeof(BlockPropertyAttribute), false);
                                BlockPropertyAttribute attr = attrs[0] as BlockPropertyAttribute;

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
            foreach (UserControl block in Blocks)
            {                
                XElement blockElement = new XElement("Block");
                
                // Find the GUID for the block type, and add it to the element.
                object[] attrs = block.GetType().GetCustomAttributes(typeof(BlockAttribute), false);
                BlockAttribute attr = attrs[0] as BlockAttribute;
                blockElement.SetAttributeValue("Guid", attr.Guid);

                // Add the properties to the block.
                var properties = from p in block.GetType().GetProperties()
                                 where p.IsDefined(typeof(BlockPropertyAttribute), false)
                                 select new XElement(p.Name, p.GetValue(block, null));
                blockElement.Add(properties);

                // Add the block to the list.
                blocksElement.Add(blockElement);
            }

            // Save to file.
            doc.Save(xiFileName);
        }
        #endregion
    }
}