using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction forward;
    public InputAction backward;
    public InputAction left;
    public InputAction right;

    public float speed = 5f;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // basic movement shit


        Vector3 move = new Vector3(0, 0, 0);

        if (forward.ReadValue<float>() > 0)
        {
            move += transform.forward;
        }
        if (backward.ReadValue<float>() > 0)
        {
            move -= transform.forward;
        }
        if (left.ReadValue<float>() > 0)
        {
            move -= transform.right;
        }
        if (right.ReadValue<float>() > 0)
        {
            move += transform.right;
        }

        move.y = 0;

        rb.velocity = move.normalized * speed;
    }

    private void OnEnable()
    {
        forward.Enable();
        backward.Enable();
        left.Enable();
        right.Enable();
    }

    private void OnDisable()
    {
        forward.Disable();
        backward.Disable();
        left.Disable();
        right.Disable();
    }
}
