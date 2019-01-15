using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using DataInsights.Helpers;
using DataInsights.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataInsights
{
    /// <summary>
    /// Interaction logic for YearBandGraph.xaml
    /// </summary>
    public partial class YearBandStdevGraph : UserControl
    {
        public SeriesCollection YearBandAverages { get; set; }
        public string[] YearBands { get; set; }

        public YearBandStdevGraph()
        {
            InitializeComponent();

            var models = new ProcessedStackoverflowModelReader().ProcessedStackoverflowModels;
            var averages = YearBandDataInsights.GetStdevByYearBand(models);

            var stdevProfessionalList = (Dictionary<YearBand, double>) averages.ProfessionalStdevs;
            var professionalValues = stdevProfessionalList.Select(r => r.Value);

            var stdevNonProfessionalList = (Dictionary<YearBand, double>) averages.NonProfessionalStdevs;
            var nonprofessionalValues = stdevNonProfessionalList.Select(r => r.Value);

            var yearBands = new List<string>();
            foreach (YearBand yearBand in Enum.GetValues(typeof(YearBand)))
            {
                yearBands.Add(yearBand.ToString());
            }

            YearBands = yearBands.ToArray();

            YearBandAverages = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Professional",
                    Values = new ChartValues<double>(professionalValues)
                },
                new LineSeries
                {
                    Title = "Non-Professional",
                    Values = new ChartValues<double>(nonprofessionalValues)
                }
            };

            DataContext = this;
        }
    }
}
