using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace FollowerBot.DataBase
{
    internal  class SQLiteScript
    {
        private readonly string connectionString = "Data Source=SurfCoffee.db";
        public List<string> Food = new List<string>();
        public void createDB()
        {
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                SqliteCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Shippers'";
                var tableName = cmd.ExecuteScalar();
                if (tableName == null)
                {
                    cmd.CommandText = "CREATE TABLE Shippers(_ShipperId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO Shippers(Name) VALUES ('ИП Судин')";
                    cmd.ExecuteNonQuery();
                }

                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Food'";
                tableName = cmd.ExecuteScalar();
                if (tableName == null)
                {
                    cmd.CommandText = "CREATE TABLE Food (" +
                                  "FoodId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                  "ShipperId INTEGER NOT NULL, " +
                                  "Name TEXT NOT NULL, " +
                                  "Description TEXT, " +
                                  "Composition TEXT, " +
                                  "Weight REAL, " +
                                  "Proteins REAL, " +
                                  "Fats REAL, " +
                                  "Carbohydrates REAL, " +
                                  "Calorie REAL)";
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "INSERT INTO Food (ShipperId, Name, Description, Composition, Weight, Proteins, Fats, Carbohydrates, Calorie) " +
                              "VALUES (@ShipperId, @Name, @Description, @Composition, @Weight, @Proteins, @Fats, @Carbohydrates, @Calorie)";
                    cmd.Parameters.AddWithValue("@ShipperId", 1);
                    cmd.Parameters.AddWithValue("@Name", "КЕШЬЮ КЕЙК СЛИВОЧНЫЙ ПЛОМБИР С ТЫКВЕННЫМИ СЕМЕЧКАМИ");
                    cmd.Parameters.AddWithValue("@Description", "Сочетание нежнейшего ванильного мусса и запеченных тыквенных семечек с морской солью будет понятной и приятной классикой для большинства ваших гостей, особенно когда в десерте нет сахара и глютена, 100% vegan");
                    cmd.Parameters.AddWithValue("@Composition", "Кешью активированный, органические кокосовые сливки, масло кокосовое первого холодного отжима, сироп топинамбура, тыквенные семечки запеченные, миндаль обжаренный, финики, ванилин, соль");
                    cmd.Parameters.AddWithValue("@Weight", 100.00);
                    cmd.Parameters.AddWithValue("@Proteins", 10.11);
                    cmd.Parameters.AddWithValue("@Fats", 36.61);
                    cmd.Parameters.AddWithValue("@Carbohydrates", 19.06);
                    cmd.Parameters.AddWithValue("@Calorie", 447.39);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO Food (ShipperId, Name, Description, Composition, Weight, Proteins, Fats, Carbohydrates, Calorie) " +
                              "VALUES (@ShipperId, @Name, @Description, @Composition, @Weight, @Proteins, @Fats, @Carbohydrates, @Calorie)";
                    cmd.Parameters.AddWithValue("@ShipperId", 2);
                    cmd.Parameters.AddWithValue("@Name", "КЕШЬЮ КЕЙК МАНГО");
                    cmd.Parameters.AddWithValue("@Description", "Легкий, свежий десерт с натуральными кусочками манго  в сочетании со сливочностью кокоса и коржа из миндаля и фиников, без сахара и глютена полностью на растительной основе с любовью о вас и планете, 100% vegan");
                    cmd.Parameters.AddWithValue("@Composition", "Манго быстрозамороженный, манго сушёный, миндаль обжаренный, финики, кешью активированный, органические кокосовые сливки, масло кокосовое первого холодного отжима, сироп топинамбура, натуральный ароматизатор манго");
                    cmd.Parameters.AddWithValue("@Weight", 108);
                    cmd.Parameters.AddWithValue("@Proteins", 7.28);
                    cmd.Parameters.AddWithValue("@Fats", 26.47);
                    cmd.Parameters.AddWithValue("@Carbohydrates", 35.59);
                    cmd.Parameters.AddWithValue("@Calorie", 404.55);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();



                    /*cmd.CommandText = "INSERT INTO Food (ShipperId, Name, Description, Composition, Weight, Proteins, Fats, Carbohydrates, Calorie) " +
                              "VALUES (@ShipperId, @Name, @Description, @Composition, @Weight, @Proteins, @Fats, @Carbohydrates, @Calorie)";
                    cmd.Parameters.AddWithValue("@ShipperId", 3);
                    cmd.Parameters.AddWithValue("@Name", "ШОКОЛАДНЫЙ КЕШЬЮ КЕЙК (with sugar)");
                    cmd.Parameters.AddWithValue("@Description", "-");
                    cmd.Parameters.AddWithValue("@Composition", "Кешью, масло какао холодного отжима органическое, сливки кокосовые органические, какао порошок, ванилин, сироп топинамбура, миндаль, финик, лесной орех, сахар тростниковый.");
                    cmd.Parameters.AddWithValue("@Weight", );
                    cmd.Parameters.AddWithValue("@Proteins", );
                    cmd.Parameters.AddWithValue("@Fats", );
                    cmd.Parameters.AddWithValue("@Carbohydrates", );
                    cmd.Parameters.AddWithValue("@Calorie", );
                    cmd.ExecuteNonQuery(); */

                    cmd.CommandText = "INSERT INTO Food (ShipperId, Name, Description, Composition, Weight, Proteins, Fats, Carbohydrates, Calorie) " +
                              "VALUES (@ShipperId, @Name, @Description, @Composition, @Weight, @Proteins, @Fats, @Carbohydrates, @Calorie)";
                    cmd.Parameters.AddWithValue("@ShipperId", 7);
                    cmd.Parameters.AddWithValue("@Name", "АРАХИСОВОЕ ПИРОЖНОЕ В ШОКОЛАДЕ");
                    cmd.Parameters.AddWithValue("@Description", "Десерт внутри которого карамель из органических кокосовых сливок с запечённым арахисом и сливочной начинкой облитая супер молочным, легким веган шоколадом оставит ещё надолго гостя под впечатлением и желанием вернуться за этим потрясающим вкусом!");
                    cmd.Parameters.AddWithValue("@Composition", "Кешью активированный, органические кокосовые сливки, масло кокосовое первого холодного отжима, сироп топинамбура, сахар, глюкоза, кешью-шоколад (масло какао, нерафинированный тростниковый сахар-сырец, какао тертое, кешью жаренный, мука из зелёной гречки, кокосовая мука, кокосовый сахар, менее 0,1 эмульгатор лецитин подсолнечный, натуральный ароматизатор ваниль, соль розовая гималайская)");
                    cmd.Parameters.AddWithValue("@Weight", 84);
                    cmd.Parameters.AddWithValue("@Proteins", 6.52);
                    cmd.Parameters.AddWithValue("@Fats", 28.03);
                    cmd.Parameters.AddWithValue("@Carbohydrates", 25.93);
                    cmd.Parameters.AddWithValue("@Calorie", 368.35);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    Console.WriteLine("Data has been inserted into the Food table");
                }

            }
            Console.Read();
        }
        public List<string> getFood(int idShipper)
        {
            using (var con = new SqliteConnection(connectionString))
            {
                con.Open();
                SqliteCommand cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM Food";
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            var shipperId = reader.GetValue(1);
                            var Name = reader.GetValue(2);
                            var Description = reader.GetValue(3);
                            var Composition = reader.GetValue(4);
                            var Weight = reader.GetValue(5);
                            var Proteins = reader.GetValue(6);
                            var Fats = reader.GetValue(7);
                            var Carbohydrates = reader.GetValue(8);
                            var Calories = reader.GetValue(9);

                            string info = $"ID: {id} || ShipperId: {shipperId} ||  Name: {Name} || " +
                                $"Description: {Description} || Composition: {Composition} || " +
                                $"Weight: {Weight} || Proteins: {Proteins} || Fats: {Fats} || " +
                                $"Carbohydrates: {Carbohydrates} || Calories: {Calories} ";
                            Food.Add(info);
                        }
                    }
                }
            }
            return Food;
        }
    }
}
//"P:\Visual Studio 2022\sours\repos\Job_projects\FollowerBot\bin\Debug\net7.0\SurfCoffee.db"