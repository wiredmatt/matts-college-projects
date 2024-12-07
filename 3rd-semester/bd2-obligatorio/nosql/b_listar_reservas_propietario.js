// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
db = conn.getDB("CatHotel");

// B. Listar reservas del propietario con documento "12345678"
const resultado = db.reservas.find({
  "propietario.propietarioDocumento": "12345678",
});

printjson(resultado);
