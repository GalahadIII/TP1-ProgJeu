using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float speed;
    private Vector2 target;
    private Vector2 groundTarget;
    private Vector2 skyTarget;
    
    
        
    void Start()
    {
        var position = transform.position;
        groundTarget = new Vector2(position.x, minHeight);
        skyTarget = new Vector2(position.x, maxHeight);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var position = transform.position;
        if (transform.position.y >= maxHeight)
        {
            target = groundTarget;
        }
        else if (position.y <= minHeight)
        {
            target = skyTarget;
        }
        
        transform.position = Vector2.MoveTowards(position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        col.transform.SetParent(transform);
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        col.transform.SetParent(null);
    }

}
