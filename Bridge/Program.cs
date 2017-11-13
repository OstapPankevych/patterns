using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    class Program
    {
        public interface ICoffeeMakerModule
        {
            void MakeCofee();
        }

        public class AmericanoCoffeeMaker : ICoffeeMakerModule
        {
            public void MakeCofee()
            {
                Console.WriteLine("Making 'Americano' ...");
                Console.WriteLine("Americano ready!");
            }
        }

        public class LatteCoffeeMaker : ICoffeeMakerModule
        {
            public void MakeCofee()
            {
                Console.WriteLine("Making 'Latte' ...");
                Console.WriteLine("Latte ready!");
            }
        }

        public abstract class CoffeeMashine
        {
            public ICoffeeMakerModule CoffeeMaker { get; set; }

            protected CoffeeMashine(ICoffeeMakerModule defaultCoffeeMaker)
            {
                CoffeeMaker = defaultCoffeeMaker;
            }

            public abstract void MakeCoffee();
        }

        public class ClassicCoffeeMashine : CoffeeMashine
        {
            public ClassicCoffeeMashine(ICoffeeMakerModule coffeeMaker)
                : base(coffeeMaker)
            {}

            public override void MakeCoffee()
            {
                CoffeeMaker.MakeCofee();
            }
        }

        public class WiFiCoffeeMashine : CoffeeMashine
        {
            public WiFiCoffeeMashine(ICoffeeMakerModule coffeeMaker)
                : base(coffeeMaker)
            { }

            public override void MakeCoffee()
            {
                SetAsideGlass();
                PutUnderGlass();
                CoffeeMaker.MakeCofee();
            }

            protected void PutUnderGlass()
            {
                Console.WriteLine("Putting glass under.");
            }

            protected void SetAsideGlass()
            {
                Console.WriteLine("Setting glass aside if need.");
            }
        }

        static void Main(string[] args)
        {
            var latteMaker = new LatteCoffeeMaker();
            var americanoMaker = new AmericanoCoffeeMaker();

            Console.WriteLine("Classic Coffee Mashine");
            Console.WriteLine("*******************************");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Command: Make americano...");
            var classicCoffeeMashine = new ClassicCoffeeMashine(americanoMaker);
            classicCoffeeMashine.MakeCoffee();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Command: Make latte...");
            classicCoffeeMashine.CoffeeMaker = latteMaker;
            classicCoffeeMashine.MakeCoffee();
            Console.WriteLine("*******************************");

            Console.WriteLine("WiFi Coffee Mashine");
            Console.WriteLine("*******************************");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Command: Make americano...");
            var wifiCoffeeMashine = new WiFiCoffeeMashine(americanoMaker);
            wifiCoffeeMashine.MakeCoffee();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Command: Make latte...");
            wifiCoffeeMashine.CoffeeMaker = latteMaker;
            wifiCoffeeMashine.MakeCoffee();
            Console.WriteLine("*******************************");

            Console.ReadLine();
        }
    }
}
