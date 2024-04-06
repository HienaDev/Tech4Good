using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Vector2 velocity;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        Flip();
    }

    private void MovementInput()
    {

        velocity = Vector2.zero;

        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");



        if(velocity.magnitude > 1f)
        {
            velocity.Normalize();
        }

        animator.SetFloat("MovSpeedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("MovSpeedY", rb.velocity.y);

        rb.velocity = velocity * speed;
    }

    private void Flip()
    {
        if (rb.velocity.x < 0 && transform.right.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
        else if (rb.velocity.x > 0 && transform.right.x < 0)
        {
            transform.rotation = Quaternion.identity;

        }
    }
}
