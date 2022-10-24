using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootCollection : MonoBehaviour
{
    private PlayerController player;
    
    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Component col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            player.goldCollected += 1;
            Destroy(col.gameObject);
        }
        
    }
}
