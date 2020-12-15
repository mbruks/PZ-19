using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ПЗ19__1
{
	struct Edge
	{
		public int first;
		public int second;

		public void DisplayInfo()
		{
			Console.WriteLine("First: " + first + " " + "Second: " + second);
		}
	}
	class Program
	{
		static void swap(int[] raz, int i, int j)
		{
			int bufer = raz[i];
			raz[i] = raz[j];
			raz[j] = bufer;
		}
		static bool NextCombobjsoc(int[] soc, int n, int k)
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
		static bool NextCombobjraz(int[] raz, int n, int k)
		{
			int j;
			do
			{
				j = n - 1;
				while (j != -1 && raz[j] >= raz[j + 1]) j--; //ищем пока следующий элемент будет больше предыдущего

				if (j == -1)
					return false; //  размещений нет
				int l = n - 1;


				while (raz[j] >= raz[l]) l--; //ищем элемент больше j
				swap(raz, j, l);

				int q = j + 1, p = n - 1; // сортируем за j
				while (q < p) swap(raz, q++, p--);

			} while (j > k - 1); // повторяем пока не будет найдено следующее размещение

			return true;
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
		static void Main()
		{
			//Время работы программы
			DateTime time1 = DateTime.Now;
			for (int i = 0; i < 200000000; i++) { }
			DateTime time2 = DateTime.Now;
			Console.WriteLine("Время выполнения: {0}", (time2 - time1).Milliseconds);

			StreamWriter sw = new StreamWriter(@"C:\Users\Admin\source\repos\PZ№6\PZ№6\txt.txt");
			int p, k, q;

			Console.Write("Введите количество вершин = ");
			p = Convert.ToInt32(Console.ReadLine());

			Console.Write("Введите количество ребер = ");
			q = Convert.ToInt32(Console.ReadLine());

			k = 2;
			int[] raz = new int[p + 1];

			for (int i = 0; i < p; i++)
				raz[i] = i;

			int m = 0;//счетчик ребер

			Edge[] edge = new Edge[500];//Массив ребер

			edge[m].first = raz[0];
			edge[m].second = raz[1];
			m++;

			//Построения всех возможных ребер в заданном графе
			while (NextCombobjraz(raz, p, k))
			{
				edge[m].first = raz[0];
				edge[m].second = raz[1];
				m++;
			}

			Console.WriteLine("Максимальное количество ребер: " + m);

			for (int i = 0; i < m; i++)
			{
				edge[i].DisplayInfo(); //Вывод всех ребер
			}


			if (q < m)
			{
				Console.WriteLine($"Строим сочитания из ребер");



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
				}
				Console.WriteLine();

				kol_P_Q++;
				PrintArray(Matrix, p, p, sw);
				Reset(ref Matrix, p, p);


				if (p == m) Console.WriteLine("1 граф");
				else
				{

					//Строим сочитания из ребер
					while (NextCombobjsoc(socq, m, q))
					{
						for (int i = 0; i < q; i++)
						{
							Console.Write(socq[i] + " ");
							//Заполняем таблицу смежности
							Matrix[edge[socq[i]].first, edge[socq[i]].second] = 1;
						}
						Console.WriteLine();

						PrintArray(Matrix, p, p, sw); 
						Reset(ref Matrix, p, p); //обнуляем
						kol_P_Q++;
					}

				}


				sw.WriteLine("Количество ориентированных графов - " + kol_P_Q);
				sw.Close();
			}
			else Console.WriteLine("Число заданных ребер больше допустимого числа");
			Console.WriteLine("Программа завершена");

			Console.ReadKey();
		}
	}
}
