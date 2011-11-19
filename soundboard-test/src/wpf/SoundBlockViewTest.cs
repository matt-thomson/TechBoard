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
    public class SoundBlockViewTest
    {
        #region Constants
        // Properties of a sound.
        private const string TITLE = "Sound Title";
        private const string FILE_NAME = "sound.mp3";
        private const double VOLUME = 0.8;
        #endregion

        #region Private properties
        // Object under test.
        SoundBlockView mSoundBlockView;

        // Mock objects.
        private Mock<IMediaController> mMockMediaController;

        // Other objects.
        private SoundBlock mSoundBlock;
        private Window mWindow;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create a sound block for this view.
            mSoundBlock = new SoundBlock();

            // Create mock objects.
            mMockMediaController = new Mock<IMediaController>(MockBehavior.Strict);
            SoundBlockView.MediaController = mMockMediaController.Object;

            // Create the sound block view, and set the data context.
            mSoundBlockView = new SoundBlockView();
            mSoundBlockView.DataContext = mSoundBlock;

            // Create a window, and add the view to it.
            mWindow = new Window();
            mWindow.Content = mSoundBlockView;

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
        public void TestLoadBoard()
        {
            // Check that the block is empty.
            Assert.AreEqual(0, mSoundBlockView.Items.Count);

            // Create a sound.
            Sound sound = new Sound(TITLE, FILE_NAME);
            sound.Volume = VOLUME;

            // Add the sound to the block.
            mSoundBlock.Sounds.Add(sound);

            // Check the block is loaded.
            Assert.AreEqual(1, mSoundBlockView.Items.Count);

            // Check the button text.
            Button button = GetButton(0);
            Assert.IsNotNull(button);
            Assert.AreEqual(TITLE, button.Content);
        }

        [Test]
        public void TestAddRemoveSounds()
        {
            // Check the block is empty.
            Assert.AreEqual(0, mSoundBlockView.Items.Count);

            // Create a sound, and add it to the block.
            Sound sound = new Sound(TITLE, FILE_NAME);
            sound.Volume = VOLUME;
            mSoundBlock.Sounds.Add(sound);

            // Check the sound is added to the view.
            Assert.AreEqual(1, mSoundBlockView.Items.Count);

            // Check the button text.
            Button button = GetButton(0);
            Assert.IsNotNull(button);
            Assert.AreEqual(TITLE, button.Content);

            // Remove the sound from the block.
            mSoundBlock.Sounds.Remove(sound);

            // Check the block is empty.
            Assert.AreEqual(0, mSoundBlockView.Items.Count);
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
            mSoundBlockView.UpdateLayout();

            // Find the ListViewItem for the specified index.
            ListViewItem item = (ListViewItem)mSoundBlockView.ItemContainerGenerator.ContainerFromIndex(xiIndex);

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
