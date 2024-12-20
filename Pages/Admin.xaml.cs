using PR6_Lashkov.Models;
using PR6_Lashkov.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PR6_Lashkov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        public ObservableCollection<Employees> employees { get; set; }

        Employees admin;
        public Admin(Users user)
        {
            InitializeComponent();

            try
            {
                admin = DbHelper.GetEmployeeByUserId(user.id);
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                this.NavigationService.GoBack();
            }

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

            lblGreeting.Text = $"{greeting},\n{admin.surname} {admin.name} {admin.patronymic}";

            employees = new ObservableCollection<Employees>(DbHelper.GetEmployees());


            DataContext = this;
        }

        private void CardButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.CommandParameter is Employees clickedEmployee)
            {
                MessageBox.Show($"Кликнуто: {clickedEmployee.surname} ({clickedEmployee.name})");
            }
        }

    }
}
