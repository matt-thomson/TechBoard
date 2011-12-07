using SoundBoard.WPF;

namespace SoundBoard.Controller
{
	public interface IMediaController
    {
        #region Methods
        void Play(SoundBlock xiSoundBlock);
        void Stop();
        #endregion
    }
}
