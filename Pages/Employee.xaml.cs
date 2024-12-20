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
        Employees employee;
        public Employee(Users user)
        {
            InitializeComponent();
            try
            {
                employee = DbHelper.GetEmployeeByUserId(user.id);
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

            lblGreeting.Text = $"{greeting},\n{employee.surname} {employee.name} {employee.patronymic}";
        }
    }
}
