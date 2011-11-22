using System.ComponentModel;
using SoundBoard.Model;

namespace SoundBoard.Controller
{
	public interface IBoardController
    {
        #region Properties
        Board CurrentBoard { get; }
        #endregion

        #region Methods
        void New();
        void Load(string xiFileName);
        void Save(string xiFileName);
        void Add(SoundBlock xiSound);
        void Remove(SoundBlock xiSound);
        #endregion
    }
}
