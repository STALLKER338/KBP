using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Unilife_Test_Client
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        const int port = 8888;
        bool but = false;
        const string address = "127.0.0.1";
        TcpClient client = null;
        string login, kod, password;
        public Main(string login1, string password1, string kod1)
        {
            login = login1;
            kod = kod1;
            password = password1;      
            InitializeComponent();                  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (but == false)
            {
                string predmet = Predmets.SelectedItem.ToString();
                string potok;
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                potok = String.Format("4 {0} {1} {2} {3}", login, password, kod, predmet);
                byte[] data = Encoding.Unicode.GetBytes(potok);
                stream.Write(data, 0, data.Length);
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);
                potok = builder.ToString();
                builder = new StringBuilder();

                Predmets.Items.Clear();
                int a = Convert.ToInt32(potok);
                for (int i = 0; i < a; i++)
                {
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        potok = builder.ToString();
                        builder = new StringBuilder();
                        Predmets.Items.Add(potok);
                    }
                    while (stream.DataAvailable);
                    data = Encoding.Unicode.GetBytes(potok);
                    stream.Write(data, 0, data.Length);

                }
                Button2.Visibility = Visibility.Visible;
                but = true;
            }
            else
            {

            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Window_Loaded(sender,e);
        }   
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Predmets.Items.Clear();
            but = false;
            string potok;
            Button2.Visibility = Visibility.Collapsed;
            Login.Text = "Логин: " + login;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                potok = String.Format("3 {0} {1} {2}", login, password, kod);
                byte[] data = Encoding.Unicode.GetBytes(potok);
                stream.Write(data, 0, data.Length);
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);
                potok = builder.ToString();
                builder = new StringBuilder();
                string group = potok;
                Group.Text = "Группа: " + group;

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);


                potok = builder.ToString();
                builder = new StringBuilder();
                int a = Convert.ToInt32(potok);
                for (int i = 0; i < a; i++)
                {
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        potok = builder.ToString();
                        builder = new StringBuilder();
                        Predmets.Items.Add(potok);
                    }
                    while (stream.DataAvailable);
                    data = Encoding.Unicode.GetBytes(potok);
                    stream.Write(data, 0, data.Length);

                }
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            Info inf = new Info(login, password, kod);
            inf.Left = this.Left;
            inf.Top = this.Top;
            inf.Height = this.Height;
            inf.Width = this.Width;
            inf.Show();
            this.Close();
        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
