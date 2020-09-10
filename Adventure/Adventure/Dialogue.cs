using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Dialogue
    {
        static public string wdud = "What do you do?";
        static public string Chat, optionsworking, lookupsworking, bonusworking;
        static public string[] Options = new string[5], Lookup = new string[5];
        static public int count;

        static public void locationlook()
        {
            Querys.query = "Select name from locations where ref = '" + Character.LocationID + "'";
            Querys.SelectLocation();
        }
        static public void gatherchat()
        {
            Querys.query = "Select * from '" + Character.temp + "' where id = '" + Character.CurrentID + "'";
            Querys.SelectChat();
            Options = optionsworking.Split(',');
            Lookup = lookupsworking.Split(',');
        }
        static public void displaychat()
        {
            Console.WriteLine(Chat);
            Console.WriteLine();
            Console.WriteLine(wdud);
            Console.WriteLine();

            count = Options.Count();
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(Options[i]);
            }
            Console.WriteLine();
            Console.WriteLine("I (Inventry)    P (Potion)");


        }

        public static void levelchange()
        {
            if (Character.LocationLookup != "x")
            {
                string[] temparray = new string[2];
                temparray = Character.LocationLookup.Split(',');
                Character.LocationID = Convert.ToInt32(temparray[0]);
                Character.CurrentID = Convert.ToInt32(temparray[1]);

                locationlook();
                gatherchat();
            }
        }
    }
}
