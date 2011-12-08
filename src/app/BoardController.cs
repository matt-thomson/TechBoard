using System.Windows;
using System.Windows.Controls;

namespace SoundBoard.App
{
    public class BoardController : DependencyObject, IBoardController
    {
        #region Private members
        private static DependencyProperty CurrentBoardProperty = DependencyProperty.Register("CurrentBoard",
                                                                                             typeof(Board),
                                                                                             typeof(BoardController));
        #endregion

        #region Properties
        public Board CurrentBoard 
        {
            get
            {
                return (Board)GetValue(CurrentBoardProperty);
            }
            private set
            {
                SetValue(CurrentBoardProperty, value);
            }
        }
        #endregion

        #region Public methods
        public void New()
        {
            CurrentBoard = new Board();
        }

        public void Load(string xiFileName)
        {
            CurrentBoard = Board.Load(xiFileName);
        }

        public void Save(string xiFileName)
        {
            CurrentBoard.Save(xiFileName);
        }

        public void Add(UserControl xiBlock)
        {
            CurrentBoard.Blocks.Add(xiBlock);
        }

        public void Remove(UserControl xiBlock)
        {
            CurrentBoard.Blocks.Remove(xiBlock);
        }
        #endregion
    }
}
