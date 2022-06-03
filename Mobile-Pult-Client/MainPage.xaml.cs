using MobilePultClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_Pult_Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Connect_Clicked(object sender, EventArgs e)
        {
            try
            {
                TcpClient client = new TcpClient();
                //await client.ConnectAsync(IPAddress.Text, Convert.ToInt32(Port.Text));
                await client.ConnectAsync("192.168.0.15", Convert.ToInt32("1234"));
                if (client.Connected)
                {
                    Connection.Instance.client = client;
                    Application.Current.MainPage = new NavigationPage(new OperationsPage());

                    await DisplayAlert("Connected", "Connected to server successfully!", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Connection unsuccessful!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "" + ex.ToString(), "Ok");
            }
        }

    }
}
