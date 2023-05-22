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

using static BakeryGudosh.ClassHelper.EFClass;
using static BakeryGudosh.ClassHelper.UserDataClass;
using BakeryGudosh.Windows;
using BakeryGudosh.ClassHelper;

namespace BakeryGudosh
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_RegisrtClick(object sender, RoutedEventArgs e)
        {
            //Переход на окно регистрации

            RegistrWindow registrWindow = new RegistrWindow();
            registrWindow.Show();
            this.Close();
        }

        private void Button_SignClick(object sender, RoutedEventArgs e)
        {
            //создаем переменную которая будет стравнивать введенную нами информацию с информацией в бд
            var userAuth = db.User.FirstOrDefault(i => i.Login == tbxLogin.Text && i.Password == pbPass.Password);

            if (userAuth != null)
            {
                //Сохранение авторизированного пользователя в класс
                UserDataClass.user = userAuth;

                //Переход на страницу меню
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
    }
}
