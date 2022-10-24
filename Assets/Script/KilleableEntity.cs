using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilleableEntity : MonoBehaviour
{
    
    public int hp; 
    public bool tookDamage;
    public int knockbackDirection;
    
    public void TakeDamage(int damageTaken, bool facingRight)
    {
        knockbackDirection = facingRight ? 1 : -1;
        hp -= damageTaken;
        tookDamage = true;
    }
}
