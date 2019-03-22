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
            var variations = new List<int>(count);
            var quote = 0;
            for (var i = 0; i < count; i++)
            {
                var readLine = Console.ReadLine();
                var item = int.Parse(readLine);
                quote += item;
                variations.Add(quote);
            }
            var minQuote = int.MaxValue;
            var maxGain = int.MinValue;
            for (int i = 0; i < count; i++)
            {
                var curQuote = variations[i];
                if (curQuote < minQuote)
                {
                    minQuote = curQuote;
                }
                else if (curQuote - minQuote > maxGain)
                {
                    maxGain = curQuote - minQuote;
                }
            }
            Console.WriteLine(maxGain);
        }
    }
}