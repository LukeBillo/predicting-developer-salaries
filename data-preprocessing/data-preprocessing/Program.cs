using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using DataPreprocessing.Models;

namespace DataPreprocessing
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading stackoverflow survey CSV...");

            var processedSurveyRecordModels = new List<ProcessedSurveyRecordModel>();

            using (TextReader textReader = File.OpenText("survey_results_public.csv")) 
            {
                var csvReader = new CsvReader(textReader);
                var surveyRecords = csvReader.GetRecords<StackOverflowSurveyRecordModel>();

                var recordsWithRelevantInfo = surveyRecords.Where(sr => sr.HasValidCountry &&
                                                                        sr.HasValidStudentStatus &&
                                                                        sr.IsEmployed && 
                                                                        sr.HasValidFormalEducation &&
                                                                        sr.HasValidSalary &&
                                                                        sr.HasValidYearsWorked &&
                                                                        sr.HasValidUndergradMajor);

                processedSurveyRecordModels.AddRange(recordsWithRelevantInfo.Select(ModelConversionHelpers.ProcessStackOverflowSurveyRecordModel));
            }
       }
    }
}
