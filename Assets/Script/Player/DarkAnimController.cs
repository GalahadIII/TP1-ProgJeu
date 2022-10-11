using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DarkAnimController : MonoBehaviour
{
    public static DarkAnimController instance;
    public Animator m_Anim;

    private string lastState;

    private PlayerController player;
    private Rigidbody2D m_RB;
    private MovementController m_MC;

    private string state;
    private float timeSpentIdle;
    public bool idle;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GetComponent<PlayerController>();
        GameObject darkChild = GameObject.Find("Dark");
        m_Anim = darkChild.GetComponent<Animator>();
        
        m_RB = player.m_RB;
        m_MC = player.m_MC;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (m_MC.grounded)
        {
            if (Mathf.Abs(m_RB.velocity.x) > 0.01)
            {
                state = PlayerAnimState.RUN;
            }
            
            if (m_RB.velocity.x.Equals(0) && m_RB.velocity.y.Equals(0) && !player.attacking)
            {
                state = Idle();
            }
            else { idle = false; }
        }

        if (m_RB.velocity.y > 0.01)
        {
            state = PlayerAnimState.JUMP;
        }

        if (m_RB.velocity.y < -0.01)
        {
            state = PlayerAnimState.FALL;
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
