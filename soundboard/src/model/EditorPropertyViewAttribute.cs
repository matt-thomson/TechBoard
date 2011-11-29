using System;

namespace SoundBoard.Model
{
    public class EditorPropertyViewAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public EditorPropertyViewAttribute(Type xiType)
        {
            ViewType = xiType;
        }
    }
}
