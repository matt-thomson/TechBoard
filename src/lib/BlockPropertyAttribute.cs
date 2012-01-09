using System;

namespace TechBoard
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BlockPropertyAttribute : Attribute
    {
        public virtual object FromFile(string xiFileName,
                                       string xiValue)
        {
            return xiValue;
        }

        public virtual string ToFile(string xiFileName,
                                     object xiValue)
        {
            return xiValue.ToString();
        }
    }
}
