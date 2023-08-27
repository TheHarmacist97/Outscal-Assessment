using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpImpulse;
    [SerializeField, Range(0.1f, 1.0f)] private float decelerationRate;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private MovementStats moveStats;
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private float moveDir;
    private bool isGrounded;
    private bool takeInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        takeInput = true;    
    }

    private void OnEnable()
    {
        Player.OnDamageEvent += OnPlayerDamage;
        Player.OnDeathEvent += OnGameEnd;
    }

    private void OnDisable()
    {
        Player.OnDamageEvent -= OnPlayerDamage;
        Player.OnDeathEvent -= OnGameEnd;
    }

    private void OnPlayerDamage()
    {
        //Rotating my damage impulse vector by an angle that is dependent on my horizontal velocity
        float currentXVelocity = rb.velocity.x;
        float lerpTValue = Mathf.InverseLerp(-moveStats.maxSpeed, moveStats.maxSpeed, currentXVelocity);
        float angle = Mathf.Lerp(-45, 45, lerpTValue);

        Vector2 originalVector = Vector2.right; 
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);
        Vector2 rotVector = Vector2.zero;

        rotVector.x = -sin * originalVector.x + cos * originalVector.y;
        rotVector.y = cos * originalVector.x + sin * originalVector.y;

        rb.AddForce(rotVector * 25f, ForceMode2D.Impulse);
    }

    public void OnGameEnd()
    {
        takeInput = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        coll.enabled = false;
    }

    private void Update()
    {
        if (!takeInput) return;

        //basic ground check
        isGrounded = Physics2D.BoxCast(transform.position, coll.bounds.extents, 0, Vector2.down, coll.bounds.extents.y + 0.01f, groundLayer);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (!takeInput) return;

        moveDir = Input.GetAxisRaw("Horizontal");

        if (moveDir == 0 && isGrounded) //GetAxisRaw only gives -1, 0 or 1 value 
        {
            //decelerate
            rb.velocity = new Vector2(rb.velocity.x * decelerationRate, rb.velocity.y);
        }

        if (Mathf.Abs(rb.velocity.x) > moveStats.maxSpeed && moveDir * rb.velocity.x >= 0)
        {
            return; //if the player is at max speed
                    //and not trying to decelerate
        }

        rb.AddForce(moveDir * moveStats.moveForce * Vector2.right, ForceMode2D.Force);

    }
}
