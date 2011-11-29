using System;

namespace SoundBoard.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class EditorPropertyAttribute : Attribute
    {
        public virtual object FromString(string xiValue)
        {
            return xiValue;
        }
    }
}
