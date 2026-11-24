using Flower.Models;
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

namespace Flower
{
    /// <summary>
    /// Логика взаимодействия для WindowRegistration.xaml
    /// </summary>
    public partial class WindowRegistration : Window
    {
        public WindowRegistration()
        {
            InitializeComponent();
        }

        private bool _noApartmentCheckBox = false;
        FlowersContext DataBase = new FlowersContext();

        private void NoApartmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            if(NoApartmentCheckBox.IsChecked == true)
            {
                _noApartmentCheckBox = true;
                ApartmentTextBox.Visibility = Visibility.Hidden;
                ApartmentTextBox.Clear();
            }
            else
            {
                _noApartmentCheckBox = false;
                ApartmentTextBox.Visibility = Visibility.Visible;
                ApartmentTextBox.Clear();
            }
            
        }

        public bool ChekedInputsForUser()
        {

            if (string.IsNullOrEmpty(NameTextBox.Text) ||
                string.IsNullOrEmpty(LastnameTextBox.Text) ||
                string.IsNullOrEmpty(SurnameTextBox.Text) ||
                string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text) ||
                !PhoneMaskedBox.IsMaskFull || // <-- добавили восклицательный знак (!)
                string.IsNullOrEmpty(StreetTextBox.Text) ||
                string.IsNullOrEmpty(HouseTextBox.Text))
            {
                return false;
            }

            // Если галочка «Нет квартиры» НЕ стоит, проверяем, что поле квартиры заполнено
            if (NoApartmentCheckBox.IsChecked != true && string.IsNullOrEmpty(ApartmentTextBox.Text))
            {
                return false;
            }

            return true;
        }

        public string GetSpisokStreat()
        {
            string res = "";
            List<Streat> SpisokSteatInBD = DataBase.Streats.ToList();
            foreach(Streat itemsInSpisok in SpisokSteatInBD)
            {
                res += itemsInSpisok.Name.ToString() + " ";
            }
            return res;
        }

        private void RegisterSubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ChekedInputsForUser())
            {
                MessageBox.Show("Заполните все данные");
                return;
            }

            if(!DataBase.Streats.Any(x => x.Name == StreetTextBox.Text))
            {
                string SpisokStreatInBD = GetSpisokStreat();
                MessageBox.Show("Список улиц которые обслуживаються: " + SpisokStreatInBD);
                return;
            }

            Adress adress = new Adress
            {
                IdStreat = DataBase.Streats.Where(x=> x.Name == StreetTextBox.Text).Select(x => x.Id).FirstOrDefault(),
                NumberHouse = HouseTextBox.Text,
                NumberOpartamens = Convert.ToInt32(ApartmentTextBox.Text)
            };
            DataBase.Adresses.Add(adress);
            DataBase.SaveChanges();

            User newUser = new User
            {
                Name = NameTextBox.Text,
                Lastname = LastnameTextBox.Text,
                Surname = SurnameTextBox.Text,
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text,
                Telephone = PhoneMaskedBox.Text,
                Address = adress.Id
            };

            DataBase.Users.Add(newUser);
            DataBase.SaveChanges();
            MessageBox.Show("Пользователь успешно заригестрирован");
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void StreetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void HouseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(HouseTextBox.Text, out int _)) HouseTextBox.Clear();
        }

        private void ApartmentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(ApartmentTextBox.Text, out int _)) ApartmentTextBox.Clear();
        }
    }
}
