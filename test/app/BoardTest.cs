using NUnit.Framework;
using SoundBoard.Plugins.Test;

namespace SoundBoard.App.Test
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