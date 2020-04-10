namespace Reface.AppStarter.Predicates
{
    public class NotPredicate : Predicate
    {
        private readonly Predicate predicate;

        public NotPredicate(Predicate predicate)
        {
            this.predicate = predicate;
        }

        public override bool IsTrue()
        {
            return this.predicate.IsFalse();
        }
    }
}
