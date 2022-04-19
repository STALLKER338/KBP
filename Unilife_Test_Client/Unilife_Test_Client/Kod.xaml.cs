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
    /// Логика взаимодействия для kod.xaml
    /// </summary>
    public partial class Kod : Window
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        TcpClient client = null;
        string login, password;
        public Kod(string login1, string password1)
        {
            login = login1;
            password = password1;
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(Key);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string kod = Key.Text, potok;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();
                potok = String.Format("2 {0} {1} {2}", login,password, kod);
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
                    client.Close();
                    Main main = new Main(login,password,kod);
                    main.Left = this.Left;
                    main.Top = this.Top;
                    main.Height = this.Height;
                    main.Width = this.Width;
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eror: Неверный код...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
