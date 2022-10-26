using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilleableEntity : MonoBehaviour
{
    
    public int hp; 
    public bool tookDamage;

    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;
        tookDamage = true;
    }
}
