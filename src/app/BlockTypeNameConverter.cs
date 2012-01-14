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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace TechBoard.App
{
    public class BlockTypeNameConverter : IValueConverter
    {
        public object Convert(object xiValue,
                                  Type xiTargetType,
                                  object xiParameter,
                                  CultureInfo xiCulture)
        {
            // Extract the type name.
            Type type = xiValue as Type;

            if (type == null)
            {
                type = xiValue.GetType();
            }

            string result = type.Name;

            // If the type name ends in "Block", strip this off.
            if (result.EndsWith("Block"))
            {
                result = result.Substring(0, result.Length - 5);
            }

            // Convert to camel case.
            result = Regex.Replace(result,
                                   "([A-Z][a-z]+)",
                                   " $1",
                                   RegexOptions.Compiled).Trim();
            
            return result;
        }

        public object ConvertBack(object xiValue,
                                  Type xiTargetType,
                                  object xiParameter,
                                  CultureInfo xiCulture)
        {
            return null;
        }
    }
}
