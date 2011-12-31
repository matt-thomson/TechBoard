using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace SoundBoard.App
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
