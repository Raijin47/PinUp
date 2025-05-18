using UnityEngine;

public class FastFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Awake() => _transform = transform;

    private void LateUpdate()
    {
        _transform.position = _target.position;
    }
}