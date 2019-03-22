using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using static System.String;

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
            var i = int.Parse(Console.ReadLine());
            var first = Console.ReadLine();
            var j = int.Parse(Console.ReadLine());
            var second = Console.ReadLine();
            var response = Empty;
            var secondCursor = 0;
            var firstCursor = 0;
            response = ShortestCommon(first, second);

            Console.WriteLine(response);
        }

        public static string LongestCommonSubstring(string str1, string str2, out int i1, out int i2)
        {
            i1 = i2 = 0;
            var sequence = string.Empty;
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                return sequence;

            var num = new int[str1.Length, str2.Length];
            var maxlen = 0;
            var lastSubsBegin = 0;
            var sequenceBuilder = new StringBuilder();

            for (int i = 0; i < str1.Length; i++)
            {
                for (var j = 0; j < str2.Length; j++)
                {
                    if (str1[i] != str2[j])
                        num[i, j] = 0;
                    else
                    {
                        if ((i == 0) || (j == 0))
                            num[i, j] = 1;
                        else
                            num[i, j] = 1 + num[i - 1, j - 1];

                        if (num[i, j] <= maxlen)
                        {
                            continue;
                        }
                        maxlen = num[i, j];
                        var thisSubsBegin = i - num[i, j] + 1;
                        if (lastSubsBegin == thisSubsBegin)
                        {//if the current LCS is the same as the last time this block ran
                            sequenceBuilder.Append(str1[i]);
                        }
                        else //this block resets the string builder if a different LCS is found
                        {
                            lastSubsBegin = thisSubsBegin;
                            sequenceBuilder.Length = 0; //clear it
                            sequenceBuilder.Append(str1.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                        }
                    }
                }
            }

            var longestCommonSubstring = sequenceBuilder.ToString();
            i1 = str1.IndexOf(longestCommonSubstring);
            i2 = str2.IndexOf(longestCommonSubstring);
            return longestCommonSubstring;
        }

        private static string ShortestCommon(string first, string second)
        {
            var response = new StringBuilder();
            int i1, i2 = 0;

            if (string.IsNullOrEmpty(first))
            {
                return second;
            }
            if (string.IsNullOrEmpty(second))
            {
                return first;
            }
            var sub=LongestCommonSubstring(first, second, out i1, out i2);
            if (string.IsNullOrEmpty(sub))
            {
                response.Append(first);
                response.Append(second);
                return response.ToString();
            }
            response.Append(ShortestCommon(first.Substring(0, i1), second.Substring(0, i2)));
            response.Append(sub);
            response.Append(ShortestCommon(first.Substring(i1+sub.Length), second.Substring(i2+sub.Length)));
            return response.ToString();
        }
    }
}