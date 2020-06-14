namespace Reface.AppStarter.Predicates
{
    public class OrPredicateTemplate<T> : PredicateTemplate<T>
    {
        private readonly PredicateTemplate<T> a;
        private readonly PredicateTemplate<T> b;

        public OrPredicateTemplate(PredicateTemplate<T> a, PredicateTemplate<T> b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool IsTrue(T target)
        {
            return this.a.IsTrue(target) || this.b.IsTrue(target);
        }
    }
}
