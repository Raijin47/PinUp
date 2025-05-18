using UnityEngine;
using Utilities.Container;

public class MonoContainer : MonoBehaviour, IContainer, IHavingInitContainer
{
    protected virtual void Awake()
    {
        Game.Instance.Add(this);
    }

    protected virtual void OnDestroy()
    {
        Game.Instance.Remove(this);
    }

    public virtual void Init()
    {

    }
}