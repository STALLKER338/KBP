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
    /// Логика взаимодействия для Addgroupxaml.xaml
    /// </summary>
    public partial class Addgroup : Window
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        TcpClient client = null;
        string login, password, kod;
        public Addgroup(string login, string password, string kod)
        {
            this.login = login;
            this.password = password;
            this.kod = kod;
            InitializeComponent();
            client = new TcpClient(address, port);
            NetworkStream stream = client.GetStream();
            string potok;
            potok = String.Format("5 {0} {1} {2}", login, password, kod);
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
