<?php
?>
<!DOCTYPE html>
<?php
$PARAM_hote='10.6.0.1'; // le chemin vers le serveur 217.128.90.45
$PARAM_port='3306';
$PARAM_nom_bd='parking'; // le nom de votre base de données
$PARAM_utilisateur='parking'; // nom d'utilisateur pour se connecter
$PARAM_mot_passe='Nantes44'; // mot de passe de l'utilisateur pour se connecter
$connexion = new PDO('mysql:host='.$PARAM_hote.';port='.$PARAM_port.';dbname='.$PARAM_nom_bd, $PARAM_utilisateur, $PARAM_mot_passe);
?>
<html>
    
    <title> occupation parking </title>
    <link rel="stylesheet" href="css_parking.css"/>
    
    <header class="page-header"> 
        
            <meta charset="utf-8" />
                     
            <h1> OCCUPATION PARKING ST-FELIX </h1> <img src="image\logo_st_felix.png" width="180px"  />
                        
    </header>
    <body>
            <main>
               
            <?php
            $resultats = $connexion->query("SELECT * FROM place ");
            $resultats->setFetchMode(PDO::FETCH_OBJ);
            date_default_timezone_set("Europe/Paris");
            $date = date("d/m/Y");
            $heure = date("H:i");

           $a = 0;
            
            for($i = 0; $i <=5; $i ++)
            {
                $place = $resultats->fetch();
                $id = $place->id;
                $etat = $place->etat;
                
                if($etat === "occupe")
                {
                    $a = $a + 1;
                }
                
            }
            $diff = 6 - $a;
            
            if($diff > 0)
            {
                ?>
                
                <?php echo '<p> Places disponibles : ' . $diff . '</br> Nous sommes le : '.$date. '</br> il est : ' .$heure. '</font> </p>' ?>
                
                <div class="position">
                <img src="image\rond_vert_1.png"/>
                </div>
                <?php
            }
            else
            {
                ?>
                
                <?php echo '<p> Parking plein </br> Nous sommes le : ' .$date. ' </br> il est : ' .$heure. '</font> </p>';?>
                
                <div class="position">
                <img src="image\rond_rouge.png"/>
                </div>    
                <?php
            }
            ?>

        </main>
        
    </body>
        
</html>


<script type="text/javascript">
  setInterval("reload();",10000); 
 
    function reload(){
        window.location = location.href;
    }
</script>
