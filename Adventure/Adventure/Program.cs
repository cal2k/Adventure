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
                Console.ReadLine();
                Querys.SelectCharFunc();
                Console.ReadLine();
                // Character.SetChar();

            }
            else
            {
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

                //go to "taven" GAME START!
            }
        }
        static public void SetChar()
        {
            Health = Convert.ToInt32(Querys.templist[2]);
            Wounds = Convert.ToInt32(Querys.templist[3]);
            Items = Querys.templist[4];
            Armor = Querys.templist[5];
            Weapon = Querys.templist[6];
            Gold = Convert.ToInt32(Querys.templist[7]);
            CurrentID = Convert.ToInt32(Querys.templist[8]);
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
                                Console.WriteLine(reader.ToString());
                                Console.ReadLine();
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
        static private string chat;
        static private string[] options = new string[5];
        static private int[] lookups = new int[5];

        static public string Chat;
        static public string[] Options = new string[5];
        static public int[] Lookups = new int[5];

        static public void Gather()
        {
            
        }
    }
    class Program
    {
        static string wdud = "What do you do?", read;
        static int choice;
        static List<string> options = new List<string>();
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
                    
                Console.Clear();
            }
        }

        
        static void validselection()
        {
            try
            {
                options.Clear();
                Console.Clear(); 
                choice = Convert.ToInt32(read);
            }
            catch (Exception)
            {
                Console.Clear();
            }
            
        }

        static void taven()
        {
            while (valid == false)
            {
                options.Add("1 - Investigate commotion.");
                options.Add("2 - Finish eating meal.");

                Console.WriteLine("You're sat eating a meal in the Scotney taven when you hear a commotion in the streets outside.");
                Console.WriteLine();
                Console.WriteLine(wdud);
                Console.WriteLine();
                for (int i = 0; i < options.Count(); i++)
                {
                    Console.WriteLine(options[i]);
                }

                read = Console.ReadLine();

                validselection();

                switch(choice)
                {
                    case 1:
                        investigatecommotion();
                        break;
                    case 2:
                        meal();
                        break;
                    default:
                        break;
                }
            }
        }

        static void meal()
        {
            while (valid == false)
            {
                options.Clear();
                options.Add("1 - Investigate commontion.");
                options.Add("2 - Buy Drink");
                options.Add("3 - Do nothing");


                Console.WriteLine("As you finish your meal you hear the commotion growing louder.");
                Console.WriteLine(wdud);
                Character.Health = Character.Health + 1;
                Console.WriteLine("");

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine(options[i]);
                }

                read = Console.ReadLine();

                validselection();
               
                switch (choice)
                {
                    case 1:
                        investigatecommotion();
                        break;
                    case 2:
                        //drink();
                        break;
                    case 3:
                        //nothing();
                        break;
                    default:
                        break;
                }
            }
        }

        static void investigatecommotion()
        {
            Console.WriteLine("outside");
            Console.ReadLine();
        }
        
    }

    
}
