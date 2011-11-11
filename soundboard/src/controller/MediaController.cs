using System;
using System.Windows.Media;

namespace SoundBoard.Controller
{
    public class MediaController : IMediaController
    {
        #region Private members
        private MediaPlayer mMediaPlayer = new MediaPlayer();
        #endregion
        
        #region Public methods
        public void Play(string xiFileName, double xiVolume)
        {
            mMediaPlayer.Stop();
            mMediaPlayer.Volume = xiVolume;
            mMediaPlayer.Open(new Uri(xiFileName));
            mMediaPlayer.Play();
        }

        public void Stop()
        {
            mMediaPlayer.Stop();
        }
        #endregion
    }
}
