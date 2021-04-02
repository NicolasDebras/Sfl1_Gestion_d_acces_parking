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
    public partial class Delete_user : Window
    {
        //nom 
        string badge;
        private Connexion connexion;
        public Delete_user(Connexion connexion)
        {
            this.connexion = connexion;
            InitializeComponent();
        }

        //cancel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //fonction button qui correspond a "ok"
        private void add_Click(object sender, RoutedEventArgs e)
        {
            badge = Txt_first_name.Text;

            if (CheckBox_delete.IsChecked == true && CheckBox_suspension.IsChecked == true)
                System.Windows.Forms.MessageBox.Show("SELECTION ERRONE");
            else if (CheckBox_suspension.IsChecked == true) 
                connexion.suspension(badge);
            else if (CheckBox_delete.IsChecked == true)
                connexion.delete_user(badge);
            else 
                System.Windows.Forms.MessageBox.Show("AUCUNE SELECTION");

            
            this.Close();
        }

        private void CheckBox_suspension_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
