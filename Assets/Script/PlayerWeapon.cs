using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private CapsuleCollider2D weaponRange;
    public List<GameObject> enemiesInRange;

    // Start is called before the first frame update
    void Start()
    {
        weaponRange = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject GameObj = col.gameObject;
        if (GameObj.CompareTag($"Enemy"))
        {
            enemiesInRange.Add(GameObj);
            //Debug.Log(col.gameObject.name + "Enter");

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GameObject GameObj = col.gameObject;
        if (GameObj.CompareTag($"Enemy"))
        {
            enemiesInRange.Remove(GameObj);
            //Debug.Log(col.gameObject.name + "Left");
        }
    }
}
