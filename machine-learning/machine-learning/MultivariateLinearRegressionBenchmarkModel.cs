using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Models.Regression.Linear;
using Benchmarks.Models;

namespace MachineLearning
{
    public class MultivariateLinearRegressionBenchmarkModel
    {
        public double Error;

        public MultivariateLinearRegressionBenchmarkModel(List<ProcessedSurveyRecordModel> models)
        {
            var inputs = new double[models.Count][];
            var outputs = new double[models.Count];

            for (var i = 0; i < models.Count; i++)
            {
                var currentModel = models[i];
                var currentInputs = new List<double>();

                outputs[i] = (double) currentModel.Salary;

                currentInputs.Add((double) currentModel.YearsCoding);
                currentInputs.Add((double) currentModel.YearsProfessionalCoding);
                currentInputs.Add(Convert.ToDouble(currentModel.HasAdditionalEducation));
                inputs[i] = currentInputs.ToArray();
            }

            var regressionModel = new OrdinaryLeastSquares().Learn(inputs, outputs);
            var predictions = regressionModel.Transform(inputs);

            var squareLoss = new SquareLoss(outputs) {Mean = true};

            Error = new SquareLoss(outputs).Loss(predictions);
        }
    }
}
