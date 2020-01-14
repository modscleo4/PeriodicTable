using PeriodicTable.MobileApp.Controls;
using PeriodicTable.Model.DAO;
using PeriodicTable.Model.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PeriodicTable.MobileApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        private readonly uint rowLock = PeriodicTableUtils.GetPeriodMaxE(5);

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var elements = await ElementDAO.SelectAsync();
            foreach (var element in elements)
            {
                var gridElement = new GridElement()
                {
                    Symbol = element.Symbol,
                    BorderColor = Color.FromRgba(element.GroupBlock.Color.R, element.GroupBlock.Color.G, element.GroupBlock.Color.B, element.GroupBlock.Color.A)
                };

                var column = PeriodicTableUtils.GetGroup(element.AtomicNumber);
                var row = PeriodicTableUtils.GetPeriod(element.AtomicNumber);

                if ((row == 1 && column >= 2) || (row <= 5 && column > 2))
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

                ElementsGrid.Children.Add(gridElement, (int)column - 1, (int)row - 1);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}
