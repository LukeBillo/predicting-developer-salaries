using System;
using System.Collections.Generic;
using System.Linq;
using DataPreprocessing.Models;
using DataPreprocessing.Models.Enums;

namespace DataPreprocessing
{
    class DataInsights
    {
        public static void DisplayYearsSalaryRelationships(List<ProcessedSurveyRecordModel> processedModels)
        {
            var yearsCodingSalaryModels = processedModels
                .Select(model => new
                    {
                        YearsCoding = model.YearsCoding,
                        YearsProfCoding = model.YearsProfessionalCoding,
                        Salary = model.Salary
                    }
                ).ToList();

            var averagePairsNonProfessional = new Dictionary<YearBand, decimal>();
            var averagePairsProfessional = new Dictionary<YearBand, decimal>();

            foreach (YearBand yearBand in Enum.GetValues(typeof(YearBand)))
            {
                var averageForYearBandUnprofessional = yearsCodingSalaryModels
                    .Where(model => model.YearsCoding == yearBand)
                    .Average(model => model.Salary);

                var averageForYearBandProfessional = yearsCodingSalaryModels
                    .Where(model => model.YearsProfCoding == yearBand)
                    .Average(model => model.Salary);

                averagePairsNonProfessional.Add(yearBand, averageForYearBandUnprofessional);
                averagePairsProfessional.Add(yearBand, averageForYearBandProfessional);
            }
        }
    }
}
