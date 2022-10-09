using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator m_Anim;
    private MovementController m_MC;
    private Rigidbody2D m_RB;

    private string state;
    private string newState;
    
    private void Awake()
    {
        GameObject SpriteChild = GameObject.Find("Light");
        m_Anim = SpriteChild.GetComponent<Animator>();
        m_MC = GetComponent<MovementController>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(m_RB.velocity.x) > 0.01)
        {
            newState = LightAnimState.RUN;
        }

        if (m_RB.velocity.x.Equals(0))
        {
            newState = LightAnimState.IDLE_BLINK;
        }

        if (m_RB.velocity.y > 0.01)
        {
            newState = LightAnimState.JUMP;
        }
        
        if (m_RB.velocity.y < -0.01)
        {
            newState = LightAnimState.FALL;
        }

        if (newState == state) {return;}
        state = newState;
        m_Anim.Play(newState);  
        
    }
}

public static class LightAnimState
{
    public const string ATTACK1 = "Attack1";
    public const string ATTACK2 = "Attack2";
    public const string ATTACK3 = "Attack3";
    public const string DASH = "Dash";
    public const string FALL = "Fall";
    public const string IDLE_BLINK = "IdleBlink";
    public const string JUMP = "Jump";
    public const string LOOK_BACK = "LookBack";
    public const string LOOK_BLINK = "LookBlink";
    public const string LOOK_INTRO = "LookIntro";
    public const string ROLL = "Roll";
    public const string RUN = "Run";
    public const string TRANSFORM = "Transform";
    public const string WALL_GRAB = "WallGrab";
    public const string WALL_SLIDE = "WallSlide";
}