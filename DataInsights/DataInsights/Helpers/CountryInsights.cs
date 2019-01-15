using System;
using System.Collections.Generic;
using System.Linq;
using DataInsights.Models;

namespace DataInsights.Helpers
{
    public class CountryInsights
    {
        public dynamic GetAverageByCountry(List<ProcessedStackoverflowModel> models)
        {
            foreach (Country country in Enum.GetValues(typeof(Country)))
            {
                models.Select(model => model
                                           .GetType()
                                           .GetProperty(country.ToString())
                                           .GetValue() == true);

            }
    }
}
