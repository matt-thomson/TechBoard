using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SoundBoard.Plugins.Time
{
    /// <summary>
    /// Interaction logic for StopWatchBlock.xaml
    /// </summary>
    [Block("1AD71F7E-2DBB-4AAE-B263-9E742DFB1118")]
    public partial class StopWatchBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty DurationProperty = DependencyProperty.Register("Duration",
                                                                                         typeof(string),
                                                                                         typeof(StopWatchBlock));
        private static DependencyProperty StoppedProperty = DependencyProperty.Register("Stopped",
                                                                                        typeof(bool),
                                                                                        typeof(StopWatchBlock));
        #endregion

        #region Private members
        private ITimerController mTimerController;
        private DateTime mStartTime;
        private TimeSpan mStopDuration;

        private string Duration
        {
            get { return (string)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        private bool Stopped
        {
            get { return (bool)GetValue(StoppedProperty); }
            set { SetValue(StoppedProperty, value); }
        }
        #endregion

        #region Constructors
        public StopWatchBlock()
        {
            mTimerController = TimerController.StaticInstance;
            Init();
        }

        public StopWatchBlock(ITimerController xiTimerController)
        {
            mTimerController = xiTimerController;
            Init();
        }
        #endregion

        #region Initialization
        public void Init()
        {
            // Initialize.
            InitializeComponent();

            // Register for timer pops.
            mTimerController.TimerPop += HandleTimerPop;

            // Set up the initial properties.
            ResetDuration();
            Stopped = true;
        }
        #endregion

        #region Button event handlers
        private void HandleStartStopButtonClick(object sender, RoutedEventArgs e)
        {
            if (Stopped)
            {
                Stopped = false;
                mStartTime = mTimerController.Now;
            }
            else
            {
                Stopped = true;
                TimeSpan duration = mTimerController.Now.Subtract(mStartTime);
                mStopDuration = mStopDuration.Add(duration);
            }
        }

        private void HandleResetButtonClick(object sender, RoutedEventArgs e)
        {
            ResetDuration();
        }
        #endregion

        #region Timer event handlers
        private void HandleTimerPop()
        {
            if (!Stopped)
            {
                TimeSpan duration = mTimerController.Now.Subtract(mStartTime);
                duration = duration.Add(mStopDuration);
                Duration = duration.ToString(@"hh\:mm\:ss\.ff");
            }
        }
        #endregion

        #region Private methods
        private void ResetDuration()
        {
            Duration = "00:00:00.00";
            mStopDuration = new TimeSpan(0);
        }
        #endregion
    }

    internal class StartStopButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;
            bool stopped = (bool)value;

            if (stopped)
            {
                text = "Start";
            }
            else
            {
                text = "Stop";
            }

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
