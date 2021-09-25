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
    /// Interaction logic for editClientWindow.xaml
    /// </summary>
    public partial class editClientWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        int sid;
        public editClientWindow(int id, string currentName)
        {
            InitializeComponent();
            sid = id;
            cName.Text = currentName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cName.Text == "" || int.TryParse(cName.Text, out int num))
            {
                MessageBox.Show("Prašome įvesti vardą");
            }
            else if (checkIfClientNameIsTaken(cName.Text))
            {
                MessageBox.Show("Toks klientas jau egzistuoja");
            }
            else
            {
                client editedC = (from o in dataB.clients
                                  where o.idClient == sid
                                  select o).Single();
                editedC.clientName = cName.Text;
                dataB.SaveChanges();
                ClientWindow.datagrid.ItemsSource = dataB.clients.ToList();
                this.Hide();
            }
           
        }
        private bool checkIfClientNameIsTaken(string checkingName)
        {
            for (int i = 0; i < dataB.clients.ToList().Count; i++)
            {
                if (checkingName == dataB.clients.ToList()[i].clientName) return true;
            }
            return false;

        }
    }
}
