using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using DataPreprocessing.Models;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading stackoverflow survey CSV...");

            var intermediateModels = new List<IntermediateProcessedRecordModel>();

            using (TextReader textReader = File.OpenText("survey_results_public.csv")) 
            {
                var csvReader = new CsvReader(textReader);
                var surveyRecords = csvReader.GetRecords<StackOverflowSurveyRecordModel>();

                var recordsWithRelevantInfo = surveyRecords.Where(sr => sr.HasValidCountry &&
                                                                        sr.HasValidStudentStatus &&
                                                                        sr.IsEmployed && 
                                                                        sr.HasValidFormalEducation &&
                                                                        sr.HasValidDevelopmentTypes &&
                                                                        sr.HasValidSalary &&
                                                                        sr.HasValidYearsWorked &&
                                                                        sr.HasValidUndergradMajor);

                // intermediate has all labelled fields, no one-hot encoding
                intermediateModels.AddRange(recordsWithRelevantInfo.Select(IntermediateModelConversionHelpers.ProcessStackOverflowSurveyRecordModel));
            }

            // processed models use one hot encoding
            // some fields left as labelled where clear ordinality
            var processedModels = new List<ProcessedSurveyRecordModel>();
            foreach (var intermediateProcessedRecordModel in intermediateModels)
            {
                processedModels.Add(new ProcessedSurveyRecordModel(intermediateProcessedRecordModel));
            }

            // write results back to .csv
            using (TextWriter textWriter = File.CreateText("survey_processed_results.csv"))
            {
                var csvWriter = new CsvWriter(textWriter);
                csvWriter.WriteHeader<ProcessedSurveyRecordModel>();

                foreach (var country in Enum.GetValues(typeof(Country)))
                {
                    csvWriter.WriteField(country);
                }

                foreach (var status in Enum.GetValues(typeof(StudentStatus)))
                {
                    csvWriter.WriteField(status);
                }

                foreach (var status in Enum.GetValues(typeof(EmploymentStatus)))
                {
                    csvWriter.WriteField(status);
                }

                foreach (var level in Enum.GetValues(typeof(EducationLevel)))
                {
                    csvWriter.WriteField(level);
                }

                foreach (var major in Enum.GetValues(typeof(UndergraduateMajor)))
                {
                    csvWriter.WriteField(major);
                }

                foreach (var type in Enum.GetValues(typeof(DevelopmentType)))
                {
                    csvWriter.WriteField(type);
                }

                csvWriter.NextRecord();

                foreach (var model in processedModels)
                {
                    // should have default type converters, just write
                    csvWriter.WriteField(model.Id);
                    csvWriter.WriteField(model.YearsCoding);
                    csvWriter.WriteField(model.YearsProfessionalCoding);
                    csvWriter.WriteField(model.Salary);
                    csvWriter.WriteField(model.HasAdditionalEducation);

                    // use enumerable type converter for these
                    var enumerableTypeConverter = new EnumerableTypeConverter();
                    csvWriter.WriteField(model.Country, enumerableTypeConverter);
                    csvWriter.WriteField(model.StudentStatus, enumerableTypeConverter);
                    csvWriter.WriteField(model.EmploymentStatus, enumerableTypeConverter);
                    csvWriter.WriteField(model.EducationLevel, enumerableTypeConverter);
                    csvWriter.WriteField(model.UndergraduateMajor, enumerableTypeConverter);
                    csvWriter.WriteField(model.DevelopmentTypes, enumerableTypeConverter);

                    // needs to be called to complete writing record
                    csvWriter.NextRecord();
                }
            }
       }
    }
}
