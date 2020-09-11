using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Adventure
{
    class Program
    {
        static Thread Save = new Thread(Machanices.ThreadingTest);
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
                Console.WriteLine(" ___                            _    _   _                     _");
                Console.WriteLine("| _ \\ ___  ___  __  __  ___  __| |  | | / / _  __   __ ___  __| | ___  __  __");
                Console.WriteLine("|| |||   ||   ||  \\/  || 0_||    |  | |/ / | ||  \\ | ||   ||    ||   ||  \\/  |");
                Console.WriteLine("||_||| 0 || 0 || |\\/| || |_ | 0  |  | |\\ \\ | || |\\\\| || 0 || 0  || 0 || |\\/| |");
                Console.WriteLine("|___/|___||___||_|  |_||___||____|  |_| \\_\\|_||_| \\__||__ ||____||___||_|  |_|");
                Console.WriteLine("                                                      ||_||");
                Console.WriteLine("                 A FishBoy Production                 |___|");
                Console.WriteLine();
               
                for(int i = 0; i < menu.Count(); i ++)
                {
                    Console.WriteLine(menu[i]);
                }

                Character.temp = Console.ReadLine();
                Machanices.ValidSelection();
                try
                {
                    input = Convert.ToInt32(Character.temp);
                    switch(input)
                    {
                        case 1:
                            Console.Clear();
                            Machanices.NewChar();
                            break;
                        case 2:
                            Console.Clear();
                            Machanices.LoadChar();
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

            Save.Start();
            bool valid = false;
            while (valid == false)
            {
                Combat.hostile();
                Dialogue.locationlook();
                Dialogue.gatherchat();
                Loot.DialogueBonus();
                Dialogue.levelchange();
                Dialogue.displaychat();
                Character.temp = Console.ReadLine();
                Machanices.KeyChecker();
                Machanices.ValidSelection();
            }
        }

        

        

        
    }
}
