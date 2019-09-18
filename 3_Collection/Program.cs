using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryFastDecisions;
using static System.Console;
using System.Reflection;

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


        static Func <string, string, bool> СomparerString = (x, y) =>
        {
            if (x.Length <= y.Length)
                return x == y.Substring(0, x.Length);
            return false;
        };

        public class InfoFindInUniversityStudent
        {
            public string StartName = "";
            public string StartSurname = "";
            public string StartUniversity = "";
            public string StartDepartment = "";
            public string StartYearStudent = "";
            public string EndYearStudent = "";
            public string Сourse = "";
            public string Performance = "";
            public string StartCity = "";
        }



        static void Main(string[] args)
        {
            var arrayNumForOnlyNum = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8' };

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

            // Создадим необобщенный список
            ArrayList list = new ArrayList();
            // Запомним время в начале обработки данных
            DateTime dt = DateTime.Now;
            StreamReader sr = new StreamReader("..\\..\\students_1.csv");

            var info = new InfoFindInUniversityStudent();            
            Select(info, SamplingOptions(q.Question<string>("Выберите параметры выборки студентов (1 - имя, 2 - фамилия, 3 - университет, 4 - кафедра,  5 - сколько лет студенту" +
                ", 6 - курс, 7 - успеваемость, 8 - город). Вводите все необходимые параметры в любом порядке:", new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8' }, true)));

            var sumBM = new SumBakalavrMagistr();
            var lev = new LevelUn();
            var stud = new Student();
            var listSelectStudent = new List<string[]>();
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
                    GetStudent(listSelectStudent, info, s, СomparerString);
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

            //foreach (var v in stud.Sort1)
            //    foreach (var s in v.Value)
            //        Console.WriteLine($"{s[0]} {s[1]} в возрасте {v.Key}");

            //foreach (var v in stud.Sort2)
            //    foreach (var v2 in v.Value)
            //        foreach (var s in v2.Value)
            //            Console.WriteLine($"{s[0]} {s[1]} курса {v.Key} в возрасте {v2.Key}");

            foreach (var student in listSelectStudent)
                Console.WriteLine($"{student[0]} {student[1]} курса {student[6]} в возрасте {student[5]}");

            Console.WriteLine($"Всего выбрано студентов {listSelectStudent.Count}.");

            //foreach (var v in list) Console.WriteLine(v);
            //Вычислим время обработки данных
            Console.WriteLine(DateTime.Now - dt);
            Console.ReadKey();
        }

        public static void GetStudent(List<string[]> list, InfoFindInUniversityStudent info, string[] student, Func<string, string, bool> func)
        {
            var b = true;
            if (info.StartName != "" && b && !( func(info.StartName, student[0].ToLower()))) b = false;
            if (info.StartSurname != "" && b && !(func(info.StartSurname, student[1].ToLower()))) b = false;
            if (info.StartUniversity != "" && b && !(func(info.StartUniversity, student[2].ToLower()))) b = false;
            if (info.StartDepartment != "" && b && !(func(info.StartDepartment, student[4].ToLower()))) b = false;
            if ((info.StartYearStudent != "" && info.EndYearStudent != "") && b && !(
                ((info.StartYearStudent == "")? 0:int.Parse(info.StartYearStudent)) <= int.Parse(student[5]) && 
                int.Parse(student[5]) <= ((info.EndYearStudent == "") ? 0 : int.Parse(info.EndYearStudent)))) b = false;
            if (info.Сourse != "" && b && !(func(info.Сourse, student[6].ToLower()))) b = false;
            if (info.Performance != "" && b && !(func(info.Performance, student[7].ToLower()))) b = false;
            if (info.StartCity != "" && b && !(func(info.StartCity, student[8].ToLower()))) b = false;
            if (b) list.Add(student);
        }

        static Action<InfoFindInUniversityStudent, HashSet<int>> Select = (info, listSelect) =>
        {
            var q = new Questions();
            var arrayEng_NumSymbol = new HashSet<char>() { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', };
            var arrayNumForOnlyNum2 = new HashSet<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var arrayNumForOnlyNum3 = new HashSet<char>() { '1', '2', '3', '4', '5', '6' };
            foreach (var e in listSelect)
            {
                switch (e)
                {
                    case 1: info.StartName = q.Question<string>("Начальные буквы имени?", arrayEng_NumSymbol, true).ToLower(); break;
                    case 2: info.StartSurname = q.Question<string>("Начальные буквы фамилия?", arrayEng_NumSymbol, true).ToLower(); break;
                    case 3: info.StartUniversity = q.Question<string>("Начальные буквы университета?", arrayEng_NumSymbol, true).ToLower(); break;
                    case 4: info.StartDepartment = q.Question<string>("Начальные буквы кафедры?", arrayEng_NumSymbol, true).ToLower(); break;
                    case 5:
                        {
                            info.StartYearStudent = q.Question<string>("Диапазон возроста от (лет)?", arrayNumForOnlyNum2, true);
                            info.EndYearStudent = q.Question<string>("Диапазон возроста до (лет)?", arrayNumForOnlyNum2, true);
                            break;
                        }
                    case 6: info.Сourse = q.Question<string>("Курс?", arrayNumForOnlyNum3, true).ToLower(); break;
                    case 7: info.Performance = q.Question<string>("Успеваемость?", arrayNumForOnlyNum3, true).ToLower(); break;
                    case 8: info.StartCity = q.Question<string>("Начальные буквы города?", arrayEng_NumSymbol, true).ToLower(); break;
                    default:
                        Console.WriteLine("Вы выбрали что то не то.");
                        break;
                }
            }
        };

        //public static HashSet<int> SamplingOptions(string answer)
        //{
        //    var hash = new HashSet<int>();
        //    foreach(var t in answer)            
        //        hash.Add(int.Parse(new string(new char[] { t })));            
        //    return hash;
        //}

        static Func<string, HashSet<int>> SamplingOptions = answer =>
        {
            var hash = new HashSet<int>();
            foreach (var t in answer)
                hash.Add(int.Parse(new string(new char[] { t })));
            return hash;
        };
    }

}
