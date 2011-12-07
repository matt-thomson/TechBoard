using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SoundBoard.Model;

namespace SoundBoard.Test
{
    // N.B. This class should move to the test project once we're reflecting
    // over block types.
    [Block("{018C517C-973E-4954-BAA1-9D0A3ADA375F}")]
    public class TestBlock : UserControl
    {
        [TestBlockProperty]
        public string MyProperty { get; set; }
    }
}
