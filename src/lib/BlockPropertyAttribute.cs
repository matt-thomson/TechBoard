using System;

namespace SoundBoard
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BlockPropertyAttribute : Attribute
    {
        public virtual object FromString(string xiValue)
        {
            return xiValue;
        }
    }
}
