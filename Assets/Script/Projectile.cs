using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public bool facingRight;
    private int direction;

    [SerializeField] private Collider2D[] targets;
    [SerializeField] private Animator m_Anim;
    [SerializeField] private Rigidbody2D m_RB;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        direction = facingRight ? 1 : -1;
        
        if (!facingRight)
        {
            Utilities.FlipTransform(facingRight, transform);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    
    }

    private void FixedUpdate()
    {
        m_RB.velocity = new Vector2(speed * direction, m_RB.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (targets.Contains(col.collider))
        {
            //Todo deal damage
        }

        Destroy(this.GameObject());
    }
}