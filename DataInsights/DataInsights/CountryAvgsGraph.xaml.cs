using System;
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
using DataInsights.Helpers;
using DataInsights.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataInsights
{
    /// <summary>
    /// Interaction logic for CountryAvgsGraph.xaml
    /// </summary>
    public partial class CountryAvgsGraph : UserControl
    {
        public SeriesCollection CountryAverages { get; set; }
        public string[] Countries { get; set; }

        public CountryAvgsGraph()
        {
            InitializeComponent();

            var models = new ProcessedStackoverflowModelReader().ProcessedStackoverflowModels;
            var countryAverageSalaries = CountryInsights.GetAverageByCountry(models);

            Countries = Enum.GetNames(typeof(Country));

            CountryAverages = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Average Salary",
                    Values = new ChartValues<decimal>(countryAverageSalaries.Select(cas => cas.Value))
                }
            };

            DataContext = this;
        }
    }
}
