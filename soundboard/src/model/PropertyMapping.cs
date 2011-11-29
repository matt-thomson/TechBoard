using System.Reflection;

namespace SoundBoard.Model
{
    public class PropertyMapping
    {
        public PropertyInfo Property { get; private set; }
        public object Target { get; private set; }

        public PropertyMapping(PropertyInfo xiProperty,
                               object xiTarget)
        {
            Property = xiProperty;
            Target = xiTarget;
        }
    }
}
