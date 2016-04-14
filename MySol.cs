using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace GameofEggsEg
{
    internal class MySol
    {
        //initialize first state
        //maintain a copy in another duplicate
        //traverse through the first
        //getcount of neighbours
        //ensure edgecases are accounted for
        //update corresponding in duplicate
        //after entire traversal is done,replace and print

        private static int[,] M1;
        private static int[,] M2;
        private static int r = 0, c = 0;

        public static void Main(string[] args)
        {
            //generate a matrix
            //generate();

            //game of life
            initialize();
            auxMatrix();
            print();
            while (true)
            {
                System.Threading.Thread.Sleep(100);//sleep 2000 = 2 secs
                traverseM1andUpdateM2();
                updateM1();
                print();
            }
        }

        private static void generate()
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\genmatrix.txt";
            System.IO.File.WriteAllText(path, string.Empty);

            StreamWriter fileWrite = new StreamWriter(path);

            Random t = new Random();
            string s = "";

            r = t.Next(10, 100);
            c = t.Next(10, 100);
            M1 = new int[r, c];

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    var v = t.Next(0, 2);
                    M1[i, j] = v;
                    s = v + " ";
                    fileWrite.Write(s);
                }
                fileWrite.WriteLine();
            }

            fileWrite.Close();
        }

        private static void initialize()
        {
            List<String> rows = new List<string>();

            string line;
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\genmatrix.txt";//matrix glider
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                r = r + 1;
                rows.Add(line);
            }
            file.Close();

            c = rows[0].Trim().Split(' ').Count();

            int[] temp;
            if (M1 == null)
                M1 = new int[r, c];
            for (int i = 0; i < r; i++)
            {
                temp = Array.ConvertAll(rows[i].Trim().Split(' '), Int32.Parse);
                for (int j = 0; j < c; j++)
                    M1[i, j] = temp[j];
            }

            //M1 = new int[,]{{0,0,0,1},
            //                {0,1,1,0},
            //                {0,1,1,0},
            //                {0,0,0,1}};
        }

        private static void auxMatrix()
        {
            M2 = new int[r, c];
        }

        private static void updateM1()
        {
            M1 = M2;
            auxMatrix();
        }

        private static void traverseM1andUpdateM2()
        {
            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M1.GetLength(1); j++)
                {
                    int neighbours = getNeighbours(i, j);

                    if (M1[i, j] == 1)
                    {
                        if (neighbours <= 1 || neighbours >= 4)
                            M2[i, j] = 0;
                        else
                            M2[i, j] = 1;
                    }
                    else
                    {
                        if (neighbours == 3)
                            M2[i, j] = 1;
                    }
                }
            }
        }

        private static void print()
        {
            Console.Clear();

            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M1.GetLength(1); j++)
                {
                    if (M1[i, j] == 1)
                        Console.Write("■");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        private static int getNeighbours(int x, int y)
        {
            int n = 0;
            if (y - 1 > 0)
            {
                n += (x - 1 > 0) ? M1[x - 1, y - 1] : 0;
                n += M1[x, y - 1];
                n += (x + 1 < M1.GetLength(0)) ? M1[x + 1, y - 1] : 0;
            }

            n += (x - 1 > 0) ? M1[x - 1, y] : 0;
            n += (x + 1 < M1.GetLength(0)) ? M1[x + 1, y] : 0;

            if (y + 1 < M1.GetLength(1))
            {
                n += (x - 1 > 0) ? M1[x - 1, y + 1] : 0;
                n += M1[x, y + 1];
                n += (x + 1 < M1.GetLength(0)) ? M1[x + 1, y + 1] : 0;
            }
            return n;
        }
    }
}