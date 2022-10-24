using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private bool menuInput;
    private bool isUp;

    private GameObject menuGUI;
    private Canvas canvas;
    
    // Start is called before the first frame update
    private void Start()
    {
        menuGUI = this.GameObject();
        canvas = menuGUI.GetComponent<Canvas>();
        isUp = false;
    }

    // Update is called once per frame
    private void Update()
    {
        menuInput = Input.GetButtonDown("Escape");
        
        if (menuInput)
        {
            SpawnMenu();
        }
    }
    
    // Spawn or remove menu
    public void SpawnMenu()
    {
        canvas.enabled = !isUp;
        isUp = !isUp;
        Time.timeScale = isUp ? 0 : 1;
    }
}
