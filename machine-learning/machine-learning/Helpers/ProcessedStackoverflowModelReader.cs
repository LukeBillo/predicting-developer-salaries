using System.Collections.Generic;
using System.IO;
using System.Linq;
using Benchmarks.Models;
using CsvHelper;

namespace MachineLearning.Helpers
{
    public class ProcessedStackoverflowModelReader
    {
        public List<ProcessedSurveyRecordModel> ProcessedStackoverflowModels;

        public ProcessedStackoverflowModelReader()
        {
            using (TextReader textReader = File.OpenText("survey_processed_results.csv"))
            {
                var csvReader = new CsvReader(textReader);
                ProcessedStackoverflowModels = csvReader.GetRecords<ProcessedSurveyRecordModel>().ToList();
            }
        }
    }
}
