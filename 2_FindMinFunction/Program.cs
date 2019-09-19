using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;


namespace _2_FindMinFunction
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var ex = new Extension();
//            var q = new Questions();
//            WriteLine("С# - Уровень 1. Задание 6.1");
//            WriteLine("Кузнецов");
//            WriteLine(
//                "2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата." + Environment.NewLine +
//                "   а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум." + Environment.NewLine +
//                "   б) Используйте массив(или список) делегатов, в котором хранятся различные функции." + Environment.NewLine +
//                "   в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений.Пусть она" + Environment.NewLine +
//                "   возвращает минимум через параметр.");


//            ex.Pause();

//        }
//    }


//    using System;
//using System.IO;
//namespace DoubleBinary
{
    delegate double SimpleDelegate(double x);
    delegate bool SimpleDelegateBool(double x, double y);
    class Program
    {
        /// <summary>
        /// Список делегатов математических выражений
        /// </summary>
        public static List<SimpleDelegate> ListD = new List<SimpleDelegate>()
        {
            new SimpleDelegate (x => x * x - 50 * x + 10),
            new SimpleDelegate (x => Math.Cos(x)),
            new SimpleDelegate (x => Math.Sin(x)),
        };

        /// <summary>
        /// Список делегатов булевого типа
        /// </summary>
        public static List<SimpleDelegateBool> ListParamD = new List<SimpleDelegateBool>()
        {
            new SimpleDelegateBool ((x,min) => x < min),
            new SimpleDelegateBool ((x,min) => x > min),
            new SimpleDelegateBool ((x,min) => x < (min * min)/2),
        };
        /// <summary>
        /// Запись параметров
        /// </summary>
        /// <param name="d"></param>
        /// <param name="fileName"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="h"></param>
        public static void SaveFunc(SimpleDelegate d, string fileName, double a, double b, double h)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(d(x));
                x += h;// x=x+h;
            }
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// Реализация загрузки из файла с быстрым выводом значений на экран
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="fileName"></param>
        /// <param name="delegateBool"> Делегат выборки минимума</param>
        /// <returns></returns>
        public static IEnumerable<double> Load(double[] answer, string fileName, SimpleDelegateBool delegateBool)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            var l = new List<double>();
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (delegateBool(d, min))
                    min = d;
                //Возвращаем полученное значение перечеслителю
                yield return d;
            }
            bw.Close();
            fs.Close();
            answer[0] = min;
            yield break;
        }
        static void Main(string[] args)
        {

            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-'};
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 6.2");
            WriteLine("Кузнецов");
            WriteLine(
                "2. Модифицировать программу нахождения минимума функции так, чтобы можно было передавать функцию в виде делегата." + Environment.NewLine +
                "   а) Сделайте меню с различными функциями и предоставьте пользователю выбор, для какой функции и на каком отрезке находить минимум." + Environment.NewLine +
                "   б) Используйте массив(или список) делегатов, в котором хранятся различные функции." + Environment.NewLine +
                "   в) *Переделайте функцию Load, чтобы она возвращала массив считанных значений.Пусть она" + Environment.NewLine +
                "   возвращает минимум через параметр.");
            var n1 = int.Parse(q.Question<int>("Задайте начальный диапазон нахождения минимума:", arrayNumForOnlyNum, true));
            var n2 = int.Parse(q.Question<int>("Задайте конечный  диапазон нахождения минимума:", arrayNumForOnlyNum, true));

            WriteLine("Укажите функцию которая будет использоваться 1 - (x * x - 50 * x + 10), 2 - Cos(x), 3 - Sin(x).");
            var answer = ReadKey();
            if (answer.Key == ConsoleKey.NumPad1 || answer.Key == ConsoleKey.D1)            
                SaveFunc(ListD[0], "data.bin", Math.Min(n1, n2), Math.Max(n1, n2), 0.5);           
            if (answer.Key == ConsoleKey.NumPad2 || answer.Key == ConsoleKey.D2)            
                SaveFunc(ListD[1], "data.bin", Math.Min(n1, n2), Math.Max(n1, n2), 0.5);
            if (answer.Key == ConsoleKey.NumPad3 || answer.Key == ConsoleKey.D3)
                SaveFunc(ListD[2], "data.bin", Math.Min(n1, n2), Math.Max(n1, n2), 0.5);
            var answerD = new double[1];
            foreach (var deleg in ListParamD)
            {
                foreach(var u in Load(answerD, "data.bin", deleg))
                    Console.Write($"{u}, ");
                Console.WriteLine(Environment.NewLine + $"Ответ: {answerD[0]}");
            }

            Console.ReadKey();
        }
    }
}