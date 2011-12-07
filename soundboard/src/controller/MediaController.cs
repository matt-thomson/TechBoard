using System;
using System.Windows.Media;
using SoundBoard.WPF;

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
        public void Play(SoundBlock xiBlock)
        {
            mMediaPlayer.Stop();
            mMediaPlayer.Volume = xiBlock.Volume;
            mMediaPlayer.Open(new Uri(xiBlock.FileName));
            mMediaPlayer.Play();
        }

        public void Stop()
        {
            mMediaPlayer.Stop();
        }
        #endregion
    }
}
