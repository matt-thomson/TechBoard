using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using SoundBoard.Model;

namespace SoundBoard.WPF
{
    public class EditBoardPropertyConverter : IValueConverter
    {
        public object Convert(object xiValue,
                              Type xiTargetType,
                              object xiParameter,
                              CultureInfo xiCulture)
        {
            object result = null;

            if (xiValue != null)
            {
                result = from p in xiValue.GetType().GetProperties()
                         where p.IsDefined(typeof(EditorPropertyAttribute), false)
                         select new PropertyMapping(p, xiValue);
            }

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
