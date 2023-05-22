using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
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
using BakeryGudosh.ClassHelper;
using BakeryGudosh.Windows;
using BakeryGudosh.DB;
using System.Xml.Linq;


namespace BakeryGudosh.Windows
{
    public partial class EditWindow : Window
    {
        private string pathPhoto = null;

        private bool isEdit = false;

        private Product editProduct;

        public EditWindow()
        {
            InitializeComponent();

            //Выставляет нулевой индекс в комбобоксе
            CMBTypeProduct.ItemsSource = db.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            //показываем ему из какого столбца бд брать имя выбранного поля комбобокса
            CMBTypeProduct.DisplayMemberPath = "Name";
        }

        public EditWindow(Product product)
        {
            InitializeComponent();

            CMBTypeProduct.ItemsSource = db.ProductType.ToList();
            CMBTypeProduct.SelectedIndex = 0;
            CMBTypeProduct.DisplayMemberPath = "Name";

            //при нажатии кнопки редактирования автоматически заполняются поля открытой нами плитки товара
            tbxTitle.Text = product.Title.ToString();
            tbxDesc.Text = product.Description.ToString();
            CMBTypeProduct.SelectedItem = db.ProductType.Where(i => i.ID == product.TypeID).FirstOrDefault();

            //проверка на то чтобы поставить фотку
            if (product.Image != null)
            {
                using (MemoryStream stream = new MemoryStream(product.Image))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    ImgProduct.Source = bitmapImage;

                }
            }

            isEdit = true;
            editProduct = product;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //при нажатии на кнопку редактирования он будет проверять есть этот товар или нет
            if (isEdit)
            {
                //изменение товара

                editProduct.Title = tbxTitle.Text;
                editProduct.Description = tbxDesc.Text;
                if (pathPhoto != null)
                {
                    editProduct.Image = File.ReadAllBytes(pathPhoto);
                }
                db.SaveChanges();
                MessageBox.Show("OK");
            }
            else
            {
                //добавление товара 
                Product product = new Product();
                product.Title = tbxTitle.Text;
                product.Description = tbxDesc.Text;
                product.ID = (CMBTypeProduct.SelectedItem as ProductType).ID;
                if (pathPhoto != null)
                {
                    product.Image = File.ReadAllBytes(pathPhoto);
                }
                //при заполнении всех полей сохраняются изменения и добавляются в базу
                db.Product.Add(product);

                db.SaveChanges();
                MessageBox.Show("OK");
            }

            this.Close();
        }

        private void btnChoseImage_Click(object sender, RoutedEventArgs e)
        {
            //кнопка выбора изображения
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImgProduct.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                //определаяем фото в бд
                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}
