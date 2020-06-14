namespace Reface.AppStarter.Predicates
{
    public class NotPredicateTemplate<T> : PredicateTemplate<T>
    {
        private readonly PredicateTemplate<T> predicate;

        public NotPredicateTemplate(PredicateTemplate<T> predicate)
        {
            this.predicate = predicate;
        }

        public override bool IsTrue(T target)
        {
            return this.predicate.IsFalse(target);
        }
    }
}
