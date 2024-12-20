using PR6_Lashkov.Models;
using PR6_Lashkov.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PR6_Lashkov.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        int click;
        Timer timer;
        int seconds;
        public Auth()
        {
            InitializeComponent();
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;
            timer = new Timer(1000);
            timer.Elapsed += Timer_Elapsed;
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
            string password = Hash.HashPassword(tbPassword.Password.Trim());

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
                    tbPassword.Password = String.Empty;
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
                    GenerateCapctcha();
                    if (click >= 3)
                    {
                        btnEnter.IsEnabled = false;
                        btnEnterGuest.IsEnabled = false;
                        tbLogin.IsEnabled = false;
                        tbPassword.IsEnabled = false;
                        tbCaptcha.IsEnabled = false;
                        seconds = 0;
                        timer.Start();
                    }
                }
            }
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs elapsed)
        {
            if (seconds < 10)
            {
                Dispatcher.Invoke(() =>
                {
                    tbTimer.Text = $"Повторите попытку через {10 - seconds}";
                });
            }
            else
            {
                timer.Stop();
                Dispatcher.Invoke(() =>
                {
                    btnEnter.IsEnabled = true;
                    btnEnterGuest.IsEnabled = true;
                    tbLogin.IsEnabled = true;
                    tbPassword.IsEnabled = true;
                    tbCaptcha.IsEnabled = true;
                    click = 1;
                    tbTimer.Text = String.Empty;
                });
            }
            seconds++;
            
                
        }

        private void LoadPage(Users user)
        {
            click = 0;
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;
            tbCaptcha.Text = "";
            tbPassword.Password = "";
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
