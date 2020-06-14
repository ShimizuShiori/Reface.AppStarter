using System;

namespace Reface.AppStarter.Predicates
{
    public class PredicateTemplateBuilder<T>
    {
        public  PredicateTemplate<T> Create(Func<T, bool> func)
        {
            return new FuncPredicateTemplate<T>(func);
        }
    }
}
