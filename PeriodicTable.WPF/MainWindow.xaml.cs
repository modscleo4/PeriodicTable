using PeriodicTable.Model.Entity;
using PeriodicTable.Model.DAO;
using System;
using System.Windows;

namespace PeriodicTable
{
    /// <summary>
    /// Interação lógica para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Modscleo4.WPFUI.Controls.Window
    {
        private Element element;

        private ElementDAO ElementDAO = new ElementDAO();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadElement()
        {
            if (element.AtomicMass != null)
            {
                LabelMass.Text = element.AtomicMass.ToString();
            }

            if (element.Symbol != null)
            {
                LabelSymbol.Text = element.Symbol;
            }

            if (element.AtomicNumber != null)
            {
                LabelNumber.Text = element.AtomicNumber.ToString();
            }

            if (element.Name != null)
            {
                LabelName.Text = element.Name;
            }

            if (element.AtomicRadius != null)
            {
                LabelAtomicRadius.Text = element.AtomicRadius.ToString();
            }

            if (element.MeltingPoint != null)
            {
                LabelMeltingPoint.Text = element.MeltingPoint.ToString() + " K";
            }

            if (element.BoilingPoint != null)
            {
                LabelBoilingPoint.Text = element.BoilingPoint.ToString() + " K";
            }

            if (element.Density != null)
            {
                LabelDensity.Text = element.Density.ToString() + " g/cm³";
            }

            if (element.ElectronAffinity != null)
            {
                LabelElectronAffinity.Text = element.ElectronAffinity.ToString() + " kJ mol⁻¹";
            }

            if (element.ElectronNegativity != null)
            {
                LabelElectronNegativity.Text = element.ElectronNegativity.ToString();
            }

            if (element.ElectronicConfiguration != null)
            {
                LabelElectronicConfiguration.Text = element.ElectronicConfiguration;
            }

            if (element.GroupBlock != null)
            {
                LabelGroupBlock.Text = element.GroupBlock.Name;
            }

            if (element.IonRadius != null)
            {
                LabelIonRadius.Text = element.IonRadius + " pm";
            }

            if (element.IonizationEnergy != null)
            {
                LabelIonizationEnergy.Text = element.IonizationEnergy.ToString() + " kJ mol⁻¹";
            }

            if (element.OxidationStates != null)
            {
                LabelOxidationStates.Text = element.OxidationStates;
            }

            if (element.StandardStates != null)
            {
                LabelStandardStates.Text = element.StandardStates.Value;
            }

            if (element.VanDerWallsRadius != null)
            {
                LabelVanDerWallsRadius.Text = element.VanDerWallsRadius.ToString() + " pm";
            }

            if (element.YearDiscovered != null)
            {
                LabelYearDiscovered.Text = element.YearDiscovered.ToString();
            }
            else
            {
                LabelYearDiscovered.Text = "Ancient";
            }
        }

        private void Clear()
        {
            LabelMass.Text = "Mass";
            LabelSymbol.Text = "Symbol";
            LabelNumber.Text = "Number";

            LabelName.Text = String.Empty;
            LabelAtomicRadius.Text = String.Empty;
            LabelMeltingPoint.Text = String.Empty;
            LabelBoilingPoint.Text = String.Empty;
            LabelDensity.Text = String.Empty;
            LabelElectronAffinity.Text = String.Empty;
            LabelElectronNegativity.Text = String.Empty;
            LabelElectronicConfiguration.Text = String.Empty;
            LabelGroupBlock.Text = String.Empty;
            LabelIonRadius.Text = String.Empty;
            LabelIonizationEnergy.Text = String.Empty;
            LabelOxidationStates.Text = String.Empty;
            LabelStandardStates.Text = String.Empty;
            LabelVanDerWallsRadius.Text = String.Empty;
            LabelYearDiscovered.Text = String.Empty;
        }

        private void Window_Search(object sender, RoutedEventArgs e)
        {
            var element = SearchboxValue;
            Clear();

            if (int.TryParse(element, out int n))
            {
                try
                {
                    this.element = ElementDAO.ByNumber(n);
                    LoadElement();
                }
                catch (Exception ex)
                {
                    Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                try
                {
                    this.element = ElementDAO.BySymbol(element);
                    LoadElement();
                }
                catch (Exception ex)
                {
                    Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ElementDAO.ByNumber(1);
                Clear();
            }
            catch (Exception)
            {
                Modscleo4.WPFUI.MessageBox.Show("Unable to get information from server!", "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Environment.Exit(0);
            }
        }
    }
}
