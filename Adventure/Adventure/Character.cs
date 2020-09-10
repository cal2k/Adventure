using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static int LocationID { get { return locationid; } set { locationid = value; } }
        static public int input;
    }
}
