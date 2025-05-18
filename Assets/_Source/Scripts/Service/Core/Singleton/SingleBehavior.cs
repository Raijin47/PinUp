using System;
using UnityEngine;

namespace Utilities.Singleton
{
    public class SingleBehavior<T> : MonoBehaviour, ISingle
        where T: SingleBehavior<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                return;
            }
            
            Instance = (T)this;
        }
    }

    public class SingleBehavior<T, SingleModifyPattern> : SingleBehavior<T>
        where SingleModifyPattern: ISingleModifyPattern
        where T: SingleBehavior<T, SingleModifyPattern>
    {
        protected override void Awake()
        {
            base.Awake();
            if (Instance != null)
            {
                Activator.CreateInstance<SingleModifyPattern>().Init(Instance);
            }
        }
    }
}