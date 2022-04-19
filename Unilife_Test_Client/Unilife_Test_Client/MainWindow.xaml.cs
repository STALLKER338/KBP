using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;

namespace Unilife_Test_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        TcpClient client = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.Focus(Password);
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text, password = Password.Text, potok;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                potok = String.Format("1 {0} {1} ", login, password);
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
                if (potok[0] == '1')
                {
                    Kod taskWindow = new Kod(login, password);
                    taskWindow.Show();
                    taskWindow.Left = this.Left;
                    taskWindow.Top = this.Top;
                    taskWindow.Height = this.Height;
                    taskWindow.Width = this.Width;
                    this.Close();
                    client.Close();
                }
                else
                {
                    MessageBox.Show("Eror: Не обнаружено пользователя с данным логином и паролем...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(Login);
        }
    }
}