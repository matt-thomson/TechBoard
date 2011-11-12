namespace SoundBoard.Controller
{
	public interface IMediaController
    {
        #region Methods
        void Play(string xiFileName, double xiVolume);
        void Stop();
        #endregion
    }
}
