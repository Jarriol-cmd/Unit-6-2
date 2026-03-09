using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    public Animator anim;
    Rigidbody rb;
    public float speed = 6.0f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Transform cam;
    InputAction moveAction;
    InputAction jumpAction;
    public bool isgrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        anim.SetBool("IsWalking", false);

        float horizontal = moveAction.ReadValue<Vector2>().x;
        float vertical = moveAction.ReadValue<Vector2>().y;

        Vector2 direction = moveAction.ReadValue<Vector2>().normalized;
        
        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 vel = moveDir.normalized * speed;

            rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

            anim.SetBool("IsWalking", true);
        }

        if (jumpAction.IsPressed() && isgrounded == true)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 6f, rb.linearVelocity.z);
        }


    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            isgrounded = true;
            anim.SetBool("IsAirbourne", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            isgrounded = false;
            anim.SetBool("IsAirbourne", true);
        }
    }
}

