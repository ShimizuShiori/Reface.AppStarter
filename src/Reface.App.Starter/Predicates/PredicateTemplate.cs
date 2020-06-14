using System;

namespace Reface.AppStarter.Predicates
{
    /// <summary>
    /// 断言模板。
    /// 与断言不同，模板可以对一某个入参类型进行判断。
    /// 这些断言可以复用，每次传入不同的参数就可以得到不同的结果，
    /// 开发者使用此类型可以不同重复创建断言的实例。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PredicateTemplate<T>
    {

        public abstract bool IsTrue(T target);

        public bool IsFalse(T target)
        {
            return !this.IsTrue(target);
        }

        public PredicateTemplate<T> And(PredicateTemplate<T> other)
        {
            return new AndPredicateTemplate<T>(this, other);
        }

        public PredicateTemplate<T> Or(PredicateTemplate<T> other)
        {
            return new OrPredicateTemplate<T>(this, other);
        }

        public PredicateTemplate<T> Not()
        {
            return new NotPredicateTemplate<T>(this);
        }
    }
}
