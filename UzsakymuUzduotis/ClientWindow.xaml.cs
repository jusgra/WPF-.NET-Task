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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        public static DataGrid datagrid;

        public ClientWindow()
        {
            InitializeComponent();
            Load();
        }
        private void Load()
        {
            customGrid.ItemsSource = dataB.clients.ToList();
            datagrid = customGrid;
        }

        private void backToOrders(object sender, RoutedEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }

        private void backtoProducts(object sender, RoutedEventArgs e)
        {
            ProductsWindow PW = new ProductsWindow();
            PW.Show();
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addClientWindow addPage = new addClientWindow();
            addPage.ShowDialog();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int ide = (customGrid.SelectedItem as client).idClient;
            string nm = (customGrid.SelectedItem as client).clientName;
            if (checkIfClientIsInOrder(nm))
            {
                MessageBox.Show("Šis klientas turi užsakymų, koreguoti kliento negalima");
            }
            else
            {
                editClientWindow editPage = new editClientWindow(ide, nm);
                editPage.ShowDialog();
            }
            
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            int id = (customGrid.SelectedItem as client).idClient;
            if (!checkIfObjectIsUsed(id))
            {
                var deleteCleint = dataB.clients.Where(o => o.idClient == id).Single();
                dataB.clients.Remove(deleteCleint);
                dataB.SaveChanges();
                customGrid.ItemsSource = dataB.clients.ToList();
            }
            else MessageBox.Show("Šis klientas turi užsakymą, pašalinti negalima");

        }

        private bool checkIfObjectIsUsed(int ide)
        {
            for (int i = 0; i < dataB.orders.ToList().Count; i++)
            {
                if (dataB.clients.Where(o => o.idClient == ide).Single().clientName == dataB.orders.ToList()[i].orderClient) return true;
            }
            return false;
        }

        private bool checkIfClientIsInOrder(string checkingName)
        {
            for (int i = 0; i < dataB.orders.ToList().Count; i++)
            {
                if (checkingName == dataB.orders.ToList()[i].orderClient) return true;
            }
            return false;
        }
    }
}
