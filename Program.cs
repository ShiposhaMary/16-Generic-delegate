using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic_delegate
{
    public delegate int ObjectComparer<T>(T x, T y);

    public class Program
    {
        public static void Sort<T>(T[] array, ObjectComparer<T> comparer)
        {
            for (int i = array.Length - 1; i > 0; i--)
                for (int j = 1; j <= i; j++)
                {
                    var element1 = array[j - 1];
                    var element2 = array[j];
                    if (comparer(element1, element2) > 0)
                    {
                        var temporary = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temporary;
                    }
                }
        }

        static int CompareStringLength(string x, string y)
        {
            return x.Length.CompareTo(y.Length);
        }

        class Comparer
        {
            public bool Descending { get; set; }
            public int CompareStrings(string x, string y)
            {
                return x.CompareTo(y) * (Descending ? -1 : 1);
            }
        }

        static void Main()
        {
        //    var f = new Func<int, List<int>>[0];
        //    int result = (f[0](0))[0];

            var strings = new[] { "A", "B", "AA" };
            //Обратите внимание на вывод типов: по первому аргументу компилятор
            //догадывается, что T - это string, и понимает, что был передан правильный метод.
            Sort(strings, CompareStringLength);
            var obj = new Comparer { Descending = true };
            Sort(strings, obj.CompareStrings);
        }
    }
}
//Анонимный делегат позволяет не писать метод, а определить его по месту использования
//Компилятор напишет метод за вас и сам придумает ему имя и тип возвращаемого значения
//Это позволяет еще больше сократить объем инфраструктурного кода.
//Sort(strings, delegate (string x, string y)
//	{
//		return x.Length.CompareTo(y.Length);
//	});
//Лямбда-выражение - еще более краткая форма записи.
//Теперь компилятор догадывается не только до типа возвращаемого значения,
//но и до типа аргументов. 
//Обратите внимание, что типы во всей программе выводятся из строки с 
//объявлением массива!
//Sort(strings, (x, y) => x.Length.CompareTo(y.Length));