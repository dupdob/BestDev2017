using System;
using System.Collections.Generic;
using System.Linq;

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
            var chips = int.Parse(Console.ReadLine());
            var cake = Console.ReadLine();

            cake += cake;
            var maxDark = int.MinValue;
            for (var i = 0; i < chips; i++)
            {
                var share = cake.Substring(i, chips/2);
                var count = 0;
                foreach (var chip in share)
                {
                    if (chip == 'x')
                        count++;
                }
                maxDark = Math.Max(maxDark, count);
            }
            Console.WriteLine(maxDark);
        }
    }
}