using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CapsuleCollider2D attack1Zone;
    private MovementController s_MC;
    private bool facingRight;
    private int ATK1_DMG = 2;

    // Start is called before the first frame update
    void Start()
    {
        attack1Zone = GetComponent<CapsuleCollider2D>();
        s_MC = GetComponentInParent<MovementController>();
        attack1Zone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        facingRight = s_MC.facingRight;
    }

    public IEnumerator DealDamage()
    {
        attack1Zone.enabled = true;
        yield return new WaitForSeconds(0.1f);
        attack1Zone.enabled = false;
    }

    private void OnTriggerEnter2D(Component col)
    {
        GameObject GameObj = col.gameObject;
        if (GameObj.CompareTag("Enemy"))
        {
            GameObj.GetComponent<KilleableEntity>().TakeDamage(ATK1_DMG, facingRight);
        }
    }
}
