using PeriodicTable.Model.DAO;
using PeriodicTable.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PeriodicTable.WPF
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Modscleo4.WPFUI.Controls.Window
    {
        private ElementDAO ElementDAO = new ElementDAO();

        public MainWindow()
        {
            InitializeComponent();
        }

        private int GetPeriod(int numberOfElectrons)
        {
            int period = 1;
            if (numberOfElectrons > 2)
            {
                numberOfElectrons -= 2;
                period++;
            }

            if (numberOfElectrons > 8)
            {
                numberOfElectrons -= 8;
                period++;
            }

            if (numberOfElectrons > 8)
            {
                numberOfElectrons -= 8;
                period++;
            }

            if (numberOfElectrons > 18)
            {
                numberOfElectrons -= 18;
                period++;
            }

            if (numberOfElectrons > 18)
            {
                numberOfElectrons -= 18;
                period++;
            }

            if (numberOfElectrons > 32)
            {
                numberOfElectrons -= 32;
                period++;
            }

            if (numberOfElectrons > 32)
            {
                period++;
            }

            return period;
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

                var column = element.AtomicNumber;
                var row = GetPeriod(element.AtomicNumber);

                if (row == 1)
                {
                    if (element.AtomicNumber >= 2)
                    {
                        // Helium (2) is at column 18 (17 in the grid)
                        column += 17;
                    }
                }
                else if (row == 2)
                {
                    // The last element before row 2 was Helium (2)
                    column -= 2;
                    if (column > 2)
                    {
                        // Borum (5) is at column 13 (12 in the grid)
                        column += 10;
                    }
                }
                else if (row == 3)
                {
                    // The last element before row 2 was Neonium (10)
                    column -= 10;
                    if (column > 2)
                    {
                        // Alluminum (13) is at column 13 (12 in the grid)
                        column += 10;
                    }
                }
                else if (row == 4)
                {
                    // The last element before row 3 was Argon (18)
                    column -= 18;
                }
                else if (row == 5)
                {
                    // The last element before row 2 was Krypton (36)
                    column -= 36;
                }
                else if (row == 6)
                {
                    // The last element before row 2 was Xenon (54)
                    column -= 54;

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
                    // The last element before row 2 was Radon (54)
                    column -= 86;

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

                Grid.SetRow(gridElement, row - 1);
                Grid.SetColumn(gridElement, column - 1);

                ElementsGrid.Children.Add(gridElement);
            }
        }
    }
}
