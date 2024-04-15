using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset = new();

    private Transform _target;
    private Transform _transform;
    private float _defaultWidth;

    private void Awake()
    {
        _transform = transform;
        _defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        Camera.main.orthographicSize = _defaultWidth / Camera.main.aspect;

        if (_target != null)
            _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, Time.deltaTime * _smooth);
    }
}