using System.ComponentModel;
using SoundBoard.Model;

namespace SoundBoard.Controller
{
	public interface IBoardController
    {
        #region Properties
        Board CurrentBoard { get; }
        #endregion

        #region Events
        event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        void New();
        void Load(string xiFileName);
        void Save(string xiFileName);
        void Add(Sound xiSound);
        void Remove(Sound xiSound);
        #endregion
    }
}
