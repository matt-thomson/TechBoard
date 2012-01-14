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

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;

namespace TechBoard.Test
{
    [TestFixture, RequiresSTA]
    public class FileBlockPropertyEditorTest : DependencyObject
    {
        #region Dependency properties
        private static DependencyProperty DepProperty = DependencyProperty.Register("MyProperty",
                                                                                    typeof(string),
                                                                                    typeof(FileBlockPropertyEditorTest));
        #endregion

        #region Constants
        private const string FILTER = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        private const string FILENAME_1 = "file.txt";
        private const string FILENAME_2 = "other_file.txt";
        #endregion

        #region Public properties
        // Property for the editor to bind to.
        [FileBlockProperty(FILTER)]
        public string MyProperty
        {
            get { return (string)GetValue(DepProperty); }
            set { SetValue(DepProperty, value); }
        }
        #endregion

        #region Private members
        // Object under test.
        private FileBlockPropertyEditor mEditor;

        // Mock objects.
        private Mock<IFileDialogController> mMockFileDialogController;

        // Other objects.
        private Window mWindow;
        private TextBox mTextBox;
        private Button mButton;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the property.
            MyProperty = FILENAME_1;

            // Create mock objects.
            mMockFileDialogController = new Mock<IFileDialogController>(MockBehavior.Strict);

            // Create the editor.
            mEditor = new FileBlockPropertyEditor(mMockFileDialogController.Object);

            // Set the data context for the editor.
            PropertyInfo property = GetType().GetProperty("MyProperty");
            PropertyMapping mapping = new PropertyMapping(property, this);
            mEditor.DataContext = mapping;

            // Get references to the controls in the editor.
            Grid grid = mEditor.Content as Grid;
            mTextBox = grid.Children[0] as TextBox;
            mButton = grid.Children[1] as Button;

            // Create a window, and add the editor to it.
            mWindow = new Window();
            mWindow.Content = mEditor;

            // Show the window to activate the bindings.
            mWindow.Show();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestTextBox()
        {
            // Check the initial value of the text box.
            Assert.AreEqual(FILENAME_1, mTextBox.Text);

            // Set the property, and check that the textbox is updated.
            MyProperty = FILENAME_2;
            Assert.AreEqual(FILENAME_2, mTextBox.Text);

            // Edit the textbox, and check that the property is updated.
            mTextBox.Text = FILENAME_1;
            Assert.AreEqual(FILENAME_1, MyProperty);
        }

        [Test]
        public void TestOpenFileOK()
        {
            // Clicking on the button will trigger the controller to display the "Open File" 
            // dialog.  The user selects a file and clicks "OK".
            mMockFileDialogController.Setup(c => c.OpenFile(FILTER)).Returns(FILENAME_2);

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);

            // This updates the text box and property.            
            Assert.AreEqual(FILENAME_2, mTextBox.Text);
            Assert.AreEqual(FILENAME_2, MyProperty);
        }

        [Test]
        public void TestOpenFileCancel()
        {
            // Clicking on the button will trigger the controller to display the "Open File" 
            // dialog.  The user clicks "Cancel".
            mMockFileDialogController.Setup(c => c.OpenFile(FILTER)).Returns<string>(null);

            // Click on the button.
            RoutedEventArgs args = new RoutedEventArgs(Button.ClickEvent, mButton);
            mButton.RaiseEvent(args);

            // The text box and property are not updated.
            Assert.AreEqual(FILENAME_1, mTextBox.Text);
            Assert.AreEqual(FILENAME_1, MyProperty);
        }
        #endregion

        #region Clean up
        [TearDown]
        public void TestEnd()
        {
            mWindow.Hide();
        }
        #endregion
    }
}
