using System;
using System.Collections.Generic;

namespace IoC
{
    interface IContainer
    {
        TTypeToResolve Resolve<TTypeToResolve>();
        void Register<TTypeToResolve, TConcreate>();
    }

    class Container : IContainer
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            try
            {
                return (TTypeToResolve)_instances[typeof(TTypeToResolve)];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Doesn't exists registered type by {typeof(TTypeToResolve)}");
            }

        }

        public void Register<TTypeToResolve, TConcreate>()
        {
            var instance = Activator.CreateInstance<TConcreate>();
            try
            {
                _instances.Add(typeof(TTypeToResolve), instance);
            }
            catch (ArgumentException)
            {
                throw new Exception($"The IoC contains arleady registered instance by {typeof(TTypeToResolve)}");
            }
        }
    }

    interface IA
    {
        void PrintHello();
    }

    class A : IA
    {
        public void PrintHello()
        {
            Console.WriteLine("Hello!!!");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var ioC = new Container();
            ioC.Register<IA, A>();
            var a = ioC.Resolve<A>();
            a.PrintHello();
            Console.Read();
        }
    }
}