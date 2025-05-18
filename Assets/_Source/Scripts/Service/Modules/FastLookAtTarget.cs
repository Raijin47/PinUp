using UnityEngine;

public class FastLookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Awake() => _transform = transform;

    private void LateUpdate()
    {
        _transform.rotation = _target.rotation;
    }
}