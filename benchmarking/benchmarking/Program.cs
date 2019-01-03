using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Accord.Controls;
using benchmarks.Helpers;
using Benchmarks.Models;
using Benchmarks.Models.Enums;
using CsvHelper;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Columns;
using Encog.ML.Data.Versatile.Sources;

namespace benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = new List<ProcessedSurveyRecordModel>();
            var boolEnumerableTypeConverter = new EnumerableTypeConverter<bool>();

            using (var textReader = File.OpenText("survey_processed_results.csv"))
            {
                var csvReader = new CsvReader(textReader);

                // reads headers
                csvReader.Read();
                csvReader.ReadHeader();

                while (csvReader.Read())
                {
                    records.Add(new ProcessedSurveyRecordModel
                    {
                        Id = csvReader.GetField<int>("Id"),
                        YearsCoding = csvReader.GetField<YearBand>("YearsCoding"),
                        YearsProfessionalCoding = csvReader.GetField<YearBand>("YearsProfessionalCoding"),
                        Salary = csvReader.GetField<decimal>("Salary"),
                        HasAdditionalEducation = csvReader.GetField<bool>("HasAdditionalEducation"),
                        Country = csvReader.GetField<IEnumerable<bool>>("Country", boolEnumerableTypeConverter),
                        StudentStatus = csvReader.GetField<IEnumerable<bool>>("StudentStatus", boolEnumerableTypeConverter),
                        EmploymentStatus = csvReader.GetField<IEnumerable<bool>>("EmploymentStatus", boolEnumerableTypeConverter),
                        EducationLevel = csvReader.GetField<IEnumerable<bool>>("EducationLevel", boolEnumerableTypeConverter),
                        UndergraduateMajor = csvReader.GetField<IEnumerable<bool>>("UndergraduateMajor", boolEnumerableTypeConverter),
                        DevelopmentTypes = csvReader.GetField<IEnumerable<bool>>("DevelopmentTypes", boolEnumerableTypeConverter)
                    });
                }
            }

            var data = new VersatileMLDataSet(new CSVDataSource("survey_processed_results.csv", true, ','));

            data.DefineSourceColumn("Id", 0, ColumnType.Ignore);
            data.DefineSourceColumn("YearsCoding", 1, ColumnType.Ordinal);
            data.DefineSourceColumn("YearsProfessionalCoding", 2, ColumnType.Ordinal);
        }
    }
}
