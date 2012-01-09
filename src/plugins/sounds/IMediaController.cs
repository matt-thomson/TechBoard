﻿namespace TechBoard.Plugins.Sounds
{
	public interface IMediaController
    {
        #region Properties
        SoundBlock CurrentSoundBlock { get; }
        #endregion

        #region Methods
        void Play(SoundBlock xiSoundBlock);
        void Stop();
        void Fade(double xiDuration);
        #endregion
    }
}
