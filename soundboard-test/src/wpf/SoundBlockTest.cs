using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Moq;
using NUnit.Framework;
using SoundBoard.Controller;
using SoundBoard.Model;

namespace SoundBoard.WPF.Test
{
    [TestFixture, RequiresSTA]
    public class SoundBlockTest
    {
        #region Constants
        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.8;
        #endregion

        #region Private properties
        // Object under test.
        SoundBlock mSoundBlock;

        // Mock objects.
        private Mock<IBoardController> mMockBoardController;
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private Window mWindow;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockBoardController = new Mock<IBoardController>(MockBehavior.Strict);
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);

            // Create the sound block.
            mSoundBlock = new SoundBlock(mMockBoardController.Object,
                                         mMockMediaController.Object);

            // Create a window, and add the sound block to it.
            mWindow = new Window();
            mWindow.Content = mSoundBlock;

            // Show the window to activate the bindings.
            mWindow.Show();
        }
        #endregion

        #region Clean up
        [TearDown]
        public void TestEnd()
        {
            mWindow.Hide();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestDataContext()
        {
            Assert.AreEqual(mSoundBlock.DataContext, mMockBoardController.Object);
        }

        [Test]
        public void TestLoadBoard()
        {
            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);
            sound.Volume = VOLUME;

            // Create a board, and add the sound to it.
            Board board = new Board();
            board.Sounds.Add(sound);

            // The BoardController will return this board.
            mMockBoardController.Setup(c => c.CurrentBoard).Returns(board);

            // Fire the BoardController's PropertyChanged event.
            mMockBoardController.Raise(c => c.PropertyChanged += null,
                                       mMockBoardController.Object, 
                                       new PropertyChangedEventArgs("CurrentBoard"));

            // Check the board is loaded.
            Assert.AreEqual(1, mSoundBlock.Items.Count);

            // Check the button text.
            Button button = GetButton(0);
            Assert.IsNotNull(button);
            Assert.AreEqual(TITLE, button.Content);
        }

        [Test]
        public void TestAddRemoveSounds()
        {
            // Create a board.
            Board board = new Board();
            
            // The BoardController will return this board.
            mMockBoardController.Setup(c => c.CurrentBoard).Returns(board);

            // Fire the BoardController's PropertyChanged event.
            mMockBoardController.Raise(c => c.PropertyChanged += null,
                                       mMockBoardController.Object,
                                       new PropertyChangedEventArgs("CurrentBoard"));

            // Check the block is empty.
            Assert.AreEqual(0, mSoundBlock.Items.Count);

            // Create a sound, and add it to the board.
            Sound sound = new Sound(TITLE, FILE_NAME);
            sound.Volume = VOLUME;
            board.Sounds.Add(sound);

            // Check the sound is added to the block.
            Assert.AreEqual(1, mSoundBlock.Items.Count);

            // Check the button text.
            Button button = GetButton(0);
            Assert.IsNotNull(button);
            Assert.AreEqual(TITLE, button.Content);

            // Remove the sound from the board.
            board.Sounds.Remove(sound);

            // Check the block is empty.
            Assert.AreEqual(0, mSoundBlock.Items.Count);
        }

        [Test]
        public void TestClickButton()
        {
            // Set up a board with one button.
            TestLoadBoard();

            // Find the button.
            Button button = GetButton(0);

            // Clicking the button will play the sound.
            mMockMediaController.Setup(c => c.Play(FILE_NAME, VOLUME));

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, button);
            button.RaiseEvent(args);
        }
        #endregion

        #region Utility functions
        private Button GetButton(int xiIndex)
        {
            // Update the layout.
            mSoundBlock.UpdateLayout();

            // Find the ListViewItem for the specified index.
            ListViewItem item = (ListViewItem)mSoundBlock.ItemContainerGenerator.ContainerFromIndex(xiIndex);

            // Create a stack of framework elements.
            Stack<FrameworkElement> tree = new Stack<FrameworkElement>();
            tree.Push(item);

            while (tree.Count > 0)
            {
                FrameworkElement current = tree.Pop();
                if (current is Button) 
                {
                    return current as Button;
                }

                int count = VisualTreeHelper.GetChildrenCount(current);
                for (int i = 0; i < count; ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(current, i);
                    if (child is FrameworkElement)
                    {
                        tree.Push((FrameworkElement)child);
                    }
                }
            }

            return null;
        }
        #endregion
    }
}
