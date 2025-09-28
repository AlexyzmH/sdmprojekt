using System;
using System.Data.SqlClient;
using System.IO;
using DotNetEnv;

namespace Fuhrparkverwaltung
{
    internal static class SqlManager
    {
        private static readonly string connectionString;

        static SqlManager()
        {
            // Pfad zur .env-Datei
            /*
             * Beispiel-Inhalt der .env-Datei:

                DB_SERVER=""
                DB_PORT=""
                DB_NAME=""
                DB_USER=""
                DB_PASSWORD=""

             */


            string envPath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\.env");
            string fullEnvPath = Path.GetFullPath(envPath);

            if (File.Exists(fullEnvPath))

                //.env credentials laden:
            {
                Env.Load(fullEnvPath);

                var server = Env.GetString("DB_SERVER");
                var port = Env.GetString("DB_PORT");
                var dbName = Env.GetString("DB_NAME");
                var trustedConnection = Env.GetString("DB_TRUSTED_CONNECTION");

                // Verbindung mit der Datenbank erstellen:

                if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(port) &&
                    !string.IsNullOrEmpty(dbName))
                {
                    if (!string.IsNullOrEmpty(trustedConnection) && trustedConnection.ToLower() == "true")
                    {
                        connectionString = $"Server={server},{port};Database={dbName};Trusted_Connection=True;";
                    }
                    else
                    {
                        var user = Env.GetString("DB_USER");
                        var password = Env.GetString("DB_PASSWORD");

                        if (!string.IsNullOrEmpty(user))
                        {
                            connectionString = $"Server={server},{port};Database={dbName};User Id={user};Password={password};";
                        }
                    }
                }
            }
        }


        //Methode, um ein Fahrzeug in der Datenbank hinzuzufügen:

        public static void InsertFahrzeug(Fahrzeug fahrzeug)
        {
            //Verbindung mit Microsoft SQL Server Management Studio überprüfen:

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Keine Verbindung vorhanden.");
                return;
            }

            //SQL insert statement:

            string insertFahrzeugQuery = @"INSERT INTO Fahrzeuge (Kennzeichen, Hersteller, Modell, Baujahr) VALUES (@Kennzeichen, @Hersteller, @Modell, @Baujahr)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand command = new SqlCommand(insertFahrzeugQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Kennzeichen", fahrzeug.GetKennzeichen());
                        command.Parameters.AddWithValue("@Hersteller", fahrzeug.GetHersteller());
                        command.Parameters.AddWithValue("@Modell", fahrzeug.GetModell());
                        command.Parameters.AddWithValue("@Baujahr", fahrzeug.GetBaujahr());

                        command.ExecuteNonQuery();
                    }

                    // Insert statement für PKW-Spalten:  

                    if (fahrzeug is PKW pkw)
                    {
                        string insertPKWQuery = @"INSERT INTO PKW (Kennzeichen, AnzahlTueren) VALUES (@Kennzeichen, @AnzahlTueren)";

                        using (SqlCommand command = new SqlCommand(insertPKWQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Kennzeichen", pkw.GetKennzeichen());
                            command.Parameters.AddWithValue("@AnzahlTueren", pkw.GetAnzahlTueren());

                            command.ExecuteNonQuery();
                        }
                    }

                    // Insert statement für LKW-Spalten:

                    else if (fahrzeug is LKW lkw)
                    {
                        string insertLKWQuery = @"INSERT INTO LKW (Kennzeichen, Ladekapazitaet) VALUES (@Kennzeichen, @Ladekapazitaet)";

                        using (SqlCommand command = new SqlCommand(insertLKWQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Kennzeichen", lkw.GetKennzeichen());
                            command.Parameters.AddWithValue("@Ladekapazitaet", lkw.GetLadekapazitaet());

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Fahrzeug erfolgreich hinzugefügt.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Fehler beim Einfügen des Fahrzeugs: " + ex.Message);
                }
            }
        }


        //Methode, um ein Fahrzeug aus der Datenbank abzufragen:

        public static Fahrzeug GetFahrzeugByKennzeichen(string kennzeichen)
        {
            //Verbindung mit Microsoft SQL Server Management Studio überprüfen:

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Keine Verbindung vorhanden.");
                return null;
            }

            // SQL statement, um ein Fahrzeug aus der Datenbank abzufragen:

            string selectFahrzeugQuery = @"SELECT Kennzeichen, Hersteller, Modell, Baujahr FROM Fahrzeuge WHERE Kennzeichen = @Kennzeichen";

            // SQL statement, um die Daten von einem PKW abzufragen:

            string selectPKWQuery = @"SELECT AnzahlTueren FROM PKW WHERE Kennzeichen = @Kennzeichen";

            // SQL statement, um die Daten von einem LKW abzufragen:

            string selectLKWQuery = @"SELECT Ladekapazitaet FROM LKW WHERE Kennzeichen = @Kennzeichen";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Fahrzeugdaten abfragen:

                    using (SqlCommand cmdFahrzeug = new SqlCommand(selectFahrzeugQuery, connection))
                    {
                        cmdFahrzeug.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        using (SqlDataReader reader = cmdFahrzeug.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                Console.WriteLine("Kein Fahrzeug mit diesem Kennzeichen gefunden.");
                                return null;
                            }

                            // Fahrzeugdaten lesen:

                            string kfzKennzeichen = reader.GetString(0);
                            string hersteller = reader.GetString(1);
                            string modell = reader.GetString(2);
                            int baujahr = reader.GetInt32(3);

                            reader.Close();

                            // PKW Daten lesen:

                            using (SqlCommand cmdPKW = new SqlCommand(selectPKWQuery, connection))
                            {
                                cmdPKW.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                                object pkwResult = cmdPKW.ExecuteScalar();

                                if (pkwResult != null)
                                {
                                    int anzahlTueren = Convert.ToInt32(pkwResult);
                                    return new PKW(kfzKennzeichen, hersteller, modell, baujahr, anzahlTueren);
                                }
                            }

                            // LKW Daten lesen:

                            using (SqlCommand cmdLKW = new SqlCommand(selectLKWQuery, connection))
                            {
                                cmdLKW.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                                object lkwResult = cmdLKW.ExecuteScalar();

                                if (lkwResult != null)
                                {
                                    double ladekapazitaet = Convert.ToDouble(lkwResult);
                                    return new LKW(kfzKennzeichen, hersteller, modell, baujahr, ladekapazitaet);
                                }
                            }

                            return new Fahrzeug(kfzKennzeichen, hersteller, modell, baujahr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler beim Abrufen des Fahrzeugs: " + ex.Message);
                    return null;
                }
            }
        }



        //Methode, um ein Fahrzeug aus der Datenbank zu löschen:

        public static void DeleteFahrzeugByKennzeichen(string kennzeichen)
        {
            //Verbindung mit Microsoft SQL Server Management Studio überprüfen:

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Keine Verbindung vorhanden.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // PKW löschen:

                    string deletePKW = "DELETE FROM PKW WHERE Kennzeichen = @Kennzeichen";
                    using (SqlCommand cmd = new SqlCommand(deletePKW, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        cmd.ExecuteNonQuery();
                    }

                    // LKW löschen:

                    string deleteLKW = "DELETE FROM LKW WHERE Kennzeichen = @Kennzeichen";
                    using (SqlCommand cmd = new SqlCommand(deleteLKW, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        cmd.ExecuteNonQuery();
                    }

                    // Fahrzeug löschen:

                    string deleteFahrzeug = "DELETE FROM Fahrzeuge WHERE Kennzeichen = @Kennzeichen";
                    using (SqlCommand cmd = new SqlCommand(deleteFahrzeug, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            Console.WriteLine("Kein Fahrzeug mit diesem Kennzeichen gefunden.");
                            transaction.Rollback();
                            return;
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Fahrzeug erfolgreich gelöscht.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Fehler beim Löschen des Fahrzeugs: " + ex.Message);
                }
            }
        }



        //Methode, um die Daten eines Fahrzeugs zu ändern:

        public static void UpdateFahrzeug(Fahrzeug fahrzeug)
        {
            //Verbindung mit Microsoft SQL Server Management Studio überprüfen:

            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Keine Verbindung vorhanden.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Update Fahrzeuge Tabelle:
                    string updateFahrzeugQuery = @"
                UPDATE Fahrzeuge
                SET Hersteller = @Hersteller,
                    Modell = @Modell,
                    Baujahr = @Baujahr
                WHERE Kennzeichen = @Kennzeichen";

                    using (SqlCommand cmd = new SqlCommand(updateFahrzeugQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", fahrzeug.GetKennzeichen());
                        cmd.Parameters.AddWithValue("@Hersteller", fahrzeug.GetHersteller());
                        cmd.Parameters.AddWithValue("@Modell", fahrzeug.GetModell());
                        cmd.Parameters.AddWithValue("@Baujahr", fahrzeug.GetBaujahr());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows == 0)
                        {
                            Console.WriteLine("Kein Fahrzeug mit diesem Kennzeichen gefunden.");
                            transaction.Rollback();
                            return;
                        }
                    }

                    if (fahrzeug is PKW pkw)
                    {
                        // Update PKW Tabelle:

                        string updatePKW = @"
                    UPDATE PKW
                    SET AnzahlTueren = @AnzahlTueren
                    WHERE Kennzeichen = @Kennzeichen";

                        using (SqlCommand cmd = new SqlCommand(updatePKW, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Kennzeichen", pkw.GetKennzeichen());
                            cmd.Parameters.AddWithValue("@AnzahlTueren", pkw.GetAnzahlTueren());

                            int rows = cmd.ExecuteNonQuery();
                            if (rows == 0)
                                Console.WriteLine("Kein PKW-Datensatz gefunden, bitte Fahrzeug neu anlegen.");
                        }
                    }
                    else if (fahrzeug is LKW lkw)
                    {
                        // Update LKW Tabelle:

                        string updateLKW = @"
                    UPDATE LKW
                    SET Ladekapazitaet = @Ladekapazitaet
                    WHERE Kennzeichen = @Kennzeichen";

                        using (SqlCommand cmd = new SqlCommand(updateLKW, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Kennzeichen", lkw.GetKennzeichen());
                            cmd.Parameters.AddWithValue("@Ladekapazitaet", lkw.GetLadekapazitaet());

                            int rows = cmd.ExecuteNonQuery();
                            if (rows == 0)
                                Console.WriteLine("Kein LKW-Datensatz gefunden, bitte Fahrzeug neu anlegen.");
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Fahrzeug erfolgreich aktualisiert.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Fehler beim Aktualisieren des Fahrzeugs: " + ex.Message);
                }
            }
        }

    }
}
