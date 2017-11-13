using System;

namespace Proxy
{
    public interface IAdd
    {
        int Add(int a, int b);
    }

    public interface IRemove
    {
        int Remove(int a, int b);
    }

    public interface IDevide
    {
        int Devide(int a, int b);
    }

    public interface IMultiple
    {
        int Multiple(int a, int b);
    }

    public interface IStandartCalculator : IAdd, IRemove, IMultiple, IDevide
    {
    }

    public class StandartCalculator : IStandartCalculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Devide(int a, int b)
        {
            return a / b;
        }

        public int Multiple(int a, int b)
        {
            return a * b;
        }

        public int Remove(int a, int b)
        {
            return a - b;
        }
    }

    public class StandartCalculatorProxy : IStandartCalculator
    {
        private readonly IStandartCalculator _standartCalculator;

        public StandartCalculatorProxy(IStandartCalculator standartCalculator)
        {
            _standartCalculator = standartCalculator;
        }

        public int Add(int a, int b)
        {
            return _standartCalculator.Add(a, b);
        }

        public int Devide(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException("The second parameter is 0.");
            return _standartCalculator.Devide(a, b);
        }

        public int Multiple(int a, int b)
        {
            return _standartCalculator.Multiple(a, b);
        }

        public int Remove(int a, int b)
        {
            return _standartCalculator.Remove(a, b);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new StandartCalculator();
            var proxy = new StandartCalculatorProxy(calculator);

            Console.WriteLine($"1 + 5 = {proxy.Add(1, 5)}");

            try
            {
                Console.WriteLine($"4 / 0 is:");
                proxy.Devide(4, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
