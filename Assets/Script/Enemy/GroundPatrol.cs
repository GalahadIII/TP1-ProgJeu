using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;
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
        mustPatrol = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            // Check Ground
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
            Debug.Log(mustTurn);
        }
        m_ANIM.SetFloat("Velocity", m_RB.velocity.x);
    }

    private void Patrol()
    {
        if (mustTurn)
        {
            SwitchSide();
        }
        m_RB.velocity = new Vector2(patrolSpeed * Time.fixedDeltaTime, m_RB.velocity.y);

    }

    private void SwitchSide()
    {
        Utilities.FlipTransform(facingRight, transform);
        patrolSpeed *= -1;
        mustTurn = false;
    }
}
