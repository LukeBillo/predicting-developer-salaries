using System;
using System.Collections.Generic;
using System.IO;
using Encog;
using Encog.MathUtil.Randomize.Generate;
using Encog.ML;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Division;
using Encog.ML.Factory;
using Encog.ML.Model;
using Encog.Persist;

namespace benchmarking
{
    public class SvmModel
    {
        private readonly EncogModel _model;
        private readonly VersatileMLDataSet _dataset;

        public SvmModel(VersatileMLDataSet dataset)
        {
            _model = new EncogModel(dataset);
            _model.SelectMethod(dataset, MLMethodFactory.TypeSVM);
            _model.Report = new ConsoleStatusReportable();

            dataset.Normalize();
            _model.HoldBackValidation(0.3, true, 1001);
            _model.SelectTrainingType(dataset);
            
        }

        public void Run()
        {
            var bestMethod = _model.Crossvalidate(5, true) as IMLRegression;

            Console.WriteLine($"Training error: {_model.CalculateError(bestMethod, _model.TrainingDataset)}");
            Console.WriteLine($"Validation error: {_model.CalculateError(bestMethod, _model.ValidationDataset)}");

            var dataDivisions = new List<DataDivision> { new DataDivision(0.5), new DataDivision(0.5) };
            _dataset.Divide(dataDivisions, true, new MersenneTwisterGenerateRandom());

            foreach (var data in dataDivisions[0].Dataset)
            {
                var result = bestMethod.Compute(data.Input);
                Console.WriteLine($"Result: {result}, Actual: {data.Ideal}");
            }

            EncogDirectoryPersistence.SaveObject(new FileInfo("SvmModel.eg"), bestMethod);
        }
    }
}
