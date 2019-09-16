using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;

namespace _1_PresentationFuncDelegates
{
    delegate double SimpleDelegate(double x, double y);

    class Program
    {
        static void Main(string[] args)
        {
            var arrayEng_NumSymbol = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
                'а', 'б', 'в'
            };
            var arrayEng_NumSymbol2 = new HashSet<char>()
            {
                'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m',
                'Q','W','E','R','T','Y','U','I','O','P','A','S','D','F','G','H','J','K','L','Z','X','C','V','B','N','M',
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
            };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 6.1");
            WriteLine("Кузнецов");
            WriteLine(
                "1. Изменить программу вывода функции так, чтобы можно было передавать функции типа double (double,double)." + Environment.NewLine +
                "   Продемонстрировать работу на функции с функцией a*x^2 и функцией a*sin(x).");

            MyFunc(MathFunc1a, 5.5, 10.5);
            MyFunc(MathFunc2a, 7.5, 15.5);

            MyFuncB(MathFunc1b, 5.5, 10.5);
            MyFuncB(MathFunc2b, 7.5, 15.5);

            ex.Pause();
        }
        static void MyFunc(SimpleDelegate f, double w, double n)
        {
            WriteLine(f(w, n));
        }

        static Action<Func<double, double, double>, double, double> MyFuncB = (f, w, n) =>
        {
            WriteLine(f(w, n));
        };

        static Func<double, double, double> MathFunc1b = (x, a) => a * (x * x);

        static Func<double, double, double> MathFunc2b = (x, a) => a * Math.Sin(x);

        static double MathFunc1a (double x, double a) => a * (x * x);

        static double MathFunc2a (double x, double a) => a * Math.Sin(x);
    }


}
