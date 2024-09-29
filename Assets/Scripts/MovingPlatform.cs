using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float leftMax;
    public float rightMax;
    private bool moveRight = true;
    private float distance;
    public GameObject Player;
    public Rigidbody2D PlayerRigidBody;
    private bool isCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        distance=transform.position.x;
    }

    // Update is called once per frame
    void Update()
        {
        //&& transform.position.x <= rightMax
        if (moveRight)
            {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
            //Debug.Log(transform.position);
            if (transform.position.x >= (distance + rightMax))
                {
                moveRight = false;
                }


            }
        else
            {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
            //Debug.Log(transform.position);
            if (transform.position.x <= (distance + leftMax))
                {
                moveRight = true;
                }
            }
        if (isCollided && Input.GetButtonDown("Jump"))
            {
            Player playerScript = Player.GetComponent<Player>();
            if (playerScript != null)
                {
                Debug.Log("Jumping from platform");
                PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, playerScript.jumpForce);
                Player.transform.SetParent(null);
                }
            else
                {
                Debug.LogError("Player script is not attached to the Player GameObject!");
                }
            }
        }

    void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.CompareTag("Player"))
            {
            Debug.Log("collisionEnter");
            collision.gameObject.transform.SetParent(transform);
            isCollided = true;
            }

        }

    // Detect when the player exits the platform
    void OnCollisionExit2D(Collision2D collision)
        {
        if (collision.gameObject.CompareTag("Player"))
            {
            Debug.Log("collisionExit");
            collision.gameObject.transform.SetParent(null);
            isCollided = false;
            }
        }
    }
