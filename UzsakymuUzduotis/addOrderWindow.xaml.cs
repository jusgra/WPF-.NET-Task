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
    /// Interaction logic for addOrderWindow.xaml
    /// </summary>
    /// 


    public partial class addOrderWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();

        public addOrderWindow()
        {
            InitializeComponent();
            orderClientCombo.ItemsSource = dataB.clients.ToList();
            orderProductCombo.ItemsSource = dataB.products.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(orderProductCombo.SelectedItem == null || orderClientCombo.SelectedItem == null)
            {
                MessageBox.Show("Prašome nepalikti tuščių laukų");
            }
            else
            {
                order newOrder = new order()
                {
                    orderProduct = (orderProductCombo.SelectedItem as product).productTitle.ToString(),
                    orderClient = (orderClientCombo.SelectedItem as client).clientName.ToString()
                };

                dataB.orders.Add(newOrder);
                dataB.SaveChanges();
                MainWindow.datagrid.ItemsSource = dataB.orders.ToList();
                this.Hide();
            }
            
        }
    }
}
