﻿using System;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;

namespace SoundBoard.Plugins.Sounds
{
    public class MediaController : IMediaController
    {
        #region Constants
        // Fade step, in milliseconds.
        private double FADE_STEP = 10;
        #endregion

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
        private int mFadeStepsDone;
        private double mFadeDuration;
        private double mFadeOrigVolume;
        private DispatcherTimer mFadeTimer;
        #endregion

        #region Constructor
        public MediaController()
        {
            // Set up the media player.
            mMediaPlayer = new MediaPlayer();
            mMediaPlayer.MediaEnded += HandleMediaEnded;

            // Set up the fade timer.
            mFadeTimer = new DispatcherTimer();
            mFadeTimer.Interval = TimeSpan.FromMilliseconds(FADE_STEP);
            mFadeTimer.Tick += HandleFadeTimerTick;
        }
        #endregion

        #region Public methods
        public void Play(SoundBlock xiBlock)
        {
            Stop();

            mMediaPlayer.Volume = xiBlock.Volume;
            mMediaPlayer.Open(new Uri(xiBlock.FileName));
            mMediaPlayer.Play();

            CurrentSoundBlock = xiBlock;
        }

        public void Stop()
        {
            mMediaPlayer.Stop();
            mFadeTimer.IsEnabled = false;
            CurrentSoundBlock = null;
        }

        public void Fade(double xiDuration)
        {
            if (!mFadeTimer.IsEnabled)
            {
                mFadeStepsDone = 0;
                mFadeDuration = xiDuration;
                mFadeOrigVolume = mMediaPlayer.Volume;
                mFadeTimer.IsEnabled = true;
            }
        }

        #endregion

        #region Event handlers
        private void HandleMediaEnded(object sender, EventArgs e)
        {
            CurrentSoundBlock = null;
        }

        private void HandleFadeTimerTick(object sender, EventArgs e)
        {
            mFadeStepsDone++;
            mMediaPlayer.Volume = mFadeOrigVolume - ((FADE_STEP / mFadeDuration) * mFadeStepsDone);

            if (mMediaPlayer.Volume <= 0)
            {
                Stop();
                mFadeTimer.IsEnabled = false;
            }
        }
        #endregion
    }
}