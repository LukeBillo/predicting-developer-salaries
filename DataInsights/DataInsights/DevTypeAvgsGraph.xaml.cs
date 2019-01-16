using System;
using System.Linq;
using System.Windows.Controls;
using DataInsights.Helpers;
using DataInsights.Models;
using LiveCharts;
using LiveCharts.Wpf;

namespace DataInsights
{
    /// <summary>
    /// Interaction logic for DevTypeAvgsGraph.xaml
    /// </summary>
    public partial class DevTypeAvgsGraph : UserControl
    {
        public SeriesCollection DevTypeAverages { get; set; }
        public string[] DevTypes { get; set; }

        public DevTypeAvgsGraph()
        {
            InitializeComponent();

            var models = new ProcessedStackoverflowModelReader().ProcessedStackoverflowModels;
            var averages = DevTypeInsights.GetAvgByDevType(models);

            DevTypes = Enum.GetNames(typeof(DevelopmentType));

            DevTypeAverages = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Average Salary",
                    Values = new ChartValues<decimal>(averages.Select(avg => avg.Value))
                },
            };

            DataContext = this;
        }
    }
}
