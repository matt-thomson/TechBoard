using System;
using System.Windows.Media;

namespace SoundBoard.Controller
{
    public class MediaController : IMediaController
    {
        #region Public properties
        public static MediaController StaticInstance
        {
            get
            {
                if (mStaticInstance == null)
                {
                    mStaticInstance = new MediaController();
                }

                return mStaticInstance;
            }
        }
        #endregion

        #region Static properties
        private static MediaController mStaticInstance;
        #endregion

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
