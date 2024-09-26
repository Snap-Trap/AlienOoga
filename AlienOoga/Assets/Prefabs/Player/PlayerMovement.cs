using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public InputAction forward;
    public InputAction backward;
    public InputAction left;
    public InputAction right;
    public InputAction jump;

    public float speed = 5f;
    public float jumpForce = 7f;
    public float maxRayDistance = 1.2f;

    private Rigidbody rb;

    public LayerMask whatIsGround;

    [SerializeField] private bool isGrounded;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // basic movement shit


        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (forward.ReadValue<float>() == 1)
        {
            rb.velocity += new Vector3(0, 0, speed);
        }
        if (backward.ReadValue<float>() == 1)
        {
            rb.velocity += new Vector3(0, 0, -speed);
        }
        if (left.ReadValue<float>() == 1)
        {
            rb.velocity += new Vector3(-speed, 0, 0);
        }
        if (right.ReadValue<float>() == 1)
        {
            rb.velocity += new Vector3(speed, 0, 0);
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
