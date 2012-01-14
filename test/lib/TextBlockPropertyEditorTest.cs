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
using NUnit.Framework;

namespace TechBoard.Test
{
    [TestFixture, RequiresSTA]
    public class TextBlockPropertyEditorTest : DependencyObject
    {
        #region Dependency properties
        private static DependencyProperty DepProperty = DependencyProperty.Register("MyProperty",
                                                                                    typeof(string),
                                                                                    typeof(TextBlockPropertyEditorTest));
        #endregion

        #region Constants
        private const string TEXT_1 = "Some text";
        private const string TEXT_2 = "Some other text";
        #endregion

        #region Public properties
        // Property for the editor to bind to.
        [TextBlockProperty]
        public string MyProperty
        {
            get { return (string)GetValue(DepProperty); }
            set { SetValue(DepProperty, value); }
        }
        #endregion

        #region Private members
        // Object under test.
        private TextBlockPropertyEditor mEditor;

        // Other objects.
        private Window mWindow;
        private TextBox mTextBox;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Set up the property.
            MyProperty = TEXT_1;

            // Create the editor.
            mEditor = new TextBlockPropertyEditor();

            // Set the data context for the editor.
            PropertyInfo property = GetType().GetProperty("MyProperty");
            PropertyMapping mapping = new PropertyMapping(property, this);
            mEditor.DataContext = mapping;

            // Get references to the controls in the editor.
            mTextBox = mEditor.Content as TextBox;

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
            Assert.AreEqual(TEXT_1, mTextBox.Text);

            // Set the property, and check that the textbox is updated.
            MyProperty = TEXT_2;
            Assert.AreEqual(TEXT_2, mTextBox.Text);

            // Edit the textbox, and check that the property is updated.
            mTextBox.Text = TEXT_1;
            Assert.AreEqual(TEXT_1, MyProperty);
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
