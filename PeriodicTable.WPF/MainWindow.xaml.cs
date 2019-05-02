using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Support;
using PeriodicTable.WPF.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PeriodicTable.WPF
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Modscleo4.WPFUI.Controls.Window
    {
        private readonly ElementDAO ElementDAO = new ElementDAO();
        private readonly PeriodicTableUtils PeriodicTableUtils = new PeriodicTableUtils();

        private readonly uint rowLock;

        public MainWindow()
        {
            rowLock = PeriodicTableUtils.GetPeriodMaxE(5);

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
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
                        // Lanthanoids/actinoids are at row 8
                        row += 3;
                        column++;
                    }
                    else
                    {
                        // Bring the elements 14 columns back (removed all the lanthanoids/actinoids)
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
