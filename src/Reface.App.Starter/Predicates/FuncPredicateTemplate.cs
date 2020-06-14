using System;

namespace Reface.AppStarter.Predicates
{
    public class FuncPredicateTemplate<T> : PredicateTemplate<T>
    {
        private readonly Func<T, bool> func;

        public FuncPredicateTemplate(Func<T, bool> func)
        {
            this.func = func;
        }

        public override bool IsTrue(T target)
        {
            return this.func(target);
        }
    }
}
