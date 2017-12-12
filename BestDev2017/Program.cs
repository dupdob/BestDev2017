using System;
using System.Collections.Generic;

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
            var fragmentsCount = int.Parse(Console.ReadLine());
            var fragments = new List<string>(fragmentsCount);
            var totalLength = 0;
            for (int index = 0; index < fragmentsCount; index++)
            {
                var readLine = Console.ReadLine();
                fragments.Add(readLine);
                totalLength += readLine.Length;
            }

            totalLength /= 2;
            var firstRef = string.Empty;
            var secondRef = string.Empty;
            var fragments1 = string.Empty;
            var fragments2 = string.Empty;
            Scan(firstRef, secondRef, fragments, totalLength, fragments1, fragments2);
            Console.ReadLine();
        }

        private static bool Scan(string firstRef, string secondRef, List<string> fragments, int totalLength, string fragments1, string fragments2)
        {
            if (fragments.Count == 0)
            {
                if (firstRef.Length == totalLength && IsCompatible(firstRef, secondRef))
                {
                    Console.WriteLine("{0}#{1} ", fragments1, fragments2);
                    return true;
                }
            }
            if (firstRef.Length > secondRef.Length)
            {
                var temp = secondRef;
                secondRef = firstRef;
                firstRef = temp;

                temp = fragments1;
                fragments1 = fragments2;
                fragments2 = temp;
            }
            for (var i = 0; i < fragments.Count; i++)
            {
                var first = firstRef + fragments[i];
                if (first.Length > totalLength)
                {
                    continue;
                }
                var newFragment = fragments1 + (fragments1.Length>0 ? " " : "") + fragments[i];
                if (IsCompatible(newFragment, fragments2))
                {
                    var newFragments = new List<string>(fragments);
                    newFragments.RemoveAt(i);
                    if (Scan(first, secondRef, newFragments, totalLength, newFragment, fragments2))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsCompatible(string first, string second)
        {
            int j = 0;
            for (int i = 0; i < Math.Min(first.Length, second.Length); i++)
            {
                if (first[i] == ' ')
                {
                    continue;
                }
                if (second[j] == ' ')
                {
                    j++;
                }
                switch (first[i])
                {
                    case 'A':
                        if (second[i] != 'T')
                            return false;
                        break;
                    case 'T':
                        if (second[i] != 'A')
                            return false;
                        break;
                    case 'G':
                        if (second[i] != 'C')
                            return false;
                        break;
                    case 'C':
                        if (second[i] != 'G')
                            return false;
                        break;
                        default:
                            return false;
                }
            }
            return true;
        }
    }
}