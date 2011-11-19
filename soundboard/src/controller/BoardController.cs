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

            // TODO Shouldn't need this eventually...
            SoundBlock block = new SoundBlock();
            CurrentBoard.Blocks.Add(block);
        }

        public void Load(string xiFileName)
        {
            CurrentBoard = Board.Load(xiFileName);
        }

        public void Save(string xiFileName)
        {
            CurrentBoard.Save(xiFileName);
        }

        // TODO should be in the SoundBlockView plugin
        public void Add(Sound xiSound)
        {
            CurrentBoard.Blocks[0].Sounds.Add(xiSound);
        }

        // TODO should be in the SoundBlockView plugin
        public void Remove(Sound xiSound)
        {
            CurrentBoard.Blocks[0].Sounds.Remove(xiSound);
        }
        #endregion
    }
}
