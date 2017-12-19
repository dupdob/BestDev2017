using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpContestProject
{
    static class Utils
    {
        public static void  LocalPrint(string texte)
        {
            Console.Error.WriteLine(texte);
        }

        public static void LocalPrintArray(IEnumerable collec)
        {
            foreach (var item in collec)
            {
                Console.Error.Write("{0},", item);
            }
            Console.Error.WriteLine();
        }
    }
}
