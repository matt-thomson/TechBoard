using System.Windows;

namespace SoundBoard.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize controllers.
            BoardController boardController = new BoardController();

            // Create the main window.
            MainWindow window = new MainWindow(boardController);

            // Call into the base class.
            base.OnStartup(e);

            // Show the window.
            window.Show();
        }
    }
}
