using System;
using System.Windows.Media;

namespace SoundBoard.Plugins.Sounds
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

        public SoundBlock CurrentSoundBlock { get; private set; }
        #endregion

        #region Static properties
        private static MediaController mStaticInstance;
        #endregion

        #region Private members
        private MediaPlayer mMediaPlayer;
        #endregion

        #region Constructor
        public MediaController()
        {
            // Set up the media player.
            mMediaPlayer = new MediaPlayer();
            mMediaPlayer.MediaEnded += HandleMediaEnded;
        }
        #endregion

        #region Public methods
        public void Play(SoundBlock xiBlock)
        {
            mMediaPlayer.Stop();
            mMediaPlayer.Volume = xiBlock.Volume;
            mMediaPlayer.Open(new Uri(xiBlock.FileName));
            mMediaPlayer.Play();

            CurrentSoundBlock = xiBlock;
        }

        public void Stop()
        {
            mMediaPlayer.Stop();

            CurrentSoundBlock = null;
        }
        #endregion

        #region Event handlers
        private void HandleMediaEnded(object sender, EventArgs e)
        {
            CurrentSoundBlock = null;
        }
        #endregion
    }
}