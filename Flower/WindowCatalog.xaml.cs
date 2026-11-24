using Flower.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
    /// Логика взаимодействия для WindowCatalog.xaml
    /// </summary>
    public partial class WindowCatalog : Window
    {
        public WindowCatalog()
        {
            InitializeComponent();
        }

        private User _user;
        FlowersContext dataBase = new FlowersContext();
        public User User { get {  return _user; } set { _user = value; } }

        private void UserLoginLabel_Loaded(object sender, RoutedEventArgs e)
        {
            GetCatalog();
            UserLoginLabel.Content = $"{User.Name} {User.Lastname} {User.Surname}";
            UpdateDataGridCorisna();
        }

        public void UpdateDataGridCorisna()
        {
            CorsinaDataGrid.ItemsSource = dataBase.Corsinas.Where(x => x.UserId == User.Id).ToList();
            UpdateTotalPriceCorsina();
        }

        public void UpdateTotalPriceCorsina()
        {
           decimal totalPriceInCorsina = (decimal)dataBase.Corsinas.Include(x => x.Flowers).Where(x => x.UserId == User.Id).ToList().Sum(x => x.Count * x.Flowers.Price);
           TotalPriceLabel.Content = totalPriceInCorsina + " Р";
        }

        public void GetCatalog()
        {
            FlowerListView.ItemsSource = dataBase.Flowers.ToList();
            foreach (Models.Flower stroka in FlowerListView.Items)
            {
                string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", stroka.Img);
                if (fullPath == null)
                {
                    fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "picture.png");
                }
                stroka.Img = fullPath;
            }
            FlowerListView.Items.Refresh();
        }

        private void FlowerListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FlowerListView.SelectedItem != null)
            {
                Models.Flower SelectedFlower = (Models.Flower)FlowerListView.SelectedItem;
                Corsina itemCorsina = new Corsina();
                if (dataBase.Corsinas.Any(x => x.FlowersId == SelectedFlower.Id))
                {
                    itemCorsina = dataBase.Corsinas.Where(x => x.FlowersId == SelectedFlower.Id).FirstOrDefault();
                    itemCorsina.Count += 1;
                }
                else
                {
                    itemCorsina = new Corsina
                    {
                        UserId = User.Id,
                        FlowersId = SelectedFlower.Id,
                        Count = 1,
                    };
                    dataBase.Corsinas.Add(itemCorsina);
                }
                
                dataBase.SaveChanges();
                UpdateDataGridCorisna();
            }
            
        }

        private void DeleteFlowerInCorsinaButton_Click(object sender, RoutedEventArgs e)
        {
            if(CorsinaDataGrid.SelectedItem != null)
            {
                Corsina SelectedItemCorsina = (Corsina)CorsinaDataGrid.SelectedItem;
                dataBase.Corsinas.Remove(SelectedItemCorsina);
                dataBase.SaveChanges();
                CorsinaDataGrid.SelectedItem = null;
                UpdateDataGridCorisna();
            }
            else
            {
                MessageBox.Show("Выберите объект коризны");
            }
        }

        private void CreateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if(dataBase.Corsinas.Where(x=> x.UserId == User.Id).Any(x=> x.FlowersId != null))
            {
                WindowOrder window = new WindowOrder();
                window.User = User;
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните корзину");
            }
        }
    }
}
