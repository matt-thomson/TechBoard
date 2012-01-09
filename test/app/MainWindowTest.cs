using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Moq;
using NUnit.Framework;
using TechBoard.Plugins.Test;

namespace TechBoard.App.Test
{
    [TestFixture, RequiresSTA]
    public class MainWindowTest
    {
        #region Constants
        private const string FILTER = "TechBoards (*.board)|*.board|All files (*.*)|*.*";
        private const string BOARD_FILE_NAME = "my.board";
        #endregion

        #region Private properties
        // Object under test.
        private MainWindow mWindow;

        // Mock objects.
        private Mock<IBoardController> mMockBoardController;
        private Mock<IFileDialogController> mMockFileDialogController;

        // Other objects.
        private Menu mMenu;
        private ListView mBlocksList;
        #endregion

        #region Initialization
        [SetUp]
        public void TestInit()
        {
            // Create mock objects.
            mMockBoardController = new Mock<IBoardController>(MockBehavior.Strict);
            mMockFileDialogController = new Mock<IFileDialogController>(MockBehavior.Strict);

            // Create the main window.
            mWindow = new MainWindow(mMockBoardController.Object,
                                     mMockFileDialogController.Object);

            // Set up references to other objects.
            DockPanel panel = mWindow.Content as DockPanel;
            mMenu = panel.Children[0] as Menu;
            mBlocksList = panel.Children[1] as ListView;

            // Show the window to activate it.
            mWindow.Show();
        }
        #endregion

        #region Test cases
        [Test]
        public void TestMenuOptionNew()
        {
            // Find the "New" menu option.
            MenuItem option = GetMenuOption("_File", "_New");

            // Expect to create a new board when the option is clicked.
            mMockBoardController.Setup(c => c.New());

            // Click on the option.
            RoutedEventArgs args = new RoutedEventArgs(MenuItem.ClickEvent, option);
            option.RaiseEvent(args);
        }

        [Test]
        public void TestMenuOptionOpenOK()
        {
            // Find the "Open" menu option.
            MenuItem option = GetMenuOption("_File", "_Open...");

            // Expect to display the "Open" dialog when the option is clicked.  
            // Select a file from the dialog.
            mMockFileDialogController.Setup(c => c.OpenFile(FILTER)).Returns(BOARD_FILE_NAME);

            // This triggers the board to be opened.
            mMockBoardController.Setup(c => c.Load(BOARD_FILE_NAME));

            // Click on the option.
            RoutedEventArgs args = new RoutedEventArgs(MenuItem.ClickEvent, option);
            option.RaiseEvent(args);
        }

        [Test]
        public void TestMenuOptionOpenCancel()
        {
            // Find the "Open" menu option.
            MenuItem option = GetMenuOption("_File", "_Open...");

            // Expect to display the "Open" dialog when the option is clicked.  
            // Cancel the dialog.
            mMockFileDialogController.Setup(c => c.OpenFile(FILTER)).Returns((string)null);

            // Click on the option.  Nothing else happens.
            RoutedEventArgs args = new RoutedEventArgs(MenuItem.ClickEvent, option);
            option.RaiseEvent(args);
        }

        [Test]
        public void TestMenuOptionSaveOK()
        {
            // Find the "Save" menu option.
            MenuItem option = GetMenuOption("_File", "_Save...");

            // Expect to display the "Save" dialog when the option is clicked.  
            // Select a file from the dialog.
            mMockFileDialogController.Setup(c => c.SaveFile(FILTER)).Returns(BOARD_FILE_NAME);

            // This triggers the board to be saved.
            mMockBoardController.Setup(c => c.Save(BOARD_FILE_NAME));

            // Click on the option.
            RoutedEventArgs args = new RoutedEventArgs(MenuItem.ClickEvent, option);
            option.RaiseEvent(args);
        }

        [Test]
        public void TestMenuOptionSaveCancel()
        {
            // Find the "Save" menu option.
            MenuItem option = GetMenuOption("_File", "_Save...");

            // Expect to display the "Save" dialog when the option is clicked.  
            // Cancel the dialog.
            mMockFileDialogController.Setup(c => c.SaveFile(FILTER)).Returns((string)null);

            // Click on the option.  Nothing else happens.
            RoutedEventArgs args = new RoutedEventArgs(MenuItem.ClickEvent, option);
            option.RaiseEvent(args);
        }

        [Test]
        public void TestMenuOptionStayOnTop()
        {
            // Find the "Stay On Top" menu option.
            MenuItem option = GetMenuOption("_View", "Stay on _Top");

            // Check the value of the topmost property.
            Assert.IsFalse(mWindow.Topmost);
            Assert.IsFalse(option.IsChecked);

            // Check the option.
            option.IsChecked = true;

            // The window becomes topmost.
            Assert.IsTrue(mWindow.Topmost);

            // Uncheck the option.
            option.IsChecked = false;

            // The window is no longer topmost.
            Assert.IsFalse(mWindow.Topmost);
        }
        #endregion

        #region Utility functions
        private MenuItem GetMenuOption(string xiMenuName, string xiMenuOption)
        {
            // Find the menu.
            MenuItem[] menus = (from MenuItem m in mMenu.Items
                                where (string)m.Header == xiMenuName
                                select m).ToArray<MenuItem>();
            Assert.AreEqual(1, menus.Length);
            MenuItem menu = menus[0];

            // Find the option.
            MenuItem[] options = (from Control m in menu.Items
                                  where ((m.GetType() == typeof(MenuItem)) &&
                                         (string)(((MenuItem)m).Header) == xiMenuOption)
                                  select m as MenuItem).ToArray<MenuItem>();
            Assert.AreEqual(1, options.Length);

            return options[0];            
        }
        #endregion
    }
}
