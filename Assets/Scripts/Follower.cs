using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private float _smooth;
    [SerializeField] private Vector3 _offset = new();

    private Transform _target;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
            _transform.position = Vector3.Lerp(_transform.position, _target.position + _offset, Time.deltaTime * _smooth);
    }
}