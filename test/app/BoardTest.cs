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

using NUnit.Framework;
using TechBoard.Plugins.Test;

namespace TechBoard.App.Test
{
    [TestFixture, RequiresSTA]
    public class BoardTest
    {
        #region Constants        
        // Properties of a block.
        private const string TEXT_1 = "Some text 1";
        private const string TEXT_2 = "Some text 2";
        #endregion

        #region Private properties
        // The object under test.
        private Board Board { get; set; }
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the object under test.
            Board = new Board();

            // Create two blocks, and add them to the board.
            TestBlock block = new TestBlock();
            block.MyProperty = TEXT_1;
            Board.Blocks.Add(block);

            block = new TestBlock();
            block.MyProperty = TEXT_2;
            Board.Blocks.Add(block);
        }
        #endregion

        #region Test cases
        [Test]
        public void TestBoardContents()
        {
            Assert.AreEqual(2, Board.Blocks.Count);

            TestBlock block = Board.Blocks[0] as TestBlock;
            Assert.AreEqual(TEXT_1, block.MyProperty);

            block = Board.Blocks[1] as TestBlock;
            Assert.AreEqual(TEXT_2, block.MyProperty);
        }
        #endregion
    }
}