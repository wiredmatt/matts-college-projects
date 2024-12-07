// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
db = conn.getDB("CatHotel");

// E. Listar nombre de propietario y cantidad de reservas con fecha de inicio en julio 2024, para los
// propietarios que tengan más de una reserva en ese mes
const resultado = db.reservas.aggregate([
  // filtrar por el mes de julio
  {
    $match: {
      reservaFechaInicio: {
        $gte: "2024-07-01",
        $lte: "2024-07-31",
      },
    },
  },
  // agrupar por .propietario
  {
    $group: {
      _id: "$propietario", // agrupar con el objeto entero para evitar prod. cartesiano asociado
      cantidadReservas: { $sum: 1 }, // por cada reserva en el grupo, sumar 1
    },
  },
  // filtrar por aquellos grupos con mas de 1 reserva
  {
    $match: {
      cantidadReservas: { $gt: 1 },
    },
  },
  {
    // 0: no mostrar/no aplicar, 1: si mostrar/ si aplicar
    $project: {
      _id: 0, // para suprimir el subdocumento `_id`
      documento: "$_id.propietarioDocumento", // mappear a propiedades de primer nivel
      nombrePropietario: "$_id.propietarioNombre", // mappear a propiedades de primer nivel
      cantidadReservas: 1, // para poder mostrar cantidadReservas.
    },
  },
]);

printjson(resultado);
