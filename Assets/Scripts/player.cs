using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Player : MonoBehaviour
    {
    public float moveSpeed;
    public float jumpForce;

    public GameObject speedPowerup;
    public GameObject jumpPowerup;
    public GameObject Win;

    private Rigidbody2D rb;
    private float moveInput;
    public bool isGrounded;

    // Buff management
    private float originalMoveSpeed;
    private float originalJumpForce;
    private bool isPowerUpActive = false;  // Flag to ensure one power-up at a time

    void Start()
        {
        rb = GetComponent<Rigidbody2D>();

        // Store the original values of move speed and jump force
        originalMoveSpeed = moveSpeed;
        originalJumpForce = jumpForce;
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

        if (collision.gameObject.CompareTag("Pit") || collision.gameObject.CompareTag("Obstacle"))
            {
            Destroy(gameObject);
            }

        if (collision.gameObject.CompareTag("Power"))
            {
            if (!isPowerUpActive)  // Only apply power-up if no active power-up exists
                {
                StartCoroutine(ApplyRandomPowerUp());
                }

            Destroy(collision.gameObject);  // Remove the power-up object
            }

        if (collision.gameObject.CompareTag("Goal1"))
            {
            SceneManager.LoadScene("Level 2");
            }
        if (Win!=null&&collision.gameObject.CompareTag("Goal2"))
            {
            Win.SetActive(true);
            }
        }

    // Coroutine to handle temporary power-up effects
    private IEnumerator ApplyRandomPowerUp()
        {
        isPowerUpActive = true;  // Flag to prevent other power-ups from activating during this time
        int randomChoice = Random.Range(0, 2);

        if (randomChoice == 0)
            {
            float randomJumpBoost = 5f;
            jumpForce += randomJumpBoost;

            // Check if the GameObject is active before setting it
            Debug.Log("Activating Jump Powerup: " + jumpPowerup.activeSelf);
            jumpPowerup.SetActive(true);
            Debug.Log("Jump Powerup Active After Activation: " + jumpPowerup.activeSelf);

            Debug.Log($"Jump force increased by {randomJumpBoost}! New jump force: {jumpForce}");

            // Power-up lasts for 5 seconds
            yield return new WaitForSeconds(5f);

            // Reset jump force after power-up duration ends
            jumpPowerup.SetActive(false);
            jumpForce = originalJumpForce;
            Debug.Log("Jump force reset to original value.");
            }
        else
            {
            float randomSpeedBoost = 10f;
            moveSpeed += randomSpeedBoost;

            Debug.Log("Activating Speed Powerup: " + speedPowerup.activeSelf);
            speedPowerup.SetActive(true);
            Debug.Log("Speed Powerup Active After Activation: " + speedPowerup.activeSelf);

            Debug.Log($"Move speed increased by {randomSpeedBoost}! New move speed: {moveSpeed}");

            // Power-up lasts for 5 seconds
            yield return new WaitForSeconds(5f);

            // Reset move speed after power-up duration ends
            speedPowerup.SetActive(false);
            moveSpeed = originalMoveSpeed;
            Debug.Log("Move speed reset to original value.");
            }

        isPowerUpActive = false;  // Reset the flag to allow new power-ups
        }


    private void OnCollisionExit2D(Collision2D collision)
        {
        // Empty for now, but can be expanded if needed
        }
    }
