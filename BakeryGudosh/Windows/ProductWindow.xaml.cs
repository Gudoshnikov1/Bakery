﻿using BakeryGudosh.ClassHelper;
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
using System.Windows.Shapes;

using static BakeryGudosh.ClassHelper.EFClass;
using BakeryGudosh.Windows;
using BakeryGudosh.DB;

namespace BakeryGudosh.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {

        //Лист который содержит в себе возможные параметры сортировки

        List<string> strings = new List<string>()
        {
            "По умолчанию",
            "По возрастанию",
            "По убыванию",
            "По цене",
            "В наличии"

        };

        public ProductWindow()
        {
            InitializeComponent();

            //Определяет логин пользователя в менюшке с профилем
            tbxUser.Text = UserDataClass.user.Login;

            //стартовый индекс в комбобоксе
            CmdSort.ItemsSource = strings;
            CmdSort.SelectedIndex = 0;

            //при страрте окна, вызываем метод содержащий информацию о товарах
            GetListProduct();
        }

        private void GetListProduct()
        {
            //Этот лист принимает в себя продукты которые добавлены в бд

            List<Product> products = new List<Product>();
            products = EFClass.db.Product.ToList();

            //Значение сортировки которое будет выставлено по умолчанию
            var StartIndex = CmdSort.SelectedIndex;

            //Поиск
            products = products.Where(i => i.Title.ToLower().Contains(tbxPoisk.Text.ToLower())).ToList();

            //Сортировка

            switch (StartIndex)
            {
                //в зависимости от выбранного списка, будет выполняться действие
                case 0:
                    products = products.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    products = products.OrderBy(i => i.Title.ToLower()).ToList();
                    break;
                case 2:
                    products = products.OrderByDescending(i => i.Title.ToLower()).ToList();
                    break;
                case 3:
                    products = products.OrderBy(i => i.Cost).ToList();
                    break;
                case 4:
                    products = products.OrderBy(i => i.Quantity).ToList();
                    break;

                default:
                    break;
            }

            LvProduct.ItemsSource = products;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }
            //передаем данные с продукта на кнопку и дальше
            var product = button.DataContext as Product;
            //обьявляем окно и открываем его
            EditWindow editProductWindow = new EditWindow(product);
            editProductWindow.ShowDialog();

            GetListProduct();
        }

        private void tbxPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetListProduct();
        }

        private void CmdSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetListProduct();
        }

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            //говорим что конкретно нажатая кнопка будет отвечать за что то
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var product = button.DataContext as Product;
            //добавляем выбранную плитку в коллекцию
            CardAddClass.observableCollectionProduct.Add(product);
            //выводим сообщение о добавлении товара
            MessageBox.Show($"Товар {product.Title} успешно добавлен в корзину");
        }

        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CardProdWindow card = new CardProdWindow();
            this.Hide();
            card.ShowDialog();
            this.Show();
        }
    }
}
