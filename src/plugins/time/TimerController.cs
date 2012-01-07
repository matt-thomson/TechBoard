using System;
using System.Windows.Threading;

namespace SoundBoard.Plugins.Time
{
    public class TimerController : ITimerController
    {
        #region Events
        public event TimerDelegate TimerPop;
        #endregion

        #region Private members
        private static TimerController mStaticInstance;
        private DispatcherTimer mTimer = new DispatcherTimer();
        #endregion

        #region Properties
        public static TimerController StaticInstance
        {
            get
            {
                if (mStaticInstance == null)
                {
                    mStaticInstance = new TimerController();
                }

                return mStaticInstance;
            }
        }

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
        #endregion

        #region Constructor
        public TimerController()
        {
            // Set the timer.
            mTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            mTimer.Tick += HandleTimerElapsed;
            mTimer.Start();
        }
        #endregion

        #region Event handlers
        private void HandleTimerElapsed(object sender, EventArgs e)
        {
            if (TimerPop != null)
            {
                TimerPop();
            }
        }
        #endregion
    }
}
