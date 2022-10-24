using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    [SerializeField] private bool isDead;
    private float dyingTime;

    public Animator m_Anim;
    private Rigidbody2D m_RB;
    private GroundPatrol s_Patrol;
    private KilleableEntity s_Hp;
    private int lastHp;
    
    // Start is called before the first frame update
    private void Start()
    {
        m_Anim = GetComponentInChildren<Animator>();
        m_RB = GetComponent<Rigidbody2D>();
        s_Patrol = GetComponent<GroundPatrol>();
        s_Hp = GetComponent<KilleableEntity>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (s_Hp.tookDamage && !isDead)
        {
            m_Anim.SetTrigger("Hit");
            s_Hp.tookDamage = false;
        }
        
        if (!isDead)
        {
            if (s_Hp.hp > 0) return;
            isDead = true;
            dyingTime = 0;
            s_Patrol.mustPatrol = false;
        }
        else
        {
            dyingTime += Time.deltaTime;
            Die();
        }
    }

    private void Die()
    {
        m_Anim.SetTrigger("Death");
        m_RB.bodyType = RigidbodyType2D.Static;
        if (dyingTime > 5)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
