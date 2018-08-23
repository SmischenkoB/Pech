using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pech
{

    


    class Map
    {
        private int[,] array = {{ 65535, 15, 7 },   // првила заполнения    МАТРИЦА - КВАДРАТНАЯ (3*3 или n*n)
                                { 15, 65535, 5 },   // если пути нет, ставится MaxInt(65535)
                                { 7, 5, 65535  } };// симметрия относительно главной диагонали, т.е значения в ячейках i,j и j,i
                                                      // (0,1)(1,0)  должны совпадать; главная диагональ - MaxInt т.к
                                                        //нет пути сам в себя. 
        private int[] dist;
        private bool[] used;
        private int[] prev;
        public Map()
        {
            /*array = new int[numOfCities, numOfCities];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = 65535;
           
            for (int i = 0; i < numOfRoads; i++)
            {
                Console.WriteLine("num of road" + i);
                int x = Convert.ToInt32(Console.ReadLine());
                int y = Convert.ToInt32(Console.ReadLine());
                int z = Convert.ToInt32(Console.ReadLine());
                array[x, y] = z;
                array[y, x] = z;
            }*/
            dist = new int[array.GetLength(0)];
            used = new bool[array.GetLength(0)];
            prev = new int[array.GetLength(0)];
        }
        public void print()
        {
            for (int i = 0; i < dist.GetLength(0); i++)
            {
                Console.Write(dist[i] + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < dist.GetLength(0); i++)
            {
                Console.Write(prev[i] + " ");
            }
        }
        public void printRout(int id)
        {
            if (prev[id] != -1) { printRout(prev[id]); }
            else return;
            Console.Write(prev[id] + " ");
        }
        public void solve(int Start_id, int Last_id)
        {
            for (int i = 0; i < dist.Length; i++)
            {
                dist[i] = 65535;
                prev[i] = -1;
            }
            dist[Start_id] = 0;
            while (true)
            {
                int min = -1;
                for (int nv = 0; nv < dist.Length; nv++)
                {
                    if (!used[nv] && dist[nv] < 65535 && (min == -1 || dist[min] > dist[nv]))
                        min = nv;
                }

                if (min == -1) break;
                used[min] = true;
                for (int nv = 0; nv < dist.Length; nv++)
                {
                    if (!used[nv] && array[min, nv] < 65535)
                    {
                        dist[nv] = dist[nv] < dist[min] + array[min, nv] ? dist[nv] : dist[min] + array[min, nv];
                        prev[nv] = min;
                    }
                }

            }
            if (prev[Last_id] == -1) { Console.WriteLine("no root"); return; }

            printRout(Last_id);
            Console.WriteLine(Last_id);
            Console.Write(dist[Last_id]);
        }






    }
}
