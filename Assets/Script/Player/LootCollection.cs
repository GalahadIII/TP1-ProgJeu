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

    private void OnTriggerEnter2D(Collider2D col)
    {
        PickUpCoin(col);
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        PickUpCoin(col);
    }

    private void PickUpCoin(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            if (col.gameObject.GetComponent<Items>().collectable)
            {
                player.goldCollected += 1;
                Destroy(col.gameObject);
            }
        }
    }
}
