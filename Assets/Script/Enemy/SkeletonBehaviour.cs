using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    #region Public

    [HideInInspector] public bool cooling;

    public float walkSpeed;
    public float runSpeed;
    public float rayCastLength;
    public float attackDistance;

    
    public LayerMask rayCastMask;
    public Transform rayCast;
    public Transform leftLimit;
    public Transform rightLimit;
    public Skeleton enemyControlller;
    
    #endregion
    

    #region Private Var
    
    private float moveSpeed;
    private float distance; // distance between enemy and player
    [SerializeField] private bool attackMode;
    private KilleableEntity s_hp;
    private bool inRange;
    private bool facingRight;
    private RaycastHit2D hit;
    private Transform target;
    private Animator m_Anim;
    private SkeletonWeapon weapon;
    private Rigidbody2D m_RB;
    private bool interupted;
    
    private static readonly int CanWalkAnim = Animator.StringToHash("canWalk");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");

    #endregion

    #region timer

    public float atkCooldown;
    public float atkTime = 1.3f;
    public float hitInteruptTime = 0.5f;
    private float atkTimer;
    private float lastAttackTimer;
    private float lastHitTimer;
    private static readonly int Hit = Animator.StringToHash("Hit");

    #endregion

    private void Awake()
    {
        moveSpeed = walkSpeed;
        facingRight = true;
        atkTimer = atkCooldown;
        
    }

    private void Start()
    {
        s_hp = GetComponent<KilleableEntity>();
        weapon = GetComponentInChildren<SkeletonWeapon>();

        m_Anim = GetComponentInChildren<Animator>();
        SelectTarget();
    }

    private void Update()
    {
        //Debug.Log(target);
        if (enemyControlller.isDead)
        {
            interupted = true;
            return;
        }

        lastAttackTimer += Time.deltaTime;
        lastHitTimer -= Time.deltaTime;
        

        if (s_hp.tookDamage)
        {
            m_Anim.SetTrigger(Hit);
            lastHitTimer = hitInteruptTime;
            s_hp.tookDamage = false;
        }
        interupted = lastHitTimer > 0;

        
        if (interupted) return;
        
        if (!attackMode)
        {
            Move();
        }
        
        if ((facingRight && target.transform.position.x < transform.position.x ||
            !facingRight && target.transform.position.x > transform.position.x) && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            Flip();
        
        if (!InsideLimits() && !inRange && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTarget();
        }
        
        switch (atkTimer)
        {
            case >= 0:
                atkTimer -= Time.deltaTime;
                break;
            case < 0:
                cooling = false;
                break;
        }
        
        if (atkTimer < 0)
        {
            s_hp.tookDamage = false;
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
        else if (attackDistance >= distance && !cooling && lastAttackTimer > 1.3f)
        {
            Attack();
        }

        if (cooling)
        {
            m_Anim.SetBool(AttackAnim, false);
        }
    }

    private void Move()
    {
        m_Anim.SetBool(CanWalkAnim, true);
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            moveSpeed = target.CompareTag("Player") ? runSpeed : walkSpeed;

            var position = transform.position;
            Vector2 targetPosition = new Vector2(target.position.x, position.y);
            position = Vector2.MoveTowards(position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = position;
        }
    }

    private void Attack()
    {
        atkTimer = atkCooldown;
        attackMode = true;
        lastAttackTimer = 0;

        m_Anim.SetBool(CanWalkAnim, false);
        m_Anim.SetBool(AttackAnim, true);
        
        Invoke(nameof(CallWeapon), 0.50f);
    }

    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        m_Anim.SetBool(AttackAnim, false);
    }

    private bool InsideLimits()
    {
        var position = transform.position;
        return position.x > leftLimit.position.x && position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        var position = transform.position;
        float distanceToLeft = Vector2.Distance(position, leftLimit.position);
        float distanceToRight = Vector2.Distance(position, rightLimit.position);

        target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;
        
        if ((target != leftLimit || !facingRight) && (target != rightLimit || facingRight)) return;
        Flip();

    }

    private void Flip()
    {
        facingRight = Utilities.FlipTransform(facingRight, transform);
    }

    private void CallWeapon()
    {
        if (interupted) return;
        weapon.DealDamage();
    }
}
