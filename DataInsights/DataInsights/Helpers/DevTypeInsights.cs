using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInsights.Models;

namespace DataInsights.Helpers
{
    class DevTypeInsights
    {
        public static Dictionary<string, decimal> GetAvgByDevType(List<ProcessedStackoverflowModel> models)
        {
            var averagesByDevType = new Dictionary<string, decimal>();

            foreach (var devType in Enum.GetNames(typeof(DevelopmentType)))
            {
                var devTypeProperty = typeof(ProcessedStackoverflowModel).GetProperty(devType);

                var runningTotal = 0.0M;
                var totalAdded = 0;

                foreach (var model in models)
                {
                    if (!(bool) devTypeProperty.GetValue(model)) continue;

                    runningTotal += model.Salary;
                    totalAdded += 1;
                }

                averagesByDevType.Add(devType, (runningTotal / totalAdded));
            }

            return averagesByDevType;
        }
    }
}
