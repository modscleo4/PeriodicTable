using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Support;
using PeriodicTable.WPF.Controls;
using System.Net;
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
        private ElementDAO ElementDAO = new ElementDAO();
        private PeriodicTableUtils PeriodicTableUtils = new PeriodicTableUtils();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ElementDAO.UpdateFromAPI();
            }
            catch (WebException)
            {
                Modscleo4.WPFUI.MessageBox.Show("Unable to get information from server! Using from cached.", "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            var elements = ElementDAO.Select();
            foreach (var element in elements)
            {
                var gridElement = new GridElement();

                gridElement.AtomicNumber = element.AtomicNumber.ToString();
                gridElement.Symbol = element.Symbol;

                var column = PeriodicTableUtils.GetGroup(element.AtomicNumber);
                var row = PeriodicTableUtils.GetPeriod(element.AtomicNumber);

                if (row == 1)
                {
                    if (column >= 2)
                    {
                        // Helium (2) is at column 18 (17 in the grid)
                        column += 17;
                    }
                }
                else if (row == 2)
                {
                    if (column > 2)
                    {
                        // Borum (5) is at column 13 (12 in the grid)
                        column += 10;
                    }
                }
                else if (row == 3)
                {
                    if (column > 2)
                    {
                        // Alluminum (13) is at column 13 (12 in the grid)
                        column += 10;
                    }
                }
                else if (row == 6)
                {
                    if (column > 2)
                    {
                        // Lanthanoids start at column 3 (2 in the grid)
                        if (column <= 17)
                        {
                            // Lanthanoids is at row 8
                            row = 9;
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
                        if (column <= 17)
                        {
                            // Actinoids is at row 9
                            row = 10;
                            column += 1;
                        }
                        else
                        {
                            // Bring the elements 14 columns back (removed all the actinoids)
                            column -= 14;
                        }
                    }
                }

                gridElement.BorderBrush = new SolidColorBrush(element.GroupBlock.Color);

                Grid.SetRow(gridElement, (int)row - 1);
                Grid.SetColumn(gridElement, (int)column - 1);

                ElementsGrid.Children.Add(gridElement);
            }
        }
    }
}
