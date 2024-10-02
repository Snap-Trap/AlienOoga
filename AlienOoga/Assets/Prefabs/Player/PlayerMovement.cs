using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public CameraRotation cameraRotation;

    public InputAction forward;
    public InputAction backward;
    public InputAction left;
    public InputAction right;
    public InputAction jump;

    public float speed;
    public float jumpForce;
    private float maxRayDistance = 1.2f;
    private float playerRotation;

    private Rigidbody rb;

    public LayerMask whatIsGround;

    [SerializeField] private bool isGrounded;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        

        playerRotation = cameraRotation.turn.x;

        Quaternion playerRotationQuaternion = Quaternion.Euler(0, playerRotation, 0);

        Vector3 forwardMovement = playerRotationQuaternion * Vector3.forward;
        Vector3 rightMovement = playerRotationQuaternion * Vector3.right;

        Vector3 movement = Vector3.zero;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // basic movement shit

        if (forward.ReadValue<float>() == 1)
        {
            movement += forwardMovement * speed; 
        }
        if (backward.ReadValue<float>() == 1)
        {
            movement += -forwardMovement * speed;
        }
        if (left.ReadValue<float>() == 1)
        {
            movement += -rightMovement * speed; 
        }
        if (right.ReadValue<float>() == 1)
        {
            movement += rightMovement * speed; 
        }

        
        if (isGrounded && jump.triggered)
        {
            rb.velocity += new Vector3(0, jumpForce, 0);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit groundHit, maxRayDistance, whatIsGround))
        {
            Debug.Log("Floor hit");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
            Debug.Log("Did not hit the floor");
        }


        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.red, maxRayDistance);
    }

    private void OnEnable()
    {
        forward.Enable();
        backward.Enable();
        left.Enable();
        right.Enable();
        jump.Enable();
    }

    private void OnDisable()
    {
        forward.Disable();
        backward.Disable();
        left.Disable();
        right.Disable();
        jump.Disable();
    }
}
