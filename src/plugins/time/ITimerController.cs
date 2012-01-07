using System;

namespace SoundBoard.Plugins.Time
{
    public delegate void TimerDelegate(DateTime xiDateTime);

    public interface ITimerController
    {
        #region Properties
        DateTime Now { get; }
        #endregion

        #region Events
        event TimerDelegate TimerPop;
        #endregion
    }
}
