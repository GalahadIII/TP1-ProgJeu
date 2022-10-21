using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Transform wallCheckPos;
    [SerializeField] private Animator m_ANIM;
    private bool facingRight;
    private bool mustTurn;
    private Rigidbody2D m_RB;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        GameObject Visual = GameObject.Find("Visual");
        m_ANIM = Visual.GetComponent<Animator>();
        groundCheckPos = GameObject.Find("GroundDetect").transform;
        wallCheckPos = GameObject.Find("WallDetect").transform;
        mustPatrol = true;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            Patrol();
        }
        
        if (mustPatrol)
        {
            Vector2 groundTransform = groundCheckPos.position;
            Vector2 wallTransform = wallCheckPos.position;
            // Check Ground
            mustTurn = !Physics2D.OverlapCircle(groundTransform, 0.1f, groundLayer)
                //Check for wall
                || Physics2D.OverlapCircle(wallTransform, 0.1f, groundLayer);
        }
        m_ANIM.SetFloat("Velocity", Mathf.Abs(m_RB.velocity.x));
    }

    private void Patrol()
    {
        if (mustTurn)
        {
            SwitchSide();
        }
        m_RB.velocity = new Vector2(patrolSpeed * (facingRight ? -1 : 1), m_RB.velocity.y);

    }

    private void SwitchSide()
    {
        facingRight = Utilities.FlipTransform(facingRight, transform);
        mustTurn = false;
    }
}
