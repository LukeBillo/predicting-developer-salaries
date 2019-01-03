using System.Collections;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DataPreprocessing
{
    public class EnumerableTypeConverter : ITypeConverter
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var enumerable = (IEnumerable) value;
            var stringifiedEnumerable = enumerable.Cast<object>().Aggregate("", (current, item) => current + $"{item};");

            // remove last semi-colon
            stringifiedEnumerable = stringifiedEnumerable.Remove(stringifiedEnumerable.Length - 1);

            return stringifiedEnumerable;
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return text.Split(";").ToArray();
        }
    }
}
