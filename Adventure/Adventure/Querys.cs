using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Querys
    {
        static public SQLiteConnection conn = new SQLiteConnection("Data Source = DoomedKingdom.db; version =3;");
        static public SQLiteCommand cmd;
        static public SQLiteDataReader reader;

        static public string query;
        static public int count;

        static public List<string> templist = new List<string>();

        static public void Count()
        {
            cmd = new SQLiteCommand(query, conn);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (cmd)
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader.GetInt32(0);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        }

        static public void Insert()
        {
            cmd = new SQLiteCommand(query, conn);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (cmd)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
            Console.Clear();
        }

        static public void LoadChar()
        {
            cmd = new SQLiteCommand(query, conn);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (cmd)
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Character.Health = reader.GetInt32(2);
                                Character.Wounds = reader.GetInt32(3);
                                Character.Items = reader.GetString(4);
                                Character.Armor = reader.GetString(5);
                                Character.Weapon = reader.GetString(6);
                                Character.Gold = reader.GetInt32(7);
                                Character.CurrentID = reader.GetInt32(8);
                                Character.LocationID = reader.GetInt32(9);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static public void SelectChat()
        {
            cmd = new SQLiteCommand(query, conn);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (cmd)
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dialogue.Chat = reader.GetString(1);
                                Dialogue.optionsworking = reader.GetString(2);
                                Dialogue.lookupsworking = reader.GetString(3);
                                Dialogue.bonusworking = reader.GetString(4);
                                Character.LocationLookup = reader.GetString(5);

                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static public void SelectLocation()
        {
            cmd = new SQLiteCommand(query, conn);
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (cmd)
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Character.temp = reader.GetString(0);
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
