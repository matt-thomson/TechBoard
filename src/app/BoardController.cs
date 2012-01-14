/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace TechBoard.App
{
    public class BoardController : DependencyObject, IBoardController
    {
        #region Private members
        private static DependencyProperty CurrentBoardProperty = DependencyProperty.Register("CurrentBoard",
                                                                                             typeof(Board),
                                                                                             typeof(BoardController));
        #endregion

        #region Properties
        public Board CurrentBoard 
        {
            get
            {
                return (Board)GetValue(CurrentBoardProperty);
            }
            private set
            {
                SetValue(CurrentBoardProperty, value);
            }
        }

        public Dictionary<Guid, Type> BlockTypes { get; private set; }
        #endregion

        #region Constructor
        public BoardController()
        {
            // Create a new board.
            New();
            
            // Populate the list of block types by looking at the DLLs in the plugins directory.
            BlockTypes = new Dictionary<Guid, Type>();

            foreach (string dll in Directory.GetFiles("plugins\\", "*.dll"))
            {
                // Don't load the techboard library DLL, as we've already loaded it.
                if (dll != "plugins\\techboard-lib.dll")
                {
                    LoadBlocksFromDll(dll);
                }
            }
        }
        #endregion

        #region Public methods
        public void New()
        {
            CurrentBoard = new Board();
        }

        public void Load(string xiFileName)
        {
            // Create a new board.
            New();

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

                        foreach (XElement propertyElement in blockElement.Descendants())
                        {
                            PropertyInfo propInfo = blockType.GetProperty(propertyElement.Name.ToString());

                            if (propInfo != null)
                            {
                                // Find the editor property attribute.  This contains the function to convert
                                // the saved string to the property value.
                                object[] attrs = propInfo.GetCustomAttributes(typeof(BlockPropertyAttribute), false);
                                BlockPropertyAttribute attr = attrs[0] as BlockPropertyAttribute;

                                propInfo.SetValue(block, 
                                                  attr.FromFile(xiFileName, propertyElement.Value), 
                                                  null);
                            }
                        }

                        CurrentBoard.Blocks.Add(block);
                    }
                }
            }
        }

        public void Save(string xiFileName)
        {
            // Create an XML document for this techboard.
            XElement doc = new XElement("Board");

            XElement blocksElement = new XElement("Blocks");
            doc.Add(blocksElement);

            // Format the list of blocks as an XML element.
            foreach (UserControl block in CurrentBoard.Blocks)
            {
                XElement blockElement = new XElement("Block");

                // Find the GUID for the block type, and add it to the element.
                object[] attrs = block.GetType().GetCustomAttributes(typeof(BlockAttribute), false);
                BlockAttribute attr = attrs[0] as BlockAttribute;
                blockElement.SetAttributeValue("Guid", attr.Guid);

                // Add the properties to the block.
                var properties = from p in block.GetType().GetProperties()
                                 where p.IsDefined(typeof(BlockPropertyAttribute), false)
                                 select p;

                foreach (PropertyInfo prop in properties)
                {
                    // Find the editor property attribute.  This contains the function to convert
                    // the saved string to the property value.
                    attrs = prop.GetCustomAttributes(typeof(BlockPropertyAttribute), false);
                    BlockPropertyAttribute propAttr = attrs[0] as BlockPropertyAttribute;
                    string value = propAttr.ToFile(xiFileName, prop.GetValue(block, null));

                    blockElement.Add(new XElement(prop.Name, value));
                }

                // Add the block to the list.
                blocksElement.Add(blockElement);
            }

            // Save to file.
            doc.Save(xiFileName);
        }

        public void Add(UserControl xiBlock)
        {
            CurrentBoard.Blocks.Add(xiBlock);
        }

        public void Remove(UserControl xiBlock)
        {
            CurrentBoard.Blocks.Remove(xiBlock);
        }

        public void MoveUp(UserControl xiBlock)
        {
            int index = CurrentBoard.Blocks.IndexOf(xiBlock);

            if (index > 0)
            {
                CurrentBoard.Blocks.Move(index, index - 1);
            }
        }

        public void MoveDown(UserControl xiBlock)
        {
            int index = CurrentBoard.Blocks.IndexOf(xiBlock);

            if (index < CurrentBoard.Blocks.Count - 1)
            {
                CurrentBoard.Blocks.Move(index, index + 1);
            }
        }
        #endregion

        #region Private methods
        private void LoadBlocksFromDll(string path)
        {
            Assembly asm = null;

            try
            {
                asm = Assembly.LoadFrom(path);
            }
            catch (FileLoadException e)
            {
                Console.WriteLine(e);
            }

            if (asm != null)
            {
                var blockTypes = from t in asm.GetTypes()
                                 where ((t.IsClass) &&
                                        (t.IsDefined(typeof(BlockAttribute), false)))
                                 select t;

                foreach (Type blockType in blockTypes)
                {
                    // Find the block attribute, and extract the GUID from it.
                    object[] attrs = blockType.GetCustomAttributes(typeof(BlockAttribute), false);
                    BlockAttribute attr = attrs[0] as BlockAttribute;
                    BlockTypes.Add(attr.Guid, blockType);
                }
            }
        }
        #endregion
    }
}
