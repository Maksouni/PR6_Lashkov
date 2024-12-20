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
using PR6_Lashkov.Models;
using PR6_Lashkov.Services;

namespace PR6_Lashkov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        bool isGoingBack;
        Customers customer;
        public Client(Users user)
        {
            InitializeComponent();
            isGoingBack = false;
            Loaded += ClientPage_Loaded;
            string timeInterval = TimeGetter.GetTimeInterval();
            string greeting = "";
            switch (timeInterval)
            {
                case "утро":
                    greeting = "Доброе утро";
                    break;
                case "день":
                    greeting = "Добрый день";
                    break;
                case "вечер":
                    greeting = "Добрый вечер";
                    break;
                case "ночь":
                    greeting = "Доброй ночи";
                    break;
            }
            if (user != null)
            {
                try
                {
                    customer = DbHelper.GetCustomerByUserId(user.id);
                }
                catch
                {
                    MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    isGoingBack = true;
                }
                lblGreeting.Text = $"{greeting},\n{customer.surname} {customer.name} {customer.patronymic}";
            }
            else
                lblGreeting.Text = $"{greeting}, Гость";
        }

        private void ClientPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
