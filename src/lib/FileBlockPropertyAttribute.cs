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

using System;

namespace TechBoard
{
    [BlockPropertyEditor(typeof(FileBlockPropertyEditor))]
    public class FileBlockPropertyAttribute : BlockPropertyAttribute
    {
        public string Filter { get; private set; }

        public FileBlockPropertyAttribute(string xiFilter)
        {
            Filter = xiFilter;
        }

        public override object FromFile(string xiFileName,
                                        string xiValue)
        {
            Uri fileUri = new Uri(xiFileName);
            Uri propUri = new Uri(fileUri, xiValue as string);

            return propUri.ToString();
        }

        public override string ToFile(string xiFileName,
                                      object xiValue)
        {
            Uri fileUri = new Uri(xiFileName);
            Uri propUri = new Uri(xiValue as string);
            Uri relUri = fileUri.MakeRelativeUri(propUri);

            return relUri.ToString();
        }
    }
}
