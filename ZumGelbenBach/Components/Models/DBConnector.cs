using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Data;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Diagnostics; // Für Batteries.Init()

namespace ZumGelbenBach
{
    internal class DBConnector
    {
        SqliteConnection conn;
        String dbPath;

        public DBConnector() 
        {
            // Initialisiere SQLitePCL+
            Batteries.Init();

            string binDirectory = Environment.CurrentDirectory;
            dbPath =(binDirectory)+ "\\wwwroot\\Datenbank\\Produktdb.db";
            
            conn = OpenConnection(dbPath);           
        }

        private SqliteConnection OpenConnection(string dbPath)
        {
            SqliteConnection conn;
            
            String connString = String.Format("Data Source={0}", dbPath);
            conn = new SqliteConnection(connString);

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conn;
        }

        public void CloseConnection(SqliteConnection conn)
        {
            conn.Close();
        }

        public SqliteDataReader ReadData(String table, String[] columns)
        {
            SqliteCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            String columnsString;

            if (columns.Length > 1)
            {
                columnsString = String.Join(", ", columns);
            }
            else
            {
                columnsString = columns[0];
            }

            String commString = String.Format(@$"Select {columnsString} FROM {table}");
            
            cmd.CommandText = commString;
            //cmd.ExecuteReader();
            return cmd.ExecuteReader();
        }

        
        


        public SqliteDataReader ReadDataWhere(String table, String[] columns, String[] where, String[] title)
        {
            if (where.Length > 0)
            {

                SqliteCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                String columnsString = "";

                if (columns.Length > 1)
                {
                    for(int i = 0; i < columns.Length; i++)
                        columnsString = columnsString + ", " + columns[i] + " as '" + title[i] + "'";
                    columnsString = columnsString.Substring(2);
                }
                else
                {
                    columnsString = columns[0];
                }

                String whereString = "";

                for(int i = 0; i < where.Length; i++)
                {
                    whereString = String.Join(" AND ", where[i] );
                }


                String commString = String.Format("Select {0} FROM {1} WHERE {2}", columnsString, table, whereString);

                cmd.CommandText = commString;
                //cmd.ExecuteReader();
                return cmd.ExecuteReader();

            }
            else
            {
                return ReadData(table, columns);
            }
        }



        public SqliteDataReader ReadMenuWithImages(String table, String[] columns, string tableIdColumn, String joinTable, String joinTableIdColumn)
        {
            SqliteCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            String columnsString;

            if (columns.Length > 1)
            {
                columnsString = String.Join(", ", columns);
            }
            else
            {
                columnsString = columns[0];
            }

            String commString = String.Format(@$"Select {columnsString} FROM {table} left join {joinTable} on {table}.{tableIdColumn} = {joinTable}.{joinTableIdColumn}");

            cmd.CommandText = commString;
            //cmd.ExecuteReader();
            return cmd.ExecuteReader();
        }

        public SqliteDataReader ReadOrders(String table, String[] columns)
        {
            SqliteCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            String columnsString;

            if (columns.Length > 1)
            {
                columnsString = String.Join(", ", columns);
            }
            else
            {
                columnsString = columns[0];
            }

            String commString = String.Format(@$"Select {columnsString} FROM {table}");

            cmd.CommandText = commString;
            //cmd.ExecuteReader();
            return cmd.ExecuteReader();
        }


        /// <summary>
        /// Ausführen eines SQL-Befehls 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns>Falls select als befehlt ausgeführt werden soll dann wird ein reader returned </returns>
        public SqliteDataReader ExecuteCommand(string sqlCommand)
        {
            try
            {
                using (SqliteCommand cmd = new SqliteCommand(sqlCommand, conn))
                {
                    if (sqlCommand.Trim().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                    {
                        return cmd.ExecuteReader();
                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler in Methode: {new StackTrace().GetFrame(0).GetMethod().Name}" + " Fehlermeldung: {ex.Message}");
                return null;
            }
        }



        //INSERET      
        public async Task SaveDataAsync(string table, string[] columns, List<string> values, List<int> type)
        {
            // INT von Dictionary:
            // 1 == String
            // 2 == INT
            // 3 == numeric

            SqliteCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;

            // columns
            string columnString;

            if (columns.Length > 1)
                columnString = string.Join(", ", columns);
            else
                columnString = columns[0];

            string valueString = "";
            // values
            if (values.Count > 1)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    if (type[i] == 1)
                        valueString += $"'{values[i]}', ";
                    else if (type[i] == 2)
                        valueString += $"{values[i]}, ";
                    else if (type[i] == 3)
                        valueString += values[i].Contains(",") ? $"{values[i].Replace(",", ".")}, " : $"{values[i]}, ";
                }

                valueString = valueString.Substring(0, valueString.Length - 2);
            }
            else
            {
                if (type[0] == 1)
                    valueString = $"'{values[0]}'";
                else if (type[0] == 2)
                    valueString = values[0];
                else if (type[0] == 3)
                    valueString = values[0].Contains(",") ? values[0].Replace(",", ".") : values[0];
            }

            valueString = $"({valueString})";

            System.Diagnostics.Debug.WriteLine(valueString);

            string commString = $"INSERT INTO {table}({columnString}) VALUES {valueString}";

            

            try
            {
                cmd.CommandText = commString;
                await cmd.ExecuteNonQueryAsync();
            }
            catch (SqliteException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine(commString);
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                Console.WriteLine($"SQL State: {sqlEx.SqliteErrorCode}");
                Console.WriteLine($"SQL Command: {sqlEx.BatchCommand}");
            }
        }



        /// <summary>
        ///     Aktualisiert Daten in einer angegebenen Tabelle.
        ///     Aufruf:UpdateData("MeineTabelle", new string[] { "Spalte1", "Spalte2" }, new List<object> { newValue1, newValue2}, "ID", 123);
        /// </summary>
        /// <typeparam name="T">Der Typ der Werte, die aktualisiert werden sollen.</typeparam>
        /// <param name="table">Der Name der Tabelle, die aktualisiert werden soll.</param>
        /// <param name="columns">Ein Array von Spaltennamen, die aktualisiert werden sollen.</param>
        /// <param name="values">Eine Liste von Werten, die in den Spalten aktualisiert werden sollen.</param>
        /// <param name="conditionColumn">Die Spalte, die als Bedingung für die Aktualisierung verwendet wird.</param>
        /// <param name="conditionValue">Der Wert, der die Bedingung für die Aktualisierung darstellt.</param>
        /// </summary>
        public void UpdateData<T>(string table, string[] columns, List<T> values, string conditionColumn, T conditionValue)
        {
            try
            {
                // Erstellen Sie den SQL-Befehl für das Update
                string setClause = string.Join(", ", columns.Select((c, i) => $"{c} = @{c}"));
                string commandText = $"UPDATE {table} SET {setClause} WHERE {conditionColumn} = @{conditionColumn}";

                // Verwenden Sie using, um sicherzustellen, dass die Verbindung ordnungsgemäß freigegeben wird
                using (SqliteCommand cmd = new SqliteCommand(commandText, conn))
                {
                    // Iterieren Sie durch jede Spalte und fügen Sie den entsprechenden Wert als Parameter hinzu
                    for (int i = 0; i < columns.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@" + columns[i], values[i]);
                    }

                    // Fügen Sie den Bedingungswert als Parameter hinzu
                    cmd.Parameters.AddWithValue("@" + conditionColumn, conditionValue);

                    // Führen Sie den UPDATE-Befehl aus
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }
            
        }

        /// <summary>
        /// Prüft ob Column vorhanden ist oder nicht 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool HasColumn(IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<SqliteDataReader> GetTotalPriceAsync(string dbID)
        {
            SqliteCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;

            string commString = @$"SELECT Preis FROM Menue WHERE Menue_ID = {dbID}";
            cmd.CommandText = commString;
            return await cmd.ExecuteReaderAsync();
        }




    }
}
