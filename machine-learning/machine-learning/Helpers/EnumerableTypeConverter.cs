using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace benchmarks.Helpers
{
    public class EnumerableTypeConverter<T> : ITypeConverter where T : IConvertible
    {
        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var enumerable = (IEnumerable)value;
            var stringifiedEnumerable = enumerable.Cast<object>().Aggregate("", (current, item) => current + $"{item};");

            // remove last semi-colon
            stringifiedEnumerable = stringifiedEnumerable.Remove(stringifiedEnumerable.Length - 1);

            return stringifiedEnumerable;
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var splitItems = text.Split(';');
            return splitItems.Select(item => (T) Convert.ChangeType(item, typeof(T))).ToList();
        }
    }
}
