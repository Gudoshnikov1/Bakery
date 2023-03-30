using BakeryGudosh.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryGudosh.ClassHelper
{
    internal class CardAddClass
    {
        public static List<Product> products = new List<Product>();
        public static ObservableCollection<Product> observableCollectionProduct = new ObservableCollection<Product>(ClassHelper.CardAddClass.products);
    }
}
