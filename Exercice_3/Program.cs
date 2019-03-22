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
            var cities = int.Parse(Console.ReadLine());
            var buildings = int.Parse(Console.ReadLine());
            int minBuildingsize = int.MaxValue;
            int bestCity = -1;
            for (int index = 0; index < cities; index++)
            {
                var maxSize = int.MinValue;
                var readLine = Console.ReadLine().Split(' ');
                foreach (var building in readLine)
                {
                    var size = int.Parse(building);
                    if (size > maxSize)
                    {
                        maxSize = size;
                    }
                }
                if (maxSize < minBuildingsize)
                {
                    minBuildingsize = maxSize;
                    bestCity = index;
                }
            }
            Console.WriteLine(bestCity);

        }
    }
}