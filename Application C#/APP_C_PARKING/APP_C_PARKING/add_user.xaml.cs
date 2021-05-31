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
using MySql.Data.MySqlClient;

namespace APP_C_PARKING
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>

    

    public partial class Add_user : Window
    {

        private Connexion connexion;

        public Add_user(Connexion connexion)
        {
            InitializeComponent();
            this.connexion = connexion;
        }

        //fonction_bouton qui correspond pour ajouter un utilisateur
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nom = nom_textBox.Text;
            string prenom = TextBox_prenom.Text;
            string badge = TextBox_badge.Text;
            string role = "users";

            if (roles_CheckBox.IsChecked == true) {
                role = "admin";
            }

            if (badge != "" && connexion.verification(badge) == true) {

                connexion.add_user(nom, prenom, badge, role);
                this.Close();
            }
            else {

                System.Windows.Forms.MessageBox.Show("NUMERO DE BADGE INDENTIQUE");
            }
            
        }
        public void Set_TextBox_Badge(string badge)
        {
            TextBox_badge.Text = badge;
        }
    }
}
