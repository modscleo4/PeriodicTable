using PeriodicTable.Model.DAO;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace PeriodicTable.WPF
{
    /// <summary>
    /// Lógica interna para APIUpdate.xaml
    /// </summary>
    public partial class APIUpdate : Modscleo4.WPFUI.Controls.Window
    {
        private readonly ElementDAO ElementDAO = new ElementDAO();

        public APIUpdate()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Update();
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
                        LabelStatus.Content = "Checking API status";
                    }
                    else
                    {
                        // Updating cache
                        UpdateProgressBar.IsIndeterminate = false;
                        LabelStatus.Content = "Updating cache";
                        UpdateProgressBar.Value = percent;
                    }
                }));
            }
            catch (WebException)
            {
                Modscleo4.WPFUI.MessageBox.Show("Unable to get information from server! Using from cached.", "Periodic Table", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            finally
            {
                new MainWindow().Show();
                Close();
            }
        }
    }
}
