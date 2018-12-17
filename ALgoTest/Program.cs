using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALgoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sizeString = Console.ReadLine();
                int size = int.Parse(sizeString.Trim());
                Console.WriteLine("ok");

                int[,] board = new int[size, size];

                var startPointsStrings = Console.ReadLine().Split(',');
                var startPoints = startPointsStrings.Select(x => new Point()
                {
                    X = int.Parse(x.Replace("{", "").Replace("}", "").Split(';')[0]),
                    Y = int.Parse(x.Replace("{", "").Replace("}", "").Split(';')[1])
                });
                foreach (var point in startPoints)
                {
                    board[point.X, point.Y] = 1;
                }
                Console.WriteLine("ok");
                var generator = new Random();
                var result = Console.ReadLine();
                if (result == "start")
                {
                    var points = GenerateNextRandomPoint(board, size, generator);
                    Console.WriteLine(PointsToString(points));
                    result = Console.ReadLine();
                }
                while (true)
                {
                    var points = StringToPoints(result);
                    foreach (var p in points)
                    {
                        board[p.X, p.Y] = 1;
                    }
                    var pointsToSend = GenerateNextRandomPoint(board, size, generator);
                    if (pointsToSend == null)
                    {
                        return;
                    }
                    Console.WriteLine(PointsToString(pointsToSend));
                    result = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("log.txt", ex.Message);
            }

        }

        static private string PointToString(Point p) => $"{{{p.X};{p.Y}}}";
        static private string PointsToString(Point[] p) => string.Join(",", p.Select(x => $"{{{x.X};{x.Y}}}"));
        static private Point[] StringToPoints(string s) => s.Split(',').Select(x => new Point()
        {
            X = int.Parse(x.Replace("{", "").Replace("}", "").Split(';')[0]),
            Y = int.Parse(x.Replace("{", "").Replace("}", "").Split(';')[1])
        }).ToArray();


        static private Point[] GenerateNextRandomPoint(int[,] tab, int size, Random generator)
        {
            bool isOK = false;
            Point[] res = new Point[2];
            int iterationCounter = 0;
            while (!isOK && iterationCounter < 300)
            {
                int x = generator.Next(0, size );
                int y = generator.Next(0, size );
                if (tab[x, y] == 0 && x < size - 1 && tab[x + 1, y] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(x + 1, y) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && x > 1 && tab[x - 1, y] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(x - 1, y) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && x == 0 && tab[size - 1, y] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(size - 1, y) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && x == size - 1 && tab[0, y] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(0, y) };
                    isOK = true;
                }
                else
                    if (tab[x, y] == 0 && y < size - 1 && tab[x, y + 1] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(x, y + 1) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && y > 1 && tab[x, y - 1] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(x, y - 1) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && y == 0 && tab[x, size - 1] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(x, size - 1) };
                    isOK = true;
                }
                else if (tab[x, y] == 0 && y == size - 1 && tab[x, 0] == 0)
                {
                    res = new Point[] { new Point(x, y), new Point(0, x) };
                    isOK = true;
                }
                iterationCounter++;
            }
            if (isOK)
            {
                foreach (var p in res)
                {
                    tab[p.X, p.Y] = 1;
                }
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}
