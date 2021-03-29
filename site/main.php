<?php
?>
<!DOCTYPE html>
<?php
$PARAM_hote='localhost'; // le chemin vers le serveur
$PARAM_port='3306';
$PARAM_nom_bd='parking'; // le nom de votre base de données
$PARAM_utilisateur='administrateur_parking'; // nom d'utilisateur pour se connecter
$PARAM_mot_passe='Nantes44'; // mot de passe de l'utilisateur pour se connecter
$connexion = new PDO('mysql:host='.$PARAM_hote.';port='.$PARAM_port.';dbname='.$PARAM_nom_bd, $PARAM_utilisateur, $PARAM_mot_passe);
?>
<html>
    
    <title> occupation parking </title>
    <link rel="stylesheet" href="css_parking.css"/>
    
    <header class="page-header"> 
        
            <meta charset="utf-8" />
           
            
            <h1> OCCUPATION PARKING ST FELIX </h1> <img src="image\logo_st_felix.png" width="190px"  />
            
        
        
        
              
    </header>
    <body>
            <main>
               
            
            <?php
            echo date('d/m/Y h:i:s'); 
            $resultats = $connexion->query("SELECT * FROM place ");
            $resultats->setFetchMode(PDO::FETCH_OBJ);

           $a = 0;
            
            for($i = 0; $i <=5; $i ++)
            {
                $place = $resultats->fetch();
                $id = $place->id;
                $etat = $place->etat;
                
                
                if($etat == 1)
                {
                    $a = $a + 1;
                }
                
                

            }
            $diff = 6 - $a;
            
            if($diff > 0)
            {
                 echo '<p class="texte-centre">place dispo : ' . $diff . '</p>';
                 ?><img src="image\rond_vert.png"/><?php
            }
            else
            {
                echo '<p> parking pleins </p>';
                echo '<p class="texte-centre">place dispo : ' . $diff . '</p>';
                ?><img src="image\rond_rouge.png"/><?php
            }
            ?>

        </main>
        
    </body>
        
</html>