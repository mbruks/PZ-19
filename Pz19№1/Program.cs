using System;
using System.IO;

namespace Pz19_1
{
    internal struct Edge
    {
        public int first;
        public int second;

        public void DisplayInfo()
        {
            Console.WriteLine("First: " + first + " " + "Second: " + second);
        }
    }

    internal class Program
    {
        private static bool NextCombobj(int[] soc, int n, int k)
        {
            for (int i = k - 1; i >= 0; --i)//начинаем идти с конца в начало
                if (soc[i] < n - k + i)
                {
                    soc[i]++;//берем следующий элемент

                    for (int j = i + 1; j < k; j++)//следующий элемент меняем на предыдущий + 1
                        soc[j] = soc[j - 1] + 1;

                    return true;
                }
            return false;
        }

        static public void Reset(ref int[,] arr, int n, int m) //Обнуление массива
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    arr[i, j] = 0;
        }

        static public void PrintArray(int[,] arr, int n, int m, StreamWriter sw)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    sw.Write(arr[i, j] + " ");

                sw.WriteLine();
            }
            sw.WriteLine(" ");
        }

        private static void Main()
        {
            //Время работы программы
            DateTime time1 = DateTime.Now;
            for (int i = 0; i < 200000000; i++) { }
            DateTime time2 = DateTime.Now;
            Console.WriteLine("Время выполнения: {0}", (time2 - time1).Milliseconds);

            StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\Pz19№1\Pz19№1\txt.txt");
            int p, k, q;

            Console.Write("Введите количество вершин = ");
            p = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите количество ребер = ");
            q = Convert.ToInt32(Console.ReadLine());

            k = 2;

            int[] soc = new int[k];

            for (int i = 0; i < k; i++)
                soc[i] = i;

            int m = 0;//счетчик ребер

            Edge[] edge = new Edge[50];//Массив ребер

            edge[m].first = soc[0];
            edge[m].second = soc[1];
            m++;
            if (p > k)
            {
                //Построения всех возможных ребер в заданном графе
                while (NextCombobj(soc, p, k))
                {
                    edge[m].first = soc[0];
                    edge[m].second = soc[1];
                    m++;
                }
            }
            else Console.WriteLine("Условие не выполняется");

            Console.WriteLine("Максимальное количество ребер: " + m);

            for (int i = 0; i < m; i++)
            {
                edge[i].DisplayInfo(); //Вывод всех ребер
            }

            if (q < m)
            {
                Console.WriteLine("Строим сочитания из ребер");

                int kol_P_Q = 0;//Счетчик числа p,q графов

                int[,] Matrix = new int[p, p];
                Reset(ref Matrix, p, p);

                int[] socq = new int[q];

                for (int i = 0; i < q; i++)
                {
                    socq[i] = i;
                    Console.Write(socq[i] + " ");
                    //Заполняем таблицу смежности
                    Matrix[edge[socq[i]].first, edge[socq[i]].second] = 1;
                    Matrix[edge[socq[i]].second, edge[socq[i]].first] = 1;
                }
                Console.WriteLine();

                kol_P_Q++;
                PrintArray(Matrix, p, p, sw);
                Reset(ref Matrix, p, p);

                if (p == m) Console.WriteLine("1 граф");
                else
                {
                    //Строим сочитания из ребер
                    while (NextCombobj(socq, m, q))
                    {
                        for (int i = 0; i < q; i++)
                        {
                            Console.Write(socq[i] + " ");
                            //Заполняем таблицу смежности
                            Matrix[edge[socq[i]].first, edge[socq[i]].second] = 1;
                            Matrix[edge[socq[i]].second, edge[socq[i]].first] = 1;
                        }
                        Console.WriteLine();

                        PrintArray(Matrix, p, p, sw); //выводим в файл
                        Reset(ref Matrix, p, p); //обнуляем
                        kol_P_Q++;
                    }
                }

                sw.WriteLine("Количество графов - " + kol_P_Q);
                sw.Close();
            }
            else Console.WriteLine("Число заданных ребер больше допустимого числа");
            Console.WriteLine("Программа завершена");
            Console.ReadKey();
        }
    }
}