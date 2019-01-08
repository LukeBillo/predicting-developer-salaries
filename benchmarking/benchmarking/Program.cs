using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using benchmarks.Helpers;
using Benchmarks.Models;
using Benchmarks.Models.Enums;
using CsvHelper;
using Encog;
using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.Wizard;
using Encog.Engine.Network.Activation;
using Encog.MathUtil.Randomize.Generate;
using Encog.ML;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Columns;
using Encog.ML.Data.Versatile.Division;
using Encog.ML.Data.Versatile.Normalizers.Strategy;
using Encog.ML.Data.Versatile.Sources;
using Encog.ML.Factory;
using Encog.ML.Model;
using Encog.Neural.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Persist;
using Encog.Util.CSV;
using Encog.Util.Normalize;
using Encog.Util.Normalize.Input;
using Encog.Util.Normalize.Segregate.Index;

namespace benchmarking
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataset = new VersatileMLDataSet(new CSVDataSource("survey_processed_results.csv", true, CSVFormat.English));

            ColumnDefinition outputColumnDefinition = null;

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
                        yearsColumn.DefineClass(new [] { "ZeroToTwo",
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

                    var booleanColumn = dataset.DefineSourceColumn(header, i, ColumnType.Ordinal);
                    booleanColumn.DefineClass(new [] { "False", "True" });
                }
            }

            dataset.DefineSingleOutputOthersInput(outputColumnDefinition);
            dataset.Analyze();

            var model = new EncogModel(dataset);
            model.SelectMethod(dataset, MLMethodFactory.TypeSVM);
            model.Report = new ConsoleStatusReportable();

            dataset.Normalize();
            model.HoldBackValidation(0.3, true, 1001);
            model.SelectTrainingType(dataset);
            var bestMethod = model.Crossvalidate(5, true) as IMLRegression;

            Console.WriteLine($"Training error: {model.CalculateError(bestMethod, model.TrainingDataset)}");
            Console.WriteLine($"Validation error: {model.CalculateError(bestMethod, model.ValidationDataset)}");

            var dataDivisions = new List<DataDivision> { new DataDivision(0.5), new DataDivision(0.5) };
            dataset.Divide(dataDivisions, true, new MersenneTwisterGenerateRandom());

            foreach (var data in dataDivisions[0].Dataset)
            {
                var result = bestMethod.Compute(data.Input);
                Console.WriteLine($"Result: {result}, Actual: {data.Ideal}");
            }

            EncogDirectoryPersistence.SaveObject(new FileInfo("SvmModel_NoNormalisation.eg"), bestMethod);
        }
    }
}
