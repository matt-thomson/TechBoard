using System;

namespace SoundBoard.Model
{
    public class BlockPropertyEditorAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public BlockPropertyEditorAttribute(Type xiType)
        {
            ViewType = xiType;
        }
    }
}
