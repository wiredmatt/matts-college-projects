// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
db = conn.getDB("CatHotel");

// C. Listar las reservas que incluyen el servicio "PELUQUERIA "
const resultado = db.reservas.find({
  "servicios.servicioNombre": "PELUQUERIA",
});

printjson(resultado);
