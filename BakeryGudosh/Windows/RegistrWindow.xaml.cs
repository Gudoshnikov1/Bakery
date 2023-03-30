using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

using static BakeryGudosh.ClassHelper.EFClass;
using static BakeryGudosh.ClassHelper.UserDataClass;
using BakeryGudosh.Windows;
using BakeryGudosh.ClassHelper;

namespace BakeryGudosh.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrWindow.xaml
    /// </summary>
    public partial class RegistrWindow : Window
    {
        public RegistrWindow()
        {
            InitializeComponent();
        }

        private void Button_RegClick(object sender, RoutedEventArgs e)
        {

            //Проверка ввода данных

            if (pbPass.Password == "")
            {
                MessageBox.Show("Введите пароль");
            }

            //Добавление пользователя в базу данных

            db.User.Add(new DB.User
            {
                Login = tbxLogin.Text,
                Password = pbPass.Password,
                Email = tbxEmail.Text,
            });

            db.SaveChanges();

            MessageBox.Show("OK");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
