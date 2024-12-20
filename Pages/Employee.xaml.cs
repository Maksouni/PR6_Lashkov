using PR6_Lashkov.Models;
using PR6_Lashkov.Services;
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

namespace PR6_Lashkov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        bool isGoingBack;
        Employees employee;
        public Employee(Users user)
        {
            InitializeComponent();
            isGoingBack = false;
            Loaded += EmployeePage_Loaded;
            try
            {
                employee = DbHelper.GetEmployeeByUserId(user.id);
            }
            catch
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                isGoingBack = true;
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
                    MessageBox.Show("Доступ запрещён в нерабочее время", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Stop);
                    isGoingBack = true;
                    break;
            }

            lblGreeting.Text = $"{greeting},\n{employee.surname} {employee.name} {employee.patronymic}";
        }

        private void EmployeePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (isGoingBack) NavigationService.GoBack();
        }
    }
}
