using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Adventure
{
    class Character
    {
        private static string name, items, armor, weapon;
        private static int health, wounds, gold, currentid;
        public static string Name { get { return name; } set { name = value; } }
        public static string Items { get { return items; } set { items = value; } }
        public static string Armor { get { return armor; } set { armor = value; } }
        public static string Weapon { get { return weapon; } set { weapon = value; } }

        public static int Health { get { return health; } set { health = value; } }
        public static int Wounds { get { return wounds; } set { wounds = value; } }
        public static int Gold { get { return gold; } set { gold = value; } }
        public static int CurrentID { get { return currentid; } set { currentid = value; } }


        static public void CheckName()
        {
            Querys.query = "SELECT COUNT (name) from char WHERE name = '" + Character.Name + "'";

            Querys.CountFunc();

            if (Querys.count > 0)
            {
                Console.Clear();
                Querys.query = "SELECT * from char where name = '" + Character.Name + "'";
                Querys.SelectCharFunc();
                //gose to chat controller. using the users currentid to load correct place

            }
            else
            {
                Console.Clear();
                Character.Health = 10;
                Character.Wounds = 0;
                Character.Items = "x";
                Character.Armor = "x";
                Character.Weapon = "x";
                Character.Gold = 5;
                Character.CurrentID = 1;

                Querys.query = "insert into char (name, health, wounds, items, armor, weapon, gold, currentid)" +
                "values('" + Character.Name + "', '" + Character.Health + "', '" + Character.Wounds + "', '" + Character.Items +
                "', '" + Character.Armor + "', '" + Character.Weapon + "', '" + Character.Gold + "', '" + Character.CurrentID + "')";

                Querys.InsertFunc();
                
                //chat controller to load relevent 
            }
        }
    }
    class Querys
    {
        static public SQLiteConnection conn = new SQLiteConnection("Data Source = DoomedKingdom.db; version =3;");
        static public SQLiteCommand cmd;
        static public SQLiteDataReader reader;

        static public string query;
        static public int count;

        static public List<string> templist = new List<string>();

        static public void CountFunc()
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

        static public void InsertFunc()
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

        static public void SelectCharFunc()
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
    class Dialogue
    {
        static public string wdud = "What do you do?", read;
        static public string Chat, optionsworking, lookupsworking;
        static public string[] Options = new string[5], Lookup = new string[5];
        static public int input, workingID, count;
        static public void chatcontroller()
        {
            Querys.query = "SELECT * from dialogue where id = '" + Character.CurrentID + "'";
            Querys.SelectChat();

            Dialogue.Options = Dialogue.optionsworking.Split(',');
            Dialogue.Lookup = Dialogue.lookupsworking.Split(',');

            Console.WriteLine(Dialogue.Chat);
            Console.WriteLine();
            Console.WriteLine(Dialogue.wdud);
            Console.WriteLine();

            count = Dialogue.Options.Count();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(Dialogue.Options[i]);
            }

            read = Console.ReadLine();
            ValidInput();
        }

        static public void ValidInput()
        {

            try
            {
                input = Convert.ToInt32(read);
            }
            catch(Exception ex)
            {
                Console.Clear();
                chatcontroller();
            }

            input = input - 1;
            if (input >= Dialogue.count)
            {
                Console.Clear();
                chatcontroller();
            }
            else
            {
                Console.Clear();
                workingID = Convert.ToInt32(Lookup[input]);
                Character.CurrentID = workingID;
                chatcontroller();
            }
        }

       
    }
    class Program
    {
        static bool valid = false;

        static void Main(string[] args)
        {
            menu();
        }
        static void menu()
        {

            while (valid == false)
            {
                Console.WriteLine(" ___                             _   _    _ _                  _");
                Console.WriteLine("| _ \\  ___  ___  __  __  ___  __||  | | / /|_| __   __ ____  __|| ___  __  __");
                Console.WriteLine("|| || | _ || _ ||  \\/  || o_|| _ |  | |/ /  _ |  \\ | || _  || _ || _ ||  \\/  |");
                Console.WriteLine("||_|| ||_||||_||| |\\/| || |_ ||_||  | |\\ \\ | || |\\\\| |||_| |||_||||_||| |\\/| |");
                Console.WriteLine("|___/ |___||___||_|  |_||___||___|  |_| \\_\\|_||_| \\__||___ ||___||___||_|  |_|");
                Console.WriteLine("                                                      | |_||");
                Console.WriteLine("                                                      |____|");
                Console.WriteLine("");
                Console.WriteLine("Please enter your name.");


                Character.Name = Console.ReadLine();

                Character.CheckName();
                Dialogue.chatcontroller();
                
            }
        }
        
        
    }
}
