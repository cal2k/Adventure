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
        public static string temp;

        private static string name, items, armor, weapon, locationlookup;
        private static int health, wounds, gold, currentid, locationid;
        public static string Name { get { return name; } set { name = value; } }
        public static string Items { get { return items; } set { items = value; } }
        public static string Armor { get { return armor; } set { armor = value; } }
        public static string Weapon { get { return weapon; } set { weapon = value; } }
        public static string LocationLookup { get { return locationlookup; } set { locationlookup = value; } }

        public static int Health { get { return health; } set { health = value; } }
        public static int Wounds { get { return wounds; } set { wounds = value; } }
        public static int Gold { get { return gold; } set { gold = value; } }
        public static int CurrentID { get { return currentid; } set { currentid = value; } }
        public static int LocationID {  get { return locationid; } set { locationid = value; } }

        private static bool valid = false;

        //new char
        public static void NewChar()
        {
            while (valid == false)
            {
                Console.WriteLine("Character's name:");
                Name = Console.ReadLine();
                validname();
            }
        }
        private static void validname()
        {
            Querys.query = "SELECT COUNT (name) from char WHERE name = '" + Character.Name + "'";
            Querys.Count();
            if(Querys.count>0)
            {
                Console.Clear();
                Console.WriteLine("Character name already in use.");
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
                Character.locationid = 1;

                Querys.query = "insert into char (name, health, wounds, items, armor, weapon, gold, currentid, locationid)" +
                "values('" + Character.Name + "', '" + Character.Health + "', '" + Character.Wounds + "', '" + Character.Items +
                "', '" + Character.Armor + "', '" + Character.Weapon + "', '" + Character.Gold + "', '" + Character.CurrentID + "', '" + Character.LocationID + "')";

                Querys.Insert();
            }
        }

        //load char
        public static void LoadChar()
        {
            Querys.query = "select * from char where name = '" + Name + "'";
            Querys.LoadChar();
            Program.game();
        }

        public static void UserInputs()
        {
            if (temp == "I")
            {
                Inventory();
            }
        }
        public static void Inventory()
        {
            Console.WriteLine("Inventory");
            Console.ReadLine();
            //triggerd by user input i
            //gather list of items weapons and armor on character split and display list with number for selection
            //equip item by typing equip + item number
            //updates char table
            //runs load char to get back to the same spot
            
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
                                Character.LocationLookup = reader.GetString(0);
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
        static public string Chat, optionsworking, lookupsworking, bonusworking;
        static public string[] Options = new string[5], Lookup = new string[5];
        static public int input, count;
        static public void chatcontroller()
        {
            Querys.query = "SELECT * from scotney where id = '" + Character.CurrentID + "'";
            Querys.SelectChat();

            Options = optionsworking.Split(',');
            Lookup = lookupsworking.Split(',');
            loot.DialogueBonus();

            if(Character.LocationLookup != "x")
            {
                Character.LocationID = Convert.ToInt32(Character.LocationLookup);
                Querys.query = "Select name from locations where ref = '" + Character.LocationID + "'";
                Querys.SelectLocation();

                Character.CurrentID = 1;
                Querys.query = "select * from '" + Character.LocationLookup + "' where id = '" + Character.CurrentID + "'";
                Querys.SelectChat();

                Options = optionsworking.Split(',');
                Lookup = lookupsworking.Split(',');
                loot.DialogueBonus();
            }
            
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

        static public void locationlook()
        {
            
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
                Character.CurrentID = Convert.ToInt32(Lookup[input]);
                Querys.query = "update char set currentid = '" + Character.CurrentID + "' where name = '" + Character.Name + "'";
                Querys.Insert();
                chatcontroller();
            }
        }

       
    }
    class Combat
    {

    }
    class loot
    {
        static public void DialogueBonus()
        {
            if (Dialogue.bonusworking != "x")
            {
                if (Dialogue.bonusworking.StartsWith("G"))
                {
                    int i = Convert.ToInt32(Dialogue.bonusworking.Substring(1));
                    Character.Gold = Character.Gold + i;
                    Querys.query = "update char set gold = '" + Character.Gold + "' where name = '" + Character.Name + "'";
                    Querys.Insert();
                }
                if (Dialogue.bonusworking.StartsWith("W"))
                {
                    Character.Weapon = Dialogue.bonusworking.Substring(1);
                    Querys.query = "update char set weapon = '" + Character.Weapon + "' where name = '" + Character.Name + "'";
                    Querys.Insert(); 
                }
                if (Dialogue.bonusworking.StartsWith("A"))
                {
                    Character.Armor = Dialogue.bonusworking.Substring(1); 
                    Querys.query = "update char set armor = '" + Character.Armor + "' where name = '" + Character.Name + "'";
                    Querys.Insert();
                    Console.ReadLine();
                }
                if (Dialogue.bonusworking.StartsWith("H"))
                {
                    
                    int i = Convert.ToInt32(Dialogue.bonusworking.Substring(1));
                    int ii = Character.Health;
                    Character.Health = ii + i;
                    Querys.query = "update char set health = '" + Character.Health + "' where name = '" + Character.Name + "'";
                    Querys.Insert();
                }
                if (Dialogue.bonusworking.StartsWith("D"))
                {
                    int i = Convert.ToInt32(Dialogue.bonusworking.Substring(1));
                    Character.Health = Character.Health - i;
                    Querys.query = "update char set health = '" + Character.Health + "' where name = '" + Character.Name + "'";
                    Querys.Insert();
                }
            }
        }

        static public void Random()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            menu();
        }
        static void menu()
        {
            bool valid = false;
            string menutext = "1 - New Game,2 - Load Character,3 - Quit";
            string[] menu = menutext.Split(',');
            int input;

            while (valid == false)
            {
                Console.WriteLine(" ___                             _    _   _                     _");
                Console.WriteLine("| _ \\  ___  ___  __  __  ___  __| |  | | / / 0  __   __ ___  __| | ___  __  __");
                Console.WriteLine("|| || |   ||   ||  \\/  || 0_||    |  | |/ /  _ |  \\ | ||   ||    ||   ||  \\/  |");
                Console.WriteLine("||_|| | 0 || 0 || |\\/| || |_ | 0  |  | |\\ \\ | || |\\\\| ||0  || 0  || 0 || |\\/| |");
                Console.WriteLine("|___/ |___||___||_|  |_||___||____|  |_| \\_\\|_||_| \\__||__ ||____||___||_|  |_|");
                Console.WriteLine("                                                       ||_||");
                Console.WriteLine("                                                       |___|");
                Console.WriteLine("");
               
                for(int i = 0; i < menu.Count(); i ++)
                {
                    Console.WriteLine(menu[i]);
                }

                Character.temp = Console.ReadLine();
                Character.UserInputs();
                try
                {
                    input = Convert.ToInt32(Character.temp);
                    switch(input)
                    {
                        case 1:
                            Console.Clear();
                            Character.NewChar();
                            break;
                        case 2:
                            Console.Clear();
                            Character.LoadChar();
                            break;
                        case 3:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid selection");
                            break;

                    }
                }
                catch(Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid selection");
                }                
            }
        }

        public static void game()
        {
            Console.WriteLine("Ingame");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
