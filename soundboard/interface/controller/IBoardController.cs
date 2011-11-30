using SoundBoard.Model;
using SoundBoard.WPF;

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
