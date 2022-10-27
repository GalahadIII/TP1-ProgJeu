using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private Canvas winCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
        }
    }

    private void Win()
    {
        winCanvas.enabled = true;
    }
    
}
