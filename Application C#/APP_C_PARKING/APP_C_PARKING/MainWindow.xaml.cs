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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Add_user add = new Add_user();
            add.ShowDialog();
        }

        //button delete 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Delete_user delete = new Delete_user();
            delete.ShowDialog();
        }

        //button refresh
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
