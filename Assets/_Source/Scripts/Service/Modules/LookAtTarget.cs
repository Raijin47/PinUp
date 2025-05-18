using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private bool _freezeY;
    [SerializeField, Range(1, 10)] private float _rotateSpeed;

    private Transform _transform;

    private void Awake() => _transform = transform;

    private void LateUpdate()
    {
        Vector3 direction = _target.transform.position - _transform.position;
        direction.y = _freezeY ? 0 : direction.y;

        Quaternion rotation = Quaternion.LookRotation(direction);

        _transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, Time.deltaTime * _rotateSpeed);
    }
}