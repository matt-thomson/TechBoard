/*
 *  This file is part of TechBoard.
 *  Copyright (C) 2011-2012 Matt Thomson
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 *  
 *  For more information on TechBoard, see 
 *  <http://www.matt-thomson.co.uk/software/techboard>.
 */

namespace TechBoard.Plugins.PowerPoint
{
	public class PowerPointController : IPowerPointController
    {
        #region Public properties
        public static PowerPointController StaticInstance
        {
            get
            {
                if (mStaticInstance == null)
                {
                    mStaticInstance = new PowerPointController();
                }

                return mStaticInstance;
            }
        }
        #endregion

        #region Static properties
        private static PowerPointController mStaticInstance;
        #endregion

        #region Public methods
        public void NextSlide()
        {
        }

        public void PreviousSlide()
        {
        }
        #endregion
    }
}
