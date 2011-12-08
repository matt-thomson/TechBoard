using SoundBoard.Model;
using SoundBoard.WPF;
using System.Windows.Controls;

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
        void Add(UserControl xiBlock);
        void Remove(UserControl xiBlock);
        #endregion
    }
}
