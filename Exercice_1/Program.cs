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
            var count = int.Parse(Console.ReadLine());
            var total = 0;
            for (int index = 0; index < count; index++)
            {
                var readLine = Console.ReadLine();
                total += int.Parse(readLine);
            }
            Console.WriteLine(total);
        }
    }
}