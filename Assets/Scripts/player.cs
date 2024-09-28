using UnityEngine;

public class Player : MonoBehaviour
    {
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private float moveInput;
    public bool isGrounded;

    void Start()
        {
        rb = GetComponent<Rigidbody2D>();
        }

    void Update()
        {
        moveInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
            {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; 
            }
        }

    private void FixedUpdate()
        {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

    private void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.CompareTag("Platform"))
            {
            isGrounded = true;
            }
        if (collision.gameObject.CompareTag("Pit"))
            {
            Destroy(gameObject);
            }
        if (collision.gameObject.CompareTag("Power"))
            {
            Debug.Log("true");
            Destroy(collision.gameObject);
            }
        }
    private void OnCollisionExit2D(Collision2D collision)
        {
       
        }
    }
