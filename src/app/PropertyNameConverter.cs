using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace SoundBoard.App
{
    public class PropertyNameConverter : IValueConverter
    {
        public object Convert(object xiValue,
                              Type xiTargetType,
                              object xiParameter,
                              CultureInfo xiCulture)
        {            
            string result = xiValue as string;
            result = Regex.Replace(result, 
                                   "([A-Z]+[a-z]*)",
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
