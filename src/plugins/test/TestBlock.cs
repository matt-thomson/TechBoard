using System.Windows.Controls;

namespace SoundBoard.Plugins.Test
{
    [Block("{018C517C-973E-4954-BAA1-9D0A3ADA375F}")]
    public class TestBlock : UserControl
    {
        [TestBlockProperty]
        public string MyProperty { get; set; }
    }
}
