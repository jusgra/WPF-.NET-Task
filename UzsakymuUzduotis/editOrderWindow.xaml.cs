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
    public partial class editOrderWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        int idet;
        public editOrderWindow(int ide, object sP, object sC)
        {
            InitializeComponent();
            orderClientCombo.ItemsSource = dataB.clients.ToList();
            orderProductCombo.ItemsSource = dataB.products.ToList();
            idet = ide;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (orderProductCombo.SelectedItem == null || orderClientCombo.SelectedItem == null)
            {
                MessageBox.Show("Prašome nepalikti tuščių laukų");
            }
            else
            {
                order editedOrder = (from o in dataB.orders
                                     where o.idOrder == idet
                                     select o).Single();
                editedOrder.orderProduct = (orderProductCombo.SelectedItem as product).productTitle.ToString();
                editedOrder.orderClient = (orderClientCombo.SelectedItem as client).clientName.ToString();
                dataB.SaveChanges();
                MainWindow.datagrid.ItemsSource = dataB.orders.ToList();
                this.Hide();
            }
        }
    }
}
