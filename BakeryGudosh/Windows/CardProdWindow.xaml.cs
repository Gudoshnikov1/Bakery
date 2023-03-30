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

using static BakeryGudosh.ClassHelper.EFClass;
using BakeryGudosh.Windows;
using BakeryGudosh.DB;
using BakeryGudosh.ClassHelper;
using static BakeryGudosh.ClassHelper.CardAddClass;

namespace BakeryGudosh.Windows
{
    /// <summary>
    /// Логика взаимодействия для CardProdWindow.xaml
    /// </summary>
    public partial class CardProdWindow : Window
    {

        public CardProdWindow()
        {
            InitializeComponent();

            LvCardProd.ItemsSource = observableCollectionProduct;
        }

        private void BtnBack1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBuyProduct1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
