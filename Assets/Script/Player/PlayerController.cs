using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool darkForm = false;
    
    public bool attacking = false;

    #region Componenents

    public static PlayerController instance;
    private GameObject lightChild;
    private GameObject darkChild;
    public Rigidbody2D m_RB;

    private SpriteRenderer m_LightSR;
    private SpriteRenderer m_DarkSR;
    
    public MovementController m_MC;

    #endregion

    private void Awake()
    {
        instance = this;
        
        m_RB = GetComponent<Rigidbody2D>();
        m_MC = GetComponent<MovementController>();
        

    }
    
    // Start is called before the first frame update
    private void Start()
    {
        lightChild = GameObject.Find("Light");
        darkChild = GameObject.Find("Dark");
        
        m_LightSR = lightChild.GetComponent<SpriteRenderer>();
        m_DarkSR = darkChild.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //Transform();
        }
    }

    private void Attack()
    {
        if (!attacking && m_RB.velocity.x == 0 && m_RB.velocity.y == 0)
        {
            attacking = true;
        }
    }

    public void Transform()
    {
        if (!darkForm)
        {
            darkForm = true;
            m_DarkSR.enabled = true;
            m_LightSR.enabled = false;
        }
        else
        {
            darkForm = false;
            m_LightSR.enabled = true;
            m_DarkSR.enabled = false;
        }
    }
}
