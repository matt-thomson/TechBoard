using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace TechBoard.App
{
	public interface IBoardController
    {
        #region Properties
        Board CurrentBoard { get; }
        Dictionary<Guid, Type> BlockTypes { get; }
        #endregion

        #region Methods
        void New();
        void Load(string xiFileName);
        void Save(string xiFileName);
        void Add(UserControl xiBlock);
        void Remove(UserControl xiBlock);
        void MoveUp(UserControl xiBlock);
        void MoveDown(UserControl xiBlock);
        #endregion
    }
}
