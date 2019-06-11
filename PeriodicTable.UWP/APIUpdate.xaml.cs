using PeriodicTable.Model.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace PeriodicTable.UWP
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class APIUpdate : Page
    {
        private readonly ElementDAO ElementDAO = new ElementDAO();

        public APIUpdate()
        {
            this.InitializeComponent();
        }

        private async Task Update()
        {
            try
            {
                await ElementDAO.UpdateFromAPI(new Progress<int>(percent =>
                {
                    if (percent == -1)
                    {
                        // Checking API
                        UpdateProgressBar.IsIndeterminate = true;
                        LabelStatus.Text = "Checking API status";
                    }
                    else
                    {
                        // Updating cache
                        UpdateProgressBar.IsIndeterminate = false;
                        LabelStatus.Text = "Updating cache";
                        UpdateProgressBar.Value = percent;
                    }
                }));
            }
            catch (WebException)
            {
                await new ContentDialog
                {
                    Title = "Periodic Table",
                    Content = "Unable to get information from server! Using from cached.",
                    CloseButtonText = "Ok"
                }.ShowAsync();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Update();

            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage), e);
        }
    }
}
