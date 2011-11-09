using System;
using System.Windows.Media;

namespace SoundBoard.Controller
{
    public class MediaController
    {
        #region Private members
        private static MediaController mInstance;
        private MediaPlayer mMediaPlayer = new MediaPlayer();
        #endregion

        #region Singleton property
        public static MediaController Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new MediaController();
                }

                return mInstance;
            }
        }
        #endregion

        #region Public methods
        public static void Play(string xiFileName, double xiVolume)
        {
            Instance.mMediaPlayer.Stop();
            Instance.mMediaPlayer.Volume = xiVolume;
            Instance.mMediaPlayer.Open(new Uri(xiFileName));
            Instance.mMediaPlayer.Play();
        }

        public static void Stop()
        {
            Instance.mMediaPlayer.Stop();
        }
        #endregion
    }
}
