using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    class Loot
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
}
