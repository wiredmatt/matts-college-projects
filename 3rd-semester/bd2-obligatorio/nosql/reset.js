// conn = Mongo("mongodb://<username>:<password>@localhost:27017/<authDB>");
conn = Mongo("mongodb://root:example@localhost/admin");
conn.getDB("CatHotel").dropDatabase();
