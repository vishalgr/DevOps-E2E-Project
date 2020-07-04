using System;

namespace DevOps.Tests.Production
{
    class Program
    {
         static void Main(string[] args)
        {
            int num1 = 10;
            int num2 = 14;
            Helper a = new Helper();
            int p = a.add(num1, num2);
            Console.WriteLine(p);
            randomnumbergenerator r = new randomnumbergenerator();
            int q = r.RandomNumber();

            Console.WriteLine(q);
        }
    }
    public class Helper
    {

        public int add(int a, int b)
        {

            int x = a + b;
            return x;
        }

    }

    public class randomnumbergenerator
    {
        public int RandomNumber()
        {
            System.Random random = new System.Random();
            int num = random.Next(1, 50);
            return num;
        }
    }
}
