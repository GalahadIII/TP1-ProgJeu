using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject[] lootTable;
    [SerializeField] private int minGold;
    [SerializeField] private int maxGold;
    
    private Vector3 center;
    private PointEffector2D area;
    
    [SerializeField] private float radius;
    

    private int numObjects;

    private void Start()
    {
         area = GetComponent<PointEffector2D>();
    }

    private Vector3 RandomCircle() {
        // create random angle between 0 to 360 degrees
        var ang = Random.value * 360;
        center = transform.position;

        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = 0;
        return pos;
    }
    
    public IEnumerator Drop()
    {
        numObjects = Random.Range(minGold, maxGold);
        
        for (int i = 0; i < numObjects; i++){
            Vector3 pos = RandomCircle();
            Instantiate(lootTable[0], pos, new Quaternion());
        }

        yield return new WaitForSeconds(0.3f);
        area.enabled = false;
    }
}
