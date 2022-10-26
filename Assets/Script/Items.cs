using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool collectable;
    
    [SerializeField] private float cooldown;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        collectable = false;
        timer = cooldown;
        Physics.IgnoreLayerCollision(6, 7);        
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer < 0)
        {
            collectable = true;
        }
    }
}
