using System.ComponentModel;
using SoundBoard.Model;

namespace SoundBoard.Controller
{
    public class BoardController : IBoardController
    {
        #region Private members
        private Board mCurrentBoard = null;
        #endregion

        #region Properties
        public Board CurrentBoard 
        {
            get
            {
                return mCurrentBoard;
            }
            private set
            {
                mCurrentBoard = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentBoard"));
                }
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
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
