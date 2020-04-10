namespace Reface.AppStarter.Predicates
{
    public class OrPredicate : Predicate
    {
        private readonly Predicate a;
        private readonly Predicate b;

        public OrPredicate(Predicate a, Predicate b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool IsTrue()
        {
            return a.IsTrue() || b.IsTrue();
        }
    }
}
