using PeriodicTable.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeriodicTable.MobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class APIUpdate : ContentPage
    {
        private readonly ElementDAO ElementDAO = new ElementDAO();

        public APIUpdate()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await Update();
        }

        private async Task Update()
        {
            try
            {
                await ElementDAO.UpdateFromAPI();
            }
            catch (WebException)
            {
                await DisplayAlert("Periodic Table", "Unable to get information from server! Using from cached.", "Ok");
            }
            finally
            {
                Application.Current.MainPage = new MainPage();
            }
        }
    }
}