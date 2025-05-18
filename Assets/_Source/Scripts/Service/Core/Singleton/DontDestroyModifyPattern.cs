using UnityEngine;

namespace Utilities.Singleton
{
    public struct DontDestroyModifyPattern : ISingleModifyPattern
    {
        public void Init<T>(T instance)
        {
            Object.DontDestroyOnLoad(instance as MonoBehaviour);
        }
    }
}