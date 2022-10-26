using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CapsuleCollider2D attack1Zone;
    private bool facingRight;

    [SerializeField]private List<KilleableEntity> enemiesInRange = new ();
    
    [SerializeField]private int ATK1_DMG = 2;

    // Start is called before the first frame update
    void Start()
    {
        attack1Zone = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DealDamage()
    {
        foreach (KilleableEntity entity in enemiesInRange)
        {
            entity.TakeDamage(ATK1_DMG);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.isTrigger) return;
        KilleableEntity entity = col.gameObject.GetComponentInParent<KilleableEntity>();
        if (!entity) return;
        
        if (entity.CompareTag("Enemy") && !enemiesInRange.Contains(entity))
        {
            enemiesInRange.Add(entity);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        KilleableEntity entity = col.gameObject.GetComponentInParent<KilleableEntity>();
        
        enemiesInRange.Remove(entity);

    }
}
