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

        public MainWindow()
        {
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

                if (row == 1)
                {
                    if (column == 2)
                    {
                        column = PeriodicTableUtils.GetPeriodMaxE(4);
                    }
                }
                else if (row == 2)
                {
                    if (column > 2)
                    {
                        column += PeriodicTableUtils.GetPeriodMaxE(4) - PeriodicTableUtils.GetPeriodMaxE(row);
                    }
                }
                else if (row == 3)
                {
                    if (column > 2)
                    {
                        column += PeriodicTableUtils.GetPeriodMaxE(4) - PeriodicTableUtils.GetPeriodMaxE(row);
                    }
                }
                else if (row == 6)
                {
                    if (column > 2)
                    {
                        if (column < PeriodicTableUtils.GetPeriodMaxE(5))
                        {
                            // Lanthanoids is at row 8
                            row += 3;
                            column += 1;
                        }
                        else
                        {
                            // Bring the elements 14 columns back (removed all the lanthanoids)
                            column -= 14;
                        }
                    }
                }
                else if (row == 7)
                {
                    if (column > 2)
                    {
                        // Actinoids start at column 3 (2 in the grid)
                        if (column < PeriodicTableUtils.GetPeriodMaxE(5))
                        {
                            // Actinoids is at row 9
                            row += 3;
                            column += 1;
                        }
                        else
                        {
                            // Bring the elements 14 columns back (removed all the actinoids)
                            column -= 14;
                        }
                    }
                }

                Grid.SetRow(gridElement, (int)row - 1);
                Grid.SetColumn(gridElement, (int)column - 1);

                ElementsGrid.Children.Add(gridElement);
            }
        }
    }
}
