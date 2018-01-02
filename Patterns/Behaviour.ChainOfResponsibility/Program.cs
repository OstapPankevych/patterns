using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Behaviour.ChainOfResponsibility
{
    public class Program
    {
        static void Main(string[] args)
        {
            var list = new[] { 0, 4, 5, 5, 2, -1 };

            var multiplier2 = new MultiplierHandler(null, 2);
            var zeroValidator = new ZeroValidationHandler(multiplier2);
            var multiplier4 = new MultiplierHandler(zeroValidator, 4);

            PrintArray(list);
            multiplier4.Handle(list);
            PrintArray(list);

            Console.ReadLine();
        }

        private static void PrintArray(IEnumerable<int> array)
        {
            Console.Write('[');
            foreach (var x in array)
            {
                Console.Write($" <{x}> ");
            }
            Console.WriteLine(']');
        }
    }
}
