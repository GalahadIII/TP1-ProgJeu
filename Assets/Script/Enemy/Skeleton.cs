using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public bool isDead;
    [SerializeField] private Collider2D skeletonCollider;
    
    
    private float dyingTime;
    
    public Animator m_Anim;
    private Rigidbody2D m_RB;
    private KilleableEntity s_Hp;
    private Loot s_Loot;
    
    private int lastHp;
    
    private static readonly int DeathAnimID = Animator.StringToHash("Death");
    private static readonly int HitAnimId = Animator.StringToHash("Hit");

    // Start is called before the first frame update
    private void Start()
    {
        m_Anim = GetComponentInChildren<Animator>();
        m_RB = GetComponent<Rigidbody2D>();
        s_Hp = GetComponent<KilleableEntity>();
        s_Loot = GetComponentInChildren<Loot>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (s_Hp.tookDamage && !isDead)
        {
            
        }
        
        if (!isDead)
        {
            if (s_Hp.hp > 0) return;
            isDead = true;
            StartCoroutine(s_Loot.Drop());
            m_Anim.SetTrigger(DeathAnimID);
            dyingTime = 0;
        }
        else
        {
            dyingTime += Time.deltaTime;
            Die();
        }
    }

    private void Die()
    {
        m_RB.bodyType = RigidbodyType2D.Static;
        skeletonCollider.enabled = false;
        if (dyingTime > 5)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
