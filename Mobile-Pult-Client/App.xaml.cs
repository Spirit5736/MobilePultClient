using MobilePultClient;
using System;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Pult_Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {

            var client = Connection.Instance.client;
            NetworkStream stream = client.GetStream();
            String s = "EXT7";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);

        }

        protected override void OnResume()
        {
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
