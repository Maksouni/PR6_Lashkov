using PR6_Lashkov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для EmployeeEdit.xaml
    /// </summary>
    public partial class EmployeeEdit : Page
    {
        public EmployeeEdit(Employees employee)
        {
            InitializeComponent();

            tbSurname.Text = employee.surname;
            tbName.Text = employee.name;
            tbPatronymic.Text = employee.patronymic;
            tbPhone.TextChanged -= tbPhone_TextChanged;
            tbPhone.Text = employee.phone;
            tbPhone.TextChanged += tbPhone_TextChanged;
            tbSalary.Text = employee.salary.ToString();
            tbLogin.Text = employee.Users.login;
            cbPosition.ItemsSource = DbHelper.GetPositions();
            cbPosition.SelectedValue = employee.position_id;

        }

        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9,]");
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbPhone.TextChanged -= tbPhone_TextChanged;
            string text = Regex.Replace(tbPhone.Text, "[^0-9]", "");
            if (text.Length > 11)
                text = text.Substring(0, 11);

            // Добавляем форматирование
            if (text.StartsWith("7"))
            {
                text = "+7" + text.Substring(1);
            }
            else if (!text.StartsWith("+7"))
            {
                text = "+7" + text;
            }

            tbPhone.Text = text;
            tbPhone.CaretIndex = text.Length; 

            tbPhone.TextChanged += tbPhone_TextChanged;
        }
    }
}
