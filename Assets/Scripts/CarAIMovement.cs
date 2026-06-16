using UnityEngine;
using UnityEngine.Splines;

public class CarAIMovement : MonoBehaviour
{
    [Header("Spline")]
    [SerializeField] private SplineContainer spline;
    [SerializeField, Range(0f, 1f)] private float startOffset;

    [Header("Speed")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float decelerationSpeed = 9f;

    [Header("Vehicle")]
    [SerializeField] private float motorTorque = 1500f;
    [SerializeField] private float brakeTorque = 1500f;

    [Header("Detection")]
    [SerializeField] private LayerMask rightTurnLayer;
    [SerializeField] private float lookAheadDistance = 30f;

    [Header("Wheels")]
    [SerializeField] private WheelCollider[] rearWheelColliders;

    [SerializeField] private WheelCollider[] frontWheelColliders;
    [SerializeField] private float turnAngle = 20f;

    private Rigidbody rb;
    private float splineLength;
    private float t;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        splineLength = spline.CalculateLength();
    }

    private void Start()
    {
        t = startOffset;

        Vector3 startPosition = (Vector3)spline.EvaluatePosition(t);
        Vector3 startDirection = ((Vector3)spline.EvaluateTangent(t)).normalized;

        rb.position = startPosition + Vector3.up;

        if (startDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(startDirection);
        }
    }

    private void FixedUpdate()
    {
        currentSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        HandleSpeedControl();
    }

    private void HandleSpeedControl()
    {
        RaycastHit hit;

        bool approachingTurn = Physics.BoxCast(
            transform.position,
            Vector3.one * 0.5f,
            transform.forward,
            out hit,
            transform.rotation,
            lookAheadDistance,
            rightTurnLayer
        );

        float targetSpeed = maxSpeed;

        if (approachingTurn)
        {
            float brakingDistance =
                Mathf.Max(currentSpeed * 1.5f, 10f);

            float brakeFactor =
                Mathf.Clamp01(
                    1f - hit.distance / brakingDistance
                );

            targetSpeed =
                Mathf.Lerp(
                    maxSpeed,
                    decelerationSpeed,
                    brakeFactor
                );

            foreach (var wheel in frontWheelColliders)
            {
                wheel.steerAngle = turnAngle;
            }
        }
        else
        {
            foreach (var wheel in frontWheelColliders)
            {
                wheel.steerAngle = 0f;
            }
        }

        foreach (var wheel in rearWheelColliders)
        {
            if (currentSpeed > targetSpeed)
            {
                wheel.motorTorque = 0f;

                float speedDifference =
                    currentSpeed - targetSpeed;

                float brakePercent =
                    Mathf.Clamp01(
                        speedDifference / maxSpeed
                    );

                wheel.brakeTorque =
                    brakeTorque * brakePercent;
            }
            else
            {
                wheel.brakeTorque = 0f;

                float speedFactor =
                    Mathf.Clamp01(
                        1f - currentSpeed / maxSpeed
                    );

                wheel.motorTorque =
                    motorTorque * speedFactor;
            }
        }

        Debug.DrawRay(
            transform.position,
            transform.forward * lookAheadDistance,
            approachingTurn ? Color.red : Color.green
        );
    }
}