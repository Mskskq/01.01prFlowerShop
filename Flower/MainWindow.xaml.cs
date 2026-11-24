using Flower.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flower
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        FlowersContext DataBase = new FlowersContext();
        User user = new User();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            user = DataBase.Users.Include(x => x.AddressNavigation)
    .ThenInclude(a => a.IdStreatNavigation).Where(x => x.Login == LoginTextBox.Text && x.Password == PasswordTextBox.Text).FirstOrDefault();
            if(user != null)
            {
                WindowCatalog window = new WindowCatalog();
                window.User = user;
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пароль или логин введён неверно");
            }
           
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            WindowRegistration window = new WindowRegistration();
            window.Show();
            this.Close();
        }
    }
}