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

namespace UzsakymuUzduotis
{
    /// <summary>
    /// Interaction logic for editProductWindow.xaml
    /// </summary>
    public partial class editProductWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        int idet;
        string originalName;
        public editProductWindow(int ide, string title, double price)
        {
            InitializeComponent();
            idet = ide;
            pTitle.Text = title;
            pPrice.Text = price.ToString();
            originalName = title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (pTitle.Text == "" || pPrice.Text == "")
            {
                MessageBox.Show("Prašome nepalikti tuščių laukų");
            }
            else if (!double.TryParse(pPrice.Text, out double num) || double.Parse(pPrice.Text) == 0)
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
                product editedP = (from o in dataB.products
                                   where o.idProduct == idet
                                   select o).Single();
                editedP.productTitle = pTitle.Text;
                editedP.productPrice = correctPrice;
                dataB.SaveChanges();
                ProductsWindow.datagrid.ItemsSource = dataB.products.ToList();
                this.Hide();
            }
        }
        

        private bool checkIfProductNameIsTaken(string checkingName)
        {
            for (int i = 0; i < dataB.products.ToList().Count; i++)
            {
                if (checkingName == dataB.products.ToList()[i].productTitle && originalName != checkingName) return true;
            }
            return false;
        }

        private double convertToPrice(string text)
        {
            double final = double.Parse(text);
            text = string.Format("{0:0.00}", final);
            final = double.Parse(text);

            return final;
        }
    }
}
