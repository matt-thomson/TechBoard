namespace SoundBoard.Controller
{
	public interface IMediaController
	{
        void Play(string xiFileName, double xiVolume);
        void Stop();
	}
}
