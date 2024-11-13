using PR6_Lashkov.Models;
using PR6_Lashkov.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        int click;
        public Auth()
        {
            InitializeComponent();
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;
            click = 0;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null));
        }

        private void GenerateCapctcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = capctchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = Hash.HashPassword(tbPassword.Text.Trim());

            DrinkFactoryEntities db = DbHelper.GetContext();

            var user = db.Users.Where(x => x.login == login && x.password == password).FirstOrDefault();
            if (click == 1)
            {
                if (user != null)
                {
                    MessageBox.Show("Вы вошли под: " + user.Roles.name.ToString());
                    LoadPage(user);
                }
                else
                {
                    MessageBox.Show("Вы ввели логин или пароль неверно!");
                    GenerateCapctcha();
                    tbPassword.Text = String.Empty;
                }
            }
            else if (click > 1)
            {
                if (user != null && tbCaptcha.Text == tblCaptcha.Text)
                {
                    MessageBox.Show("Вы вошли под: " + user.Roles.name.ToString());
                    LoadPage(user);
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                }
            }
            

        }

        private void LoadPage(Users user)
        {
            click = 0;
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;
            tbCaptcha.Text = "";
            tbPassword.Text = "";
            tbLogin.Text = "";

            switch (user.Roles.name)
            {
                case "customer":
                    NavigationService.Navigate(new Client(user));
                    break;
                case "admin":
                    NavigationService.Navigate(new Admin(user));
                    break;
                case "employee":
                    NavigationService.Navigate(new Employee(user));
                    break;
            }

        }

    }
}
