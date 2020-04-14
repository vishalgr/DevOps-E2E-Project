using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            HelperMethods helperMethods = new HelperMethods();
            var stringToPrint = helperMethods.ReturnTheInput("Hello World !!!");
            Console.WriteLine(stringToPrint);
        }
    }
}
