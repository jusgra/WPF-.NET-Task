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

namespace UzsakymuUzduotis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        public static DataGrid datagrid;

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            customGrid.ItemsSource = dataB.orders.ToList();
            datagrid = customGrid;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addOrderWindow addPage = new addOrderWindow();
            addPage.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int ide = (customGrid.SelectedItem as order).idOrder;
            var pro = customGrid.SelectedItem;
            var sClient = customGrid.SelectedItem;

            editOrderWindow editPage = new editOrderWindow(ide, pro, sClient);
           
            editPage.ShowDialog();

        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int id = (customGrid.SelectedItem as order).idOrder;
            var deleteOrder = dataB.orders.Where(o => o.idOrder == id).Single();
            dataB.orders.Remove(deleteOrder);
            dataB.SaveChanges();
            customGrid.ItemsSource = dataB.orders.ToList();
        }

        private void switchToProducts(object sender, RoutedEventArgs e)
        {
            ProductsWindow PW = new ProductsWindow();
            PW.Show();
            this.Close();

        }

        private void switchToClients(object sender, RoutedEventArgs e)
        {
            ClientWindow CW = new ClientWindow();
            CW.Show();
            this.Close();
        }
    }
}
