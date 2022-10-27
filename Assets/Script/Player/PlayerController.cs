using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject lightChild;
    //private GameObject darkChild;
    //private SpriteRenderer m_DarkSR;
    private SpriteRenderer m_LightSR;
    //private Animator m_DarkAnim;
    private Animator m_LightAnim;
    private MovementController m_MC;
    private LightAnimController m_LightANIM;
    [SerializeField]private PlayerWeapon weapon;
    [SerializeField] private Canvas deathMenu;

    public int goldCollected;


    #region Attack

    public bool isAttacking;
    //private bool wantNextAttack;
    public int currentAttackStage;
    
    private string attack;
    private bool attackInput;
    private int nextAttackStage;
    private float lastAttack;
    private KilleableEntity hp;

    #endregion
    
    void Awake()
    {
        Time.timeScale = 1;
        lightChild = GameObject.Find("Light");
        //darkChild = GameObject.Find("Dark");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<KilleableEntity>();
        
        m_LightSR = lightChild.GetComponent<SpriteRenderer>();
        //m_DarkSR = darkChild.GetComponent<SpriteRenderer>();

        m_LightAnim = lightChild.GetComponent<Animator>();
        //m_DarkAnim = darkChild.GetComponent<Animator>();

        m_MC = GetComponent<MovementController>();
        lastAttack = -1f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (hp.hp <= 0)
        {
            Die();
        }
        
        // catch input
        attackInput = Input.GetButton("Fire1");

        lastAttack -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if ((attackInput || isAttacking) && m_MC.isGrounded)
        {
            Attack();
        }
    }

    private void Attack()
    {
        switch (isAttacking)
        {
            case true when attackInput && lastAttack > 0:
                //wantNextAttack = true;
                break;
            case false:
                isAttacking = true;
                m_MC.movementLocked = true;
                break;
        }

        if (isAttacking && lastAttack <= 0.1)
        {
            isAttacking = false;
            m_MC.movementLocked = false;
        }
        
        // TODO add attack combo
        if (lastAttack < -1f || currentAttackStage == 1 || (lastAttack < 0 && ++currentAttackStage == 4))
        {
            currentAttackStage = 1;
            if (lastAttack <= 0)
            {
                Invoke(nameof(CallWeapon), 0.1f);
                lastAttack = 1f;
            }
            return;
        }
    }

    private void CallWeapon()
    {
        weapon.DealDamage();
    }

    public void Die()
    {
        Destroy(this);
        deathMenu.enabled = true;
        Time.timeScale = 0;
    }
    
}
