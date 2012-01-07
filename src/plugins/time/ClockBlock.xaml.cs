using System;
using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.Plugins.Time
{
    /// <summary>
    /// Interaction logic for ClockBlock.xaml
    /// </summary>
    [Block("{315E02FB-E665-4516-B6E8-871A540B747C}")]
    public partial class ClockBlock : UserControl
    {
        #region Dependency properties
        private static DependencyProperty TimeProperty = DependencyProperty.Register("Time",
                                                                                     typeof(string),
                                                                                     typeof(ClockBlock));
        #endregion

        #region Private members
        private ITimerController mTimerController;

        private string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        #endregion

        #region Constructors
        public ClockBlock()
        {
            mTimerController = TimerController.StaticInstance;
            Init();
        }

        public ClockBlock(ITimerController xiTimerController)
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

            // Set the initial time.
            Time = mTimerController.Now.ToString("T");
        }
        #endregion

        #region Event handlers
        private void HandleTimerPop(DateTime xiDateTime)
        {
            Time = xiDateTime.ToString("T");
        }
        #endregion
    }
}
