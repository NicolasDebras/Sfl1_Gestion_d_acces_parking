using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace APP_C_PARKING
{




    class Connexion
    {
        private MySqlConnection connexion;
        private string user;

        public Connexion()
        {

            String connexionString = "SERVER=127.0.0.1; DATABASE=parking; UID=root; PASSWORD="; ;



            this.connexion = new MySqlConnection(connexionString);


           
        }
        public string recup_info_badge()
        {
            string name;
            string prenom;
            string badge;

            this.connexion.Open();
            string query = "SELECT * FROM `utilisateur`";
            MySqlCommand cmd = new MySqlCommand(query, connexion);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            dataReader.Read();
            name = dataReader["nom"] + "";
            prenom = dataReader["prenom"] + "";
            badge = dataReader["badge"] + "";
            dataReader.Close();

            user = name + "                     "+ prenom + "                 " + badge;
            return user;
        }
    }
}
