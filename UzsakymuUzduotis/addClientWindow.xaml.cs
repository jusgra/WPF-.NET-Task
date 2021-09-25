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
    /// Interaction logic for addClientWindow.xaml
    /// </summary>
    /// 
    public partial class addClientWindow : Window
    {
        tctaskEntities dataB = new tctaskEntities();
        public addClientWindow()
        {
            InitializeComponent();
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
                client newC = new client()
                {
                    clientName = cName.Text,
                };

                dataB.clients.Add(newC);
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
