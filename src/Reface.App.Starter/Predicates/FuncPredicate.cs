using System;

namespace Reface.AppStarter.Predicates
{
    public class FuncPredicate : Predicate
    {
        private readonly Func<bool> func;

        public FuncPredicate(Func<bool> func)
        {
            this.func = func;
        }

        public override bool IsTrue()
        {
            return func();
        }
    }
}
