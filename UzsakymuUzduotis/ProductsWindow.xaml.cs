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
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        public static DataGrid datagrid;
        public ProductsWindow()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            customGrid.ItemsSource = dataB.products.ToList();
            datagrid = customGrid;
        }

        private void backToOrders(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }

        private void switchtoClients(object sender, RoutedEventArgs e)
        {
            ClientWindow CW = new ClientWindow();
            CW.Show();
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addProductWindow addPage = new addProductWindow();
            addPage.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int ide = (customGrid.SelectedItem as product).idProduct;
            string titl = (customGrid.SelectedItem as product).productTitle;
            double prc = (customGrid.SelectedItem as product).productPrice;
            if (checkIfProductIsInOrder(titl))
            {
                MessageBox.Show("Ši prekė yra įdetą į užsakymą, koreguoti negalima");
            }
            else
            {
                editProductWindow editPage = new editProductWindow(ide, titl, prc);
                editPage.ShowDialog();
            }
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {

            int id = (customGrid.SelectedItem as product).idProduct;
            if(!checkIfObjectIsUsed(id))
            {
                var deleteProduct = dataB.products.Where(o => o.idProduct == id).Single();
                dataB.products.Remove(deleteProduct);
                dataB.SaveChanges();
                customGrid.ItemsSource = dataB.products.ToList();
            }
            else MessageBox.Show("Ši prekė yra užsakyme, pašalinti negalima");
        }

        private bool checkIfObjectIsUsed(int ide)
        {
            for (int i = 0; i < dataB.orders.ToList().Count; i++)
            {
                if (dataB.products.Where(o => o.idProduct == ide).Single().productTitle == dataB.orders.ToList()[i].orderProduct) return true;
            }
            return false;
        }

        private bool checkIfProductIsInOrder(string checkingName)
        {
            for (int i = 0; i < dataB.orders.ToList().Count; i++)
            {
                if (checkingName == dataB.orders.ToList()[i].orderProduct) return true;
            }
            return false;
        }
    }
}
