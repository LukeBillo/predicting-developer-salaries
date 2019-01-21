using System;
using System.Collections.Generic;
using System.Linq;
using DataInsights.Models;
using MathNet.Numerics.Statistics;

namespace DataInsights.Helpers
{
    class YearBandDataInsights
    {
        public static dynamic GetAveragesByYearBand(List<ProcessedStackoverflowModel> processedModels)
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

            return new
            {
                NonProfessionalAverages = averagePairsNonProfessional,
                ProfessionalAverages = averagePairsProfessional
            };
        }

        public static dynamic GetStdevByYearBand(List<ProcessedStackoverflowModel> processedModels)
        {
            var yearsCodingSalaryModels = processedModels
                .Select(model => new
                    {
                        model.YearsCoding,
                        YearsProfCoding = model.YearsProfessionalCoding,
                        model.Salary
                    }
                ).ToList();

            var stdevForYearBandsUnprofessional = new Dictionary<YearBand, double>();
            var stdevForYearBandsProfessional = new Dictionary<YearBand, double>();


            foreach (YearBand yearBand in Enum.GetValues(typeof(YearBand)))
            {
                var stdevForYearBandUnprofessional = yearsCodingSalaryModels
                    .Where(model => model.YearsCoding == yearBand)
                    .Select(model => (double) model.Salary)
                    .StandardDeviation();

                var stdevForYearBandProfessional = yearsCodingSalaryModels
                    .Where(model => model.YearsProfCoding == yearBand)
                    .Select(model => (double) model.Salary)
                    .StandardDeviation();

                stdevForYearBandsUnprofessional.Add(yearBand, stdevForYearBandUnprofessional);
                stdevForYearBandsProfessional.Add(yearBand, stdevForYearBandProfessional);
            }

            return new
            {
                NonProfessionalStdevs = stdevForYearBandsUnprofessional,
                ProfessionalStdevs = stdevForYearBandsProfessional
            };
        }
    }
}
