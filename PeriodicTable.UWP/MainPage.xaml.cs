using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Support;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x416

namespace PeriodicTable.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly uint rowLock = PeriodicTableUtils.GetPeriodMaxE(5);

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var elements = await ElementDAO.SelectAsync();
            foreach (var element in elements)
            {
                var gridElement = new GridElement
                {
                    AtomicNumber = element.AtomicNumber.ToString(),
                    Symbol = element.Symbol,
                    BorderBrush = new SolidColorBrush(Color.FromArgb(element.GroupBlock.Color.A, element.GroupBlock.Color.R, element.GroupBlock.Color.G, element.GroupBlock.Color.B))
                };

                var column = PeriodicTableUtils.GetGroup(element.AtomicNumber);
                var row = PeriodicTableUtils.GetPeriod(element.AtomicNumber);

                if ((row == 1 && column >= 2) ||
                    (row <= 5 && column > 2))
                {
                    column += rowLock - PeriodicTableUtils.GetPeriodMaxE(row);
                }
                else if (column > 2)
                {
                    if (column < rowLock)
                    {
                        // Lanthanoid/actinoid are at row 8
                        row += 3;
                        column++;
                    }
                    else
                    {
                        // Bring the elements 14 columns back (removed all the lanthanoid/actinoid)
                        column -= 14;
                    }
                }

                Grid.SetRow(gridElement, (int)row - 1);
                Grid.SetColumn(gridElement, (int)column - 1);

                ElementsGrid.Children.Add(gridElement);
            }
        }
    }
}
