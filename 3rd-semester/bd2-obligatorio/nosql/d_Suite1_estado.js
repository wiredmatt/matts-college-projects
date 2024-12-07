// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
db = conn.getDB("CatHotel");

// D. Actualizar el estado de la habitación "Suite1" asegurándose que está en estado "DISPONIBLE"
// y pasándolo a estado "LLENA"
const resultado = db.habitaciones.updateOne(
  { habitacionNombre: "Suite1", habitacionEstado: "DISPONIBLE" },
  { $set: { habitacionEstado: "LLENA" } }
);

printjson(resultado);
