using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace IoC
{
    interface IContainer
    {
        TTypeToResolve Resolve<TTypeToResolve>();
        void Register<TTypeToResolve, TConcreate>();
    }

    class TransientContainer : IContainer
    {
        protected readonly Dictionary<Type, Type> Instances = new Dictionary<Type, Type>();

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            try
            {
                var type = Instances[typeof(TTypeToResolve)];
                return (TTypeToResolve)Activator.CreateInstance(type);
            }
            catch (KeyNotFoundException)
            {
                throw new Exception($"Doesn't exists registered type by {typeof(TTypeToResolve)}");
            }
        }

        public void Register<TTypeToResolve, TConcreate>()
        {
            try
            {
                Instances.Add(typeof(TTypeToResolve), typeof(TConcreate));
            }
            catch (ArgumentException)
            {
                throw new Exception($"The IoC contains arleady registered instance by {typeof(TTypeToResolve)}");
            }
        }
    }

    class SingletonContainer : IContainer
    {
        protected readonly Dictionary<Type, object> Instances = new Dictionary<Type, object>();

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            try
            {
                return (TTypeToResolve)Instances[typeof(TTypeToResolve)];
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
                Instances.Add(typeof(TTypeToResolve), instance);
            }
            catch (ArgumentException)
            {
                throw new Exception($"The IoC contains arleady registered instance by {typeof(TTypeToResolve)}");
            }
        }
    }

    enum ContainerLifeCycle
    {
        Singleton,
        Transient
    }

    interface ILifeCycleContainer
    {
        IContainer As(ContainerLifeCycle lifeCycle);
        void Add(ContainerLifeCycle lifeCycle, IContainer container);
    }

    class LifeCycleContainer: ILifeCycleContainer
    {
        protected Dictionary<ContainerLifeCycle, IContainer> Containers = new Dictionary<ContainerLifeCycle, IContainer>();
        public IContainer As(ContainerLifeCycle lifeCycle)
        {
            return Containers[lifeCycle];
        }

        public void Add(ContainerLifeCycle lifeCycle, IContainer container)
        {
            Containers.Add(lifeCycle, container);
        }
    }

    interface ICounter
    {
        int Count { get; }
        void Increase();
    }

    class Counter: ICounter
    {
        public int Count { get; protected set; }
        public void Increase()
        {
            Count++;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var container = new SingletonContainer();
            container.Register<SingletonContainer, SingletonContainer>();
            container.Register<TransientContainer, TransientContainer>();
            container.Register<ILifeCycleContainer, LifeCycleContainer>();
            var lifeCycleContainer = container.Resolve<ILifeCycleContainer>();
            var singletonContainer = container.Resolve<SingletonContainer>();
            var transientContainer = container.Resolve<TransientContainer>();

            lifeCycleContainer.Add(ContainerLifeCycle.Singleton, singletonContainer);
            lifeCycleContainer.Add(ContainerLifeCycle.Transient, transientContainer);

            lifeCycleContainer.As(ContainerLifeCycle.Singleton).Register<ICounter, Counter>();
            lifeCycleContainer.As(ContainerLifeCycle.Transient).Register<ICounter, Counter>();

            var singletonCounter = lifeCycleContainer.As(ContainerLifeCycle.Singleton).Resolve<ICounter>();
            singletonCounter.Increase();
            Console.WriteLine($"singleton Count = {singletonCounter.Count}");
            var transientCounter = lifeCycleContainer.As(ContainerLifeCycle.Transient).Resolve<ICounter>();
            transientCounter.Increase();
            Console.WriteLine($"transient Count = {transientCounter.Count}");

            singletonCounter = lifeCycleContainer.As(ContainerLifeCycle.Singleton).Resolve<ICounter>();
            singletonCounter.Increase();
            Console.WriteLine($"singleton Count = {singletonCounter.Count}");
            transientCounter = lifeCycleContainer.As(ContainerLifeCycle.Transient).Resolve<ICounter>();
            transientCounter.Increase();
            Console.WriteLine($"transient Count = {transientCounter.Count}");

            Console.Read();
        }
    }
}