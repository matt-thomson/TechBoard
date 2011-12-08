using System.Windows.Controls;

namespace SoundBoard.App
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
        void Add(UserControl xiBlock);
        void Remove(UserControl xiBlock);
        #endregion
    }
}
