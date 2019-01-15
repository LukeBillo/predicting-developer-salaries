﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Benchmarks.Models.Enums;
using DataInsights.Helpers;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataInsights
{
    /// <summary>
    /// Interaction logic for YearBandGraph.xaml
    /// </summary>
    public partial class YearBandGraph : UserControl
    {
        public SeriesCollection YearBandAverages { get; set; }
        public string[] YearBands { get; set; }

        public YearBandGraph()
        {
            InitializeComponent();

            var models = new ProcessedStackoverflowModelReader().ProcessedStackoverflowModels;
            var averages = YearBandDataInsights.GetAveragesByYearBand(models);

            var averageProfessionalList = (Dictionary<YearBand, decimal>) averages.ProfessionalAverages;
            var professionalValues = averageProfessionalList.Select(r => r.Value);

            var averageNonProfessionalList = (Dictionary<YearBand, decimal>) averages.NonProfessionalAverages;
            var nonprofessionalValues = averageNonProfessionalList.Select(r => r.Value);

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
                    Values = new ChartValues<decimal>(professionalValues)
                },
                new LineSeries
                {
                    Title = "Non-Professional",
                    Values = new ChartValues<decimal>(nonprofessionalValues)
                }
            };

            DataContext = this;
        }
    }
}