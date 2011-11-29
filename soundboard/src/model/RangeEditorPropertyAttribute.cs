using SoundBoard.WPF;

namespace SoundBoard.Model
{
    [EditorPropertyView(typeof(RangeEditorPropertyView))]
    public class RangeEditorPropertyAttribute : EditorPropertyAttribute
    {
        public override object FromString(string xiValue)
        {
            return double.Parse(xiValue);
        }
    }
}
