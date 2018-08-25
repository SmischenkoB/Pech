using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pech
{
    class Node {
       public int StorageLand { get;private set; }
       public int StorageSea { get; private set; }
       //public int CoordinateX { get;  set; }
       //public int CoordinateY { get;  set; }
    }
    class City : Node { }






    class Map
    {
        static readonly public int[,] arrayCar = {{ 65535, 15, 7 },   // првила заполнения    МАТРИЦА - КВАДРАТНАЯ (3*3 или n*n)
                                { 15, 65535, 5 },   // если пути нет, ставится MaxInt(65535)
                                { 7, 5, 65535  } };// симметрия относительно главной диагонали, т.е значения в ячейках i,j и j,i
                                                   // (0,1)(1,0)  должны совпадать; главная диагональ - MaxInt т.к
                                                   //нет пути сам в себя. 
        static readonly public int[,] arrayTrain = { { 65535, 65535, 3 }, { 65535, 65535, 1 }, { 3, 7, 65535 } };
        
        private int[] dist;
        private bool[] used;
        private int[] prev;
        private int[,] arrayInside;
        public Map(int[,] arr)
        {
            arrayInside = arr;
            dist = new int[arr.GetLength(0)];
            used = new bool[arr.GetLength(0)];
            prev = new int[arr.GetLength(0)];
        }
        public void AddNodeToMap()
        {
            int[,] temparr = new int[arrayInside.GetLength(0) + 1, arrayInside.GetLength(1) + 1];
            for (int i = 0; i < arrayInside.GetLength(0); i++)
            {
                for (int j = 0; j < arrayInside.GetLength(1); j++)
                {
                    temparr[i, j] = arrayInside[i, j];
                }
                temparr[temparr.GetLength(0) - 1, i] = 65535;
                temparr[i, temparr.GetLength(0) - 1] = 65535;
            }
            temparr[temparr.GetLength(0) - 1, temparr.GetLength(0) - 1] = 65535;
            
            dist = new int[temparr.GetLength(0)];
            used = new bool[temparr.GetLength(0)];
            prev = new int[temparr.GetLength(0)];

            arrayInside = temparr;
        }
        public void AddRoad(int x, int y, int price)
        {
            if (x >= arrayInside.GetLength(0) || y >= arrayInside.GetLength(1))
            { Console.WriteLine("Error of adding new Road"); return; }
            arrayInside[x, y] = price;
            arrayInside[y, x] = price;
        }
        public void print()/*расстояния от начального города и город - предшественник*/
        {
            Console.WriteLine("Расстояние до города от начальной точки");
            for (int i = 0; i < dist.GetLength(0); i++)
            {                
                Console.Write(dist[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Город из которого идёт дорога");
            for (int i = 0; i < dist.GetLength(0); i++)
            {
              
                Console.Write(prev[i] + " ");
            }
        }
        public void printRout(int id)/*возвращает путь от начального города к конечному*/
        {
            if (prev[id] != -1) { printRout(prev[id]); }
            else { Console.Write(id + " "); return; }
            Console.Write(id + " ");
        }
        public void printMap()/*возвращает карту*/
        {
            for(int i = 0; i < arrayInside.GetLength(0); i++)
            {
                for(int j = 0; j < arrayInside.GetLength(1); j++)
                {
                    Console.Write(arrayInside[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void solve(int Start_id, int Last_id)
        {
            if (Start_id > arrayInside.GetLength(0) || Last_id > arrayInside.GetLength(1) || arrayInside == null)
            {
                Console.WriteLine("wrong input");return;
            }

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
                    if (!used[nv] && arrayInside[min, nv] < 65535)
                    {
                        dist[nv] = dist[nv] < dist[min] + arrayInside[min, nv] ? dist[nv] : dist[min] + arrayInside[min, nv];
                        prev[nv] = min;
                    }
                }

            }
            if (prev[Last_id] == -1) { Console.WriteLine("no root"); return; }

            printRout(Last_id);
        }
        
    }
}
