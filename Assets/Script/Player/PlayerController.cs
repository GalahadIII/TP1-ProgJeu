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

    #region Attack

    public bool isAttacking;
    public int currentAttackStage;
    
    private string attack;
    private bool attackInput;
    private int nextAttackStage;
    private float lastAttack;

    #endregion
    
    void Awake()
    {
        lightChild = GameObject.Find("Light");
        //darkChild = GameObject.Find("Dark");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_LightSR = lightChild.GetComponent<SpriteRenderer>();
        //m_DarkSR = darkChild.GetComponent<SpriteRenderer>();

        m_LightAnim = lightChild.GetComponent<Animator>();
        //m_DarkAnim = darkChild.GetComponent<Animator>();

        m_MC = GetComponent<MovementController>();
        lastAttack = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        // catch input
        attackInput = Input.GetButton("Fire1");

        lastAttack -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (attackInput || isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
        }
        
        // if ((lastAttack < 0 && ++currentAttackStage == 3) || currentAttackStage == 3)
        // {
        //     currentAttackStage = 3;
        //     if (lastAttack <= 0)
        //     {
        //         lastAttack = 1.1f;
        //     }
        //     return;
        // }
        //
        // if ((lastAttack < 0 && ++currentAttackStage == 2) || currentAttackStage == 2)
        // {
        //     currentAttackStage = 2;
        //     if (lastAttack <= 0)
        //     {
        //         lastAttack = 1f;
        //     }
        //     return;
        // }
        
        if (lastAttack < -1f || currentAttackStage == 1 || (lastAttack < 0 && ++currentAttackStage == 4))
        {
            currentAttackStage = 3;
            if (lastAttack <= 0)
            {
                lastAttack = 1.1f;
            }
            return;
        }
        
    }
}
