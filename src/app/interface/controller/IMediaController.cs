using SoundBoard.WPF;

namespace SoundBoard.Controller
{
	public interface IMediaController
    {
        #region Properties
        SoundBlock CurrentSoundBlock { get; }
        #endregion

        #region Methods
        void Play(SoundBlock xiSoundBlock);
        void Stop();
        #endregion
    }
}
