using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightAnimController : MonoBehaviour
{
    private GameObject lightChild;

    public Animator m_Anim;

    private string lastState;
    private Rigidbody2D m_RB;
    private MovementController m_MC;
    private PlayerController m_PC;

    private string state;
    private float timeSpentIdle;
    public bool idle;

    private void Awake()
    {
        lightChild = GameObject.Find("Light");
    }

    private void Start()
    {
        m_Anim = lightChild.GetComponent<Animator>();

        m_RB = GetComponent<Rigidbody2D>();
        m_MC = GetComponent<MovementController>();
        m_PC = GetComponent<PlayerController>();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        if (m_MC.grounded)
        {
            if (Mathf.Abs(m_RB.velocity.x) > 0.3)
            {
                state = PlayerAnimState.RUN;
            }

            if (Mathf.Abs(m_RB.velocity.x) < 0.3 && m_RB.velocity.y == 0)
            {
                state = Idle();
            }
            else
            {
                idle = false;
            }
        }

        if (m_RB.velocity.y > 0.01)
        {
            state = PlayerAnimState.JUMP;
        }

        if (m_RB.velocity.y < -0.01)
        {
            state = PlayerAnimState.FALL;
        }

        if (m_PC.isAttacking)
        {
            state = m_PC.currentAttackStage switch
            {
                1 => PlayerAnimState.ATTACK1,
                2 => PlayerAnimState.ATTACK2,
                3 => PlayerAnimState.ATTACK3,
                _ => throw new ArgumentOutOfRangeException(nameof(m_PC.currentAttackStage)),
            };
        }

        // Execution
        if (state == lastState) return;

        m_Anim.Play(state);
        lastState = state;
    }

    private string Idle()
    {
        if (idle == false)
        {
            idle = true;
            timeSpentIdle = 0;
        }

        timeSpentIdle += Time.deltaTime;
        if (timeSpentIdle > 7)
        {
            int random = Random.Range(1, 15 + 1);

            m_Anim.SetTrigger(random == 1 ? "LookBehind" : "Blink");
            timeSpentIdle = 0;
        }

        timeSpentIdle += Time.deltaTime;

        return PlayerAnimState.IDLE;
    }

}