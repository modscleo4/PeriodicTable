using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Windows;
using System.Windows.Media;

namespace PeriodicTable.WPF
{
    /// <summary>
    /// Interação lógica para ElementDetails.xaml
    /// </summary>
    public partial class ElementDetails : Modscleo4.WPFUI.Controls.Window
    {
        private Element element;

        private ElementDAO ElementDAO = new ElementDAO();
        private PeriodicTableUtils PeriodicTableUtils = new PeriodicTableUtils();

        public ElementDetails()
        {
            InitializeComponent();
        }

        public ElementDetails(int atomicNumber)
        {
            InitializeComponent();

            try
            {
                element = ElementDAO.Select(atomicNumber);

                if (element == null)
                {
                    throw new PeriodicTableException("Element does not exist!");
                }

                LoadElement();
            }
            catch (PeriodicTableException ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(string.Format("A unexpected exception occourred! Details: {0}", ex.Message), "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void LoadElement()
        {
            ElementBorder.BorderBrush = new SolidColorBrush(element.GroupBlock.Color);
            LabelGP.Text = string.Format("{0}, {1}", PeriodicTableUtils.GetPeriod(element.AtomicNumber), PeriodicTableUtils.GetGroup(element.AtomicNumber));

            if (element.AtomicMass != null)
            {
                LabelMass.Text = element.AtomicMass.ToString();
            }

            if (element.Symbol != null)
            {
                LabelSymbol.Text = element.Symbol;
            }

            LabelNumber.Text = element.AtomicNumber.ToString();

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

            if (element.Electronegativity != null)
            {
                LabelElectronegativity.Text = element.Electronegativity.ToString();
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

            if (element.VanDerWaalsRadius != null)
            {
                LabelVanDerWallsRadius.Text = element.VanDerWaalsRadius.ToString() + " pm";
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
            LabelGP.Text = "P, G";
            LabelMass.Text = "Mass";
            LabelSymbol.Text = "Symbol";
            LabelNumber.Text = "Number";

            LabelName.Text = string.Empty;
            LabelAtomicRadius.Text = string.Empty;
            LabelMeltingPoint.Text = string.Empty;
            LabelBoilingPoint.Text = string.Empty;
            LabelDensity.Text = string.Empty;
            LabelElectronAffinity.Text = string.Empty;
            LabelElectronegativity.Text = string.Empty;
            LabelElectronicConfiguration.Text = string.Empty;
            LabelGroupBlock.Text = string.Empty;
            LabelIonRadius.Text = string.Empty;
            LabelIonizationEnergy.Text = string.Empty;
            LabelOxidationStates.Text = string.Empty;
            LabelStandardStates.Text = string.Empty;
            LabelVanDerWallsRadius.Text = string.Empty;
            LabelYearDiscovered.Text = string.Empty;
        }

        private void Window_Search(object sender, RoutedEventArgs e)
        {
            var element = SearchboxValue;
            Clear();

            try
            {
                if (int.TryParse(element, out int n))
                {
                    this.element = ElementDAO.Select(n);
                }
                else
                {
                    this.element = ElementDAO.Select(element);
                }

                if (this.element == null)
                {
                    throw new PeriodicTableException("Element does not exist!");
                }

                LoadElement();
            }
            catch (PeriodicTableException ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(string.Format("A unexpected exception occourred! Details: {0}", ex.Message), "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
