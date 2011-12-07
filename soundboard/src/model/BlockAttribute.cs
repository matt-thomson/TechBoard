using System;

namespace SoundBoard.Model
{
    public class BlockAttribute : Attribute
    {
        public Guid Guid { get; private set; }

        public BlockAttribute(String xiGuid)
        {
            Guid = new Guid(xiGuid);
        }
    }
}
