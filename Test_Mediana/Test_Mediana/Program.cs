using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Mediana
{
    class Program
    {
        static void Main(string[] args)
        {
            String in_str = "";
            int tmp;
            int window;
            List<int> signal = new List<int>();
            List<int> m_signal = new List<int>();


            //Ввод даных и проверка их корректности

            Console.WriteLine("Введите массив (пустая строка - окончание ввода)");
            do
            {
                Console.Write("Введите число: ");
                in_str = Console.ReadLine();
                if (int.TryParse(in_str, out tmp)) signal.Add(tmp);
                else continue;
            } while (in_str != "");

            Console.WriteLine("\nМассив: ");
            foreach (int e in signal) Console.Write("{0} ", e);

            do
            {
                Console.WriteLine("\n\nВведите размер окна: ");
                in_str = Console.ReadLine();
            } while (!int.TryParse(in_str, out window));

            int dx;
            int x_out = window / 2;
            if (window % 2 > 0) dx = (window / 2) + 1;
            else dx = window / 2;

            if ((signal.Count() < 1) || (window < 1) || (signal.Count() < window))
            {
                Console.WriteLine("Некорректные данные");
                Console.ReadKey();
                return;
            }

            int[] tmp_signal = new int[window];
            int right_edge, art_signal_num, m;
            int i;

            //Часть данных дублируются, т.к. окно выходит за пределы массива в начале вычисления
            for (i = 0; i < dx; i++)
            {
                right_edge = dx + i; //Расчет количества элеменотов в пределах массива
                art_signal_num = window - right_edge; //Расчет количества искуственно добавленых элементов
                signal.CopyTo(0, tmp_signal, 0, right_edge);
                signal.CopyTo(0, tmp_signal, right_edge, art_signal_num);

                if (Mediana(tmp_signal, out m)) m_signal.Add(m);
            }

            //Окно в пределах массива
            for (i = i - x_out; i <= signal.Count() - window; i++)
            {
                signal.CopyTo(i, tmp_signal, 0, window);

                if (Mediana(tmp_signal, out m)) m_signal.Add(m);
            }
            //Часть данных дублируются, т.к. окно выходит за пределы массива в конце вычисления
            for (; i < signal.Count() - x_out; i++)
            {
                art_signal_num = (i + window) - signal.Count();
                signal.CopyTo(i, tmp_signal, 0, window - art_signal_num);
                signal.CopyTo(signal.Count() - art_signal_num, tmp_signal, window - art_signal_num, art_signal_num);

                if (Mediana(tmp_signal, out m)) m_signal.Add(m);
            }


            Console.Write("\n\nРезультат: ");
            foreach (int e in m_signal) Console.Write("{0} ", e);

            Console.ReadKey();
        }
        static bool Mediana(int[] arr, out int mediana)
        {
            mediana = 0;
            if (arr.Count() == 0) return false;
            if (arr.Count() == 1)
            {
                mediana = arr[0];
                return true;
            }
            List<int> l = arr.ToList();

            l.Sort();

            if (l.Count() % 2 == 0)
                mediana = (int)Math.Round((float)(l[l.Count / 2] + l[(l.Count / 2) + 1]) / 2);
            else mediana = l[l.Count / 2];
            return true;
        }
    }
}
