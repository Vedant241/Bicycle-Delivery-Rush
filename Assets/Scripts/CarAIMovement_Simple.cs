using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Splines;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CarAIMovement_Simple : MonoBehaviour
{
    [Header("SplineContainer")]
    [SerializeField] private SplineContainer splinePath;

    [Header("Speed,Acceleration and Deceleration")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float accelerationRate = 5f;
    [SerializeField] private float decelerationRate = 5f;
    private float currentSpeed;

    [Header("StartOffset of Spline")]
    [SerializeField, Range(0f, 1f)] private float startOffset;

    [Header("Obsacle LayerMask")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask trafficLayer;
    [SerializeField] private LayerMask roadIntersectionLayer;

    [Header("Half Extents")]
    [SerializeField] private Vector3 halfExtents = new Vector3(2.7f,0.75f,2f);

    //Rigidbody
    private Rigidbody rb;

    //Spline Position & Length
    private float t;
    private float splineLength;

    //Braking and CanMove and CanTurn Bool
    private bool isBraking = false;
    private bool canMove;
    private bool canTurn;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = startOffset;
        currentSpeed = 0f;
        canMove = true;
        canTurn = true;

        splineLength = splinePath.CalculateLength();

        Vector3 startPosition = (Vector3)splinePath.EvaluatePosition(t);
        Vector3 startTangent = (Vector3)splinePath.EvaluateTangent(t);

        rb.position = startPosition;
        rb.rotation = Quaternion.LookRotation(startTangent);
    }
    private void FixedUpdate()
    {
        isBraking = false;
        canMove = true;
        canTurn = true;

        //Debug.Log(currentSpeed);
        TestObstacles(obstacleLayer);
        TestTrafficSignal();
        //TestRoadIntersection();
        Movement();
        Rotation();
    }
    private void Movement()
    {
        if (!isBraking && canMove)
        {
            currentSpeed += accelerationRate * Time.fixedDeltaTime;
            t += (currentSpeed / splineLength) * Time.fixedDeltaTime;
        }
        
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);

        if(t >= 1f)
        {
            t = startOffset;
        }

        Vector3 position = (Vector3)splinePath.EvaluatePosition(t);

        if (canMove && !isBraking)
        {
            rb.MovePosition(position);
        }
    }
    private void Rotation()
    {
        if (!canTurn) return;
        Vector3 tangent = (Vector3)splinePath.EvaluateTangent(t);

        Quaternion rotation = Quaternion.LookRotation(tangent);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotation, rotateSpeed));
    }
    private void TestObstacles(LayerMask layer)
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward * 2f;
        if (Physics.BoxCast(origin, halfExtents, transform.forward, out hit, transform.rotation, currentSpeed + 1f ,layer))
        {
            //Debug.Log("Hit");
            //Debug.Log(hit.distance);
            if(hit.distance < 3f)
            {
                DecelerateTheCar(0f, 20f);
                canMove = false;
                canTurn = false;
                isBraking = true;
            }
            else if(hit.distance < 5f)
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
                DecelerateTheCar(5f, 7f);
            }
            else if(hit.distance < 8f)
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
                DecelerateTheCar(7f, 5f);
            }else if(hit.distance < 10f)
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
                DecelerateTheCar(10f, 3f);
            }else if(hit.distance < 15f)
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
                DecelerateTheCar(15f, 1f);
            }else if(hit.distance < 20f)
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
                DecelerateTheCar(18f, 0.5f);
            }
            else
            {
                canMove = true;
                canTurn = true;
                isBraking = false;
            }
        }
        Debug.DrawRay(transform.position, transform.forward * currentSpeed , Color.green);
    }
    private void DecelerateTheCar(float minAmount, float decelerationMultiplier)
    {
        currentSpeed -= decelerationRate * decelerationMultiplier * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, minAmount, maxSpeed);
    }
    private void TestTrafficSignal()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward * 2f;
        if (Physics.BoxCast(origin, halfExtents, transform.forward, out hit, transform.rotation, currentSpeed, trafficLayer))
        {
            if(hit.collider.TryGetComponent<TrafficSignal>(out TrafficSignal signal))
            {
                if (signal.currentSignal == TrafficSignal.SignalState.Red && hit.distance <= 1.5f)
                {
                    canMove = false;
                    canTurn = false;
                    isBraking = true;
                }
                else if (signal.currentSignal == TrafficSignal.SignalState.Red && hit.distance > 1.5f)
                {
                    canTurn = false;
                    isBraking = false;
                    DecelerateTheCar(0f, 5f);
                }
                else if (signal.currentSignal == TrafficSignal.SignalState.Yellow)
                {
                    isBraking = false;
                    canMove = true;
                    canTurn = true;
                    DecelerateTheCar(5f, 5f);
                }
                else
                {
                    isBraking = false;
                    canTurn = true;
                    canMove = true;
                }
            }
        }
    }
    private void TestRoadIntersection()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + transform.forward * 2f;
        if (Physics.BoxCast(origin, halfExtents, transform.forward, out hit, transform.rotation, currentSpeed, roadIntersectionLayer))
        {
            if(hit.collider.TryGetComponent<RoadIntersectionCheck>(out RoadIntersectionCheck intersectionCheck))
            {
                if(hit.distance < 3f && !intersectionCheck.canMove)
                {
                    canMove = false;
                    canTurn = false;
                    isBraking = true;   
                    DecelerateTheCar(0f, 20f);
                }else if(hit.distance < 10f)
                {
                    canMove = true;
                    canTurn = true;
                    isBraking = false;
                    DecelerateTheCar(10f, 4f);
                }
                else
                {
                    isBraking = false;
                    canTurn = true;
                    canMove = true;
                }
            }
        }
    }
}
