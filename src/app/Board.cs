using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SoundBoard.App
{
    public class Board
    {
        #region Public properties
        public ObservableCollection<UserControl> Blocks { get; private set; }
        #endregion

        #region Constructors
        public Board() 
        {
            Blocks = new ObservableCollection<UserControl>();
        }
        #endregion
    }
}