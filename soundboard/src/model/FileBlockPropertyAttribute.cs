﻿using SoundBoard.WPF;

namespace SoundBoard.Model
{
    [BlockPropertyEditor(typeof(FileBlockPropertyEditor))]
    public class FileBlockPropertyAttribute : BlockPropertyAttribute
    {
        public string Filter { get; private set; }

        public FileBlockPropertyAttribute(string xiFilter)
        {
            Filter = xiFilter;
        }
    }
}
