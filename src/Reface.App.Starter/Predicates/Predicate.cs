using System;

namespace Reface.AppStarter.Predicates
{
    public abstract class Predicate
    {
        public static Predicate Create(Func<bool> func)
        {
            return new FuncPredicate(func);
        }

        public abstract bool IsTrue();
        public bool IsFalse()
        {
            return !this.IsTrue();
        }

        public Predicate And(Predicate other)
        {
            return new AndPredicate(this, other);
        }

        public Predicate Or(Predicate other)
        {
            return new OrPredicate(this, other);
        }

        public Predicate Not()
        {
            return new NotPredicate(this);
        }
    }
}
