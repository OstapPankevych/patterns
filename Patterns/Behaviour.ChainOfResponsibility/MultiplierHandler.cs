using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Behaviour.ChainOfResponsibility
{
    public class MultiplierHandler : Handler<int[]>
    {
        private readonly int _multiplier;

        public MultiplierHandler(Handler<int[]> next, int multiplier)
            : base(next)
        {
            _multiplier = multiplier;
        }

        public override void Handle(int[] toProcess)
        {
            if (toProcess == null || toProcess.Length == 0) return;
            for (var i = 0; i < toProcess.Length; i++)
            {
                toProcess[i] = toProcess[i] * _multiplier;
            }
            Next.Handle(toProcess);
        }
    }
}
