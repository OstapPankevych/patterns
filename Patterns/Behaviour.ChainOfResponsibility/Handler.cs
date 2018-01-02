namespace Behaviour.ChainOfResponsibility
{
    public abstract class Handler<T> where T : class
    {
        protected Handler<T> Next;

        protected Handler(Handler<T> next)
        {
            Next = next;
        }

        public abstract void Handle(T toProcess);
    }
}