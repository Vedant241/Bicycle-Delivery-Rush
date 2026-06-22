using UnityEngine;
using UnityEngine.InputSystem;

public class BicycleMovement : MonoBehaviour
{
    [SerializeField] private WheelCollider frontWheelCollider;
    [SerializeField] private WheelCollider rearWheelCollider;
    [SerializeField] private Transform frontWheelMesh;
    [SerializeField] private Transform rearWheelMesh;
    [SerializeField] private float extraTorque = 200f;
    [SerializeField] private float steerValue = 50f;
    [SerializeField] private float turnSpeed = 80f;
    [SerializeField] private float rearBrakeTorque = 1000f;
    [SerializeField] private float frontBrakeTorque = 800f;
    [SerializeField] private float maxSpeed = 25f;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction brakeButton;

    private Vector2 moveInput;
    private Vector3 pos;
    private Quaternion rot;

    private bool brakePerformed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        brakeButton = playerInput.actions["Brake"];
    }
    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (brakeButton.IsPressed())
        {
            brakePerformed = true;
        }
        else
        {
            brakePerformed = false;
        }
    }
    void FixedUpdate()
    {
        frontWheelCollider.steerAngle = moveInput.x * steerValue;

        float currentSpeed = Vector3.Dot(rb.linearVelocity,transform.forward);
        float speedFactor = Mathf.Clamp01(1 - (currentSpeed / maxSpeed));
        float turnFactor = Mathf.Lerp(1.3f, 0.4f, currentSpeed / maxSpeed);
        transform.Rotate(0, moveInput.x * turnSpeed * turnFactor * Time.fixedDeltaTime, 0);
        rearWheelCollider.motorTorque = moveInput.y * extraTorque * speedFactor;
        if (brakePerformed)
        {
            rearWheelCollider.brakeTorque = rearBrakeTorque;
            frontWheelCollider.brakeTorque = frontBrakeTorque;
        }
        else
        {
            rearWheelCollider.brakeTorque = 0f;
            frontWheelCollider.brakeTorque = 0f;
        }
    }
    private void LateUpdate()
    {
        frontWheelCollider.GetWorldPose(out pos, out rot);
        frontWheelMesh.position = pos;
        frontWheelMesh.rotation = rot;

        rearWheelCollider.GetWorldPose(out pos, out rot);
        rearWheelMesh.position = pos;
        rearWheelMesh.rotation = rot;
    }
}