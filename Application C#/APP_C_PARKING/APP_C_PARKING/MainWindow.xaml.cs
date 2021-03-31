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

/*
 *  SFL1_project  
 *  Nicolas Debras 
 * 
 */

namespace APP_C_PARKING
{
 
    public partial class MainWindow : Window
    {
        private Connexion connexion_sql;
        public MainWindow()
        {
            InitializeComponent();
            connexion_sql = new Connexion();
            Label_nom.Content = connexion_sql.recup_info_badge();
            
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            Add_user add_user = new Add_user(connexion_sql);
            add_user.ShowDialog();
            
        }
        
        //button delete 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete_user delete = new Delete_user(connexion_sql);
            delete.ShowDialog();
        }

        //button refresh
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        //button reversation
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Revervation revervation = new Revervation()
        }
    }
}
