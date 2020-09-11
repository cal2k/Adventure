using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Machanices
    {
        //INPUTS
        public static void KeyChecker()
        {
            if (Character.temp == "I")
            {
                Inventory();
            }
            if (Character.temp == "P")
            {
                Potion();
            }
        }
        public static void ValidSelection()
        {
            //check if option selection is valid
            try
            {
                Character.input = Convert.ToInt32(Character.temp);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Dialogue.displaychat();
            }

            Character.input = Character.input - 1;
            if (Character.input >= Dialogue.count)
            {
                Console.Clear();
                Dialogue.displaychat();
            }
            else
            {
                Console.Clear();
                try
                {
                    Character.CurrentID = Convert.ToInt32(Dialogue.Lookup[Character.input]);
                }
                catch(Exception ex)
                {
                    Console.Clear();
                }
                
                Querys.query = "update char set currentid = '" + Character.CurrentID + "' where name = '" + Character.Name + "'";
                Querys.Insert();
                Dialogue.locationlook();
            }
        }
        public static void ValidMenuSelection()
        {

        }

        //CHARACTER 
        public static void NewChar()
        {
            Console.WriteLine("Character's name:");
            Character.Name = Console.ReadLine();

            Querys.query = "SELECT COUNT (name) from char WHERE name = '" + Character.Name + "'";
            Querys.Count();
            if (Querys.count > 0)
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
                Character.LocationID = 1;

                Querys.query = "insert into char (name, health, wounds, items, armor, weapon, gold, currentid, locationid)" +
                "values('" + Character.Name + "', '" + Character.Health + "', '" + Character.Wounds + "', '" + Character.Items +
                "', '" + Character.Armor + "', '" + Character.Weapon + "', '" + Character.Gold + "', '" + Character.CurrentID + "', '" + Character.LocationID + "')";

                Querys.Insert();
                Program.game();
            }
        }
        public static void LoadChar()
        {
            Console.WriteLine("Characters name?");
            Character.Name = Console.ReadLine();
            Querys.query = "select * from char where name = '" + Character.Name + "'";
            Querys.LoadChar();
            Program.game();
        }
        static public void SaveChar()
        {
            Querys.query = "update char set health = '" + Character.Health + "', wounds = '" + Character.Wounds + "', items = '" + Character.Items + "', armor = '" + Character.Armor + "', weapon = '" + Character.Weapon + "', gold = '" +
                Character.Gold + "', currentid = '" + Character.CurrentID + "', locationid = '" + Character.LocationID + "' where name = '" + Character.Name + "'";
            Querys.Insert();
        }

        //
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
        public static void Potion()
        {
            Console.WriteLine("Potion");
            Console.ReadLine();
        }

        public static void ThreadingTest()
        {
            bool delay = false;
            while (delay == false)
            {
                SaveChar();
                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}
