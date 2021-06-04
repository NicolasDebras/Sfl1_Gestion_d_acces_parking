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
    
    public partial class ListUser : Window
    {
        private List<string> list_user;
        Connexion connexion;

        public ListUser(Connexion connexion)
        {
            this.connexion = connexion;

            InitializeComponent();
            add_label();
        }

        public void add_label()
        {
            int i = 0;

            list_user = connexion.getListUser();

            while (list_user.Count != i)
            {
                Label_list.Content = Label_list.Content + "\n" + list_user[i];
                i = i + 1;
            }
            
        }
    }
}
