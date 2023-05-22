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
using System.Collections.ObjectModel;

using BakeryGudosh.DB;
using BakeryGudosh.ClassHelper;
using static BakeryGudosh.ClassHelper.CardAddClass;

namespace BakeryGudosh.Windows
{

    public partial class CardProdWindow : Window
    {

        public CardProdWindow()
        {
            InitializeComponent();

            TotalCost();

            //Определяет логин пользователя в менюшке с профилем
            tbxUser.Text = UserDataClass.user.Login;

            LvCardProd.ItemsSource = observableCollectionProduct;

            Product product = new Product();

            //Делаем проверку, если сегодня 5ая пятница месяца, то применяется скидка
            if (IsFriday() && IsFifthFridayOfMonth())
            {
                decimal amount = product.Cost - (product.Cost * 0.2m);
                product.Cost = amount;
            }
        }

        //метод считающий общую сумму добавленных товаров в корзину
        private void TotalCost()
        {
            double amountCostItem = 0;

            foreach (Product product in CardAddClass.observableCollectionProduct)
            {
                amountCostItem += Convert.ToDouble(product.Cost);
                tbxAmount.Text = "Итого: " + amountCostItem.ToString();
            }
        }

        //Метод проверяет является ли сегодняшний день пятницой
        private bool IsFriday()
        {
            return DateTime.Now.DayOfWeek == DayOfWeek.Friday;
        }

        //Метод проверяет является ли сегодняшний день 5ой пятницой месяца
        private bool IsFifthFridayOfMonth()
        {
            DateTime currentDate = DateTime.Now;
            int currentDay = currentDate.Day;
            DayOfWeek currentDayOfWeek = currentDate.DayOfWeek;
            int count = 0;

            for (int day = 0; day <= currentDay; day++)
            {
                DateTime tempDate = new DateTime(currentDate.Year, currentDate.Month, day);
                if (tempDate.DayOfWeek == currentDayOfWeek)
                {
                    count++;
                }
            }

            return (count == 5 && currentDayOfWeek == DayOfWeek.Friday);
        }

        //Клик на нажатии кнопки назад (закрывает корзину) 
        private void BtnBack1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBuyProduct1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
