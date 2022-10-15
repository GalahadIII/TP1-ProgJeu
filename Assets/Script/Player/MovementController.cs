using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public static MovementController instance;
    
    #region Var

    #region Movement

    [Header("Movement")] [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float decceleration = 6f;
    [SerializeField] private float velPower = 1.2f;
    [SerializeField] private float friction = 0.2f;
    public bool movementLocked;

    #endregion

    #region Jump

    [Header("Jump")] [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpCutMultiplier = 0.1f;
    [SerializeField] private float jumpCoyoteTime = 0.15f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float fallGravityMultiplier = 1.9f;

    private float gravityScale;

    #endregion

    #region Checks

    [Header("Checks")] private bool facingRight = true;

    [SerializeField] private LayerMask groundLayer;
    public bool grounded { get; private set; }
    private bool jumping;

    #endregion

    #region Timer

    private float lastGroundedTime;
    private float lastJumpTime;

    #endregion

    #region Components
    
    public Rigidbody2D m_RB;

    #endregion

    #region Input

    private float moveInput;
    private bool jumpInput;

    #endregion

    #endregion

    // Awake is called before start
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        
        gravityScale = m_RB.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        // various checks
        grounded = IsGrounded();
        
        if (jumping && m_RB.velocity.y <= 0)
        {
            jumping = false;
        }

        // update timer
        lastGroundedTime -= Time.deltaTime;
        lastJumpTime -= Time.deltaTime;

        // catch input from the player
        jumpInput = Input.GetButtonDown("Jump");
        if (Input.GetButtonUp("Jump"))
        {
            JumpInputUp();
        }

        moveInput = Input.GetAxis("Horizontal");

        if (jumpInput)
        {
            lastJumpTime = jumpBufferTime;
            if (lastGroundedTime > 0 && lastJumpTime > 0 && !jumping)
            {
                Jump();
            }
        }


    }

    // Fixed update is called for physics updates
    private void FixedUpdate()
    {
        if (!movementLocked)
        {
            Movement();
        }
        else
        {
            m_RB.velocity = Vector2.zero;
        }
        
        FallingGravity();
    }

    private bool IsGrounded()
    {
        if (!Physics2D.OverlapCircle(transform.position + new Vector3(0.06f, -0.4f), 0.1f, groundLayer)) return false;
        lastGroundedTime = jumpCoyoteTime;
        return true;
    }

    // X axis movement
    private void Movement()
    {
        // check if the sprite need to be flipped
        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
            facingRight = Utilities.FlipTransform(facingRight, transform);

        // calculate wanted direction and desired velocity
        float targetSpeed = moveInput * moveSpeed;
        // calculate difference between current volocity and target velocity
        float speedDif = targetSpeed - m_RB.velocity.x;
        // change acceleration rate depending on situations;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        // applies acceleration to speed difference, raise to a set power so acceleration increase with higher speed
        // multiply by sign to reapply direction
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        // apply force to rigidbody, multiplying by Vector2.Right sot it only affects X axis
        m_RB.AddForce(movement * Vector2.right);

        // check if grounded
        switch (grounded)
        {
            // and not trying to stop
            case true when MathF.Abs(moveInput) < 0.01f:
            {
                // use either friction amount or velocity
                float amount = MathF.Min(MathF.Abs(m_RB.velocity.x), MathF.Abs(friction));
                // sets to movement direction
                amount *= Mathf.Sign(m_RB.velocity.x);
                // applies force against movement direction
                m_RB.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
                break;
            }
        }
    }

    // Jump
    private void Jump()
    {
        // the jump
        m_RB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // on jump actions
        jumping = true;
    }

    private void JumpInputUp()
    {
        if (m_RB.velocity.y > 0 && jumping)
        {
            // reduces current y velocity by amount[0-1] (higher the CutMultiplier the less sensitive to input it becomes)
            m_RB.AddForce(Vector2.down * m_RB.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        }
    }

    private void FallingGravity()
    {
        if (m_RB.velocity.y < 0)
        {
            Utilities.SetGravityScale(m_RB, gravityScale * fallGravityMultiplier);
        }
        else
        {
            m_RB.gravityScale = gravityScale;
        }
    }
}