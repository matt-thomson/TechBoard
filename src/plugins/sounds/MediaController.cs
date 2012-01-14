/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

using System;
using System.Timers;
using System.Windows.Media;
using System.Windows.Threading;

namespace TechBoard.Plugins.Sounds
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