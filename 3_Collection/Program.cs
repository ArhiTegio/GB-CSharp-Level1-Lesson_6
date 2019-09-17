using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;

namespace _3_Collection
{
    delegate void SimpleDelegateBool(string[] x, Info info);

    class Info
    {

    }

    class SumBakalavrMagistr : Info
    {
        public int Bakalavr = 0;
        public int Magistr = 0;
    }
    class LevelUn : Info
    {
        public int[] Level = new int[6];
    }
    class Student : Info
    {
        public SortedDictionary<string, SortedDictionary<string, int>> S = new SortedDictionary<string, SortedDictionary<string, int>>()
        {
            ["18"] = new SortedDictionary<string, int>() { ["1"] = 0, ["2"] = 0, ["3"] = 0, ["4"] = 0, ["5"] = 0, ["6"] = 0, },
            ["19"] = new SortedDictionary<string, int>() { ["1"] = 0, ["2"] = 0, ["3"] = 0, ["4"] = 0, ["5"] = 0, ["6"] = 0, },
            ["20"] = new SortedDictionary<string, int>() { ["1"] = 0, ["2"] = 0, ["3"] = 0, ["4"] = 0, ["5"] = 0, ["6"] = 0, },
        };
        public SortedDictionary<string, List<string[]>> Sort1 = new SortedDictionary<string, List<string[]>>();
        public SortedDictionary<string, SortedDictionary<string, List<string[]>>> Sort2 = new SortedDictionary<string, SortedDictionary<string, List<string[]>>>();
    }

    class Program
    {
        public static List<SimpleDelegateBool> ListD = new List<SimpleDelegateBool>()
        {
            new SimpleDelegateBool ((x, d) => 
            {
                if(d is SumBakalavrMagistr)
                {
                    var info = (SumBakalavrMagistr)d;
                    if(int.Parse(x[6]) < 5)
                        info.Bakalavr++;
                    else info.Magistr++;
                }
                else                
                    throw new Exception();     
            }),
            //а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
            new SimpleDelegateBool ((x, d) =>
            {
                if(d is LevelUn)
                    ((LevelUn)d).Level[int.Parse(x[6]) - 1]++;
                else
                    throw new Exception();
            }),

            //б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив); 
            new SimpleDelegateBool ((x, d) =>
            {
                if(d is Student)
                {
                    var info = (Student)d;
                    if(info.S.ContainsKey(x[5]))
                    {
                        if(info.S[x[5]].ContainsKey(x[6]))
                            info.S[x[5]][x[6]]++;
                        else
                            info.S[x[5]].Add(x[6], 1);
                    }
                    else
                        info.S.Add(x[5], new SortedDictionary<string, int>() {[x[6]] = 1 });
                }
                else
                    throw new Exception();
            }),

            //в) отсортировать список по возрасту студента;
            new SimpleDelegateBool ((x, d) =>
            {
                if(d is Student)
                {
                    var info = (Student)d;
                    if(info.Sort1.ContainsKey(x[5]))
                    {
                        info.Sort1[x[5]].Add(x);
                    }
                    else
                        info.Sort1.Add(x[5], new List<string[]>(){ x });
                }
                else
                    throw new Exception();
            }),

            //г) *отсортировать список по курсу и возрасту студента;
            new SimpleDelegateBool ((x, d) =>
            {
                if(d is Student)
                {
                    
                    var info = (Student)d;
                    if(info.Sort2.ContainsKey(x[6]))
                    {
                        if(info.Sort2[x[6]].ContainsKey(x[5]))
                            info.Sort2[x[6]][x[5]].Add(x);
                        else
                            info.Sort2[x[6]].Add(x[5], new List<string[]>() { x });
                    }
                    else
                        info.Sort2.Add(x[6], new SortedDictionary<string, List<string[]>>() {[x[5]] = new List<string[]>(){ x } });
                }
                else
                    throw new Exception();
            }),
        };



        static void Main(string[] args)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };
            var ex = new Extension();
            var q = new Questions();
            WriteLine("С# - Уровень 1. Задание 6.3");
            WriteLine("Кузнецов");
            WriteLine(
                "3. Переделать программу «Пример использования коллекций» для решения следующих задач:" + Environment.NewLine +
                "   а) Подсчитать количество студентов учащихся на 5 и 6 курсах; " + Environment.NewLine +
                "   б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся(частотный массив); " + Environment.NewLine +
                "   в) отсортировать список по возрасту студента; " + Environment.NewLine +
                "   г) *отсортировать список по курсу и возрасту студента; " + Environment.NewLine +
                "   д) разработать единый метод подсчета количества студентов по различным параметрам" + Environment.NewLine +
                "      выбора с помощью делегата и методов предикатов.");


            int bakalavr = 0;
            int magistr = 0;
            // Создадим необобщенный список
            ArrayList list = new ArrayList();
            // Запомним время в начале обработки данных
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("..\\..\\students_1.csv");

            var sumBM = new SumBakalavrMagistr();
            var lev = new LevelUn();
            var stud = new Student();
        
            while (!sr.EndOfStream)
            {
                try
                {
                    string[] s = sr.ReadLine().Split(';');
                    // Console.WriteLine("{0}", s[0], s[1], s[2], s[3], s[4]);
                    list.Add($"{s[1]} {s[0]}");// Добавляем склееные имя и фамилию

                    ListD[0](s, sumBM);
                    ListD[1](s, lev);
                    ListD[2](s, stud);
                    ListD[3](s, stud);
                    ListD[4](s, stud);

                    //if (int.Parse(s[6]) < 5)
                    //    bakalavr++;
                    //else magistr++;
                }
                catch
                {
                }
            }
            sr.Close();
            list.Sort();
            Console.WriteLine("Всего студентов:{0}", list.Count);
            Console.WriteLine("Магистров:{0}", sumBM.Magistr);
            Console.WriteLine("Бакалавров:{0}", sumBM.Bakalavr);
            Console.WriteLine("Учашихся на 5 курсе:{0}", lev.Level[4]);
            Console.WriteLine("Учашихся на 6 курсе:{0}", lev.Level[5]);
            for (int i = 1; i < 7; i++)
                Console.WriteLine($"Учашихся на на {i} курсе в возросте 18 лет:{stud.S["18"][i.ToString()]} 19 лет:{stud.S["19"][i.ToString()]} 20 лет:{stud.S["20"][i.ToString()]}");

            foreach (var v in stud.Sort1)
                foreach(var s in v.Value)
                    Console.WriteLine($"{s[0]} {s[1]} в возрасте {v.Key}");

            foreach (var v in stud.Sort2)
                foreach (var v2 in v.Value)
                    foreach(var s in v2.Value)
                        Console.WriteLine($"{s[0]} {s[1]} курса {v.Key} в возрасте {v2.Key}");



            //foreach (var v in list) Console.WriteLine(v);
            //Вычислим время обработки данных
            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }
    }

}
