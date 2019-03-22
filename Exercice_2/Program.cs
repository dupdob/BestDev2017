using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/*******
 * Read input from Console
 * Use Console.WriteLine to output your result.
 * Use:
 *       Utils.LocalPrint( variable); 
 * to display simple variables in a dedicated area.
 * 
 * Use:
 *      
		Utils.LocalPrintArray( collection)
 * to display collections in a dedicated area.
 * ***/

namespace CSharpContestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var line1 = Console.ReadLine();
            var dealerCard = line1.Split(' ');
            var line2 = Console.ReadLine();
            var myCard = line2.Split(' ');

            Utils.LocalPrint("Dealer "+line1);
            Utils.LocalPrint("My "+line2);
            var myTotal = Count(myCard);
            var dealerTotal = Count(dealerCard);
            if (myTotal == 21)
            {
                Console.WriteLine("BLACK JACK");
            }
            else if (myTotal < 21 && (myTotal >= dealerTotal || dealerTotal > 21))
            {
                Console.WriteLine("WIN");
            }
            else
            {
                Console.WriteLine("LOSE");
            }
        }

        static int Count(string[] cards)
        {
            var total = 0;
            foreach (var card in cards)
            {
                switch (card)
                {   
                    case "":
                        break;
                    case "J":
                    case "Q":
                    case "D":
                    case "R":
                        total += 10;
                        break;
                        default:
                            total += int.Parse(card);
                            break;
                }
            }
            return total;
        }
    }
}