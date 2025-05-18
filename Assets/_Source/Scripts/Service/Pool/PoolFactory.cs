using UnityEngine;

public class PoolFactory
{
    private readonly PoolInstantiateObject<PoolMember> Member;
    private readonly Transform Content;

    public PoolFactory(PoolInstantiateObject<PoolMember> member, Transform content) 
    {
        Content = content;
        Member = member; 
    }

    public (PoolMember, bool) Spawn(Vector3 position)
    {
        var obj = Member.GetInstantiate();
        if (obj.Item1 != null)
        {
            var transform = obj.Item1.transform;
            transform.SetParent(Content, false);
            transform.position = position;
        }
        return obj;
    }
}