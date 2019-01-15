using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using DataInsights.Models;

namespace DataInsights
{
    public class ProcessedStackoverflowModelReader
    {
        public List<ProcessedStackoverflowModel> ProcessedStackoverflowModels;

        public ProcessedStackoverflowModelReader()
        {
            using (TextReader textReader = File.OpenText("survey_processed_results.csv"))
            {
                var csvReader = new CsvReader(textReader);
                ProcessedStackoverflowModels = csvReader.GetRecords<ProcessedStackoverflowModel>().ToList();
            }
        }
    }
}
