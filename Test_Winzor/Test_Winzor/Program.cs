using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Winzor
{
    class Program
    {
        static void Main(string[] args)
        {
                int[] a1 = new int[] { 2, 3, 17, 6, 1 };
                int[] a2 = new int[] { 17, 38, 9, 63, 41, 15 };
                int[] a3 = new int[] { 7, 82, 33, 33, 33, 1, 56 };

                Console.Write("Winsor 1 = {0}\n", Winsorizing(a1, 2 / 2));
                Console.Write("Winsor 2 = {0}\n", Winsorizing(a2, 4 / 2));
                Console.Write("Winsor 3 = {0}\n", Winsorizing(a3, 2 / 2));

                Console.ReadKey();
            }
        static float Winsorizing(int[] a, int m)
        {
            int i = 0;
            int y = 0;
            int sum = 0;
            int max1 = a[0];
            int min1 = a[0];
            int max2 = a[0];
            int min2 = a[0];
            int tmp_min = a[0];
            int tmp_max = a[0];

            //Первый поиск первых пределов
            foreach (int el in a)
                if (max1 < el) max1 = el;

            foreach (int el in a)
                if (min1 > el) min1 = el;

            //Гарантируем что вторые пределы не равны первым
            max2 = min1;
            min2 = max1;

            //Цикл задает глубину винзоризации
            for (y = 0; y < m; y++)
            {
                //Поиск вторых пределов
                foreach (int el in a)
                    if ((max2 < el) && (el < max1))
                        max2 = el;

                for (i = 0; i < a.Count(); i++)
                    if (a[i] == max1)
                        a[i] = max2;

                //Замена пределов
                foreach (int el in a)
                    if ((min2 > el) && (el > min1))
                        min2 = el;

                for (i = 0; i < a.Count(); i++)
                    if (a[i] == min1)
                        a[i] = min2;

                //Сохраняем пределы и гарантируем неравенство первых и вторых пределов
                tmp_min = max1;
                tmp_max = min1;
                max1 = max2;
                min1 = min2;
                max2 = tmp_max;
                min2 = tmp_min;

            }
            foreach (int el in a)
                sum += el;
            return (float)sum / a.Count();
        }

    }
}
