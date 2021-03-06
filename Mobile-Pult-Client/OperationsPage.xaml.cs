using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePultClient
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationsPage : ContentPage
    {
        public OperationsPage()
        {
            InitializeComponent();
        }

        // Команда для кнопки Sleep
        private void Sleep_Clicked(object sender, EventArgs e)
        {
            var client = Connection.Instance.client;
            NetworkStream stream = client.GetStream();
            String s = "SLP2";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
        }

        // Команда для кнопки Shutdown
        private void Shutdown_Clicked(object sender, EventArgs e)
        {
            var client = Connection.Instance.client;
            NetworkStream stream = client.GetStream();
            String s = "SHTD3";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
        }

        // Команда для снимка экрана
        private void Screenshot_Clicked(object sender, EventArgs e)
        {
            var client = Connection.Instance.client;
            NetworkStream stream = client.GetStream();
            String s = "TSC1";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
            var data = getData(client);
            imageView.Source = ImageSource.FromStream(() => new MemoryStream(data));
        }

        // Команда для уменьшения звука
        private void VolumeDown_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = Connection.Instance.client;
                NetworkStream stream = client.GetStream();
                String s = "LSVLM5";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            }

            catch (NullReferenceException ex)
            {
                DisplayAlert("error", $"{ex.Message}", "Ok");
            }
        }

        // Команда для увеличения звука
        private void VolumeUp_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = Connection.Instance.client;
                NetworkStream stream = client.GetStream();
                String s = "MRVLM4";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            }

            catch (NullReferenceException ex)
            {
                DisplayAlert( "error", $"{ex.Message}", "Ok");
            }
        }

        // Команда для отключения звука
        private void Mute_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = Connection.Instance.client;
            NetworkStream stream = client.GetStream();
            String s = "MT6";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
            }

            catch (NullReferenceException ex)
            {
                DisplayAlert("error", $"{ex.Message}", "Ok");
            }

        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = Connection.Instance.client;
                NetworkStream stream = client.GetStream();
                String s = "PLAY8";
                byte[] message = Encoding.ASCII.GetBytes(s);
                stream.Write(message, 0, message.Length);
            }

            catch (NullReferenceException ex)
            {
                DisplayAlert("error", $"{ex.Message}", "Ok");
            }

        }

        // Сбор данных с сервера
        public byte[] getData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] fileSizeBytes = new byte[4];
            int bytes = stream.Read(fileSizeBytes, 0, fileSizeBytes.Length);
            int dataLength = BitConverter.ToInt32(fileSizeBytes, 0);

            int bytesLeft = dataLength;
            byte[] data = new byte[dataLength];

            int buffersize = 1024;
            int bytesRead = 0;

            while (bytesLeft > 0)
            {
                int curDataSize = Math.Min(buffersize, bytesLeft);
                if (client.Available < curDataSize)
                    curDataSize = client.Available;
                bytes = stream.Read(data, bytesRead, curDataSize);
                bytesRead += curDataSize;
                bytesLeft -= curDataSize;
            }
            return data;
        }
    }
}