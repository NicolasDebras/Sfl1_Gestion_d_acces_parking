var express = require('express'); // Web Framework
var app = express();
//var sql = require('mssql');

var sqlConfig = {
    user: 'TestLora',
    password: 'Password1',
    server: 'localhost',
    database: 'testlora'
	};

app.use(express.json());

app.post('/', function(request, response){
	console.log(request.body);
	console.log("--------------------------");
	
	/* var Id = request.body.DevAddr
	var Stat = request.body.data.status;
	console.log("Recu");
	if (request.body.data.frameType == 'status'){
	console.log(Id + '  ' + Stat);
	} */
	response.send("received");
});

//connexion serveur
app.listen(11300,'0.0.0.0' || 'localhost' || '10.16.2.219');