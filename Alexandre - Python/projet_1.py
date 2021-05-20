from pyModbusTCP.client import ModbusClient
import time
import base64
import mysql.connector
from mysql.connector import Error
import os



#Connexion a la base de donnees.
connexion = mysql.connector.connect(
host = "10.6.0.1",
user = "parking",
password = "Nantes44",
database = "parking",
port = 3306)


place = "libre"
droit = "1"
role = "admin"

i = 3
x = 3
a = 3
b = 3

#Connexion au capteur RFID
SERVER_HOST = "10.16.37.11"
SERVER_PORT = 502

c = ModbusClient()
c.host(SERVER_HOST)
c.port(SERVER_PORT)

while True:										#Permet de lire les badges en continu


    # open or reconnect TCP to server
    if not c.is_open():
        if not c.open():
            print("unable to connect to "+SERVER_HOST+":"+str(SERVER_PORT))

    # if open() is ok, read register (modbus function 0x03)
    if c.is_open():
        # read 4 registers at address 0, store result in regs list
        regs = c.read_holding_registers(0,4)
        # if success display registers
        if regs:
	    i = 0
	    x = 0
	    a = 0
	    b = 0
	    list_ascii = []
	    print("----------------------------")
	    print("Valeur decimale du badge :\n")
            print(regs)
	    print("----------------------------\n")

	    print("----------------------------")
	    print("Valeur hexa du badge :\n")
	    for badge in regs:
        	hex(badge)								#Transforme le decimal en hexadecimal
		valeur = hex(badge)
		valeur = valeur.replace('0x', '')
      		print(valeur)
	   	hexaToAscii =  bytearray.fromhex(valeur).decode()			#Transforme l'hexadecimal en AscII
		list_ascii.append(hexaToAscii)



	    print("----------------------------\n")
	    badges = "".join(list_ascii)
	    print("----------------------------")
	    print("ID Badge : \n")
	    print(badges)								#Affichage du numero de badge
	    print("----------------------------")




	    request = "SELECT etat FROM place"							#Requete SQL pour verifier s'il y a de la place dans le parking
	    request_2 = "SELECT id_badge FROM utilisateur"					#Requete SQL pour verifier le badge dans la base de donnees
	    request_3 = "SELECT etat FROM utilisateur WHERE utilisateur.id_badge = " + badges   #Requete SQL pour verifier l'etat du badge dans la base de donees
	    request_4 = "SELECT role FROM utilisateur WHERE utilisateur.id_badge = " + badges   #Requete SQL pour verifier le role de l'utilisateur

	    curseur = connexion.cursor(dictionary=True)
	    curseur.execute(request)


	    for verif_place in curseur.fetchall():

		#Iterateur pour recuperer la valeur de la requete SQL
	    	for k, v in verif_place.items():
			if v in place:
				x = 1
	    #Il y a de la place dans la parking
	    if x == 1:


		 print("Le parking est disponible")
		 x = 2
		 curseur.execute(request_2)
	   	 for verif_badge in curseur.fetchall():

			print("----------------------------")
			print("Requete SQL - verif badge :\n")
	    		print(verif_badge)
			print("----------------------------")

			#Iterateur pour recuperer la valeur de la requete SQL
	    		for k, v in verif_badge.items():						#Iterateur permettant de recuperer seulement la valeur et non la cle de la requete SQL
				if v in badges:

					i = 1

		 #Le badge fait partie de l'etablissement
	         if i == 1:

			curseur.execute(request_3)
			for verif_droit in curseur.fetchall():

			    #Iterateur pour recuperer la valeur de la requete SQL
			    for k, v in verif_droit.items():

				if str(v) in droit:

				    a = 1

			#L'utilisateur est autorise
			if a == 1:
			    print("Vous faites parti de l'etablissement et vous etes autorise a entrer")
			    #Ouvrir le script bash
			    os.system('sh /home/monScript.sh')
			    a = 2


			#L'utilisateur fait partie de l'etablissement mais ses droits sont suspendus
			elif a == 0:

			    print("Vous faites partie de l'etablissement mais vous avez ete suspendu.")


		 #Le badge ne fait pas partie de l'etablissement
		 elif i == 0:

			print("Vous ne faites pas partie de l'etablissement")
			i = 2


	    #L'utilisateur admin peut entrer dans un parking complet
	    elif x == 0:
			curseur.execute(request_4)
			for verif_role in curseur.fetchall():
			      for k, v in verif_role.items():
				    if v in role:
					  print(v)
					  b = 1
			#L'utilisateur admin peut entrer dans un parking complet
			if b == 1:

			      print("Le parking est complet mais vous etes admin")
			      os.system('sh /home/monScript.sh')
			      b = 2
			#L'utilisateur non admin ne peut pas entrer
			elif b == 0:

			      print("Le parking est complet.")
			      b = 2
    #Pause de deux secondes entre chaque passage de badge
    time.sleep(2)
