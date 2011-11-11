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
            MediaController controller = new MediaController();
            SoundBoardWindow window = new SoundBoardWindow(controller);

            base.OnStartup(e);

            window.Show();
        }
    }
}
