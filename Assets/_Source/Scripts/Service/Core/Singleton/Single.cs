using System;

namespace Utilities.Singleton
{
    public class Single<T> : ISingle where T: Single<T>
    {
        public static T Instance { get; protected set; }

        protected Single()
        {
            Instance ??= (T)this;
        }
    }
    
    public class Single<T, SingleModifyPattern> : Single<T>
        where SingleModifyPattern: ISingleModifyPattern
        where T: Single<T, SingleModifyPattern>
    {
        protected Single()
        {
            Instance ??= (T)this;
            Activator.CreateInstance<SingleModifyPattern>().Init(Instance);
        }
    }
}