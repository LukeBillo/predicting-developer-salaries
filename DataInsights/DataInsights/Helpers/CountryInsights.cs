using System;
using System.Collections.Generic;
using System.Linq;
using DataInsights.Models;

namespace DataInsights.Helpers
{
    public class CountryInsights
    {
        public static Dictionary<string, decimal> GetAverageByCountry(List<ProcessedStackoverflowModel> models)
        {
            var countrySalaries = new Dictionary<string, decimal>();

            foreach (var country in Enum.GetNames(typeof(Country)))
            {
                var countryField = typeof(ProcessedStackoverflowModel).GetProperty(country);

                var runningSalaryTotal = 0.0M;
                var totalModelsAdded = 0;

                foreach (var model in models)
                {
                    if (!(bool) countryField.GetValue(model)) continue;

                    runningSalaryTotal += model.Salary;
                    totalModelsAdded += 1;
                }

                if (totalModelsAdded == 0) continue;

                countrySalaries.Add(country, (runningSalaryTotal / totalModelsAdded));
            }

            return countrySalaries;
        }
    }
}
