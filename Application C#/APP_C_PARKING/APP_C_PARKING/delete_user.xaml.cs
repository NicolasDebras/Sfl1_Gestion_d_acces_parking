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
        string frist_name;
        //prénom de famille
        string name;
        public Delete_user()
        {
            InitializeComponent();
        }

        //cancel
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            frist_name = Txt_first_name.Text;
            name = Txt_name.Text;   
            this.Close();
        }
    }
}
