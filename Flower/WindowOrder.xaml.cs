using Flower.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для WindowOrder.xaml
    /// </summary>
    public partial class WindowOrder : Window
    {
        public WindowOrder()
        {
            InitializeComponent();
        }

        private User _user;
        FlowersContext dataBase = new FlowersContext();
        public bool chekedAprtaments = false;
        public User User { get { return _user; } set { _user = value; } }

        private void CreateOrderButton_Loaded(object sender, RoutedEventArgs e)
        {
            LastNameTextBox.Text = User.Lastname;
            NameTextBox.Text = User.Name;
            SurnameTextBox.Text = User.Surname;
            PhoneMaskedBox.Text = User.Telephone;
            StreetTextBox.Text = User.AddressNavigation.IdStreatNavigation.Name;
            HouseTextBox.Text = User.AddressNavigation.NumberHouse;
            ApartmentMaskedBox.Text = User.AddressNavigation.NumberOpartamens.ToString();
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckedInputsForOrder())
            {
                MessageBox.Show("Заполните все поля корректно");
                return;
            }

            DateTime localDateTime = DeliveryDatePicker.SelectedDate.Value.Date + TimeSpan.Parse(DeliveryTimeMaskedBox.Text);
            DateTime deliveryTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Local).ToUniversalTime();
            
            Order NewOrder = new Order
            {
                UserId = User.Id,
                DeliveryTime = deliveryTime,
                Cost = (decimal)dataBase.Corsinas.Include(x => x.Flowers).Where(x => x.UserId == User.Id).ToList().Sum(x => x.Count * x.Flowers.Price),
                IdAdress = User.Address
            };
            dataBase.Orders.Add(NewOrder);
            dataBase.SaveChanges();
            List<Corsina> CorsinaUser = dataBase.Corsinas.Where(x => x.UserId == User.Id).ToList();
            foreach (Corsina corsinaItem in CorsinaUser)
            {
                FlowersInOrder flowersInOrder = new FlowersInOrder
                {
                    Count = corsinaItem.Count,
                    FlowersId = corsinaItem.FlowersId,
                    OrdeId = NewOrder.Id
                };
                dataBase.FlowersInOrders.Add(flowersInOrder);
            }

            dataBase.Corsinas.RemoveRange(CorsinaUser);
            dataBase.SaveChanges();
            MessageBox.Show("Заказ оформлен");
            WindowCatalog windowCatalog = new WindowCatalog();
            windowCatalog.User = User;
            windowCatalog.Show();
            this.Close();
        }

        public bool CheckedInputsForOrder()
        {
            if (string.IsNullOrEmpty(LastNameTextBox.Text) ||
                string.IsNullOrEmpty(NameTextBox.Text) ||
                string.IsNullOrEmpty(SurnameTextBox.Text) ||
                !PhoneMaskedBox.IsMaskFull ||
                string.IsNullOrEmpty(StreetTextBox.Text) ||
                string.IsNullOrEmpty(HouseTextBox.Text) ||
                DeliveryDatePicker.SelectedDate == null ||
                !DeliveryTimeMaskedBox.IsMaskFull)
            {
                return false;
            }

            if (!TimeSpan.TryParse(DeliveryTimeMaskedBox.Text, out _))
            {
                return false;
            }

            if (NoApartmentCheckBox.IsChecked != true &&
                !int.TryParse(ApartmentMaskedBox.Text.Trim(), out _))
            {
                return false;
            }

            return true;
        }

        private void NoApartmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (NoApartmentCheckBox.IsChecked == true)
            {
                chekedAprtaments = true;
                ApartmentMaskedBox.Visibility = Visibility.Hidden;
            }
            else
            {
                chekedAprtaments = false;
                ApartmentMaskedBox.Visibility = Visibility.Visible;
            }
        }
    }
}
