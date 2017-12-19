using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

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
            var H = int.Parse(Console.ReadLine());
            var L = int.Parse(Console.ReadLine());
            var X = 0;
            var Y = 0;
            var lines = new char[L, H];
            for (var index = 0; index < H; index++)
            {
                var readLine = Console.ReadLine();
                for (int j = 0; j < L; j++)
                {
                    var c = readLine[j];
                    if (c == 'x')
                    {
                        c = '.';
                        X = j;
                        Y = index;
                    }
                    lines[j, index] = c;
                }
            }
            var line = string.Empty;
            MapHelper.Map(lines, (i, j) =>
            {
                if (lines[i, j] != '.')
                {
                    return;
                }
                var bombs = MapHelper.ReduceNeightBors<int>(lines, i, j,
                    (cell, accu) => accu + (cell == '*' ? 1 : 0));
                if (bombs > 0)
                {
                    lines[i, j] = (char)('0' + bombs);
                }
            });

            MapHelper.Map(lines, (x, y) =>
            {
                line += lines[x, y];
                if (x == L - 1)
                {
                    Utils.LocalPrint(line);
                    line = string.Empty;
                }
            });
            var res = MapHelper.FloodFill(lines, X, Y, ' ', '.', MapHelper.Mode.BorderIncluded | MapHelper.Mode.WithDiagonale);

            Console.WriteLine(res);
        }

    }

    static class MapHelper
    {
        [Flags]
        public enum Mode
        {
            Basic,
            BorderIncluded,
            WithDiagonale
        }

        public static int FloodFill(char[,] array, int x, int y, char filler, Mode mode = 0)
        {
            return RecursiveFloodFill(array, x, y, array[x, y], filler, mode);
        }

        public static int FloodFill(char[,] array, int x, int y, char filler, char empty, Mode mode = 0)
        {
            if (array[x, y] != empty)
            {
                return mode.HasFlag(Mode.BorderIncluded) ? 1 : 0;
            }
            return RecursiveFloodFill(array, x, y, empty, filler, mode);
        }

        private static int RecursiveFloodFill(char[,] array, int x, int y, char empty, char filler, Mode mode)
        {
            var result = 0;
            if (x < 0 || x > array.GetUpperBound(0) || y < 0 || y > array.GetUpperBound(1))
            {
                return result;
            }
            var color = array[x, y];
            if (color == filler || (!mode.HasFlag(Mode.BorderIncluded) && color != empty))
            {
                return result;
            }
            array[x, y] = filler;
            result++;
            if (mode.HasFlag(Mode.BorderIncluded) && color != empty)
            {
                return result;
            }

            MapNeightBors(array, x, y, (i, j, c) => result += RecursiveFloodFill(array, i, j, empty, filler, mode),
                mode);
            return result;
        }


        private static T SecuredWrapper<T>(char[,] map, int i, int j, Func<char, T, T> f, T accu)
        {
            if (i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1))
            {
                return accu;
            }
            return f(map[i, j], accu);
        }

        private static void SecuredWrapper(char[,] map, int i, int j, Action<int, int, char> f)
        {
            if (i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1))
            {
                return;
            }
            f(i, j, map[i, j]);
        }

        public static T ReduceNeightBors<T>(char[,] map, int x, int y, Func<char, T, T> func, Mode mode = Mode.WithDiagonale)
        {
            var result = default(T);

            result = SecuredWrapper(map, x + 1, y, func, result);
            result = SecuredWrapper(map, x, y + 1, func, result);
            result = SecuredWrapper(map, x, y - 1, func, result);
            result = SecuredWrapper(map, x - 1, y, func, result);
            if (!mode.HasFlag(Mode.WithDiagonale))
            {
                return result;
            }
            result = SecuredWrapper(map, x + 1, y + 1, func, result);
            result = SecuredWrapper(map, x - 1, y + 1, func, result);
            result = SecuredWrapper(map, x + 1, y - 1, func, result);
            result = SecuredWrapper(map, x - 1, y - 1, func, result);
            return result;

        }

        public static void MapNeightBors(char[,] map, int x, int y, Action<int, int, char> func,
            Mode mode = Mode.WithDiagonale)
        {
            SecuredWrapper(map, x + 1, y, func);
            SecuredWrapper(map, x, y + 1, func);
            SecuredWrapper(map, x, y - 1, func);
            SecuredWrapper(map, x - 1, y, func);
            if (!mode.HasFlag(Mode.WithDiagonale))
            {
                return;
            }
            SecuredWrapper(map, x + 1, y + 1, func);
            SecuredWrapper(map, x - 1, y + 1, func);
            SecuredWrapper(map, x + 1, y - 1, func);
            SecuredWrapper(map, x - 1, y - 1, func);
        }

        public static void Map(char[,] map, Action<int, int> action)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                for (var i = 0; i < map.GetLength(0); i++)
                {
                    action(i, j);
                }
            }
        }
    }
}