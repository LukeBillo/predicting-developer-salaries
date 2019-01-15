using System;
using System.Collections.Generic;
using System.IO;
using Encog;
using Encog.App.Analyst;
using Encog.App.Analyst.Wizard;
using Encog.MathUtil.Randomize.Generate;
using Encog.ML;
using Encog.ML.Data;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Columns;
using Encog.ML.Data.Versatile.Division;
using Encog.ML.Data.Versatile.Sources;
using Encog.ML.Factory;
using Encog.ML.Model;
using Encog.Persist;
using Encog.Util.CSV;

namespace benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            // a new dataset is created for each model so that
            // normalisation from different models does
            // not affect other machine learning models.
            // var svmDataset = CreateDataset();
            // new SvmModel(svmDataset).Run();

            var nnDataset = CreateDataset();

            foreach (var column in nnDataset.NormHelper.SourceColumns)
            {
                Console.WriteLine($"Column: {column.Name} | Mean: {column.Mean} | Stdev: {column.Sd} | High: {column.High} | Low: {column.Low}");
            }

            new NeuralNetworkModel(nnDataset).Run();
        }

        static VersatileMLDataSet CreateDataset()
        {
            var dataset = new VersatileMLDataSet(new CSVDataSource("survey_processed_results.csv", true, CSVFormat.English));

            ColumnDefinition outputColumnDefinition = null;

            // reading in the pre-processed CSV
            using (TextReader textReader = File.OpenText("survey_processed_results.csv"))
            {
                var headers = textReader.ReadLine();
                var splitHeaders = headers.Replace("\"", "").Split(',');

                for (var i = 0; i < splitHeaders.Length; i++)
                {
                    var header = splitHeaders[i];

                    if (header.Contains("Id"))
                    {
                        dataset.DefineSourceColumn(header, i, ColumnType.Ignore);
                        continue;
                    }

                    if (header.Contains("Salary"))
                    {
                        outputColumnDefinition = dataset.DefineSourceColumn(header, i, ColumnType.Continuous);
                        continue;
                    }

                    if (header.Contains("Years"))
                    {
                        var yearsColumn = dataset.DefineSourceColumn(header, i, ColumnType.Ordinal);
                        yearsColumn.DefineClass(new[] { "ZeroToTwo",
                            "ThreeToFive",
                            "SixToEight",
                            "NineToEleven",
                            "TwelveToFourteen",
                            "FifteenToSeventeen",
                            "EighteenToTwenty",
                            "TwentyOneToTwentyThree",
                            "TwentyFourToTwentySix",
                            "TwentySevenToTwentyNine",
                            "ThirtyPlus" });

                        continue;
                    }

                    // all other columns are booleans - most of which are a result
                    // of one-hot encoding.
                    var booleanColumn = dataset.DefineSourceColumn(header, i, ColumnType.Ordinal);
                    booleanColumn.DefineClass(new[] { "False", "True" });
                }
            }

            dataset.DefineSingleOutputOthersInput(outputColumnDefinition);

            dataset.Analyze();

            return dataset;
        }
    }
}
