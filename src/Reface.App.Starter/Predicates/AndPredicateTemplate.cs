namespace Reface.AppStarter.Predicates
{
    public class AndPredicateTemplate<T> : PredicateTemplate<T>
    {
        private readonly PredicateTemplate<T> a;
        private readonly PredicateTemplate<T> b;

        public AndPredicateTemplate(PredicateTemplate<T> a, PredicateTemplate<T> b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool IsTrue(T target)
        {
            return a.IsTrue(target) && b.IsTrue(target);
        }
    }
}
