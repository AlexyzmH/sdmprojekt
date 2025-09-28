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
            string envPath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\.env");
            string fullEnvPath = Path.GetFullPath(envPath);

            if (File.Exists(fullEnvPath))
            {
                Env.Load(fullEnvPath);

                var server = Env.GetString("DB_SERVER");
                var port = Env.GetString("DB_PORT");
                var dbName = Env.GetString("DB_NAME");
                var user = Env.GetString("DB_USER");
                var password = Env.GetString("DB_PASSWORD");

                if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(port) &&
                    !string.IsNullOrEmpty(dbName) && !string.IsNullOrEmpty(user))
                {
                    connectionString = $"Server={server},{port};Database={dbName};User Id={user};Password={password};";
                }
            }
        }

        public static void InsertFahrzeug(Fahrzeug fahrzeug)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is empty. Cannot insert Fahrzeug.");
                return;
            }

            string insertFahrzeugQuery = @"
        INSERT INTO Fahrzeuge (Kennzeichen, Hersteller, Modell, Baujahr)
        VALUES (@Kennzeichen, @Hersteller, @Modell, @Baujahr)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Start transaction
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

                    // Insert into PKW or LKW table if applicable
                    if (fahrzeug is PKW pkw)
                    {
                        string insertPKWQuery = @"
                    INSERT INTO PKW (Kennzeichen, AnzahlTueren)
                    VALUES (@Kennzeichen, @AnzahlTueren)";

                        using (SqlCommand command = new SqlCommand(insertPKWQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Kennzeichen", pkw.GetKennzeichen());
                            command.Parameters.AddWithValue("@AnzahlTueren", pkw.GetAnzahlTueren());

                            command.ExecuteNonQuery();
                        }
                    }
                    else if (fahrzeug is LKW lkw)
                    {
                        string insertLKWQuery = @"
                    INSERT INTO LKW (Kennzeichen, Ladekapazitaet)
                    VALUES (@Kennzeichen, @Ladekapazitaet)";

                        using (SqlCommand command = new SqlCommand(insertLKWQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Kennzeichen", lkw.GetKennzeichen());
                            command.Parameters.AddWithValue("@Ladekapazitaet", lkw.GetLadekapazitaet());

                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Fahrzeug erfolgreich in die Datenbank eingefügt.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Fehler beim Einfügen des Fahrzeugs: " + ex.Message);
                }
            }
        }

        public static Fahrzeug GetFahrzeugByKennzeichen(string kennzeichen)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is empty. Cannot read Fahrzeug.");
                return null;
            }

            // Query to get base Fahrzeug data
            string selectFahrzeugQuery = @"SELECT Kennzeichen, Hersteller, Modell, Baujahr FROM Fahrzeuge WHERE Kennzeichen = @Kennzeichen";

            // Queries to check PKW and LKW specific data
            string selectPKWQuery = @"SELECT AnzahlTueren FROM PKW WHERE Kennzeichen = @Kennzeichen";

            string selectLKWQuery = @"SELECT Ladekapazitaet FROM LKW WHERE Kennzeichen = @Kennzeichen";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Get base Fahrzeug data
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

                            // Read base Fahrzeug data
                            string kfzKennzeichen = reader.GetString(0);
                            string hersteller = reader.GetString(1);
                            string modell = reader.GetString(2);
                            int baujahr = reader.GetInt32(3);

                            reader.Close();

                            // Now check if it's a PKW
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

                            // Check if it's a LKW
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

                            // If neither PKW nor LKW, return base Fahrzeug
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



        public static void DeleteFahrzeugByKennzeichen(string kennzeichen)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is empty. Cannot delete Fahrzeug.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Delete from PKW if exists
                    string deletePKW = "DELETE FROM PKW WHERE Kennzeichen = @Kennzeichen";
                    using (SqlCommand cmd = new SqlCommand(deletePKW, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        cmd.ExecuteNonQuery();
                    }

                    // Delete from LKW if exists
                    string deleteLKW = "DELETE FROM LKW WHERE Kennzeichen = @Kennzeichen";
                    using (SqlCommand cmd = new SqlCommand(deleteLKW, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Kennzeichen", kennzeichen);
                        cmd.ExecuteNonQuery();
                    }

                    // Delete from Fahrzeuge
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



        public static void UpdateFahrzeug(Fahrzeug fahrzeug)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("Connection string is empty. Cannot update Fahrzeug.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Update Fahrzeuge base table
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
                        // Update PKW
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
                        // Update LKW
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
                    // else do nothing for base Fahrzeug - no extra tables

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
