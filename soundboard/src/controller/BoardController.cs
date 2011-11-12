using System.ComponentModel;
using SoundBoard.Model;

namespace SoundBoard.Controller
{
    public class BoardController : INotifyPropertyChanged, IBoardController
    {
        #region Private members
        private Board mCurrentBoard = new Board();
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

        public void Add(Sound xiSound)
        {
            CurrentBoard.Sounds.Add(xiSound);
        }

        public void Remove(Sound xiSound)
        {
            CurrentBoard.Sounds.Remove(xiSound);
        }
        #endregion
    }
}
