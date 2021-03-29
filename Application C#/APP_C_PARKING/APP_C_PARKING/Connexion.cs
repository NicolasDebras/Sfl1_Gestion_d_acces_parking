﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace APP_C_PARKING
{

    public class Connexion
    {
        private MySqlConnection connexion;
        private string user;
        private  List<string> list_badge;

        public Connexion()
        {

            String connexionString = "SERVER=127.0.0.1; DATABASE=parking; UID=root; PASSWORD="; ;

            this.connexion = new MySqlConnection(connexionString);
            this.connexion.Open();
            list_badge = new List<string>();

        }
        public string recup_info_badge()
        {
            string name;
            string prenom;
 
            string query = "SELECT * FROM `utilisateur`";
            MySqlCommand cmd = new MySqlCommand(query, connexion);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            int i = 0;

            while (dataReader.Read()) {

                name = dataReader["nom"] + "";
                prenom = dataReader["prenom"] + "";
                list_badge.Add(dataReader["badge"] + "");
                user = name + "                     " + prenom + "                 " + list_badge[i];
                i = i + 1;
            }
            dataReader.Close();
           
            return user;
        }
        public void add_user(string nom, string prenom, string badge, string role)
        {
            try
            {
                string query = "INSERT INTO `utilisateur` (nom, prenom, badge, role, etat) VALUES (@nom, @prenom, @badge, @role, @etat)";

                MySqlCommand cmd = new MySqlCommand(query, connexion);

                cmd.Parameters.Add("@nom", MySqlDbType.String).Value = nom;
                cmd.Parameters.Add("@prenom", MySqlDbType.String).Value = prenom;
                cmd.Parameters.Add("@badge", MySqlDbType.String).Value = badge;
                //a modifier 
                role = "users";
                cmd.Parameters.Add("@role", MySqlDbType.String).Value = role;
                cmd.Parameters.Add("@etat", MySqlDbType.VarChar).Value = 1;

                int test = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }
        public bool verification(string check)
        {
            int i = 0;

            while (list_badge.Count != i)
            {
                if (check == list_badge[i])
                    return false;
                i = i + 1;
            }
            return true;
        }
    }
}