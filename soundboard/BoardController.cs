using System.ComponentModel;
using SoundBoard.Model;

namespace SoundBoard.Controller
{
    public class BoardController : INotifyPropertyChanged
    {
        #region Private members
        private static BoardController mInstance;
        private Board mCurrentBoard = new Board();
        #endregion

        #region Singleton property
        public static BoardController Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new BoardController();
                }

                return mInstance;
            }
        }
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
        public static void New()
        {
            Instance.CurrentBoard = new Board();
        }

        public static void Load(string xiFileName)
        {
            Instance.CurrentBoard = Board.Load(xiFileName);
        }

        public static void Save(string xiFileName)
        {
            Instance.CurrentBoard.Save(xiFileName);
        }

        public static void Add(Sound xiSound)
        {
            Instance.CurrentBoard.Sounds.Add(xiSound);
        }

        public static void Remove(Sound xiSound)
        {
            Instance.CurrentBoard.Sounds.Remove(xiSound);
        }
        #endregion
    }
}
