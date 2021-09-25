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
using System.Globalization;

namespace UzsakymuUzduotis
{
    /// <summary>
    /// Interaction logic for addProductWindow.xaml
    /// </summary>
    public partial class addProductWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        public addProductWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pTitle.Text == "" || pPrice.Text == "")
            {
                MessageBox.Show("Prašome nepalikti tuščių laukų");
            }
            else if (!double.TryParse(pPrice.Text, out double num) || double.Parse(pPrice.Text) == 0 )
            {
                MessageBox.Show("Prašome įvesti teisingą kainą");
            }
            else if (pPrice.Text.Contains(","))
            {
                MessageBox.Show("Prašome kainoje naudoti tašką vietoje kabelio");
            }
            else if (int.TryParse(pTitle.Text, out int num2))
            {
                MessageBox.Show("Prašome įvesti teisingą pavadinimą");
            }
            else if (checkIfProductNameIsTaken(pTitle.Text))
            {
                MessageBox.Show("Tokia prekė jau egzistuoja");
            }
            else
            {
                double correctPrice = convertToPrice(pPrice.Text);
                product newProduct = new product()
                {
                    productTitle = pTitle.Text,
                    productPrice = correctPrice
                };
                dataB.products.Add(newProduct);
                dataB.SaveChanges();
                ProductsWindow.datagrid.ItemsSource = dataB.products.ToList();
                this.Hide();
            }
        }

        private bool checkIfProductNameIsTaken(string checkingName)
        {
            for (int i = 0; i < dataB.products.ToList().Count; i++)
            {
                if (checkingName == dataB.products.ToList()[i].productTitle) return true;
            }
            return false;

        }

        private double convertToPrice (string text)
        {
            double final = double.Parse(text);
            text = string.Format("{0:0.00}", final);
            final = double.Parse(text);

            return final;
        }
    }
}
