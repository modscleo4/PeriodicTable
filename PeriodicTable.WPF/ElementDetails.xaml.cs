using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Entity;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace PeriodicTable.WPF
{
    /// <summary>
    /// Interação lógica para ElementDetails.xaml
    /// </summary>
    public partial class ElementDetails : Modscleo4.WPFUI.Controls.Window
    {
        private readonly ElementDAO ElementDAO = new ElementDAO();
        private readonly PeriodicTableUtils PeriodicTableUtils = new PeriodicTableUtils();

        public ElementDetails()
        {
            InitializeComponent();
        }

        public ElementDetails(int atomicNumber)
        {
            InitializeComponent();

            try
            {
                LoadElement(atomicNumber);
            }
            catch (PeriodicTableException ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Modscleo4.WPFUI.MessageBox.Show($"A unexpected exception occourred! Details: {ex.Message}", "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void LoadElement(int atomicNumber)
        {
            var element = ElementDAO.Select(atomicNumber);
            LoadElementInfo(element);
        }

        private void LoadElement(string symbol)
        {
            var element = ElementDAO.Select(symbol);
            LoadElementInfo(element);
        }

        private void LoadElementInfo(Element element)
        {
            if (element == null)
            {
                throw new PeriodicTableException("Element does not exist!");
            }

            ElementBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(element.GroupBlock.Color.A, element.GroupBlock.Color.R, element.GroupBlock.Color.G, element.GroupBlock.Color.B));

            //LabelGP.Text = $"{PeriodicTableUtils.GetPeriod(element.AtomicNumber)}, {PeriodicTableUtils.GetGroup(element.AtomicNumber)}";
            //LabelFreeElectrons.Text = $"{PeriodicTableUtils.GetFreeElectrons(element.AtomicNumber)}";
            LabelElectronicDistribution.Text = PeriodicTableUtils.GetElectronsPerLevel(element.AtomicNumber);

            Title = $"{(element.Name ?? "Element")} - Periodic Table";

            LabelNumber.Text = element.AtomicNumber.ToString();

            if (element.Symbol != null)
            {
                LabelSymbol.Text = element.Symbol;
            }

            LabelMass.Text = element.AtomicMass.ToString();

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
                LabelElectronicConfiguration.ToolTip = string.Join(" ", PeriodicTableUtils.APIElectronicDistribution(element.AtomicNumber));
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
                var oxStates = new List<string>();
                foreach (var oxState in element.OxidationStates)
                {
                    oxStates.Add(oxState > 0 ? $"+{oxState}" : $"{oxState}");
                }

                LabelOxidationStates.Text = string.Join(", ", oxStates);
            }

            if (element.StandardState != null)
            {
                LabelStandardStates.Text = element.StandardState.Value;
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
            Title = "Element - Periodic Table";

            LabelElectronicDistribution.Text = "0";

            LabelNumber.Text = "Number";
            LabelSymbol.Text = "Symbol";
            LabelName.Text = "Name";
            LabelMass.Text = "Mass";

            LabelAtomicRadius.Text = string.Empty;
            LabelMeltingPoint.Text = string.Empty;
            LabelBoilingPoint.Text = string.Empty;
            LabelDensity.Text = string.Empty;
            LabelElectronAffinity.Text = string.Empty;
            LabelElectronegativity.Text = string.Empty;

            LabelElectronicConfiguration.Text = string.Empty;
            LabelElectronicConfiguration.ToolTip = string.Empty;

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
            var name = SearchboxValue;
            Clear();

            try
            {
                if (int.TryParse(name, out int n))
                {
                    LoadElement(n);
                }
                else
                {
                    LoadElement(name);
                }
            }
            catch (PeriodicTableException ex)
            {
                Modscleo4.WPFUI.MessageBox.Show(ex.Message, "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (Exception ex)
            {
                Modscleo4.WPFUI.MessageBox.Show($"A unexpected exception occourred! Details: {ex.Message}", "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
