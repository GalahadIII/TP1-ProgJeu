using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    #region Public

    [HideInInspector] public bool cooling;

    public float walkSpeed;
    public float runSpeed;
    public float rayCastLength;
    public float attackDistance;
    public float atkCooldown;
    
    public LayerMask rayCastMask;
    public Transform rayCast;
    public Transform leftLimit;
    public Transform rightLimit;

    #endregion

    #region Private Var

    private float atkTimer;
    private float moveSpeed;
    private float distance; // distance between enemy and player
    private bool attackMode;
    private bool inRange;
    private bool facingRight;
    
        private RaycastHit2D hit;
    private Transform target;
    private Animator m_Anim;

    #endregion

    private void Awake()
    {
        moveSpeed = walkSpeed;
        facingRight = true;
        SelectTarget();
        atkTimer = atkCooldown;
        GameObject visual = GameObject.Find("Sprite");
        m_Anim = visual.GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //Debug.Log(target);
        
        if (!attackMode)
        {
            Move();
        }

        if (!InsideLimits() && !inRange && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTarget();
        }
        
        if (atkTimer >= 0)
        {
            atkTimer -= Time.deltaTime;
        }
        else if (atkTimer < 0)
        {
            cooling = false;
        }
        
        if (atkTimer < 0)
        {
            cooling = false;
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right * (facingRight ? 1 : -1), rayCastLength, rayCastMask);
        }
        
        // when player detected
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (!inRange)
        {
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (!trig.gameObject.CompareTag("Player")) return;
        target = trig.transform;
        inRange = true;
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            m_Anim.SetBool("Attack", false);
        }
    }

    private void Move()
    {
        m_Anim.SetBool("canWalk", true);
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            moveSpeed = target.CompareTag("Player") ? runSpeed : walkSpeed;
            
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        atkTimer = atkCooldown;
        attackMode = true;

        m_Anim.SetBool("canWalk", false);
        m_Anim.SetBool("Attack", true);
    }

    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        m_Anim.SetBool("Attack", false);
    }

    private bool InsideLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;
        
        if ((target != leftLimit || !facingRight) && (target != rightLimit || facingRight)) return;
        Flip();

    }

    private void Flip()
    {
        facingRight = Utilities.FlipTransform(facingRight, transform);
    }
}
