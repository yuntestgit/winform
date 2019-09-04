using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace boss
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0, i, temp = 0, temp1;
            string a1, a2 = "";
            Console.WriteLine("請輸入:");
            a = int.Parse(Console.ReadLine());
            do
            {
                temp1 = a % 10;
                a /= 10;
                temp = temp * 10 + temp1;
            } while (a != 0);
            a1 = temp.ToString();

            for (i = 0; i <= a1.Length - 1; i++)
            {
                a2 += a1[i];
                if (i % 3 == 2 && i != a1.Length - 1)
                {
                    a2 += ",";
                }
            }

            for (i = a2.Length - 1; i >=0; i--)
            {
                Console.Write(a2[i]);
            }

            Console.ReadLine();

        }
    }
}