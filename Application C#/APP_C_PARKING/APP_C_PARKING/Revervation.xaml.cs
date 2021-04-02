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

namespace APP_C_PARKING
{
    /// <summary>
    /// Logique d'interaction pour Revervation.xaml
    /// </summary>
    public partial class Revervation : Window
    {
        private Connexion connexion;
        public Revervation(Connexion connexion)
        {
            InitializeComponent();
            this.connexion = connexion;

            List<string> list_user = connexion.getListUser();

            for(int i = 0; list_user.Count != i; i++)
            {
                ComboBox_user.Items.Add(list_user[i]);
            }
        }

        private void Button_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_ok_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_user.Text != "" && DateP.SelectedDate.Value != ) {
                connexion.reservation(DateP.SelectedDate.Value, ComboBox_user.Text);
                this.Close();
            }
            else 
                System.Windows.Forms.MessageBox.Show("JE SUIS PIXEL");
        }
    }
}
