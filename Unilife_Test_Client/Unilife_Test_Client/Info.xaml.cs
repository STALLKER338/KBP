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
using System.Windows.Shapes;

namespace Unilife_Test_Client
{
    /// <summary>
    /// Логика взаимодействия для Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        string login, kod, password;
        public Info(string login, string password, string kod)
        {
            this.login = login;
            this.password = password;
            this.kod = kod;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main window = new Main(login, password, kod);
            window.Left = this.Left;
            window.Top = this.Top;
            window.Height = this.Height;
            window.Width = this.Width;
            window.Show();
            this.Close();

        }
    }
}
