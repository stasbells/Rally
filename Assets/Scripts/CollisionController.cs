using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private float _impactForce = 10f;

    private AnimateCarAlongSpline _animateCarAlongSpline;

    private void Awake()
    {
        _animateCarAlongSpline = GetComponentInParent<AnimateCarAlongSpline>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().AddForce(_animateCarAlongSpline.CurrentSpeed / _impactForce * (transform.forward + Vector3.up));
    }
}