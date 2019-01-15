using System;
using System.IO;
using Encog.ML;
using Encog.ML.Data.Folded;
using Encog.ML.Data.Versatile;
using Encog.ML.Data.Versatile.Normalizers.Strategy;
using Encog.ML.Factory;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Training.Cross;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Persist;

namespace benchmarking
{
    public class NeuralNetworkModel
    {
        private readonly CrossValidationKFold _kfoldTrainer;

        public NeuralNetworkModel(VersatileMLDataSet dataset)
        {
            dataset.NormHelper.NormStrategy = new BasicNormalizationStrategy(0, 1, 0, 1);
            dataset.Normalize();

            var inputs = dataset.NormHelper.InputColumns.Count;
            var outputs = dataset.NormHelper.OutputColumns.Count;
            var hiddens = (inputs + outputs) * 1.5;

            var method = (BasicNetwork) new MLMethodFactory().Create(
                MLMethodFactory.TypeFeedforward,
                $"?:B->SIGMOID->{hiddens}:B->SIGMOID->?",
                inputs,
                outputs);

            var folds = new FoldedDataSet(dataset);
            folds.Fold(5);

            var propTrainer = new ResilientPropagation(method, folds);
            _kfoldTrainer = new CrossValidationKFold(propTrainer, 5);
        }

        public void Run()
        {
            do
            {
                _kfoldTrainer.Iteration();
                Console.WriteLine(@"Iteration #" + _kfoldTrainer.IterationNumber + @" Error:" + _kfoldTrainer.Error);
            } while (_kfoldTrainer.Error > 0.009);

            _kfoldTrainer.FinishTraining();

            var predictor = (IMLRegression) _kfoldTrainer.Method;

            /*foreach (var pair in _kfoldTrainer.Folded)
            {
                var predicted = predictor.Compute(pair.Input);
                Console.WriteLine($"Predicted: {predicted}, Actual: {pair.Ideal}");
            }*/

            EncogDirectoryPersistence.SaveObject(new FileInfo("FeedForwardModel.eg"), _kfoldTrainer.Method);
        }
    }
}
