using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using Data;

[RequireComponent(typeof(Car))]
[RequireComponent(typeof(ISpeedController))]
public class AnimateCarAlongSpline : MonoBehaviour
{
    [SerializeField] private float _positionFromCenter;

    private const float ScaleCalibration = 3f;
    private const float MaxDistance = 1f;

    private Spline _spline;
    private Car _carToAnimate;
    private Countdown _countdown;
    private SplineContainer _splineContainer;
    private ISpeedController _speedController;

    private float _currentDistance;
    private float _currentSpeed;
    private float _splineLength;
    private float _totalDistance;

    public float CurrentDistance => _currentDistance;
    public float CurrentSpeed => _currentSpeed;
    public float TotalDistance => _totalDistance;

    private void Awake()
    {
        _carToAnimate = GetComponent<Car>();
        _speedController = GetComponent<ISpeedController>();
    }

    private void OnDisable()
    {
        if (GetComponentInParent<Container>())
            _carToAnimate.transform.position = GetComponentInParent<Container>().transform.position;

        _splineContainer = null;
        _currentSpeed = 0f;
        _currentDistance = 0f;
        _totalDistance = 0f;
    }

    void Update()
    {
        if (_splineContainer == null || _carToAnimate == null)
            return;

        if (_countdown.IsDone)
            _currentSpeed = _speedController.Change(_currentSpeed);

        if (gameObject.activeSelf)
        {
            _currentDistance = (_currentDistance + _currentSpeed * Time.deltaTime / _splineLength / ScaleCalibration) % MaxDistance;
            _totalDistance = (_totalDistance + _currentSpeed * Time.deltaTime / (_splineLength * Constants.LapCount) / ScaleCalibration);
        }

        float3 posOnSplineLocal = SplineUtility.EvaluatePosition(_spline, _currentDistance);
        float3 direction = SplineUtility.EvaluateTangent(_spline, _currentDistance);
        float3 upSplineDirection = SplineUtility.EvaluateUpVector(_spline, _currentDistance);
        float3 right = math.normalize(math.cross(upSplineDirection, direction));

        _carToAnimate.transform.SetPositionAndRotation(_splineContainer.transform.TransformPoint
            (posOnSplineLocal + _positionFromCenter * right), Quaternion.LookRotation(direction));
    }

    public void SetContainer(SplineContainer road)
    {
        _splineContainer = road;
        _spline = _splineContainer.Spline;
        _splineLength = _spline.GetLength();
    }

    public void SetCountdown(Countdown countdown) { _countdown = countdown; }
}