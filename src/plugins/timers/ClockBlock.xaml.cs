using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SoundBoard.Plugins.Timers
{
    /// <summary>
    /// Interaction logic for ClockBlock.xaml
    /// </summary>
    [Block("{315E02FB-E665-4516-B6E8-871A540B747C}")]
    public partial class ClockBlock : UserControl
    {
        #region Static members
        private static DispatcherTimer mTimer = new DispatcherTimer();
        #endregion

        #region Constructor
        public ClockBlock()
        {
            // Initialize.
            InitializeComponent();

            // Set the timer.
            mTimer.Interval = new TimeSpan(0, 0, 1);
            mTimer.Tick += HandleTimerElapsed;
            mTimer.Start();

            // Set the initial time.
            Time.Content = DateTime.Now.ToString("T");
        }
        #endregion

        #region Event handlers
        private void HandleTimerElapsed(object sender, EventArgs e)
        {
            Time.Content = DateTime.Now.ToString("T");
        }
        #endregion
    }
}
