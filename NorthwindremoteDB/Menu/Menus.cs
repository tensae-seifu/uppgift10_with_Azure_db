using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindremoteDB.Menu
{
    internal class Menus
    {
        public static void MenuItem()
        {
            int answer;
            do
            {
                Console.WriteLine("\t\tEnter the corresponding number to access");
                Console.WriteLine("\t\t=========================================\n");
                Console.WriteLine("1 - List all the customers");
                Console.WriteLine("2 - View a single customer info");
                Console.WriteLine("3 - Add a new customer");
                Console.WriteLine("0 - Exit");

                answer = Convert.ToInt32(Console.ReadLine());

                char ans;
                switch (answer)
                {
                    case 1:
                        Action.GetAllCustomer();
                        break;

                    case 2:
                        do
                        {
                            Action.GetSingleCustomer();
                            Console.WriteLine("Would you like to view more customers (y/n)?\n");
                            ans = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("\n");
                        } while (ans == 'y' || ans == 'Y');
                        break;

                    case 3:
                        do
                        {
                            Action.AddCustomer();
                            Console.WriteLine("Would you like to add more (y/n)?\n");
                            ans = Convert.ToChar(Console.ReadLine());
                            Console.WriteLine("\n");
                        } while (ans == 'y' || ans == 'Y');
                        break;

                    case 0:

                        Console.WriteLine("Exiting the program.\n");

                        break;

                    default:
                        Console.WriteLine("Wrong choice. Please try again.\n");
                        break;
                }
            } while (answer != 0);
        }
    }
}
