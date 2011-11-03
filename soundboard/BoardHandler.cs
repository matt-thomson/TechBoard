using System.ComponentModel;

namespace SoundBoard.Model
{
    public class BoardHandler : INotifyPropertyChanged
    {
        #region Private members
        private static BoardHandler mInstance;
        private Board mCurrentBoard = new Board();
        #endregion

        #region Singleton property
        public static BoardHandler Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new BoardHandler();
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
        #endregion
    }
}
