using SoundBoard.WPF;

namespace SoundBoard.Model
{
    [BlockPropertyEditor(typeof(RangeBlockPropertyEditor))]
    public class RangeBlockPropertyAttribute : BlockPropertyAttribute
    {
        public override object FromString(string xiValue)
        {
            return double.Parse(xiValue);
        }
    }
}
