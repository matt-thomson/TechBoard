using System.Windows;
using SoundBoard.Controller;

namespace SoundBoard.WPF
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
            MediaController mediaController = new MediaController();

            // Create the main window.
            SoundBoardWindow window = new SoundBoardWindow(boardController,
                                                           mediaController);

            // Call into the base class.
            base.OnStartup(e);

            // Show the window.
            window.Show();
        }
    }
}
