using System.Linq;

namespace Behaviour.ChainOfResponsibility
{
    public class ZeroValidationHandler : Handler<int[]>
    {
        public ZeroValidationHandler(Handler<int[]> next)
            : base(next)
        {
        }

        public override void Handle(int[] toProcess)
        {
            if (toProcess == null || toProcess.Contains(0)) return;
            Next.Handle(toProcess);
        }
    }
}