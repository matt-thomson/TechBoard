using System.Windows;
using SoundBoard.Model;
using SoundBoard.WPF;

namespace SoundBoard.Controller
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

        public void Add(SoundBlock xiSound)
        {
            CurrentBoard.Blocks.Add(xiSound);
        }

        public void Remove(SoundBlock xiSound)
        {
            CurrentBoard.Blocks.Remove(xiSound);
        }
        #endregion
    }
}
